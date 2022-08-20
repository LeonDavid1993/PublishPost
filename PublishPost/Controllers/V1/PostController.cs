using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishPost.Contracts.V1;
using PublishPost.Contracts.V1.Requests;
using PublishPost.Domain;
using PublishPost.Extensions;
using PublishPost.Services;
using PublishPost.Utilities.Enums;
using System;
using System.Threading.Tasks;

namespace PublishPost.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        [HttpGet(ApiRoutes.PostRoutes.GetAllPubPost)]
        public async Task<IActionResult> GetAllPublished()
        {
            return Ok(await _postService.GetPostsPublishedAsync());
        }

        [Authorize(Roles = "Writer")]
        [HttpGet(ApiRoutes.PostRoutes.Get)]
        public async Task<IActionResult> Get()
        {
            var id = HttpContext.GetUserId();
            var post = await _postService.GetOwnPostsAsync(id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [Authorize(Roles ="Writer")]
        [HttpPost(ApiRoutes.PostRoutes.Create)]
        public async Task<IActionResult> Create([FromBody] PostCreateRequest postRequest)
        {
            var postNew = new Post()
            {
                Id = Guid.NewGuid(),
                Content = postRequest.Content,
                Date = DateTime.UtcNow,
                Title = postRequest.Title,
                UserId = HttpContext.GetUserId(),
                StatusId = (int)StatusEnum.Submitted
            };

            var post = await _postService.CreatePostAsync(postNew);

            if (!post)
                return NotFound();

            return Ok();
        }

        [Authorize(Roles = "Writer")]
        [HttpPut(ApiRoutes.PostRoutes.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] PutPostRequest putPostRequest)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own post ot it doesn't exist"});
            }

            var putPost = await _postService.GetPostByIdAsync(postId);

            if (putPost.StatusId == (int)StatusEnum.Approved || putPost.StatusId == (int)StatusEnum.PendingApproval)
            {
                return BadRequest(new { error = "You can't edit the published or submitted post" });
            }

            putPost.Content = putPostRequest.Content;
            putPost.Title = putPostRequest.Tittle;
            putPost.Date = DateTime.UtcNow;
            putPost.StatusId = (int)StatusEnum.Submitted;
            var postUpdated = await _postService.UpdatePostAsync(putPost);

            if (!postUpdated)
                return NotFound();

            return Ok(postUpdated);
        }

        [Authorize(Roles = "Writer")]
        [HttpPut(ApiRoutes.PostRoutes.Submit)]
        public async Task<IActionResult> Submit([FromRoute] Guid postId)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own post" });
            }

            var post = await _postService.GetPostByIdAsync(postId);

            if (post.StatusId != (int)StatusEnum.Submitted)
            {
                return BadRequest(new { error = "You can't submit the post since it is not submitted" });
            }

            post.StatusId = (int)StatusEnum.PendingApproval;
            var postSubmitted = await _postService.UpdatePostAsync(post);

            if (!postSubmitted)
                return NotFound();

            return Ok(postSubmitted);
        }

        [Authorize(Roles = "Editor")]
        [HttpGet(ApiRoutes.PostRoutes.GetAllPendPost)]
        public async Task<IActionResult> GetAllPendingApproval()
        {
            return Ok(await _postService.GetPostsPendingAprovalAsync());
        }

        [Authorize(Roles = "Editor")]
        [HttpPut(ApiRoutes.PostRoutes.Reject)]
        public async Task<IActionResult> Reject([FromRoute] Guid postId, [FromBody] RejectPostRequest rejectPostRequest)
        {
            var post = await _postService.GetPostByIdAsync(postId);

            if (post.StatusId != (int)StatusEnum.PendingApproval)
            {
                return BadRequest(new { error = "You can't reject the post since it is not pending approval" });
            }

            post.StatusId = (int)StatusEnum.Rejected;
            var postRejected = await _postService.UpdatePostAsync(post);

            if (!postRejected)
                return NotFound();

            //Add the comment
            var commentNew = new Comment()
            {
                Id = Guid.NewGuid(),
                Text = rejectPostRequest.Text,
                PostId = post.Id,
                UserId = HttpContext.GetUserId(),
                StatusId = (int)StatusEnum.Rejected
        };

            await _commentService.CreateCommentAsync(commentNew);

            return Ok(postRejected);
        }

        [Authorize(Roles = "Editor")]
        [HttpPut(ApiRoutes.PostRoutes.Approval)]
        public async Task<IActionResult> Approval([FromRoute] Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);

            if (post.StatusId != (int)StatusEnum.PendingApproval)
            {
                return BadRequest(new { error = "You can't approval the post since it is not pending approval" });
            }

            post.StatusId = (int)StatusEnum.Approved;
            var postApproved = await _postService.UpdatePostAsync(post);

            if (!postApproved)
                return NotFound();

            return Ok(postApproved);
        }
    }
}

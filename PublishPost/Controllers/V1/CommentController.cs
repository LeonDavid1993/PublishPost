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
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        public CommentController(ICommentService commentService, IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }

        [HttpGet(ApiRoutes.CommentRoutes.GetAllByUserId)]
        public async Task<IActionResult> GetAllByUserId()
        {
            return Ok(await _commentService.GetCommentsByUserIdAsync(HttpContext.GetUserId()));
        }

        [Authorize(Roles = "Writer")]
        [HttpGet(ApiRoutes.CommentRoutes.GetAllByStatus)]
        public async Task<IActionResult> GetAllRejected([FromRoute] Guid postId)
        {
            var comments = await _commentService.GetCommentsByStatusAsync(StatusEnum.Rejected, postId);

            if (comments == null)
                return NotFound();

            return Ok(comments);
        }

        [HttpPost(ApiRoutes.CommentRoutes.Create)]
        public async Task<IActionResult> Create(CommentRequest commentRequest)
        {
            var post = await _postService.GetPostByIdAsync(commentRequest.PostId);

            if (post == null || post.Id == Guid.Empty)
            {
                return BadRequest(new { error = "The post doesn't exist" });
            }

            var commentNew = new Comment()
            {
                Id = Guid.NewGuid(),
                Text = commentRequest.Text,
                PostId = commentRequest.PostId,
                UserId = HttpContext.GetUserId()
            };

            var comment = await _commentService.CreateCommentAsync(commentNew);

            if (!comment)
                return NotFound();

            return Ok();
        }
    }
}

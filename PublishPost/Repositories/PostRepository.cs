using Microsoft.EntityFrameworkCore;
using PublishPost.Contracts.V1.Requests;
using PublishPost.Contracts.V1.Responses;
using PublishPost.Data;
using PublishPost.Domain;
using PublishPost.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Repositories
{
    public class PostRepository: IPostRepository
    {
        private readonly PublishDataContext _databaseContext;

        public PostRepository(PublishDataContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Post>> GetPostsByStatusAsync(StatusEnum statusEnum)
        {
            return await _databaseContext.Posts.Where(k=> k.StatusId == (int)statusEnum).ToListAsync();
        }

        public async Task<bool> UpdatePostAsync(Post updatePost)
        {
            _databaseContext.Posts.Update(updatePost);
            var updated = await _databaseContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> UserOwnsPostAsync(Guid updatePost, string getUserId)
        {
            var post = await _databaseContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == updatePost);

            if (post == null)
            {
                return false;
            }

            if (post.UserId != getUserId)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CreatePostAsync(Post postRequest)
        {
            await _databaseContext.Posts.AddAsync(postRequest);
            var created = await _databaseContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<Post>> GetOwnPostsAsync(string userId)
        {
            return await _databaseContext.Posts.Where(k => k.UserId == userId).ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return await _databaseContext.Posts.Where(k => k.Id == postId).FirstOrDefaultAsync();
        }
    }
}

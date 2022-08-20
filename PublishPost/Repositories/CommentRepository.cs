using Microsoft.EntityFrameworkCore;
using PublishPost.Data;
using PublishPost.Domain;
using PublishPost.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly PublishDataContext _databaseContext;

        public CommentRepository(PublishDataContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> CreateCommentAsync(Comment commentRequest)
        {
            await _databaseContext.Comments.AddAsync(commentRequest);
            var created = await _databaseContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<Comment>> GetCommentsByStatusAsync(StatusEnum status, Guid postGuid)
        {
            return await _databaseContext.Comments.Where(k => k.PostId == postGuid && k.StatusId == (int)status).ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByUserIdAsync(string userId)
        {
            return await _databaseContext.Comments.Where(k => k.UserId == userId).ToListAsync();
        }
    }
}

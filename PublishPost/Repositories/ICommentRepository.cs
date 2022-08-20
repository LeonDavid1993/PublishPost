using PublishPost.Domain;
using PublishPost.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Repositories
{
    public interface ICommentRepository
    {
        Task<bool> CreateCommentAsync(Comment postRequest);
        Task<List<Comment>> GetCommentsByUserIdAsync(string userId);
        Task<List<Comment>> GetCommentsByStatusAsync(StatusEnum status, Guid postGuid);
    }
}

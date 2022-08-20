using PublishPost.Contracts.V1.Requests;
using PublishPost.Domain;
using PublishPost.Repositories;
using PublishPost.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;

        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateCommentAsync(Comment commentRequest)
        {
            return await _repository.CreateCommentAsync(commentRequest);
        }

        public async Task<List<Comment>> GetCommentsByStatusAsync(StatusEnum status, Guid postGuid)
        {
            return await _repository.GetCommentsByStatusAsync(status, postGuid);
        }

        public async Task<List<Comment>> GetCommentsByUserIdAsync(string userId)
        {
            return await _repository.GetCommentsByUserIdAsync(userId);
        }
    }
}

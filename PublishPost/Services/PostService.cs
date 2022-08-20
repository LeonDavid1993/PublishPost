using PublishPost.Contracts.V1.Requests;
using PublishPost.Contracts.V1.Responses;
using PublishPost.Domain;
using PublishPost.Repositories;
using PublishPost.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;

        public PostService(IPostRepository repository) {
            _repository = repository;
        }

        public async Task<List<Post>> GetPostsPublishedAsync()
        {
            return await _repository.GetPostsByStatusAsync(StatusEnum.Approved);
        }
        public async Task<List<Post>> GetPostsPendingAprovalAsync()
        {
            return await _repository.GetPostsByStatusAsync(StatusEnum.PendingApproval);
        }

        public async Task<bool> UpdatePostAsync(Post updatePost)
        {
            return await _repository.UpdatePostAsync(updatePost);
        }

        public async Task<bool> UserOwnsPostAsync(Guid postId, string getUserId)
        {
            return await _repository.UserOwnsPostAsync(postId, getUserId);
        }

        public async Task<bool> CreatePostAsync(Post postRequest)
        {
            return await _repository.CreatePostAsync(postRequest);
        }

        public async Task<List<Post>> GetOwnPostsAsync(string userId)
        {
            return await _repository.GetOwnPostsAsync(userId);
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return await _repository.GetPostByIdAsync(postId);
        }
    }
}

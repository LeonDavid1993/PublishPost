using PublishPost.Contracts.V1.Requests;
using PublishPost.Contracts.V1.Responses;
using PublishPost.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsPublishedAsync();
        Task<List<Post>> GetPostsPendingAprovalAsync();
        Task<List<Post>> GetOwnPostsAsync(string userId);
        Task<bool> UpdatePostAsync(Post updatePost);
        Task<bool> UserOwnsPostAsync(Guid postId, string getUserId);
        Task<bool> CreatePostAsync(Post postRequest);
        Task<Post> GetPostByIdAsync(Guid postId);
    }
}

using PublishPost.Contracts.V1.Requests;
using PublishPost.Contracts.V1.Responses;
using PublishPost.Domain;
using PublishPost.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Repositories
{
    public interface IPostRepository
    {
        Task<List<Post>> GetPostsByStatusAsync(StatusEnum statusEnum);
        Task<bool> UpdatePostAsync(Post updatePost);
        Task<bool> UserOwnsPostAsync(Guid updatePost, string getUserId);
        Task<bool> CreatePostAsync(Post postRequest);
        Task<List<Post>> GetOwnPostsAsync(string userId);
        Task<Post> GetPostByIdAsync(Guid postId);
    }
}

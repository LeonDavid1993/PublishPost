using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class PostRoutes {
            public const string GetAllPubPost = Base + "/postsPublished";
            public const string GetAllPendPost = Base + "/postsPendingApproval";
            public const string Get = Base + "/postsOwns";
            public const string Update = Base + "/posts/{postId}";
            public const string Submit = Base + "/postsSubmit/{postId}";
            public const string Reject = Base + "/postsReject/{postId}";
            public const string Approval = Base + "/postsApproval/{postId}";
            public const string Create = Base + "/posts";
        }

        public static class CommentRoutes
        {
            public const string GetAllByUserId = Base + "/comments";
            public const string GetAllByStatus = Base + "/commentsReject/{postId}";
            public const string Create = Base + "/comments";
        }

        public static class IdentityRoutes
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refresh";
        }
    }
}

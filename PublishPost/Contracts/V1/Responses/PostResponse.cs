using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Contracts.V1.Responses
{
    public class PostResponse
    {
        public PostResponse()
        {
            Comments = new List<CommentResponse>();
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public List<CommentResponse> Comments { get; set; }
    }
}

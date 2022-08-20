using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Contracts.V1.Requests
{
    public class CommentRequest
    {
        public string Text { get; set; }
        [Required]
        public Guid PostId { get; set; }
    }
}

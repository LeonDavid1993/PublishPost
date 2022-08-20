using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Domain
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public int? StatusId { get; set; }
        public Guid? PostId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual Status StatusComment { get; set; }

        [ForeignKey(nameof(PostId))]
        public virtual Post PostComment { get; set; }
    }
}

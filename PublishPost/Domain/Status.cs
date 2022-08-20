using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Domain
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsLock { get; set; }
    }
}

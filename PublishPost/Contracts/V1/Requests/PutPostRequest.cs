using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Contracts.V1.Requests
{
    public class PutPostRequest
    {
        public string Tittle { get; set; }
        public string Content { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PublishPost.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublishPost.Data
{
    public class PublishDataContext : IdentityDbContext
    {
        public PublishDataContext(DbContextOptions<PublishDataContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        
    }
}

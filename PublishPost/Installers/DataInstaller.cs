using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublishPost.Data;
using PublishPost.Repositories;
using PublishPost.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublishPost.Installers
{
    public class DataInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PublishDataContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PublishDataContext>();

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ICommentRepository, CommentRepository>();
        }
    }
}

using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PublishPost.Contracts.V1;
using PublishPost.Contracts.V1.Requests;
using PublishPost.Contracts.V1.Responses;
using PublishPost.Data;
using System.Net.Http;

namespace PublishPost.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient testClient;
        private readonly IServiceProvider serviceProvider;
        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services => {
                        services.RemoveAll(typeof(PublishDataContext));
                        services.AddDbContext<PublishDataContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                    });
                });
            serviceProvider = appFactory.Services;
            testClient = appFactory.CreateClient();
        }

        public void Dispose()
        {
            using var serviceScope = serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<PublishDataContext>();
            context.Database.EnsureDeleted();
        }

        protected async Task AuthenticateAsync()
        {
            testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await testClient.PostAsJsonAsync(ApiRoutes.IdentityRoutes.Register, 
                                new UserRegistrationRequest { 
                                    Email = "test@integration.com",
                                    Password = "CristonomoAgosto2022*"
                                });

            var regisrationResponse = await response.Content.ReadFromJsonAsync<AuthSuccessResponse>();
            return regisrationResponse.Token;
        }
    }
    
}

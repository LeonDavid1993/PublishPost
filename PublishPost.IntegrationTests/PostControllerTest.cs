using FluentAssertions;
using PublishPost.Contracts.V1;
using PublishPost.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http.Json;

namespace PublishPost.IntegrationTests
{
    public class PostControllerTest : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnEmptyResponse()
        {
            //Arrange
            await AuthenticateAsync();

            //Act
            var response = await testClient.GetAsync(ApiRoutes.PostRoutes.GetAllPubPost);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            (await response.Content.ReadFromJsonAsync<List<Post>>()).Should().BeEmpty();
        }
    }
}

using System.Collections.Generic;
using APIMMwithoutJunctionModel.Controllers;
using APIMMwithoutJunctionModel.Data;
using APIMMwithoutJunctionModel.DTOs;
using APIMMwithoutJunctionModel.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace APIMMwithoutJunctionModel.Tests.Controllers
{
    public class TokenControllerTests
    {
        [Fact]
        public void GenerateToken_ReturnsToken_WhenUserValid()
        {
            var options = new DbContextOptionsBuilder<DocPatientContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            using var ctx = new DocPatientContext(options);
            ctx.Users.Add(new User { userName = "john", email = "j@e.com", password = "p", role = "Admin" });
            ctx.SaveChanges();

            var inMemorySettings = new Dictionary<string, string>
            {
                { "Key", "this_is_a_very_long_test_key_with_32_plus_bytes_!!!!" }
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();

            var controller = new TokenController(ctx, configuration);
            var result = controller.GenerateToken(new LoginDTO { email = "j@e.com", password = "p" });

            var ok = result as OkObjectResult;
            ok.Should().NotBeNull();
            ok!.Value.Should().NotBeNull();
        }
    }
}



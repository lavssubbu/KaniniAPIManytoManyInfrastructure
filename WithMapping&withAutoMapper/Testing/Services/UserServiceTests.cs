using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIMMwithoutJunctionModel.DTOs;
using APIMMwithoutJunctionModel.Interface;
using APIMMwithoutJunctionModel.Models;
using APIMMwithoutJunctionModel.Service;
using FluentAssertions;
using Moq;
using Xunit;

namespace APIMMwithoutJunctionModel.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUser_MapsAndReturnsDto()
        {
            var repoMock = new Mock<IDocPatient<User>>();
            repoMock
                .Setup(r => r.Add(It.IsAny<User>()))
                .ReturnsAsync((User u) => { u.userId = 99; return u; });

            var service = new UserService(repoMock.Object);

            var dto = new CreateUserDto { userName = "john", email = "j@e.com", password = "p", role = "Admin" };
            var result = await service.CreateUser(dto);

            result.Should().NotBeNull();
            result.UsrId.Should().Be(99);
            result.userName.Should().Be("john");
            result.email.Should().Be("j@e.com");
            result.password.Should().Be("p");
            result.role.Should().Be("Admin");

            repoMock.Verify(r => r.Add(It.Is<User>(u => u.userName == dto.userName && u.email == dto.email && u.password == dto.password && u.role == dto.role)), Times.Once);
        }

        [Fact]
        public async Task GetAllUsers_MapsToDtos()
        {
            var users = new List<User>
            {
                new User { userId = 1, userName = "a", email = "a@e.com", password = "p1", role = "User" },
                new User { userId = 2, userName = "b", email = "b@e.com", password = "p2", role = "Admin" }
            };

            var repoMock = new Mock<IDocPatient<User>>();
            repoMock.Setup(r => r.GetAll()).ReturnsAsync(users);

            var service = new UserService(repoMock.Object);
            var result = await service.GetAllUsers();

            result.Should().HaveCount(2);
            result.Select(r => r.UsrId).Should().BeEquivalentTo(new[] { 1, 2 });
        }
    }
}



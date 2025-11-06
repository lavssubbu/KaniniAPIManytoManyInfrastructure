using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIMMwithoutJunctionModel.Controllers;
using APIMMwithoutJunctionModel.DTOs;
using APIMMwithoutJunctionModel.Models;
using APIMMwithoutJunctionModel.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace APIMMwithoutJunctionModel.Tests.Controllers
{
    public class ControllersTests
    {
        [Fact]
        public async Task DoctorsController_GetDoctors_ReturnsOkWithDtos()
        {
            var expected = new List<GetDocDto> { new GetDocDto { DocName = "Dr. A", Specialization = "Neuro", PatientName = new List<string>() } };
            var svcMock = new Mock<IService>();
            svcMock.Setup(s => s.GetAllDoc()).ReturnsAsync(expected);

            var controller = new DoctorsController(svcMock.Object);
            var resp = await controller.GetDoctors();

            var ok = resp.Result as OkObjectResult;
            ok.Should().NotBeNull();
            var value = ok!.Value as IEnumerable<GetDocDto>;
            value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task DoctorsController_AddDoctor_ReturnsOkWithDto()
        {
            var created = new DoctorDto { DocId = 1, DocName = "Dr. B", Specialization = "ENT" };
            var svcMock = new Mock<IService>();
            svcMock.Setup(s => s.CreateDoctor(It.IsAny<CreateDocDtoc>())).ReturnsAsync(created);

            var controller = new DoctorsController(svcMock.Object);
            var resp = await controller.AddDoctor(new CreateDocDtoc { DocName = "Dr. B", Specialization = "ENT" });

            var ok = resp.Result as OkObjectResult;
            ok.Should().NotBeNull();
            ok!.Value.Should().BeEquivalentTo(created);
        }

        [Fact]
        public async Task UsersController_GetUsers_ReturnsOk()
        {
            var expected = new List<UserDto> { new UserDto { UsrId = 1, userName = "u" } };
            var svcMock = new Mock<IUserService>();
            svcMock.Setup(s => s.GetAllUsers()).ReturnsAsync(expected);

            var controller = new UsersController(svcMock.Object);
            var resp = await controller.GetDoctors();

            var ok = resp.Result as OkObjectResult;
            ok.Should().NotBeNull();
            ((IEnumerable<UserDto>)ok!.Value!).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UsersController_AddUser_ReturnsOk()
        {
            var created = new UserDto { UsrId = 2, userName = "b" };
            var svcMock = new Mock<IUserService>();
            svcMock.Setup(s => s.CreateUser(It.IsAny<CreateUserDto>())).ReturnsAsync(created);

            var controller = new UsersController(svcMock.Object);
            var resp = await controller.AddDoctor(new CreateUserDto { userName = "b" });

            var ok = resp.Result as OkObjectResult;
            ok.Should().NotBeNull();
            ok!.Value.Should().BeEquivalentTo(created);
        }

        [Fact]
        public async Task PatientsController_Get_ReturnsOk()
        {
            var expected = new List<Patient> { new Patient { PatId = 1, PatName = "p" } };
            var repoMock = new Moq.Mock<APIMMwithoutJunctionModel.Interface.IDocPatient<Patient>>();
            repoMock.Setup(r => r.GetAll()).ReturnsAsync(expected);

            var service = new APIMMwithoutJunctionModel.Service.PatientService(repoMock.Object);
            var controller = new PatientsController(service);
            var resp = await controller.GetDoctors();

            var ok = resp.Result as OkObjectResult;
            ok.Should().NotBeNull();
            ((IEnumerable<Patient>)ok!.Value!).Should().BeEquivalentTo(expected);
        }
    }
}



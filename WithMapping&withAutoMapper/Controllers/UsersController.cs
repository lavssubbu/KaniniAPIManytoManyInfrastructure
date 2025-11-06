using APIMMwithoutJunctionModel.DTOs;
using APIMMwithoutJunctionModel.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMMwithoutJunctionModel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetDoctors()
        {
            var lstdoc = await _service.GetAllUsers();
            return Ok(lstdoc);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> AddDoctor(CreateUserDto dto)
        {
            return Ok(await _service.CreateUser(dto));

        }
    }
}

using APIMMwithoutJunctionModel.DTOs;
using APIMMwithoutJunctionModel.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMMwithoutJunctionModel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class DoctorsController : ControllerBase
    {
        private readonly IService _service;

        public DoctorsController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        // [AllowAnonymous]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetDocDto>>> GetDoctors()
        {
            var lstdoc = await _service.GetAllDoc();
            return Ok(lstdoc);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DoctorDto>> AddDoctor(CreateDocDtoc dto)
        {
            return Ok(await _service.CreateDoctor(dto));

        }
    }
}

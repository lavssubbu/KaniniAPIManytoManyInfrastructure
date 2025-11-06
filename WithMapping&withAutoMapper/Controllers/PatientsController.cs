using APIMMwithoutJunctionModel.DTOs;
using APIMMwithoutJunctionModel.Models;
using APIMMwithoutJunctionModel.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIMMwithoutJunctionModel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _ser;

        public PatientsController(PatientService ser)
        {
            _ser = ser;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetDoctors()
        {
            var lstpat = await _ser.GetAllPatients();

            return Ok(lstpat);
        }
    }
}

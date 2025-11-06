using APIMMwithoutJunctionModel.DTOs;
using APIMMwithoutJunctionModel.Interface;
using APIMMwithoutJunctionModel.Models;

namespace APIMMwithoutJunctionModel.Service
{
    public class PatientService 
    {
        private readonly IDocPatient<Patient> _repo;

        public PatientService(IDocPatient<Patient> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _repo.GetAll();
        }

        public async Task<Patient> CreatePatient(Patient pt)
        {
            await _repo.Add(pt);
            return pt;
        }
    }
}

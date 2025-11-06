using APIMMwithoutJunctionModel.Data;
using APIMMwithoutJunctionModel.Interface;
using APIMMwithoutJunctionModel.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMMwithoutJunctionModel.Repository
{
    public class PatientRepository : IDocPatient<Patient>
    {
        private readonly DocPatientContext _context;

        public PatientRepository(DocPatientContext context)
        {
            _context = context;
        }
        public async Task<Patient> Add(Patient entity)
        {
            await _context.Patients.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Patient>> GetAll()
        {
            return await _context.Patients.Include(p => p.Doctors).ToListAsync();
        }
    }
}

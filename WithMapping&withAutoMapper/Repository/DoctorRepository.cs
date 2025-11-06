using APIMMwithoutJunctionModel.Data;
using APIMMwithoutJunctionModel.Interface;
using APIMMwithoutJunctionModel.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMMwithoutJunctionModel.Repository
{
    public class DoctorRepository : IDocPatient<Doctor>
    {
        private readonly DocPatientContext _context;

        public DoctorRepository(DocPatientContext context)
        {
            _context = context;
        }
        public async Task<Doctor> Add(Doctor entity)
        {
           await _context.Doctors.AddAsync(entity);
           await  _context.SaveChangesAsync();
           return entity;
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return await _context.Doctors.Include(p=>p.Patients).ToListAsync();
        }
    }
}

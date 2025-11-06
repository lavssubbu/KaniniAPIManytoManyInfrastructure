using APIMMwithoutJunctionModel.Data;
using APIMMwithoutJunctionModel.Interface;
using APIMMwithoutJunctionModel.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMMwithoutJunctionModel.Repository
{
    public class UserRepository : IDocPatient<User>
    {
        private readonly DocPatientContext _context;

        public UserRepository(DocPatientContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
    }
}

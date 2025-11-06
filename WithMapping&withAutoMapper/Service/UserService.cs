using APIMMwithoutJunctionModel.DTOs;
using APIMMwithoutJunctionModel.Interface;
using APIMMwithoutJunctionModel.Models;

namespace APIMMwithoutJunctionModel.Service
{
    public class UserService : IUserService
    {
        private readonly IDocPatient<User> _repo;

        public UserService(IDocPatient<User> repo)
        {
            _repo = repo;
        }
        public async Task<UserDto> CreateUser(CreateUserDto usr)
        {
            var user = new User
            {
                userName = usr.userName,
                password = usr.password,
                email = usr.email,
                role = usr.role,
            };
            var newuser = await _repo.Add(user);
            return new UserDto
            {
                UsrId = newuser.userId,
                userName = newuser.userName,
                email = newuser.email,
                password = newuser.password,
                role = newuser.role

            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _repo.GetAll();
            return users.Select(u => new UserDto
            {
                UsrId = u.userId,
                userName = u.userName,
                email = u.email,
                password = u.password,
                role = u.role

            });
        }
    }
}

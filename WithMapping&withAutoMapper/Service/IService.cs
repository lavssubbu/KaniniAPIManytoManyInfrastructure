using APIMMwithoutJunctionModel.DTOs;
using APIMMwithoutJunctionModel.Models;

namespace APIMMwithoutJunctionModel.Service
{
    public interface IService
    {
        Task<IEnumerable<GetDocDto>> GetAllDoc();
        Task<DoctorDto> CreateDoctor(CreateDocDtoc doc);
    }

    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> CreateUser(CreateUserDto doc);
    }
}

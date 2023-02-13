using PostsApp.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostsApp.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();

        Task<UserDto> GetUser(Guid userId);

        void CreateUser(UserDto user);

        void UpdateUser(UserDto user);

        void DeleteUser(Guid userId);
    }
}

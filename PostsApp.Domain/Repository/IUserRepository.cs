using PostsApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostsApp.DataAccess.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUser(Guid userId);

        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(Guid userId);

    }
}

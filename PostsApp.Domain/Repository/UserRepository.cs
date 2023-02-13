using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PostsApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostsApp.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly IConfiguration _config;

        public UserRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var users = await connection.QueryAsync<User>("SELECT * FROM Users");

            return users;
        }

        public async Task<User> GetUser(Guid userId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var user = await connection.QueryFirstAsync<User>("SELECT * FROM Users WHERE Id = @UserId",
                new { UserId = userId }
                );

            return user;
        }

        public void CreateUser(User user)
        {
           using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

           connection.ExecuteAsync("INSERT INTO Users (FirstName, LastName, Phonenumber, Email) values(@FirstName, @LastName, @Phonenumber, @Email)", user);                   
        }

        public void UpdateUser(User user)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            connection.ExecuteAsync("UPDATE Users SET firstName = @FirstName, lastName = @LastName, phonenumber = @Phonenumber, email = @Email WHERE id = @Id ", user);                  
        }

        public void DeleteUser(Guid userId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            connection.ExecuteAsync("DELETE FROM Users WHERE Id = @UserId ", new { UserId = userId });        
        }


    }
}

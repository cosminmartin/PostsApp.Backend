using AutoMapper;
using PostsApp.DataAccess.Repository;
using PostsApp.Domain.Entities;
using PostsApp.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostsApp.Domain.Services
{
    public  class UserService : IUserService
    {
        protected readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = _mapper.Map<List<UserDto>>(await _userRepository.GetAllUsers());

            return users;
        }

        public async Task<UserDto> GetUser(Guid userId)
        {
            var user = _mapper.Map<UserDto>(await _userRepository.GetUser(userId));

            return user;
        }

        public void CreateUser(UserDto user)
        {
            var userDto = _mapper.Map<User>(user);

            _userRepository.CreateUser(userDto);
        }

        public void UpdateUser(UserDto user)
        {
            var userDto = _mapper.Map<User>(user);

            _userRepository.UpdateUser(userDto);
        }

        public void  DeleteUser(Guid userId)
        {
             _userRepository.DeleteUser(userId);         
        }

    }
}

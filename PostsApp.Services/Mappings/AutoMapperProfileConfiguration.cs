using AutoMapper;
using PostsApp.Domain.Entities;
using PostsApp.Services.Dtos;

namespace PostsApp.Domain.Mappings
{
    public  class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}

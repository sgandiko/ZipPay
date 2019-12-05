using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZipPay.Domain.dto;
using ZipPay.Domain.Entity;

namespace ZipPay.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserAccountDto, UserAccount>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<CreateUserAccountDto, UserAccount>().ReverseMap();
            CreateMap<GetUserDto, User>().ReverseMap();
            CreateMap<GetUserAccountDto, UserAccount>().ReverseMap();

        }
    }
}

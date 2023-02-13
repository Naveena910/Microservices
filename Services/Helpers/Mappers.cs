using AutoMapper;
using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class Mappers : Profile
    {
        public Mappers()
        {

            

            CreateMap<User, UserDto>();
            CreateMap<User, UserResponseDto>();
            CreateMap<User, UserUpdateDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Address, AddressUpdateDto>();
            CreateMap<Payment, PaymentDto>();

            CreateMap<UserForCreatingDto, User>();
            CreateMap<AddressForCreatingDto, Address>();
            CreateMap<AddressUpdateDto, Address>();
            CreateMap<PaymentForCreatingDto, Payment>();
            CreateMap<UserUpdateDto, User>();
            
        }


    }
}

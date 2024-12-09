using Portfolio.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Portfolio.Core.Interfaces.Services.Account.Dtos;
using Portfolio.Core.Entities.Personal;
using Portfolio.Core.Interfaces.Services.PersonalInterfaces.Dtos;

namespace Portfolio.Core.RegisterAutoMapper
{
    public class RegisterMap : Profile
    {
        public RegisterMap(IHttpContextAccessor httpContextAccessor)
        {
			CreateMap<User, RegisterDto>().ReverseMap();
			CreateMap<User, UserDto>().ReverseMap();
			CreateMap<Personal, PersonalDto>().ReverseMap();

		}
    }
}

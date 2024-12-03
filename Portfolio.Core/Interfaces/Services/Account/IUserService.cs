using Portfolio.Core.Entities.ClassBases;
using Portfolio.Core.Interfaces.Services.Account.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services.Account
{
	public interface IUserService
	{
		IResult CreateUser(RegisterDto registerDto);
	}
}

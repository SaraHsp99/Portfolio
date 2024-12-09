using Portfolio.Core.Interfaces.Services.PersonalInterfaces.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services.PersonalInterfaces
{
	public interface IPersonalService
	{
		Task<PersonalDto> GetPersonal();
		Task<PersonalDto> GetPersonalByUserId(long userId);
	}
}

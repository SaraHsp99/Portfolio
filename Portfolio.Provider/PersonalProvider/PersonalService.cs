using AutoMapper;
using Portfolio.Core.Entities.ClassBases;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Interfaces.Services.CacheInterfaces;
using Portfolio.Core.Interfaces.Services.PersonalInterfaces;
using Portfolio.Core.Interfaces.Services.PersonalInterfaces.Dtos;
using Portfolio.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Provider.PersonalProvider
{
	public class PersonalService : BaseProvider, IPersonalService
	{
		private readonly IPersonalRepository _personalRepository;
		public PersonalService(IMapper mapper, ICacheService cacheService, IResult result, IPersonalRepository personalRepository) : base(mapper, cacheService, result)
		{
			_personalRepository = personalRepository;
		}
		public async Task<PersonalDto> GetPersonal()
		{
			var personal = _personalRepository.GetAll().FirstOrDefault();
			return
			   _mapper.Map<PersonalDto>(personal);

		}
		public async Task<PersonalDto> GetPersonalByUserId(long userId)
		{
			var personal=_personalRepository.GetPersonalByUserIdAsync(userId);
			 return
				_mapper.Map<PersonalDto>(personal);

		}
	}
}

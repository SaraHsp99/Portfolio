using AutoMapper;
using Portfolio.Core.Entities.ClassBases;
using Portfolio.Core.Interfaces.Services.Account;
using Portfolio.Core.Interfaces.Services.Account.Dtos;
using Portfolio.Core.Interfaces.Services.CacheInterfaces;
using Portfolio.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Provider.AccountProvider
{
	public class UserService : BaseProvider, IUserService
	{
		public UserService(PortfolioDbContext db, IMapper mapper, ICacheService cacheService, IResult result) : base(db, mapper, cacheService, result)
		{
		}

		public IResult CreateUser(RegisterDto registerDto)
		{
			throw new NotImplementedException();
		}
	}
}

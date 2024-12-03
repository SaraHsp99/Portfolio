using AutoMapper;
using Portfolio.Core.Entities.Account;
using Portfolio.Core.Entities.ClassBases;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Interfaces.Services.Account;
using Portfolio.Core.Interfaces.Services.Account.Dtos;
using Portfolio.Core.Interfaces.Services.CacheInterfaces;
using Portfolio.Core.Securities;
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
		private readonly IUserRepository _userRepository;

		public UserService(
			PortfolioDbContext db,
			IMapper mapper,
			ICacheService cacheService,
			IResult result,
			IUserRepository userRepository)
			: base(db, mapper, cacheService, result)
		{
			_userRepository = userRepository;
		}

		public async Task<IResult> CreateUser(RegisterDto registerDto)
		{
			
			var existingUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);
			if (existingUser != null)
			{
				_result.Rsl = false;
				_result.Message = "User with this email already exists.";
				return _result;
			}

			var createDate = DateTime.Now;
			var user = _mapper.Map<User>(registerDto);
			var passwordDto = PasswordManagment.HashingPassword(registerDto.Password);
			user.Password = passwordDto.Password;
			user.SaltPassword = passwordDto.SaltPassword;
			user.CreateDate = createDate;
		

			await _userRepository.AddUserAsync(user);

			_result.Rsl = true;
			_result.Message = "User created successfully.";
			return _result;
		}
	}

}

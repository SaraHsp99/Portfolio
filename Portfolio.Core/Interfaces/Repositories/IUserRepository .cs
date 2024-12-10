using Portfolio.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Repositories
{
	public interface IUserRepository
	{
		Task AddUserAsync(User user); 
		Task<User?> GetUserByEmailAsync(string email);
		Task<User?> GetUserByUserNameAsync(string userName);
	}

}

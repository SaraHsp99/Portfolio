using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Entities.Account;
using Portfolio.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly PortfolioDbContext _db;

		public UserRepository(PortfolioDbContext db)
		{
			_db = db;
		}

		public async Task AddUserAsync(User user)
		{
			await _db.Users.AddAsync(user);
			await _db.SaveChangesAsync();
		}

		public async Task<User?> GetUserByEmailAsync(string email)
		{
			return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
		}
	}

}

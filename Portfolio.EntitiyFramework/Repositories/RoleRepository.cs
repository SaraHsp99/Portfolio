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
	public class RoleRepository : IRoleRepository
	{
		private readonly PortfolioDbContext _db;

		public RoleRepository(PortfolioDbContext db)
		{
			_db = db;
		}

		public async Task AddRoleAsync(Role role)
		{
			await _db.Roles.AddAsync(role);
			await _db.SaveChangesAsync();
		}

		public async Task<Role?> GetRoleByIdAsync(int id)
		{
			return await _db.Roles.FindAsync(id);
		}

		public async Task<IEnumerable<Role>> GetAllRolesAsync()
		{
			return await _db.Roles.ToListAsync();
		}

		public async Task UpdateRoleAsync(Role role)
		{
			_db.Roles.Update(role);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteRoleAsync(int id)
		{
			var role = await _db.Roles.FindAsync(id);
			if (role != null)
			{
				_db.Roles.Remove(role);
				await _db.SaveChangesAsync();
			}
		}
	}

}

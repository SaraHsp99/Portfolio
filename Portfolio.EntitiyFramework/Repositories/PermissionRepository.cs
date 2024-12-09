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
	public class PermissionRepository : IPermissionRepository
	{
		private readonly PortfolioDbContext _db;

		public PermissionRepository(PortfolioDbContext db)
		{
			_db = db;
		}

		public async Task AddPermissionAsync(Permission permission)
		{
			await _db.Permissions.AddAsync(permission);
			await _db.SaveChangesAsync();
		}

		public async Task<Permission?> GetPermissionByIdAsync(int id)
		{
			return await _db.Permissions.FindAsync(id);
		}

		public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
		{
			return await _db.Permissions.ToListAsync();
		}

		public async Task UpdatePermissionAsync(Permission permission)
		{
			_db.Permissions.Update(permission);
			await _db.SaveChangesAsync();
		}

		public async Task DeletePermissionAsync(int id)
		{
			var permission = await _db.Permissions.FindAsync(id);
			if (permission != null)
			{
				_db.Permissions.Remove(permission);
				await _db.SaveChangesAsync();
			}
		}
	}

}

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
	public class RolePermissionRepository : IRolePermissionRepository
	{
		private readonly PortfolioDbContext _db;

		public RolePermissionRepository(PortfolioDbContext db)
		{
			_db = db;
		}

		public async Task AddRolePermissionAsync(RolePermission rolePermission)
		{
			await _db.RolePermissions.AddAsync(rolePermission);
			await _db.SaveChangesAsync();
		}

		public async Task<IEnumerable<RolePermission>> GetRolePermissionsByRoleIdAsync(int roleId)
		{
			return await _db.RolePermissions.Where(rp => rp.RoleId == roleId).ToListAsync();
		}

		public async Task<IEnumerable<RolePermission>> GetRolePermissionsByPermissionIdAsync(int permissionId)
		{
			return await _db.RolePermissions.Where(rp => rp.PermissionId == permissionId).ToListAsync();
		}

		public async Task DeleteRolePermissionAsync(int roleId, int permissionId)
		{
			var rolePermission = await _db.RolePermissions
				.FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
			if (rolePermission != null)
			{
				_db.RolePermissions.Remove(rolePermission);
				await _db.SaveChangesAsync();
			}
		}
	}

}

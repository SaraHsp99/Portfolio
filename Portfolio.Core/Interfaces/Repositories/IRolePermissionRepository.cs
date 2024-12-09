using Portfolio.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Repositories
{
	public interface IRolePermissionRepository
	{
		Task AddRolePermissionAsync(RolePermission rolePermission);
		Task<IEnumerable<RolePermission>> GetRolePermissionsByRoleIdAsync(int roleId);
		Task<IEnumerable<RolePermission>> GetRolePermissionsByPermissionIdAsync(int permissionId);
		Task DeleteRolePermissionAsync(int roleId, int permissionId);
	}

}

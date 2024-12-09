using Portfolio.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Repositories
{
	public interface IRoleRepository
	{
		Task AddRoleAsync(Role role);
		Task<Role?> GetRoleByIdAsync(int id);
		Task<IEnumerable<Role>> GetAllRolesAsync();
		Task UpdateRoleAsync(Role role);
		Task DeleteRoleAsync(int id);
	}

}

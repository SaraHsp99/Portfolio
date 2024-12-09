using Portfolio.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Repositories
{
    public interface IPermissionRepository
    {
        Task AddPermissionAsync(Permission permission);
        Task<Permission> GetPermissionByIdAsync(int id);
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
        Task UpdatePermissionAsync(Permission permission);
        Task DeletePermissionAsync(int id);
    }

}

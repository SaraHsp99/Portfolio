using Portfolio.Core.Entities.ClassBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services.AuthenticateManagerInterfaces
{
	public interface IAuthenticateManagerService
	{
		IResult SetPermissionForAllUser();
		//List<PermissionDto> GetAllPermission();
		//List<UserPermissionDto> GetPermissionByUser(long userId);
		//List<RolePermissionDto> GetRolePermissionsByRoleId(int roleId);
		IResult InsertUserPermission(long userPermissionId, long userId);
		IResult InsertRolePermission(long rolePermissionId, int roleId);
		//List<RoleDto> GetAllUserRoleByUserId(long userId);
		IResult AddUserRole(int roleId, long userId);
		IResult DeleteUserRole(long UserRoleId);
		IResult Restart();

	}
}

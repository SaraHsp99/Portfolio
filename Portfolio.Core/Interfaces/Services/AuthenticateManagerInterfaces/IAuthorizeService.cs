using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services.AuthenticateManagerInterfaces
{
	public interface IAuthorizeService
	{
		bool IsAuthorizeByRoleName(string roleName);
		bool IsAuthorizeByRoleNames(string[] roleNames);
		bool IsAuthorizeByPermissionName(string permissionName);
		bool IsAuthorizeByPermissionNames(string[] permissionNames);
		//List<UserPermissionDto> GetPermissionUser();

	}
}

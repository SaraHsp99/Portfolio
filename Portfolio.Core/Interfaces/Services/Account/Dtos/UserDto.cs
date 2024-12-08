using Portfolio.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services.Account.Dtos;
public partial record UserDto
{

	public long Id { get; set; }
	[Required]
	[StringLength(50)]
	public string UserName { get; set; }
	[Required]
	[StringLength(200)]
	public string Email { get; set; }
	[Required]
	public byte[] Password { get; set; }
	[Required]
	public byte[] SaltPassword { get; set; }
	[Required]
	public bool? IsActive { get; set; }
	public bool IsLock { get; set; }
	public DateTime? DateLock { get; set; }
	public long? CreateBy { get; set; }
	public DateTime? CreateDate { get; set; }
	public long? UpdateBy { get; set; }
	public DateTime? UpdateDate { get; set; }
	[StringLength(50)]
	public string PhoneNumber { get; set; }
	public short? CountFailLogin { get; set; }
	public DateTime? LastDateLogin { get; set; }
	public short? CountIsLogin { get; set; }
	public string Role { get; set; } = "Admin";
	public List<string> Roles { get; set; }

	//[InverseProperty("User")]
	//public virtual ICollection<UserPermission> UserPermission { get; set; }

	//[InverseProperty("User")]
	//public virtual ICollection<UserRole> UserRole { get; set; }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities.Account;
public partial class User
{
    public User()
    {

        UserPermission = new HashSet<UserPermission>();
        UserRole = new HashSet<UserRole>();
    }


    [Key]
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
    [Column(TypeName = "datetime")]
    public DateTime? DateLock { get; set; }
    public long? CreateBy { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }
    public long? UpdateBy { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }
    [StringLength(50)]
    public string PhoneNumber { get; set; }
    public short? CountFailLogin { get; set; }
    public DateTime? LastDateLogin { get; set; }
    public short? CountIsLogin { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<UserPermission> UserPermission { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<UserRole> UserRole { get; set; }
}


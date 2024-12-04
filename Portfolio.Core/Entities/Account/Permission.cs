using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities.Account
{
    public partial class Permission
    {
        public Permission()
        {
            InversePidNavigation = new HashSet<Permission>();
            RolePermission = new HashSet<RolePermission>();
            UserPermission = new HashSet<UserPermission>();
        }

        [Key]
        public int Id { get; set; }
        [Column("PId")]
        public int? Pid { get; set; }
        [Required]
        [StringLength(50)]
        public string PermissionName { get; set; }
        [StringLength(500)]

        public string PermissionAddress { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        [ForeignKey("Pid")]
        [InverseProperty("InversePidNavigation")]
        public virtual Permission PidNavigation { get; set; }
        [InverseProperty("PidNavigation")]
        public virtual ICollection<Permission> InversePidNavigation { get; set; }
        [InverseProperty("Permission")]
        public virtual ICollection<RolePermission> RolePermission { get; set; }
        [InverseProperty("Permission")]
        public virtual ICollection<UserPermission> UserPermission { get; set; }
    }
}

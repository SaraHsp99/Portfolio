using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities.Account
{
    public partial class Role
    {
        public Role()
        {
            RolePermission = new HashSet<RolePermission>();
            UserRole = new HashSet<UserRole>();
        }
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public bool? NonDelete { get; set; }
        public long? InsertBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        public long? DeleteBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeleteDate { get; set; }
        public bool? IsDelete { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<RolePermission> RolePermission { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}

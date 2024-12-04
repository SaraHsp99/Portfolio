using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities.Account
{
    public partial class RolePermission
    {
        [Key]
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? PermissionId { get; set; }
        public bool? IsGranted { get; set; }

        [ForeignKey("PermissionId")]
        [InverseProperty("RolePermission")]
        public virtual Permission Permission { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("RolePermission")]
        public virtual Role Role { get; set; }
    }
}

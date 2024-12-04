using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities.Account
{
    public partial class UserPermission
    {
        [Key]
        public long Id { get; set; }
        public long? UserId { get; set; }
        public int? PermissionId { get; set; }
        public bool? IsGranted { get; set; }

        [ForeignKey("PermissionId")]
        [InverseProperty("UserPermission")]
        public virtual Permission Permission { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UserPermission")]
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities.Account
{
    [Table("UserRole")]
    public class UserRole
    {

        [Key]
        public long Id { get; set; }
        public int? RoleId { get; set; }
        public long? UserId { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("UserRole")]
        public virtual Role Role { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UserRole")]
        public virtual User User { get; set; }
    }
}

using Portfolio.Core.Entities.ClassBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities.Account
{
    public class UserLoginAttempt : BaseEntity<int>
    {

        public long? UserId { get; set; }
        public string UserName { get; set; }
        public string ClientIpAddress { get; set; }
        public string? ClientName { get; set; }
        public string BrowserInfo { get; set; }
        public bool IsLogin { get; set; }
        public DateTime Date { get; set; }
    }
}

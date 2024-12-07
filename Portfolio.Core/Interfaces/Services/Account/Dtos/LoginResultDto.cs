using Portfolio.Core.Entities.ClassBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services.Account.Dtos
{
    public class LoginResultDto
    {
        public LoginResult LoginResult { get; set; }
        public UserDto UserDto { get; set; }
        public IResult Result { get; set; }
    }

}

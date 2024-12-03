using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Securities
{
	public class HashPasswordDto
	{
		public byte[] Password { get; set; }
		public byte[] SaltPassword { get; set; }
	}
}

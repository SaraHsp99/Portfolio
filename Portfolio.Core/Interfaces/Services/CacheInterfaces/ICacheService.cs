using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services.CacheInterfaces
{
	public interface ICacheService
	{
		bool ClearCach(string key);
	}
}

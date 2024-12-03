using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities.ClassBases
{
	public class Result : IResult
	{
		public object Data { get; set; }
		public bool Rsl { get; set; }
		public string Message { get; set; }
		public Result()
		{
			Rsl = true;
		}
	}
}

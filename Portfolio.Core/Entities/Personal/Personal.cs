using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities.Personal
{
	public class Personal
	{
		public int Id { get; set; }
		public long UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Title { get; set; }
		public string Bio { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
	}

}

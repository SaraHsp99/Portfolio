using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities.Job
{
	public class JobExperience
	{
		public int Id { get; set; }
		public string Company { get; set; }
		public string Title { get; set; }
		public string Position { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string Description { get; set; }
		public bool IsCurrent { get; set; }
	}

}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities.Skill
{
	public class Skill
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Level { get; set; }
		public int Years { get; set; }
		public string Description { get; set; }
	}

}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Entities.Personal
{
	public class Personal
	{
		[Key]
		public int Id { get; set; }
		public long UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string JobTitle { get; set; }
		public string Bio { get; set; }
		public string Description { get; set; }
		public string ProfileImagePath { get; set; }
		public int YearsOfExperience { get; set; }
		public int SuccessfulProjects { get; set; }
		public int CustomerSatisfaction { get; set; }
	}

}

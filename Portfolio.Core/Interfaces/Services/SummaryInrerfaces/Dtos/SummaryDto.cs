using Portfolio.Core.Entities.Personal;
using Portfolio.Core.Interfaces.Services.PersonalInterfaces.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services.SummaryInrerfaces.Dtos;
public class SummaryDto
{
	public string UserName { get; set; }
	public List<string> Skills { get; set; }
	public List<string> JobExperiences { get; set; }
	public PersonalDto PersonalInfo { get; set; }
}




using Portfolio.Core.Interfaces.Services.SummaryInrerfaces.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Services.SummaryInrerfaces
{
	public interface ISummaryService
	{
		Task<SummaryDto> GetSummaryAsync();
	}
}

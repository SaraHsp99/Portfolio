using Portfolio.Core.Interfaces.Services.PersonalInterfaces.Dtos;
using Portfolio.Core.Interfaces.Services.SummaryInrerfaces.Dtos;

namespace Portfolio.Web.Models
{
    public class HomeViewModel
    {
        public SummaryDto Summary { get; set; }=new SummaryDto();
    }
}

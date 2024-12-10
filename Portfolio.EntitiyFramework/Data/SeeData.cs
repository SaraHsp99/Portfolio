using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Entities.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;


namespace Portfolio.Infrastructure.Data;

public static class SeedData
{
	public static void Initialize(IServiceProvider serviceProvider, IHostEnvironment env)
	{
		using var scope = serviceProvider.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();

		if (!context.Personals.Any())
		{
			var personal = new Personal
			{
				FirstName = "سارا",
				LastName = " حسین پناهی",
				Description = " با تجربه و مهارتی که طی این سال‌ها بدست آوردم، شما را در طول مسیر کسب و کار و ایدتان همراهی می‌کنم.",
				JobTitle = "برنامه نویس ,دات نت دولوپر",
				ProfileImagePath ="~/image/me/me.jpg",
				YearsOfExperience=3,
				SuccessfulProjects=10,
				CustomerSatisfaction=100
				

			};

			context.Personals.Add(personal);
			context.SaveChanges();
		}
	}
}



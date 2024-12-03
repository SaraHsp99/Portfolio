using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Entities.ClassBases;
using Portfolio.Core.Interfaces.Services.CacheInterfaces;
using Portfolio.Core.RegisterAutoMapper;
using Portfolio.Provider.CacheProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.IOC
{
    public static class IOC_Helper
    {
        public static void RegisterService(this IServiceCollection services)
        {

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RegisterMap(provider.GetService<IHttpContextAccessor>()));
            }).CreateMapper());

			#region define services
			services.AddScoped<IResult, Result>();
			services.AddScoped<ICacheService, CacheService>();
			#endregion
		}
	}
}

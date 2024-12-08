using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Entities.ClassBases;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Interfaces.Services.Account;
using Portfolio.Core.Interfaces.Services.CacheInterfaces;
using Portfolio.Core.RegisterAutoMapper;
using Portfolio.Infrastructure.Repositories;
using Portfolio.Provider.AccountProvider;
using Portfolio.Provider.CacheProvider;
using Portfolio.Provider.SecurityProvider;
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

			#region define repositories
			services.AddScoped<IUserRepository, UserRepository>();
			#endregion

			#region define services
			services.AddSingleton<JwtService>();
			services.AddScoped<IResult, Result>();
			services.AddScoped<ICacheService, CacheService>();
			services.AddScoped<IUserService, UserService>();
			#endregion
		}
	}
}

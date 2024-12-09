using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Entities.ClassBases;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Interfaces.Services.Account;
using Portfolio.Core.Interfaces.Services.CacheInterfaces;
using Portfolio.Core.Interfaces.Services.PersonalInterfaces;
using Portfolio.Core.Interfaces.Services.SummaryInrerfaces;
using Portfolio.Core.RegisterAutoMapper;
using Portfolio.Infrastructure.Repositories;
using Portfolio.Provider.AccountProvider;
using Portfolio.Provider.CacheProvider;
using Portfolio.Provider.PersonalProvider;
using Portfolio.Provider.SecurityProvider;
using Portfolio.Provider.SummaryProvider;
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
			//services.AddScoped<IGenericRepository<T>, GenericRepository>();
			services.AddScoped<IJobExperienceRepository, JobExperienceRepository>();
			services.AddScoped<IPermissionRepository, PermissionRepository>();
			services.AddScoped<IPersonalRepository, PersonalRepository>();
			services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
			services.AddScoped<ISkillRepository, SkillRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			#endregion

			#region define services
			services.AddSingleton<JwtService>();
			services.AddScoped<IResult, Result>();
			services.AddScoped<ICacheService, CacheService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IPersonalService, PersonalService>();
			services.AddScoped<ISummaryService, SummaryService>();
			#endregion
		}
	}
}

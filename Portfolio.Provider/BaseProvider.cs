using AutoMapper;
using Portfolio.Core.Entities.ClassBases;
using Portfolio.Core.Interfaces.Services.CacheInterfaces;
using Portfolio.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Provider
{
	public abstract class BaseProvider
	{
		protected readonly IMapper _mapper;
		protected readonly IResult _result;
		protected readonly ICacheService _cacheService;

		protected BaseProvider(
			IMapper mapper,
			ICacheService cacheService,
			IResult result)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
			_result = result;
		}

		public DateTime CurrentDate => DateTime.Now;

		protected bool ClearCache(string key)
		{
			return _cacheService.ClearCach(key);
		}
	}

}

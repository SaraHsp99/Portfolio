using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Portfolio.Core.Interfaces.Services.CacheInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Provider.CacheProvider
{
	public class CacheService : ICacheService
	{
		private readonly IMemoryCache _cache;
		private readonly IMapper _mapper;
		public CacheService(IMemoryCache cache,
			IMapper mapper)
		{
			_cache = cache;
			_mapper = mapper;
		}
		public bool ClearCach(string key)
		{
			try
			{
				_cache.Remove(key);
				return true;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
	}
}

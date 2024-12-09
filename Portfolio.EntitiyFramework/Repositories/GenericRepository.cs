using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly PortfolioDbContext _db;

		public GenericRepository(PortfolioDbContext db)
		{
			_db = db;
		}

		public async Task AddAsync(T entity)
		{
			await _db.Set<T>().AddAsync(entity);
			await _db.SaveChangesAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _db.Set<T>().FindAsync(id);
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _db.Set<T>().ToListAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_db.Set<T>().Update(entity);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _db.Set<T>().FindAsync(id);
			if (entity != null)
			{
				_db.Set<T>().Remove(entity);
				await _db.SaveChangesAsync();
			}
		}
	}


}

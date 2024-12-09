using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Entities.Personal;
using Portfolio.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Repositories
{
	public class PersonalRepository : IPersonalRepository
	{
		private readonly PortfolioDbContext _db;

		public PersonalRepository(PortfolioDbContext db)
		{
			_db = db;
		}

		public IQueryable<Personal> GetAll() 
		{ 
			return  _db.Personals.AsQueryable();
		}

		public async Task AddPersonalAsync(Personal personal)
		{
			await _db.Personals.AddAsync(personal);
			await _db.SaveChangesAsync();
		}

		public async Task<Personal?> GetPersonalByIdAsync(int id)
		{
			return await _db.Personals.FindAsync(id);
		}

		public async Task<Personal?> GetPersonalByUserIdAsync(long userId)
		{
			return await _db.Personals.FirstOrDefaultAsync(p => p.UserId == userId);
		}

		public async Task UpdatePersonalAsync(Personal personal)
		{
			_db.Personals.Update(personal);
			await _db.SaveChangesAsync();
		}

		public async Task DeletePersonalAsync(int id)
		{
			var personal = await _db.Personals.FindAsync(id);
			if (personal != null)
			{
				_db.Personals.Remove(personal);
				await _db.SaveChangesAsync();
			}
		}
	}


}

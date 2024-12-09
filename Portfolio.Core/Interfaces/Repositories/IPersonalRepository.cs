using Portfolio.Core.Entities.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Repositories
{
	public interface IPersonalRepository
	{
		IQueryable<Personal> GetAll();
		Task AddPersonalAsync(Personal personal);
		Task<Personal?> GetPersonalByIdAsync(int id);
		Task<Personal?> GetPersonalByUserIdAsync(long userId);
		Task UpdatePersonalAsync(Personal personal);
		Task DeletePersonalAsync(int id);
	}

}

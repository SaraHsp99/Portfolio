using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Entities.Skill;
using Portfolio.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Repositories
{
	public class SkillRepository : ISkillRepository
	{
		private readonly PortfolioDbContext _db;

		public SkillRepository(PortfolioDbContext db)
		{
			_db = db;
		}

		public async Task AddSkillAsync(Skill skill)
		{
			await _db.Skills.AddAsync(skill);
			await _db.SaveChangesAsync();
		}

		public async Task<Skill?> GetSkillByIdAsync(int id)
		{
			return await _db.Skills.FindAsync(id);
		}

		public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
		{
			return await _db.Skills.ToListAsync();
		}

		public async Task UpdateSkillAsync(Skill skill)
		{
			_db.Skills.Update(skill);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteSkillAsync(int id)
		{
			var skill = await _db.Skills.FindAsync(id);
			if (skill != null)
			{
				_db.Skills.Remove(skill);
				await _db.SaveChangesAsync();
			}
		}
	}

}

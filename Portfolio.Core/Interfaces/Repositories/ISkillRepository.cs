using Portfolio.Core.Entities.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Repositories
{
	public interface ISkillRepository
	{
		Task AddSkillAsync(Skill skill);
		Task<Skill?> GetSkillByIdAsync(int id);
		Task<IEnumerable<Skill>> GetAllSkillsAsync();
		Task UpdateSkillAsync(Skill skill);
		Task DeleteSkillAsync(int id);
	}

}

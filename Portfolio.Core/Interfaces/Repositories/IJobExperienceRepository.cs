using Portfolio.Core.Entities.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Repositories
{
	public interface IJobExperienceRepository
	{
		Task AddJobExperienceAsync(JobExperience jobExperience);
		Task<JobExperience?> GetJobExperienceByIdAsync(int id);
		Task<IEnumerable<JobExperience>> GetAllJobExperiencesAsync();
		Task UpdateJobExperienceAsync(JobExperience jobExperience);
		Task DeleteJobExperienceAsync(int id);
	}

}

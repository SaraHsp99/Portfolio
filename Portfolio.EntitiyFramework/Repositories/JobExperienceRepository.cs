using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Entities.Job;
using Portfolio.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Repositories
{
	public class JobExperienceRepository : IJobExperienceRepository
	{
		private readonly PortfolioDbContext _db;

		public JobExperienceRepository(PortfolioDbContext db)
		{
			_db = db;
		}

		public async Task AddJobExperienceAsync(JobExperience jobExperience)
		{
			await _db.JobExperiences.AddAsync(jobExperience);
			await _db.SaveChangesAsync();
		}

		public async Task<JobExperience?> GetJobExperienceByIdAsync(int id)
		{
			return await _db.JobExperiences.FindAsync(id);
		}

		public async Task<IEnumerable<JobExperience>> GetAllJobExperiencesAsync()
		{
			return await _db.JobExperiences.ToListAsync();
		}

		public async Task UpdateJobExperienceAsync(JobExperience jobExperience)
		{
			_db.JobExperiences.Update(jobExperience);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteJobExperienceAsync(int id)
		{
			var jobExperience = await _db.JobExperiences.FindAsync(id);
			if (jobExperience != null)
			{
				_db.JobExperiences.Remove(jobExperience);
				await _db.SaveChangesAsync();
			}
		}
	}

}

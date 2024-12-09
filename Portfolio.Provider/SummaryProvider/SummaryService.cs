using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Interfaces.Services.SummaryInrerfaces.Dtos;
using Portfolio.Core.Interfaces.Services.SummaryInrerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Core.Interfaces.Services.PersonalInterfaces.Dtos;
using Portfolio.Core.Interfaces.Services.PersonalInterfaces;
using Portfolio.Infrastructure.Repositories;

namespace Portfolio.Provider.SummaryProvider;
public class SummaryService : ISummaryService
{
	private readonly IUserRepository _userRepository;
	private readonly ISkillRepository _skillRepository;
	private readonly IJobExperienceRepository _jobExperienceRepository;
	private readonly IPersonalService _personalService;

	public SummaryService(
		IUserRepository userRepository,
		ISkillRepository skillRepository,
		IJobExperienceRepository jobExperienceRepository,
		IPersonalService personalService)
	{
		_userRepository = userRepository;
		_skillRepository = skillRepository;
		_jobExperienceRepository = jobExperienceRepository;
		_personalService = personalService;
	}

	public async Task<SummaryDto> GetSummaryAsync()
	{
		var user = await _userRepository.GetUserByEmailAsync("sarahoseinpanahi99@gmail.com");
		var skills = await _skillRepository.GetAllSkillsAsync();
		var jobExperiences = await _jobExperienceRepository.GetAllJobExperiencesAsync();
		var personal = await _personalService.GetPersonal();
		if (personal == null)
		{
			throw new Exception("User not found");
		}

		var summary = new SummaryDto
		{
			UserName = user.UserName,
			Skills = skills.Select(s => s.Name).ToList(),
			JobExperiences = jobExperiences.Select(j => j.Title).ToList(),
			PersonalInfo = personal
		};

		return summary;
	}
	//public async Task<SummaryDto> GetHomeDataAsync()
	//{
	//	var personalInfo = await _personalRepository.GetPersonalInfoAsync();
	//	var skills = await _skillRepository.GetSkillsAsync();
	//	var jobExperiences = await _jobExperienceRepository.GetJobExperiencesAsync();

	//	return new SummaryDto
	//	{
	//		UserName = personalInfo.FullName,
	//		Bio = personalInfo.Bio,
	//		ProfileImagePath = personalInfo.ProfileImagePath, // مثلاً آدرس عکس از دیتابیس
	//		YearsOfExperience = personalInfo.YearsOfExperience,
	//		SuccessfulProjects = personalInfo.SuccessfulProjects,
	//		CustomerSatisfaction = personalInfo.CustomerSatisfactionPercentage,
	//		Skills = skills.Select(s => s.Name).ToList(),
	//		JobExperiences = jobExperiences.Select(j => j.Title).ToList()
	//	};
	//}

}

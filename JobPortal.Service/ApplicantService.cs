using JobPortal.Database.Repo;
using JobPortal.Model;
using JobPortal.Service.Notifications;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly IJobRepository _jobRepository;
        public ApplicantService(IApplicantRepository applicantRepository, IUserRepository userRepository, IEmailSender emailSender, IJobRepository jobRepository)
        {
            _applicantRepository = applicantRepository;
            _userRepository = userRepository;
            _emailSender = emailSender;
            _jobRepository = jobRepository;
        }

        public async Task<Applicant> Apply(Applicant entity, int userId)
        {
            try
            {
            var applyJob = new Applicant();
            applyJob.JobId = entity.JobId;
            applyJob.AppliedBy = userId;
            applyJob.AppliedAt = DateTime.Now;
            applyJob.IsActive = true;

            var UserDetails = await _userRepository.GetById(userId);
            var jobDetails = await _jobRepository.GetById(applyJob.JobId);

            if(jobDetails != null) 
                { 
                    await _applicantRepository.AddAsync(applyJob);
                    var to = UserDetails.Email;
                    var subject = "Job Applied";
                    var body = "You have successfully applied for the job " + jobDetails.Title;
                    await _emailSender.SendEmailAsync(to, subject, body);

                    var recruiter = await _userRepository.GetById(jobDetails.CreatedBy);
                    var subjectRecruiter = "Applicant registered";
                    var bodyRecruiter = $"One Applicant have applied for the job you posted. <br /> Applicant Email : {UserDetails.Email} <br /> Applicant Name : {UserDetails.Name} <br /> Job Title : {jobDetails.Title}";
                    await _emailSender.SendEmailAsync(recruiter.Email, subjectRecruiter, bodyRecruiter);
                    return applyJob;
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred : {0}", ex);
            }
            return null;
        }

        public async Task<IEnumerable<Applicant>> Apply(List<Applicant> entities)
        {
            IEnumerable<Applicant> applicants = await _applicantRepository.AddAsync(entities);
            return applicants;
        }

        public async Task<IEnumerable<JobPostedApplicantDto>> GetAllApplicantAppliedToMyJobPosted(int userId, PagedParameters pagedParameters)
        {
            return await _applicantRepository.GetAllApplicantAppliedToMyJobPosted(userId, pagedParameters);   
        }

        public async Task<IEnumerable<ApplicantDetailsDto>> GetAllApplicantJobApplied(PagedParameters pagedParameters)
        {
            return await _applicantRepository.GetAllApplicantJobApplied(pagedParameters);
        }
    }
}

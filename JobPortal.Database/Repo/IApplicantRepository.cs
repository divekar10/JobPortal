﻿using JobPortal.Database.Infra;
using JobPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public interface IApplicantRepository : IRepository<Applicant>
    {
        Task<IEnumerable<ApplicantDetailsDto>> GetAllApplicantJobApplied();
        Task<IEnumerable<JobPostedApplicantDto>> GetAllApplicantAppliedToMyJobPosted(int userId);
    }
}

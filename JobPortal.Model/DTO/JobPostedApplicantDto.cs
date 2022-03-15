using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model
{
    public class JobPostedApplicantDto
    {
        public string ApplicantName { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public int RecruiterId { get; set; }
        public string RecruiterName { get; set; }
    }
}

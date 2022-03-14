using JobPortal.Model;
using JobPortal.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers.Applicants
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Authorize]
    public class ApplicantController : BaseController
    {
        private readonly IApplicantService _applicantService;
        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpPost]
        [Route("Apply")]
        public async Task<IActionResult> Apply(Applicant applicant)
        {
            var apply = await _applicantService.Apply(applicant, UserId);
            if(apply != null)
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Applied successfully ...", Data = apply });
            return BadRequest(new Response { Code = StatusCodes.Status400BadRequest, Message = "Wrong details"});
        }

        [HttpGet]
        [Route("GetAllApplicantJobApplied")]
        public async Task<IEnumerable<ApplicantDetailsDto>> GetAllApplicantJobApplied()
        {
            return await _applicantService.GetAllApplicantJobApplied();
        }

        [HttpGet]
        [Route("GetAllApplicantAppliedToMyJobPosted")]
        public async Task<IEnumerable<JobPostedApplicantDto>> GetAllApplicantAppliedToMyJobPosted()
        {
            return await _applicantService.GetAllApplicantAppliedToMyJobPosted(UserId);
        }
    }
}

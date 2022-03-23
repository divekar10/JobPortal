using JobPortal.Model;
using JobPortal.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers.Applicants
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class ApplicantController : BaseController
    {
        private readonly IApplicantService _applicantService;
        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpPost]
        [Route("Apply")]
        [Authorize(Policy = "AllAllowed")]
        public async Task<IActionResult> Apply(Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                var apply = await _applicantService.Apply(applicant, UserId);
                if (apply != null)
                    return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Applied successfully ...", Data = apply });
                return BadRequest(new Response { Code = StatusCodes.Status400BadRequest, Message = "Wrong details" });
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("AllApplicantJobApplied")]
        [Authorize(Policy = "AdminRecruiterOnly")]
        public async Task<IEnumerable<ApplicantDetailsDto>> GetAllApplicantJobApplied([FromQuery] PagedParameters pagedParameters)
        {
            return await _applicantService.GetAllApplicantJobApplied(pagedParameters);
        }

        [HttpGet]
        [Route("ApplicantsAppliedToRecruiterJob")]
        [Authorize(Policy = "AdminRecruiterOnly")]
        public async Task<IEnumerable<JobPostedApplicantDto>> GetAllApplicantAppliedToMyJobPosted([FromQuery] PagedParameters pagedParameters)
        {
            return await _applicantService.GetAllApplicantAppliedToMyJobPosted(UserId, pagedParameters);
        }
    }
}

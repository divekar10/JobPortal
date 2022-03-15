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
using System.Web.Http.ModelBinding;

namespace JobPortal.Api.Controllers.Applicants
{
    //public class Error
    //{
    //    public Error(string key, string message)
    //    {
    //        Key = key;
    //        Message = message;
    //    }

    //    public string Key { get; set; }
    //    public string Message { get; set; }
    //}
    //public static class Utils
    //{
    //    public static IEnumerable<Error> AllErrors(this ModelStateDictionary modelState)
    //    {
    //        var result = from ms in modelState
    //                     where ms.Value.Errors.Any()
    //                     let fieldKey = ms.Key
    //                     let errors = ms.Value.Errors
    //                     from error in errors
    //                     select new Error(fieldKey, error.ErrorMessage);

    //        return result;
    //    }
    //}
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
        [Authorize(Policy = "Allowed")]
        public async Task<IActionResult> Apply(Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("error");
            }
            var apply = await _applicantService.Apply(applicant, UserId);
            if(apply != null)
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Applied successfully ...", Data = apply });
            return BadRequest(new Response { Code = StatusCodes.Status400BadRequest, Message = "Wrong details"});
        }

        [HttpGet]
        [Route("AllApplicantJobApplied")]
        [Authorize(Policy = "Restricted")]
        public async Task<IEnumerable<ApplicantDetailsDto>> GetAllApplicantJobApplied()
        {
            return await _applicantService.GetAllApplicantJobApplied();
        }

        [HttpGet]
        [Route("ApplicantsAppliedToRecruiterJob/{id}")]
        [Authorize(Policy = "Restricted")]
        public async Task<IEnumerable<JobPostedApplicantDto>> GetAllApplicantAppliedToMyJobPosted()
        {
            return await _applicantService.GetAllApplicantAppliedToMyJobPosted(UserId);
        }
    }
}

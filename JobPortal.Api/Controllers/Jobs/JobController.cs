using JobPortal.Model;
using JobPortal.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers.Jobs
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class JobController : BaseController
    {
        private readonly IJobService _jobService;
        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        [Route("Add")]
        [Authorize(Policy = "AdminRecruiterOnly")]
        public async Task<IActionResult> Add(Job job)
        {
            if (ModelState.IsValid)
            {
                var result = await _jobService.Add(job, UserId);
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Job successfully created...", Data = result });
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "AdminRecruiterOnly")]
        public async Task<IActionResult> Update(Job job)
        {
            if (ModelState.IsValid)
            {
                var result = await _jobService.Update(job);
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Details updated successfully..", Data = result });
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "AdminRecruiterOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _jobService.Delete(id);

            if (result == true)
            {
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Job deleted successfully.." });
            }
            return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "Job not found.." });
        }

        [HttpGet]
        [Route("Jobs")]
        [Authorize(Policy = "AllAllowed")]
        public async Task<IEnumerable<Job>> Get([FromQuery] PagedParameters pagedParameters)
        {
            return await _jobService.GetJobs(pagedParameters);
        }

        [HttpGet]
        [Route("JobsPostedByMe")]
        [Authorize(Policy = "AdminRecruiterOnly")]
        public async Task<IActionResult> GetMyJobs([FromQuery] PagedParameters pagedParameters)
        {
           var jobs = await _jobService.GetMyJobs(UserId, pagedParameters);

            if(!jobs.Any())
            {
                return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "Data not found.." });
            }
            return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Success", Data = jobs });
        }
    }
}

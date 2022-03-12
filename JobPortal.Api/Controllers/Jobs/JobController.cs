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

namespace JobPortal.Api.Controllers.Jobs
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize]
    public class JobController : BaseController
    {
        private readonly IJobService _jobService;
        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        [Route("AddJob")]
        public async Task<IActionResult> Add(Job job)
        {
            var result = await _jobService.Add(job, UserId);
            return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Job successfully created...", Data = result });
        }

        [HttpPut]
        [Route("UpdateJob")]
        public async Task<IActionResult> Update(Job job)
        {
            var result = await _jobService.Update(job);
            return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Details updated successfully..", Data = result });
        }

        [HttpDelete]
        [Route("DeleteJob/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _jobService.Delete(id);

            if (result == true)
            {
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Job deleted successfully.." });
            }
            return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "User not found.." });
        }

        [HttpGet]
        [Route("AllJobs")]
        public async Task<IEnumerable<Job>> Get()
        {
            return await _jobService.GetJobs();
        }

        [HttpGet]
        [Route("MyJobs")]
        public IActionResult GetMyJobs()
        {
           var jobs = _jobService.GetMyJobs(UserId);

            if(!jobs.Any())
            {
                return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "Data not found.." });
            }
            return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Success", Data = jobs });
        }
    }
}

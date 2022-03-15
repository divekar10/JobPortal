using JobPortal.Model;
using JobPortal.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiterService _recruiterService;
        private readonly IUserService _userService;
        public RecruiterController(IRecruiterService recruiterService, IUserService userService)
        {
            _recruiterService = recruiterService;
            _userService = userService;
        }

        [HttpPost]
       
        public async Task<IActionResult> Add(User user)
        {
            var isExist = await _userService.IsEmailAlreadyExist(user.Email);
            if (isExist == true)
            {
                return BadRequest(new Response { Code = StatusCodes.Status400BadRequest, Message = "Email Already exist.." });
            }
            else
            {
                var result = await _recruiterService.Add(user);
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Recruiter successfully created...", Data = result });
            }
        }

        [HttpGet]
        [Route("Recuiters")]
        public IActionResult GetRecuiters()
        {
            var recruiters = _recruiterService.GetRecruiters();
            if (!recruiters.Any())
            {
                return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "Data not found.." });
            }
            return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Success", Data = recruiters });
        }
    }
}

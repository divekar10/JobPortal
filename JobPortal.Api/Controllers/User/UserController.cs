using JobPortal.Model;
using JobPortal.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("Users")]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userService.GetUsers();
        }

        [HttpGet]
        [Route("Candidates")]
        public IActionResult GetCandidates()
        {
            var candidates = _userService.GetCandidates();
            if (!candidates.Any())
            {
                return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "Data not found.." });
            }
            return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Success", Data = candidates });
        }

        [HttpGet]
        [Route("Recuiters")]
        public IActionResult GetRecuiters()
        {
            var recruiters = _userService.GetRecruiters();
            if (!recruiters.Any())
            {
                return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "Data not found.." });
            }
            return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Success", Data = recruiters });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(User user)
        {
            var result = await _userService.Update(user);
            if(result != null)
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Details updated successfully..", Data = result});
            return BadRequest(new Response { Code = StatusCodes.Status400BadRequest, Message = "Something went wrong", Data = result });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            if(result == true)
            {
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "User deleted successfully.." });
            }
            return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "User not found.."});
        }

        [HttpPost]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userService.ForgotPassword(email);
            if(user == true)
            {
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Otp sent to the register email address.." });
            }
            return NotFound(new Response { Code = StatusCodes.Status400BadRequest, Message = "Incorrect Details.." });
        }
     
        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(int otp, string newPassword, string confirmPassword)
        {
            var user = await _userService.ResetPassword(otp, newPassword, confirmPassword);
            if(user != null)
            {
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Password updated successfully.." });
            }
            return BadRequest(new Response { Code = StatusCodes.Status400BadRequest, Message = "Incorect details.." });
        }

        [HttpGet]
        [Route("AppliedJobsByMe")]
        public async Task<IActionResult> GetMyAllJobsApplied()
        {
            var jobs = await _userService.GetMyAllJobsApplied(UserId);
            if (!jobs.Any())
            {
                return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "Data not found.." });
            }
            return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Success", Data = jobs });
        }
    }
}

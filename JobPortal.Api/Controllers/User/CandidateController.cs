using JobPortal.Model;
using JobPortal.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class CandidateController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache;
        private readonly string candidateKey = "candidateKey";
        public CandidateController(IUserService userService, IMemoryCache memoryCache)
        {
            _userService = userService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("Users")]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> Get([FromQuery] PagedParameters pagedParameters)
        {
            //object data = null;

            //if (_memoryCache.TryGetValue(candidateKey, out data))
            //    return Ok(data);
             var data = await _userService.GetUsers(pagedParameters);

            //var cacheOptions = new MemoryCacheEntryOptions()
            //    .SetSize(51)
            //    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
            //    .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
            //_memoryCache.Set(candidateKey, data, cacheOptions);
            return Ok(data);
        }

        [HttpPut] 
        [Route("{id}")]
        [Authorize(Policy = "AllAllowed")]
        public async Task<IActionResult> Update(User user)
        {
            if (ModelState.IsValid)
            {
            var result = await _userService.Update(user);
            if (result != null)
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Details updated successfully..", Data = result });
            return BadRequest(new Response { Code = StatusCodes.Status400BadRequest, Message = "Something went wrong", Data = result });
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            if (result == true)
            {
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "User deleted successfully.." });
            }
            return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "User not found.." });
        }

        [HttpPost]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userService.ForgotPassword(email);
            if (user == true)
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
            if (user != null)
            {
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Password updated successfully.." });
            }
            return BadRequest(new Response { Code = StatusCodes.Status400BadRequest, Message = "Incorect details.." });
        }

        [HttpGet]
        [Route("Candidates")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetCandidates([FromQuery] PagedParameters pagedParameters)
        {
                var candidates = await _userService.GetCandidates(pagedParameters);
                if (!candidates.Any())
                {
                    return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "Data not found.." });
                }
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Success", Data = candidates });
        }

        [HttpGet]
        [Route("AppliedJobsByMe")]
        [Authorize(Policy = "AdminUserOnly")]
        public async Task<IActionResult> GetMyAllJobsApplied([FromQuery] PagedParameters pagedParameters)
        {
            var jobs = await _userService.GetMyAllJobsApplied(UserId, pagedParameters);
            if (!jobs.Any())
            {
                return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "Data not found.." });
            }
            return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Success", Data = jobs });
        }
    }
}

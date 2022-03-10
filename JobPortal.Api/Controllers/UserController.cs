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
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(User user)
        {
            var result = await _userService.Add(user);
            return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Account successfully created...", Data = result});
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login model)
        {
            var user = await _userService.GetUser(model.Email, model.Password);

            if(user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim("UserId", Convert.ToString(user.Id), ClaimValueTypes.Integer),
                    new Claim("Email", user.Email, ClaimValueTypes.String),
                    new Claim("RoleId", Convert.ToString(user.RoleId), ClaimValueTypes.Integer),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("AllUsers")]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userService.GetUsers();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(User user)
        {
            var result = await _userService.Update(user);
            return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Details updated successfully..", Data = result});
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);

            if(result == true)
            {
                return Ok(new Response { Code = StatusCodes.Status200OK, Message = "User deleted successfully.." });
            }
            return NotFound(new Response { Code = StatusCodes.Status404NotFound, Message = "User not found.."});
        }
     
    }
}

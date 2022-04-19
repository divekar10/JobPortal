using JobPortal.Model;
using JobPortal.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;
        public AccountController(IUserService userService, IConfiguration configuration, IRoleService roleService)
        {
            _userService = userService;
            _configuration = configuration;
            _roleService = roleService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var isExist = await _userService.IsEmailAlreadyExist(user.Email);
                if (isExist == true)
                {
                    return BadRequest(new Response { Code = StatusCodes.Status400BadRequest, Message = "Email Already exist.." });
                }
                else
                {
                    var result = await _userService.Add(user);
                    if(result != null)
                        return Ok(new Response { Code = StatusCodes.Status200OK, Message = "Account successfully created...", Data = result });
                    return BadRequest(new Response { Code = StatusCodes.Status400BadRequest, Message = "Something went wrong" });
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(Login model)
        {
            var user = await _userService.GetUser(model.Email, model.Password);
            if (user != null)
            {
                var role = await _roleService.GetRoleById(user.RoleId);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim("UserId", Convert.ToString(user.Id), ClaimValueTypes.Integer),
                    new Claim("Email", user.Email, ClaimValueTypes.String),
                    new Claim("RoleId", Convert.ToString(user.RoleId), ClaimValueTypes.Integer),
                    new Claim(ClaimTypes.Role, role.Roles, ClaimValueTypes.String),
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
            return Unauthorized(new Response { Code = StatusCodes.Status401Unauthorized, Message = "Invalid Email or password"});
        }
    }
}

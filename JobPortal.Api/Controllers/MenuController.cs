using JobPortal.Service;
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
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("GetMenus")]
        public async Task<IActionResult> GetMenu()
        {
            return Ok(await _menuService.GetNavigationBar());
        }

        [HttpGet]
        [Route("GetUserMenus/{userId}")]
        public async Task<IActionResult> GetMenu(int userId)
        {
            return Ok(await _menuService.GetUserMenus(userId));
        }
    }
}

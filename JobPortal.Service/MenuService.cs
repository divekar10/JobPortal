using JobPortal.Database.Repo;
using JobPortal.Model.DTO;
using JobPortal.Model.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> GetNavigationBar();
        Task<List<MenuDTO>> GetUserMenus(int userId);
    }
    public class MenuService :IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<List<MenuDTO>> GetNavigationBar()
        {
            var parentMenu = await _menuRepository.GetMasterMenus();
            var subMenus = await _menuRepository.GetSubMenus();
            var subChildMenus = await _menuRepository.GetSubChildMenus();

            var menu = new List<MenuDTO>();

            menu = (from p in parentMenu
                          select new MenuDTO
                          {
                              Id = p.Id,
                              Name = p.Name,
                              subMenuMaster = (from s in subMenus
                                               where p.Id == s.MasterMenuId
                                               select new SubMenuMasterDTO
                                               {
                                                   Id = s.Id,
                                                   Name = s.Name,
                                                   MasterMenuId = s.MasterMenuId,
                                                   Path = s.Path,
                                                   SubMenuChildMaster = (from c in subChildMenus 
                                                                         where s.Id == c.SubMenuId
                                                                         select new SubMenuChildMaster 
                                                                         { 
                                                                            Id = c.Id,
                                                                            Name = c.Name,
                                                                            SubMenuId = c.SubMenuId,
                                                                            Path = c.Path
                                                                         }).ToList()
                                               }).ToList()
                          }).ToList();

            return menu;

        }

        public async Task<List<MenuDTO>> GetUserMenus(int userId)
        {
            return await _menuRepository.GetUserMenus(userId);
        }
    }
}

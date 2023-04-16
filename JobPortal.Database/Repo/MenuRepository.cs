using JobPortal.Database.Infra;
using JobPortal.Model.DTO;
using JobPortal.Model.Menu;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Database.Repo
{
    public interface IMenuRepository : IRepository<UserMenuMapping>
    {
        Task<IEnumerable<MenuMaster>> GetMasterMenus();
        Task<IEnumerable<SubMenuMaster>> GetSubMenus();
        Task<IEnumerable<SubMenuChildMaster>> GetSubChildMenus();

        Task<List<MenuDTO>> GetUserMenus(int userId);
    }
    public class MenuRepository : Repository<UserMenuMapping>, IMenuRepository
    {
        public MenuRepository(JobDbContext context): base(context)
        {

        }

        public async Task<IEnumerable<MenuMaster>> GetMasterMenus()
        {
            var menus = await (from m in JobDbContext.MenuMaster
                               select m).ToListAsync();

            return menus;
        }

        public async Task<IEnumerable<SubMenuChildMaster>> GetSubChildMenus()
        {
            var menus = await (from m in JobDbContext.SubMenuChildMaster
                               select m).ToListAsync();

            return menus;
        }

        public async Task<IEnumerable<SubMenuMaster>> GetSubMenus()
        {
            var menus = await(from m in JobDbContext.SubMenuMaster
                              select m).ToListAsync();

            return menus;
        }

        public async Task<List<MenuDTO>> GetUserMenus(int userId)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@UserId", userId)
            };

            var result = await Task.Run(() => SQLHelper.CGetData<dynamic>("SP_MENU", param));

            string parentMenuJSONString = string.Empty;
            parentMenuJSONString = JsonConvert.SerializeObject(result.Tables[0]);
            IEnumerable<MenuMaster> parentMenu = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<MenuMaster>>(parentMenuJSONString));

            string subMenusJSONString = string.Empty;
            subMenusJSONString = JsonConvert.SerializeObject(result.Tables[1]);
            IEnumerable<SubMenuMaster> subMenus = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<SubMenuMaster>>(subMenusJSONString));

            string subChildMenusJSONString = string.Empty;
            subChildMenusJSONString = JsonConvert.SerializeObject(result.Tables[2]);
            IEnumerable<SubMenuChildMaster> subChildMenus = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<SubMenuChildMaster>>(subChildMenusJSONString));

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
    }
}

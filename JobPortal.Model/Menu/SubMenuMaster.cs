using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model.Menu
{
    public class SubMenuMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MasterMenuId { get; set; }
        public string Path { get; set; }
    }
    public class SubMenuMasterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MasterMenuId { get; set; }
        public string Path { get; set; }
        public List<SubMenuChildMaster> SubMenuChildMaster { get; set; }
    }
}

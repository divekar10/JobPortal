using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model.Menu
{
    public class SubMenuChildMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubMenuId { get; set; }
        public string Path { get; set; }
    }
}

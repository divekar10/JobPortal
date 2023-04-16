using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model.Menu
{
    public class UserMenuMapping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MasterMenuIds { get; set; }
        public string SubMenuIds { get; set; }
        public string SubChildMenuIds { get; set; }
    }
}

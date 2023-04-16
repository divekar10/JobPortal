using JobPortal.Model.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model.DTO
{
    public class MenuDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubMenuMasterDTO> subMenuMaster { get; set; }
    }
}

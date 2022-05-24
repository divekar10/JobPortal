using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model.Email
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Template { get; set; }
    }
}

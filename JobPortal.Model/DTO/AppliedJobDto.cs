using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model
{
    public class AppliedJobDto
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        
    }
}

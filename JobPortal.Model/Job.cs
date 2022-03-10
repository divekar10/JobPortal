using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Model
{
    public class Job
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify title.")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please specify description.")]
        [StringLength(1000, ErrorMessage = "Maximum 1000 characters.")]
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndAt { get; set; }
        public bool IsActive { get; set; }
    }
}

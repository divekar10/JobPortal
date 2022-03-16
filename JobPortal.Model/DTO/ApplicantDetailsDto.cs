
namespace JobPortal.Model
{
    public class ApplicantDetailsDto
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string ApplicantName { get; set; }
        public string JobTitle { get; set; }
        public int PostedBy { get; set; }

    }
}

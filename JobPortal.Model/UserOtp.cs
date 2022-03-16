using System;

namespace JobPortal.Model
{
    public class UserOtp
    {
        public int Id { get; set; }
        public int Otp { get; set; }
        public int UserId { get; set; }
        public DateTime ExpireAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

﻿using JobPortal.Model.Validation;
using System;

namespace JobPortal.Model
{
    public class Applicant
    {
        public int Id { get; set; }

        [RequiredNumber(ErrorMessage = "Please specify job.")]
        public int JobId { get; set; }
        public int AppliedBy { get; set; }
        public DateTime AppliedAt { get; set; }
        public bool IsActive { get; set; }
    }
}

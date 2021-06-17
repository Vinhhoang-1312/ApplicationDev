using StaffTrainee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffTrainee.ViewModels
{
    public class EnrollmentTraineeViewModel
    {

        public int CourseId { get; set; }
        public string UserId { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
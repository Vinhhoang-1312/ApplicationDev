using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StaffTrainee.Models
{
    public class Course
    {
    
        [Key]
        public int CourseId { get; set; }
        [Required]
     
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        public String Name { get; set; }
        [Required]

        public String Description { get; set; }
    }
}
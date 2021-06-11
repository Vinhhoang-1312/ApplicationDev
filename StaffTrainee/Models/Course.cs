using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StaffTrainee.Models
{
    public class Course
    {

        [Required]
        [Display(Name = "Course Id")]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }    // Linking Object to Category model

        [Required]
        [Display(Name = "Course Name")]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
    }
}
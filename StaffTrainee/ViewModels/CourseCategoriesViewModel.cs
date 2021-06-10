using StaffTrainee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffTrainee.ViewModel
{
    public class CourseCategoriesViewModel
    {
        public Course Course { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
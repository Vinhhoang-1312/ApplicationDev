using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StaffTrainee.Models
{
    public class UserInfo
    {
        [Key]

        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(7, 20, ErrorMessage = "Please enter Age value bigger than 7 and less than 20")]
        public int Phone { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
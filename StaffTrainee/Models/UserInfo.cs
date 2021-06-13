using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StaffTrainee.Models
{
    public class UserInfo
    {

        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [Range(1, 20, ErrorMessage = "Please enter Phone number value bigger than 0 and less than 20")]
        public int Phone { get; set; }

    }
}
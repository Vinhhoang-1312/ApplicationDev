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
        public int UserInfoId { get; set; }

        [ForeignKey("User")]



        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]

        public string Phone { get; set; }

    }
}
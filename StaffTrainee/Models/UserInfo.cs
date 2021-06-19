using Microsoft.AspNet.Identity.EntityFramework;
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

        public int UserInfoId { get; set; }


        [Required]


        public string UserId { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public ApplicationUser User { get; set; }
        [Required]

        public string FullName { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]

        public string Phone { get; set; }

    }
}

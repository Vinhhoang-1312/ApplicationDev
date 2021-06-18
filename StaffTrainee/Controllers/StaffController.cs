using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StaffTrainee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StaffTrainee.Controllers
{
    public class StaffController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;


        public StaffController()
        {

            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(
              new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }
        // GET: Trainee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetTrainees()
        {
            var users = _context.Users.ToList();
            var trainees = new List<ApplicationUser>();




            foreach (var user in users)
            {
                if (_userManager.GetRoles(user.Id)[0].Equals("Trainee"))
                {
                    trainees.Add(user);
                }

            }

            return View(trainees);
        }





        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult DeleteTrainees(string id)
        {
            var AccountInDB = _context.Users.SingleOrDefault(p => p.Id == id);

            if (AccountInDB == null)
            {
                return HttpNotFound();
            }

            _context.Users.Remove(AccountInDB);
            _context.SaveChanges();
            return RedirectToAction("GetTrainees", "Staff");
        }


        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult EditTrainees(string id)
        {
            var AccountInDB = _context.Users.SingleOrDefault(p => p.Id == id);
            if (AccountInDB == null)
            {
                return HttpNotFound();
            }
            return View(AccountInDB);
        }

        //EDIT
        [HttpPost]
        [Authorize(Roles = "Staff")]
        public ActionResult EditTrainees(ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var UsernameIsExist = _context.Users.
                                  Any(p => p.Email.Contains(user.Email));

            if (UsernameIsExist)
            {
                ModelState.AddModelError("Email", "Account already existed");
                return View();
            }

            var AccountInDB = _context.Users.SingleOrDefault(p => p.Id == user.Id);

            if (AccountInDB == null)
            {
                return HttpNotFound();
            }

            AccountInDB.UserName = user.UserName;
            AccountInDB.PhoneNumber = user.PhoneNumber;


            _context.SaveChanges();
            return RedirectToAction("GetTrainees", "Staff");
        }






        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult ChangePassTrainees(string id)
        {
            var AccountInDB = _context.Users.SingleOrDefault(p => p.Id == id);

            if (AccountInDB == null)
            {
                return HttpNotFound();
            }


            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            userId = AccountInDB.Id;
            if (userId != null)
            {
                //userManager           bằng quản lý người dùng mới,              mang dữ liệu mới 
                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                userManager.RemovePassword(userId);
                String newPassword = "Password1@";
                userManager.AddPassword(userId, newPassword);
            }
            _context.SaveChanges();
            return RedirectToAction("GetTrainees", "Staff");
        }
        //Khai báo biến var userId thuộc Curent.User.Identity và truy cập vào trường Id thông qua GetUserId   







        //[Authorize(Roles = "Staff")]
        //[HttpGet]
        //public ActionResult GetTrainersInfo()
        //{
        //    if (User.IsInRole("Staff"))
        //    {
        //        var viewTrainer = _context.UserInfos.Include(a => a.User).ToList();
        //        return View(viewTrainer);
        //    }
        //    if (User.IsInRole("Trainer"))
        //    {
        //        var trainerId = User.Identity.GetUserId();
        //        var trainerVM = _context.TrainerUsers.Where(te => te.TrainerID == trainerId).ToList();
        //        return View(trainerVM);
        //    }
        //    return View("Index");
        //}
    }


}
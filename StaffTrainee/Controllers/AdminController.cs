using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StaffTrainee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StaffTrainee.Controllers
{
    public class AdminController : Controller

    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;


        public AdminController()
        {

            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(
              new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult GetStaffs()
        {
            var users = _context.Users.ToList();
            var staffs = new List<ApplicationUser>();

            foreach (var user in users)
            {
                if (_userManager.GetRoles(user.Id)[0].Equals("Staff"))
                {
                    staffs.Add(user);
                }

            }

            return View(staffs);

        }

        [HttpGet]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult GetTrainers()
        {
            var users = _context.Users.ToList();
            var trainers = new List<ApplicationUser>();
            var userId = User.Identity.GetUserId();
            var userInfo = _context.UserInfos.SingleOrDefault(u => u.UserId.Equals(userId));
            foreach (var user in users)
            {
                if (_userManager.GetRoles(user.Id)[0].Equals("Trainer"))
                {
                    trainers.Add(user);
                }

            }

            return View(trainers);

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteStaffs(string id)
        {
            var AccountInDB = _context.Users.SingleOrDefault(p => p.Id == id);

            if (AccountInDB == null)
            {
                return HttpNotFound();
            }

            _context.Users.Remove(AccountInDB);
            _context.SaveChanges();
            return RedirectToAction("GetStaffs", "Admin");
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult EditStaffs(string id)
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
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult EditStaffs(ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var UsernameIsExist = _context.Users.
                                  Any(p => p.UserName.Contains(user.UserName));

            if (UsernameIsExist)
            {
                ModelState.AddModelError("UserName", "Account already existed");
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
            return RedirectToAction("GetStaffs", "Admin");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult ChangePassStaffs(string id)
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
            return RedirectToAction("GetStaffs", "Admin");
        }
        //Khai báo biến var userId thuộc Curent.User.Identity và truy cập vào trường Id thông qua GetUserId   

        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult EditTrainers(string id)
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
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult EditTrainers(ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var UsernameIsExist = _context.Users.
                                  Any(p => p.UserName.Contains(user.UserName));

            if (UsernameIsExist)
            {
                ModelState.AddModelError("UserName", "Account already existed");
                return View();
            }

            var AccountInDB = _context.Users.SingleOrDefault(p => p.Id == user.Id);

            if (AccountInDB == null)
            {
                return HttpNotFound();
            }

            AccountInDB.UserName = user.UserName;
            AccountInDB.PhoneNumber = user.PhoneNumber;
            //var userInfo = new AccountController
            //{
            //    FullName = account.FullName,
            //    Phone = userInfo.Phone,


            //};
            //_context.UserInfos.Add(userInfo);



            _context.SaveChanges();
            return RedirectToAction("GetTrainers", "Admin");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult ChangePassTrainers(string id)
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
            return RedirectToAction("GetTrainers", "Admin");
        }
        //Khai báo biến var userId thuộc Curent.User.Identity và truy cập vào trường Id thông qua GetUserId   




        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteTrainers(string id)
        {
            var AccountInDB = _context.Users.SingleOrDefault(p => p.Id == id);

            if (AccountInDB == null)
            {
                return HttpNotFound();
            }

            _context.Users.Remove(AccountInDB);
            _context.SaveChanges();
            return RedirectToAction("GetTrainers", "Admin");
        }
    }
}
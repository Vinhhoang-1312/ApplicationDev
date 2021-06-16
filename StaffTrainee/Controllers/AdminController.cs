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





        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            //var userId = User.Identity.GetUserId();

            var userInfoes = _context.Users
                // .Where(t => t.UserId.Equals(userId))
                .SingleOrDefault(s => s.UserName == id);

            _context.Users.Remove(userInfoes);
            _context.SaveChanges();
            //var userInDb = _context.Users
            //    .Where(t => t.Id.Equals(userId))
            //    .SingleOrDefault(s => s.Id == id);

            //_context.Users.Remove(userInDb);
            //_context.SaveChanges();

            return RedirectToAction("GetStaffs");
        }



        ////        [HttpGet]
        ////        public ActionResult GetTrainer()
        ////        {
        ////            var users = _context.Users.ToList();
        ////            var managers = new List<ApplicationUser>();

        ////            foreach (var user in users)
        ////            {
        ////                if (_userManager.GetRoles(user.Id)[0].Equals("Staff""Trainer"))
        ////                {
        ////                    managers.Add(user);
        ////                }
        ////            }

        ////            return View(managers);
        ////        }

    }
}
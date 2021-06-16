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




        //DELETE ACCOUNT
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            var AccountInDB = _context.Users.SingleOrDefault(p => p.Id == id);

            if (AccountInDB == null)
            {
                return HttpNotFound();
            }

            _context.Users.Remove(AccountInDB);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
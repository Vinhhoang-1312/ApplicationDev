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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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









    }


}
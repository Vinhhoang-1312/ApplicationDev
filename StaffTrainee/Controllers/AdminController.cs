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
            return RedirectToAction("GetStaffs", "Admin");
        }
        //            var userinfoindb = _userManager.user.Where(a => a.EngineId == id).ToList();

        //foreach (var vp in vps)
        //    db.VehicleProperties.Remove(vp);
        //db.SaveChanges();

        ////DELETE ACCOUNT
        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public ActionResult Delete(string id)
        //{
        //    var AccountInDB = _context.Users.Where(p => p.Id.Equals(id)).ToList();
        //    UserInfo userInfo = _context.UserInfos.Find(id);
        //    if (AccountInDB == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    //Quarto quarto = db.Quarto.Find(id);

        //    _context.Users.Remove(userInfo);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}



        //for (int i = 0; i < restaurant.Types.Count; i++)
        //{
        //    var type = restaurant.Types[i];
        //    db.DeleteObject(type);
        //    restaurant.Types.Remove(type);
        //}
        //db.SaveChanges();

        //db.DeleteObject(restaurant);
        //db.SaveChanges();





        //{
        //    List<Reserva> Reservas = db.Reserva.Where(r => r.ID_Quarto == id).ToList();
        //    db.Reserva.RemoveRange(Reservas);

        //    Quarto quarto = db.Quarto.Find(id);
        //    db.Quarto.Remove(quarto);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}




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
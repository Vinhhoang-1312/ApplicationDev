using StaffTrainee.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace StaffTrainee.Controllers
{
    [Authorize]
    public class UserInfosController : Controller
    {
        private ApplicationDbContext _context;
        public UserInfosController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: UserInfos
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var userInfo = _context.UserInfos.SingleOrDefault(u => u.UserId.Equals(userId));

            if (userInfo == null) return HttpNotFound();
            return View(userInfo);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            var userInfo = _context.UserInfos.SingleOrDefault(u => u.UserId.Equals(userId));

            if (userInfo == null) return HttpNotFound();
            return View(userInfo);
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(userInfo);
            }

            var userInfoInDb = _context.UserInfos
              .SingleOrDefault(u => u.UserId.Equals(userInfo.UserId));

            if (userInfoInDb == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            userInfoInDb.FullName = userInfo.FullName;
            userInfoInDb.Phone = userInfo.Phone;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    //var categoriesId = User.Identity.GetUserId();

        //    if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    var categoryinDb = _context.Categories
        //        //.Where(c => c.Id.Equals(userId))
        //        .SingleOrDefault(c => c.Id == id);

        //    if (categoryinDb == null) return HttpNotFound();

        //    _context.Categories.Remove(categoryinDb);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}
        //[Authorize(Roles = "Admin")]
        //public ActionResult Delete(string id)
        //{

        //    var userInfo = _context.UserInfos

        //        .SingleOrDefault(s => s.UserId == id);

        //    _context.Users.Remove(userInfo);
        //    _context.SaveChanges();


        //    return RedirectToAction("GetStaffs");
        //}






        //[HttpGet]

        //public ActionResult Delete(string id)
        //{
        //    //var userId = User.Identity.GetUserId();

        //    if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    var userInfo = _context.UserInfos
        //        //.Where(t => t.UserId.Equals(userId))
        //        .SingleOrDefault(t => t.UserId.Equals(id));

        //    if (userInfo == null) return HttpNotFound();

        //    _context.UserInfos.Remove(userInfo);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}










    }
}
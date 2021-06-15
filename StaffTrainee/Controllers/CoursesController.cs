using Microsoft.Ajax.Utilities;
using StaffTrainee.Models;
using StaffTrainee.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StaffTrainee.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ApplicationDbContext _context;

        private UserManager<ApplicationUser> _userManager;
        public CoursesController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(
       new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }
        // GET: Course

        [HttpGet]
        [Authorize(Roles = "Staff, Trainee")]
        public ActionResult Index(string searchString)
        {


            //var userId = User.Identity.GetUserId();

            var coursesInDb = _context.Courses
                .Include(t => t.Category)
                .ToList();
            //.Where(t => t.UserId.Equals(userId))








            if (!searchString.IsNullOrWhiteSpace())
            {
                coursesInDb = _context.Courses.Where(c => c.Name.Contains(searchString)).ToList();
            }

            return View(coursesInDb);
        }
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult Create()
        {
            var viewModel = new CourseCategoriesViewModel()
            {
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public ActionResult Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                var viewModels = new CourseCategoriesViewModel()
                {
                    Course = course,
                    Categories = _context.Categories.ToList()
                };

                return View(viewModels);
            }

            //var userId = User.Identity.GetUserId();
            var newCourse = new Course()
            {
                Description = course.Description,
                CategoryId = course.CategoryId,
                Name = course.Name,

            };

            _context.Courses.Add(newCourse);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //var userId = User.Identity.GetUserId();

            var courseInDb = _context.Courses
                //.Where(t => t.UserId.Equals(userId))
                .SingleOrDefault(t => t.Id == id);

            if (courseInDb == null) return HttpNotFound();

            var viewModels = new CourseCategoriesViewModel
            {
                Course = courseInDb,
                Categories = _context.Categories.ToList()
            };

            return View(viewModels);
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public ActionResult Edit(Course course)
        {
            //var userId = User.Identity.GetUserId();
            var courseInDb = _context.Courses
                //.Where(t => t.UserId.Equals(userId))
                .SingleOrDefault(t => t.Id == course.Id);

            if (!ModelState.IsValid)
            {
                var viewModels = new CourseCategoriesViewModel
                {
                    Course = course,
                    Categories = _context.Categories.ToList()
                };
                return View(viewModels);
            }

            if (courseInDb == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            courseInDb.CategoryId = course.CategoryId;
            courseInDb.Name = course.Name;
            courseInDb.Description = course.Description;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult Delete(int? id)
        {
            //var userId = User.Identity.GetUserId();

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var courseInDb = _context.Courses
                //.Where(t => t.UserId.Equals(userId))
                .SingleOrDefault(t => t.Id == id);

            if (courseInDb == null) return HttpNotFound();

            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //ENROLLMENT


        [Authorize(Roles = "Staff")]

        [HttpGet]
        public ActionResult Details(int id)
        {
            var users = _context.EnrollmentTrainees
              .Where(t => t.CourseId == id)
              .Select(t => t.User)
              .ToList();

            ViewBag.CourseId = id;

            return View(users);
        }





        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        //ENROLLMENT TRAINEES

        [Authorize(Roles = "Staff")]
        // GET: enrollment
        [HttpGet]
        public ActionResult IndexAssignTrainee()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }
        [Authorize(Roles = "Staff")]

        [HttpGet]
        public ActionResult NumberofTrainee(int id)
        {
            var users = _context.EnrollmentTrainees
              .Where(t => t.CourseId == id)
              .Select(t => t.User)
              .ToList();

            ViewBag.CourseId = id;

            return View(users);
        }
        [Authorize(Roles = "Staff")]

        [HttpGet]
        public ActionResult AssignTrainee(int id)
        {
            var users = _context.Users.ToList();

            var usersInCourse = _context.EnrollmentTrainees
              .Where(t => t.CourseId == id)
              .Select(t => t.User)
              .ToList();

            var viewmodel = new EnrollmentTraineeViewModel();

            if (usersInCourse == null)
            {
                viewmodel.CourseId = id;
                viewmodel.Users = users;


                return View(viewmodel);
            }

            var usersWithUserRole = new List<ApplicationUser>();

            foreach (var user in users)
            {
                if (_userManager.GetRoles(user.Id)[0].Equals("Trainee")
                  && !usersInCourse.Contains(user)
                  )
                {
                    usersWithUserRole.Add(user);
                }
            }

            var viewModel = new EnrollmentTraineeViewModel
            {
                CourseId = id,
                Users = usersWithUserRole
            };

            return View(viewModel);
        }
        [Authorize(Roles = "Staff")]

        [HttpPost]
        public ActionResult AssignTrainee(EnrollmentTrainee model)
        {
            var EnrollmentTrainee = new EnrollmentTrainee
            {
                CourseId = model.CourseId,
                UserId = model.UserId
            };

            _context.EnrollmentTrainees.Add(EnrollmentTrainee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Staff")]

        [HttpGet]
        public ActionResult RemoveAssignTrainee(int id, string userId)
        {
            var EnrollmentTrainee = _context.EnrollmentTrainees
              .SingleOrDefault(t => t.CourseId == id && t.UserId == userId);

            if (EnrollmentTrainee == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _context.EnrollmentTrainees.Remove(EnrollmentTrainee);
            _context.SaveChanges();

            return RedirectToAction("DetailsAssignTrainee", new { id = id });
        }

        [Authorize(Roles = "Trainee")]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var courses = _context.EnrollmentTrainees
              .Where(t => t.UserId.Equals(userId))
              .Select(t => t.Course)
              .ToList();

            return View(courses);
        }
    }




}

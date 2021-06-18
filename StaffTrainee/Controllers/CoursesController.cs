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

            var courseInDb = _context.Courses
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

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var courseInDb = _context.Courses
                .SingleOrDefault(t => t.Id == id);

            if (courseInDb == null) return HttpNotFound();

            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();

            return RedirectToAction("Index");
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
              .Where(a => a.CourseId == id)
              .Select(a => a.User)
              .ToList();

            var viewmodel = new EnrollmentTraineeViewModel();

            if (usersInCourse == null)
            {


                viewmodel.CourseId = id;
                viewmodel.Users = users;


                return View(viewmodel);
            }


            var usersWithUserRole = new List<ApplicationUser>();

            foreach (var trainee in users)
            {
                if (_userManager.GetRoles(trainee.Id)[0].Equals("Trainee")
                  && !usersInCourse.Contains(trainee)
                  )
                {
                    usersWithUserRole.Add(trainee);
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
            var enrollmentTrainee = new EnrollmentTrainee
            {
                CourseId = model.CourseId,
                UserId = model.UserId
            };
            //    if (!ModelState.IsValid)
            //    {
            //        EnrollmentTrainee viewmodel = new EnrollmentTrainee()
            //        {
            //            CourseId = model.CourseId,
            //            UserId = model.UserId
            //        };
            //        return View(viewmodel);
            //    }
            try
            {
                if (ModelState.IsValid)
                {

                    _context.EnrollmentTrainees.Add(enrollmentTrainee);
                    _context.SaveChanges();

                    return RedirectToAction("IndexAssignTrainee");
                }
            }
            catch

            {

                return View("~/Views/Error/Error404.cshtml");
            }

            return RedirectToAction("IndexAssignTrainee");

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

            return RedirectToAction("NumberofTrainee", new { id = id });
        }

        [Authorize(Roles = "Trainee")]
        public ActionResult MineTrainee()
        {
            var userId = User.Identity.GetUserId();

            var courses = _context.EnrollmentTrainees
              .Where(t => t.UserId.Equals(userId))
              .Select(t => t.Course)
              .ToList();

            return View(courses);
        }





        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        /// //////////////////////////////////////////////////////// /// //////////////////////////////////////////////////////// /// 
        //ENROLLMENT TRAINER

        [Authorize(Roles = "Staff")]
        // GET: enrollment
        [HttpGet]
        public ActionResult IndexAssignTrainer()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }
        [Authorize(Roles = "Staff")]

        [HttpGet]
        public ActionResult NumberofTrainer(int id)
        {
            var users = _context.EnrollmentTrainers
              .Where(t => t.CourseId == id)
              .Select(t => t.User)
              .ToList();

            ViewBag.CourseId = id;

            return View(users);
        }
        [Authorize(Roles = "Staff")]

        [HttpGet]
        public ActionResult AssignTrainer(int id)
        {
            var users = _context.Users.ToList();

            var usersInCourse = _context.EnrollmentTrainers
              .Where(a => a.CourseId == id)
              .Select(a => a.User)
              .ToList();

            var viewmodel = new EnrollmentTrainerViewModel();

            if (usersInCourse == null)
            {
                viewmodel.CourseId = id;
                viewmodel.Users = users;


                return View(viewmodel);
            }

            var usersWithUserRole = new List<ApplicationUser>();

            foreach (var trainer in users)
            {
                if (_userManager.GetRoles(trainer.Id)[0].Equals("Trainer")
                  && !usersInCourse.Contains(trainer)
                  )
                {
                    usersWithUserRole.Add(trainer);
                }
            }

            var viewModel = new EnrollmentTrainerViewModel
            {
                CourseId = id,
                Users = usersWithUserRole
            };

            return View(viewModel);
        }
        [Authorize(Roles = "Staff")]

        [HttpPost]
        public ActionResult AssignTrainer(EnrollmentTrainer model)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError("", "Please Enter User");
            //    return View(model);
            //}
            var enrollmentTrainer = new EnrollmentTrainer
            {
                CourseId = model.CourseId,
                UserId = model.UserId
            };

            _context.EnrollmentTrainers.Add(enrollmentTrainer);
            _context.SaveChanges();

            return RedirectToAction("IndexAssignTrainer");
        }
        [Authorize(Roles = "Staff")]

        [HttpGet]
        public ActionResult RemoveAssignTrainer(int id, string userId)
        {
            var EnrollmentTrainer = _context.EnrollmentTrainers
              .SingleOrDefault(t => t.CourseId == id && t.UserId == userId);

            if (EnrollmentTrainer == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _context.EnrollmentTrainers.Remove(EnrollmentTrainer);
            _context.SaveChanges();

            return RedirectToAction("NumberofTrainer", new { id = id });
        }

        [Authorize(Roles = "Trainer")]
        public ActionResult MineTrainer()
        {
            var userId = User.Identity.GetUserId();

            var courses = _context.EnrollmentTrainers
              .Where(t => t.UserId.Equals(userId))
              .Select(t => t.Course)
              .ToList();

            return View(courses);
        }






    }




}

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

namespace StaffTrainee.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Course

        [HttpGet]
        public ActionResult Index(string searchString)
        {


            //var userId = User.Identity.GetUserId();

            var coursesInDb = _context.Courses
                .Include(t => t.Category)
                .ToList();
            //.Where(t => t.UserId.Equals(userId))

            //var todoesInDb = _context.Todoes
            //    .Include(t => t.Category)
            //    .ToList();


            if (!searchString.IsNullOrWhiteSpace())
            {
                coursesInDb = _context.Courses.Where(t => t.Description.Contains(searchString)).ToList();
            }

            return View(coursesInDb);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CourseCategoriesViewModel()
            {
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
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
    }
}
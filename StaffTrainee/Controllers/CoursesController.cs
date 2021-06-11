using Microsoft.Ajax.Utilities;
using StaffTrainee.Models;
using StaffTrainee.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

			var course = _context.Courses
				//.Include(t => t.Category)
				//.Where(t => t.UserId.Equals(userId))
				.ToList();



			if (!searchString.IsNullOrWhiteSpace())
			{
				course = _context.Courses.Where(t => t.Description.Contains(searchString)).ToList();
			}

			return View(course);
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
				var viewModel = new CourseCategoriesViewModel()
				{
					Course = course,
					Categories = _context.Categories.ToList()
				};

				return View(viewModel);
			}

			//var userId = User.Identity.GetUserId();
			var newTodo = new Course()
			{
				Description = course.Description,
				CategoryId = course.CategoryId,
				Name = course.Name,
				
			};

			_context.Courses.Add(newTodo);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}


	}
}
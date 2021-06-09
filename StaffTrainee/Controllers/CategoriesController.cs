using StaffTrainee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StaffTrainee.Controllers
{
    public class CategoriesController : Controller
    {


        private List<Category> _categories = new List<Category>();
		//
		public CategoriesController()
		{
			_categories.Add(new Category()
			{
				Id = 1,
				Name = "Tu Nhien",
				Description = "Toan - Ly - Hoa",
				
			});

			_categories.Add(new Category()
			{
				Id = 2,
				Name = "Xa  Hoi",
				Description = "Van - Su - Dia",

			});

		}


        // GET: Categories
        public ActionResult Index()
        {
            return View(_categories);
        }
		public ActionResult Create()
		{
			return View();
		}

		public ActionResult Edit()
		{
			return View();
		}




	}
}
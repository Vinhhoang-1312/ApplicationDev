using Microsoft.Ajax.Utilities;
using StaffTrainee.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace StaffTrainee.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;
        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Categories

        public ActionResult Index(string searchString)
        {
            var categoriesInDb = _context.Categories.ToList();



            if (!searchString.IsNullOrWhiteSpace())
            {
                categoriesInDb = _context.Categories.Where(c => c.Name.Contains(searchString)).ToList();
            }

            return View(categoriesInDb);
        }

        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public ActionResult Create(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_context.Categories.Any(c => c.Name.Contains(model.Name)))
            {
                ModelState.AddModelError("", "Category Name alreay exists");
                return View(model);
            }
            var category = new Category
            {
                Name = model.Name,
                Description = model.Description
            };

            _context.Categories.Add(category);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult Delete(int? id)
        {
            //var categoriesId = User.Identity.GetUserId();

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var categoryinDb = _context.Categories
                //.Where(c => c.Id.Equals(userId))
                .SingleOrDefault(c => c.Id == id);

            if (categoryinDb == null) return HttpNotFound();

            _context.Categories.Remove(categoryinDb);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (categoryInDb == null) return HttpNotFound();

            return View(categoryInDb);
        }
        [HttpPost]
        [Authorize(Roles = "Staff")]
        public ActionResult Edit(Category category)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == category.Id);

            if (!ModelState.IsValid)
            {
                return View(category);
            }
            if (_context.Categories.Any(c => c.Name.Contains(category.Name)))
            {
                ModelState.AddModelError("", "Category Name alreay exists");
                return View(category);
            }


            categoryInDb.Description = category.Description;
            categoryInDb.Name = category.Name;


            _context.SaveChanges();

            return RedirectToAction("Index");


        }
    }
}

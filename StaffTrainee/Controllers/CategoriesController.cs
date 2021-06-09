using StaffTrainee.Models;
using System.Linq;
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
      
        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
       
        public ActionResult Create(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = new Category
            {
                Name = model.Name,
                Description = model.Description
            };

            _context.Categories.Add(category);
            try
            {
                _context.SaveChanges();

            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                ModelState.AddModelError("", "Category Name alreay exists");
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}
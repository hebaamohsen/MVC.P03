using Company.Data.Models;
using Company.Servise.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace hebamohsen_MVC_P03.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServise _departmentServise;

        public DepartmentController(IDepartmentServise departmentServise)
        {
            _departmentServise = departmentServise;
        }
        public IActionResult Index()
        {
            var departments = _departmentServise.GetAll();
            return View(departments);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
           try
            {
                if (ModelState.IsValid)
                {
                    _departmentServise.Add(department);
                    return RedirectToAction("Index");

                }
                ModelState.AddModelError("DepartmentError", "ValidationError");
                return View(department);
            }
            catch(Exception ex) 
            {
                ModelState.AddModelError("DepartmentError", ex.Message);
                return View(department);
            }
        }

        public IActionResult Details(int? id,string viewName ="Details")
        {
            var department = _departmentServise.GetById(id);
            if(department is null)
                return RedirectToAction("NotFoundPage",null,"Home");
            return View(viewName,department);

        }

        public IActionResult Update(int? id)
        {
            return Details(id, "Update") ;
        }

        [HttpPost]
        public IActionResult Update(int? id, Department department)
        {
            if(department.Id != id.Value)
                return RedirectToAction("NotFoundPage", null, "Home");
            _departmentServise.Update(department);

            return RedirectToAction(nameof(Index));
        }

       
        public IActionResult Delete(int id)
        {
            var department = _departmentServise.GetById(id);

            if (department is null)
                return RedirectToAction("NotFoundPage", null, "Home");

            _departmentServise.Delete(department);

            return RedirectToAction(nameof(Index));
        }

    }
}

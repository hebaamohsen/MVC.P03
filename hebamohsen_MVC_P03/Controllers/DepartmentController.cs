using Company.Data.Models;
using Company.Servise.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hebamohsen_MVC_P03.Controllers
{
    [Authorize]
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
        public IActionResult Create(DepartmentDto departmentDto)
        {
           try
            {
                if (ModelState.IsValid)
                {
                    _departmentServise.Add(departmentDto);
                    return RedirectToAction("Index");

                }
                ModelState.AddModelError("DepartmentError", "ValidationError");
                return View(departmentDto);
            }
            catch(Exception ex) 
            {
                ModelState.AddModelError("DepartmentError", ex.Message);
                return View(departmentDto);
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
        public IActionResult Update(int? id, DepartmentDto departmentDto)
        {
            if(departmentDto.Id != id.Value)
                return RedirectToAction("NotFoundPage", null, "Home");
            _departmentServise.Update(departmentDto);

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

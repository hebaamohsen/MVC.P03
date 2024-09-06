using Company.Data.Models;
using Company.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace hebamohsen_MVC_P03.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
           var employees = _unitOfWork.EmployeeRepository.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {

            return View(new Employee());
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            ModelState["Department"].ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Add(employee);
                _unitOfWork.Complete(); 

                return RedirectToAction("Index");
            }
            return View(employee);
        }
    }
}

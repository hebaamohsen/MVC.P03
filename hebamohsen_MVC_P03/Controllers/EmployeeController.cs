using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Servise.Interfaces;
using Company.Servise.Interfaces.Employee;
using Company.Servise.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace hebamohsen_MVC_P03.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServise _employeeServise;
        private readonly IDepartmentServise _departmentServise;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IEmployeeServise employeeServise, IDepartmentServise departmentServise , IUnitOfWork unitOfWork)
        {
            _employeeServise = employeeServise;
            _departmentServise = departmentServise;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(string searchInput)
        {
            //ViewBag.Message = "Hello From Employee Index(ViewBag)";
            IEnumerable<EmployeeDto> employees = new List<EmployeeDto>();
            if (string.IsNullOrEmpty(searchInput))
                employees = _employeeServise.GetAll();
            else
                employees = _employeeServise.GetEmployeeByName(searchInput);
                return View(employees);
           
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _departmentServise.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeServise.Add(employee);
                    return RedirectToAction(nameof(Index));

                }
				ViewBag.Departments = _departmentServise.GetAll();
				return View(employee);
            }
            catch (Exception ex)
            {
				ViewBag.Departments = _departmentServise.GetAll();

				return View(employee);
            }
        }
    }
}

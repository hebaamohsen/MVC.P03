using Company.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace hebamohsen_MVC_P03.Controllers
{
    public class DepartmentController1 : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController1(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
    }
}

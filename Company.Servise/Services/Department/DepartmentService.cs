using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Servise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Servise.Services
{
    public class DepartmentService : IDepartmentServise
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Department department)
        {
            var mappedDepartment = new Department
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = DateTime.Now
            };
            _unitOfWork.DepartmentRepository.Add(mappedDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(Department department)
        {
            _unitOfWork.DepartmentRepository.Delete(department);
            _unitOfWork.Complete();
        }

        public IEnumerable<Department> GetAll()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments;
                
        }

        public Department GetById(int? id)
        {
            if (id is null)
                return null;
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department is null)
                return null;
            return department;
            
        }

        public void Update(Department department)
        {
            _unitOfWork.DepartmentRepository.Update(department);
            _unitOfWork.Complete();

        }


    }
}

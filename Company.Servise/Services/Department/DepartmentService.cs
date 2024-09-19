using AutoMapper;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Servise.Interfaces;
using Company.Servise.Interfaces.Employee;
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
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(DepartmentDto departmentDto)
        {
            Department department = _mapper.Map<Department>(departmentDto);
            _unitOfWork.DepartmentRepository.Add(department);
            _unitOfWork.Complete();
        }

        public void Delete(DepartmentDto departmentDto)
        {
            Department department = _mapper.Map<Department>(departmentDto);
            _unitOfWork.DepartmentRepository.Delete(department);
            _unitOfWork.Complete();
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            IEnumerable<DepartmentDto> mappedDepartment = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return mappedDepartment;
                
        }

        public DepartmentDto GetById(int? id)
        {
            if (id is null)
                return null;
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department is null)
                return null;

            DepartmentDto departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
            
        }

        public void Update(DepartmentDto departmentDto)
        {
			Department department = _mapper.Map<Department>(departmentDto);
			_unitOfWork.DepartmentRepository.Update(department);
			_unitOfWork.Complete();

        }


    }
}

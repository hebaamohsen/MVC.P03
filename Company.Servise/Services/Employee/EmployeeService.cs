using AutoMapper;
using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Servise.Helper;
using Company.Servise.Interfaces;
using Company.Servise.Interfaces.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Servise.Services
{
    public class EmployeeService : IEmployeeServise
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Add(EmployeeDto employeeDto)
        {
            employeeDto.ImageUrl = DocumentSettings.UploadFile(employeeDto.Image, "Images");
          Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(EmployeeDto employeeDto)

        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
           IEnumerable<EmployeeDto> mappedemployee = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return mappedemployee;
        }

        public EmployeeDto GetById(int? id)
        {

            var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);

            if (id is null)
                return null;
            if (employee is null)
                return null;

            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
                    
        }

        public IEnumerable<EmployeeDto> GetEmployeeByName(string name)
        {
           var employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(name);
            IEnumerable<EmployeeDto> mappedemployee = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return mappedemployee;

        }
           
        

        public void Update(EmployeeDto employee)
        {
          
            _unitOfWork.Complete();
        }
    }
}

using AutoMapper;
using Company.Data.Models;
using Company.Servise.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Servise.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile() 
        {
            CreateMap<Department,DepartmentDto>().ReverseMap();
        }
    }
}

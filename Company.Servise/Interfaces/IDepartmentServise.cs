using Company.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Servise.Interfaces
{
    public interface IDepartmentServise
    {
        DepartmentDto GetById(int? id);
        IEnumerable<DepartmentDto> GetAll();
        void Add(DepartmentDto entity);
        void Update(DepartmentDto entity);
        void Delete(DepartmentDto entity);
    }
}

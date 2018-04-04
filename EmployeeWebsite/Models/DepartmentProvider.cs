using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebsite.Models
{
    public class DepartmentProvider
    {
        private readonly AppDBContext context;

        public DepartmentProvider(AppDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<Department> AllDepartments()
            => context.Departments.Include(d => d.Employees);

    }
}

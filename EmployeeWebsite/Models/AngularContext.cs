using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebsite.Models
{
    public class AngularContext
    {

        public Employee Employee { get; set; }
        public IEnumerable<Employee> Employees { get; set; }

        public Department Department { get; set; }
        public IEnumerable<Department> Departments { get; set; }

        
    }
}

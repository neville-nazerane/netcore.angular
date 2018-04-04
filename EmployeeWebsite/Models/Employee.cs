using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebsite.Models
{
    public class Employee
    {

        public int ID { get; set; }

        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [MaxLength(90)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string JobTitle { get; set; }

        public int DepartmentID { get; set; }
        public Department Department { get; set; }

    }
}

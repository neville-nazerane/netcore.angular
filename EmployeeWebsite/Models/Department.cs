using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebsite.Models
{
    public class Department
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public List<Employee> Employees { get; set; }

    }
}

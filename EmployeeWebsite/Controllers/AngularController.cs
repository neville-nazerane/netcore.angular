using EmployeeWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebsite.Controllers
{
    public class AngularController : Controller
    {
        private readonly AppDBContext context;

        public AngularController(AppDBContext context)
        {
            this.context = context;
        }

        public IActionResult Index() => View();


        [HttpPost]
        public IActionResult AddDepartment([FromBody]Department department)
        {
            if (ModelState.IsValid)
            {
                context.AddDirect(department);
                return Ok(department);
            }
            var result = PartialView(department);
            result.StatusCode = 400;
            return result;
        }

        [HttpGet]
        public IActionResult AddEmployee(int id)
            => PartialView(new Employee { DepartmentID = id });

        [HttpPost]
        public IActionResult AddEmployee([FromBody]Employee employee)
        {
            if (ModelState.IsValid)
            {
                context.AddDirect(employee);
                return Ok(employee);
            }
            var result = PartialView(employee);
            result.StatusCode = 400;
            return result;
        }

    }
}

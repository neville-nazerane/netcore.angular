using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebsite.Models
{
    public class AppDBContext : DbContext
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public int AddDirect<TEntity>(TEntity entity)
            where TEntity : class
        {
            Add(entity);
            return SaveChanges();
        }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
    }
}

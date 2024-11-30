using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Code2.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext():base("Employee4") 
        { }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProjectAssignment> ProjectAssignments { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Code2.Models
{
    public class Employee
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int Age { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Picture { get; set; }
        public bool EmployeeActive { get; set; }
        public IList<ProjectAssignment> ProjectAssignments { get; set; }
    }
}
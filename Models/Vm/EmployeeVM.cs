using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Code2.Models.Vm
{
    public class EmployeeVM
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int Age { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Picture { get; set; }
        public HttpPostedFileBase PictureFile { get; set; }
        public bool EmployeeActive { get; set; }
        public ICollection<ProjectAssignment> ProjectAssignments { get;set; }
    }
}
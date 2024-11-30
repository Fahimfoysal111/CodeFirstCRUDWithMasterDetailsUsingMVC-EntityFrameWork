using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Code2.Models;
using Code2.Models.Vm;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace Code2.Controllers
{
    public class EmployeeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Employee
        public ActionResult Index()
        {
            var employee = db.Employees.Include(x => x.ProjectAssignments.Select(c => c.Project)).OrderByDescending(b => b.EmployeeId).ToList();
            return View(employee);
        }
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeVM employeeVM, int[] projectid)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    EmployeeName = employeeVM.EmployeeName,
                    Age = employeeVM.Age,
                    DateOfJoining = employeeVM.DateOfJoining,
                    EmployeeActive = employeeVM.EmployeeActive,


                };
                HttpPostedFileBase file = employeeVM.PictureFile;
                if (file != null)
                {
                    var filepat = Path.Combine("/Images/", DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filepat));
                    employee.Picture = filepat;


                }
                foreach (var a in projectid)
                {
                    var project = db.Projects.FirstOrDefault(b => b.ProjectId == a);
                    ProjectAssignment projectAssignment = new ProjectAssignment()
                    {
                        Employee = employee,
                        ProjectId = project.ProjectId,

                    };

                    db.ProjectAssignments.Add(projectAssignment);

                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeVM);
        }
        public ActionResult AddNewProject(int? id)
        {
            ViewBag.project = new SelectList(db.Projects, "ProjectId", "ProjectName", id ?? 0);
            return PartialView("_addnewproject");


        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var emplo = db.Employees.FirstOrDefault(c => c.EmployeeId == id);
                var assign = db.ProjectAssignments.Where(c => c.EmployeeId == id);
                foreach (var pro in assign)
                {
                    db.ProjectAssignments.Remove(pro);

                }
                db.Employees.Remove(emplo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var emplo = db.Employees.Find(id);
                var pro = db.ProjectAssignments.Where(x => x.EmployeeId == emplo.EmployeeId).ToList();
                if (emplo != null)
                {
                    var vm = new EmployeeVM()
                    {
                        EmployeeId = emplo.EmployeeId,
                        EmployeeName = emplo.EmployeeName,
                        EmployeeActive = emplo.EmployeeActive,
                        Age = emplo.Age,
                        DateOfJoining = emplo.DateOfJoining,
                        Picture = emplo.Picture,
                        ProjectAssignments = pro


                    };
                    return View (vm);
                
                
                }

            
            
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(EmployeeVM employeeVM, int[] projectid)
        {
            if (ModelState.IsValid)
            {
                var eplo = db.Employees.Find(employeeVM.EmployeeId);
                eplo.EmployeeName = employeeVM.EmployeeName;
                eplo.Age = employeeVM.Age;
                eplo.DateOfJoining  = employeeVM.DateOfJoining;
                eplo.EmployeeActive = employeeVM.EmployeeActive;
                HttpPostedFileBase file = employeeVM.PictureFile;
                if (file != null)
                {
                    var filepat = Path.Combine("/Images/", DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filepat));
                    eplo.Picture = filepat;


                }
                var pro = db.ProjectAssignments.Where(x => x.EmployeeId == eplo.EmployeeId).ToList();
                db.ProjectAssignments.RemoveRange(pro);
                foreach (var a in projectid)
                {
                    var project = db.Projects.FirstOrDefault(x => x.ProjectId == a);
                    ProjectAssignment projectAssignment = new ProjectAssignment()
                    {
                        Employee = eplo,
                        ProjectId = project.ProjectId,

                    };
                    db.ProjectAssignments.Add(projectAssignment);
                    
                
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeVM);
        
        }
          
    }
    
}




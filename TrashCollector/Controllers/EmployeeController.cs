using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index(int id, DayOfWeek? dayOfWeek)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            ViewBag.DaysOfWeek = Enum.GetValues(typeof(DayOfWeek));
            var selectedEmployee = db.Employees.Include("Customers").SingleOrDefault(Employee => Employee.Id == id);
            selectedEmployee.Customers = db.Customers.Distinct().AsEnumerable().Where(Customer => Customer.PickupDay == dayOfWeek).ToList();
            return View(selectedEmployee);
        }
        [HttpPost]
        public ActionResult Index(int employeeId, int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var selectedCustomer = db.Customers.Find(id);
            selectedCustomer.MoneyOwed += 50;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = employeeId });
        }


        // GET: Employee/Details/5
        public ActionResult Details(int employeeId, int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var selectedEmployee = db.Employees.Include("Customer").Single(Employee => Employee.Id == employeeId);
            selectedEmployee.Customer = db.Customers.Find(id);
            return View(selectedEmployee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, string id)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                Employee newEmployee = new Employee()
                {
                    ApplicationUserId = id,
                    Name = collection["Name"],
                    ZipCode = int.Parse(collection["ZipCode"])
                };
                db.Employees.Add(newEmployee);
                db.SaveChanges();

                var employeeId = db.Employees.SingleOrDefault(Employee => Employee.ApplicationUserId == id).Id;
                return RedirectToAction("Index", new { id = employeeId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var employeeToEdit = db.Employees.Find(id);
            return View(employeeToEdit);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var employeeToEdit = db.Employees.SingleOrDefault(Employee => Employee.Id == employee.Id);
                employeeToEdit.Name = employee.Name;
                employeeToEdit.ZipCode = employee.ZipCode;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = employee.Id });
            }
            catch
            {
                return View(employee);
            }
        }
        
    }
}

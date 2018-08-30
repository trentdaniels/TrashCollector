﻿using System;
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
        public ActionResult Index(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var selectedEmployee = db.Employees.Find(id);
            var today = DateTime.Today.DayOfWeek;
            var nearbyCustomers = db.Customers.Where(Customer => Customer.ZipCode == selectedEmployee.ZipCode &&  today == Customer.PickupDay).ToList();
            return View(nearbyCustomers);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

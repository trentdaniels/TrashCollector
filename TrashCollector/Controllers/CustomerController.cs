using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var selectedCustomer = db.Customers.Find(id);
            return View(selectedCustomer);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var selectedCustomer = db.Customers.Find(id);
            return View(selectedCustomer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, string id)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                Customer newCustomer = new Customer()
                {
                    ApplicationUserId = id,
                    Name = collection["Name"],
                    Address = collection["Address"],
                    ZipCode = int.Parse(collection["ZipCode"]),
                    StartDate = Convert.ToDateTime(collection["StartDate"]),
                    EndDate = Convert.ToDateTime(collection["EndDate"]),
                    PickupDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), collection["PickupDay"], true)
                };
                db.Customers.Add(newCustomer);
                db.SaveChanges();

                var selectedCustomerId = db.Customers.SingleOrDefault(Customer => (Customer.ApplicationUserId == id)).Id;
                return RedirectToAction("Index", new { id = selectedCustomerId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var customerToEdit = db.Customers.Find(id);
            return View(customerToEdit);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var customerInDb = db.Customers.SingleOrDefault(Customer => Customer.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Address = customer.Address;
                customerInDb.ZipCode = customer.ZipCode;
                customerInDb.PickupDay = customer.PickupDay;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = customer.Id });
            }
            catch
            {
                return View(customer);
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var customerToDelete = db.Customers.Find(id);
            return View(customerToDelete);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(Customer customer)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                var customerInDb = db.Customers.Find(customer.Id);
                var userInDb = db.Users.Find(customerInDb.ApplicationUserId);
                db.Users.Remove(userInDb);
                db.Customers.Remove(customerInDb);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(customer);
            }
        }
    }
}

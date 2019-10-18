using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.Models.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
              return HttpNotFound();
            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
                return HttpNotFound();
            //If we do not explicitly specify the name of the view to be returned (here "New") otherwise MVC will look for the view called Edit.
            //Model behind this view(New) is "NewCustomerViewModel"
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }

        public ViewResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                /*
                    Why we are creating a new Customer object here is because it will initialize the customer properties with the default values, 
                    hence id = 0 and this way we will not get the validation error that id field is empty.
                */

                Customer = new Customer(),
                MembershipTypes = membershipTypes
            
            };
            return View("customerForm", viewModel);
        }

        //using NewCustomerViewModel viewModel is called "model binding" as it binds the parameters of the model to the request data(values provided in the form)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            //If model state is not valid it will return the same view
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            //If the customer is new and doesn't exist in the database it's id will be 0
            if (customer.Id == 0)
            {
                //Adding the changes to the context object, the changes will be in the memory & not in the database
                _context.Customers.Add(customer);
            }
            else
            {
                //Here we're using Single method and not SingleOrDefault because we only want the customers that are present in the database
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }
            //Save the changes to the database. A SQL query will be created & will run on the db to save the changes
            _context.SaveChanges();
            return RedirectToAction("index", "Customers");
        }

        // GET: Customers
        //public ActionResult Index()
        //{
          //  return View();
        //}
    }
}
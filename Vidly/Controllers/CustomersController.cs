using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity; //Include() method in Entity
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        // Setting DBSet for query Objects(Customers)
        public ApplicationDbContext _context; //has dbsets(ie.Customers...) and must be disposable
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
        public ViewResult Index()
        {
            //var customers = GetCustomers();
            //var customers = _context.Customers.ToList(); // DBContext query object in db and iterate with ToList()

            //var customers = _context.Customers.Include(c => c.MembershipType).ToList(); // Includes customers and their MembershipType referenced objects
            //return View(customers);
            return View();
        }
        public ViewResult New()
        {
            //if we want to use memberhiptype in context we have to set it in identity model
            var membershipTypes = _context.MembershipTypes.ToList(); //dbset into list
            //then we need viewmodel for two model(customer,membershiptype)
            var newCustomerVM = new CustomerFormViewModel()
            {
                //Validation summary return Customer Id although Hidden field for ID exist => instanciate customers!!!
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", newCustomerVM);
        }
        //Edit action goes to CustomerForm view
        public ActionResult Edit(int id)
        {
            //define selectedCustomer and membershipTypes for using in viewmodel
            var selectedCustomer = _context.Customers.SingleOrDefault(c => c.Id == id);
            var membershipTypes = _context.MembershipTypes.ToList(); //for populate membershipTypes drop down list
            //CustomerForm view get this object model and fill the fields
            if (selectedCustomer == null)
                return HttpNotFound();
            var customerVM = new CustomerFormViewModel()
            {
                Customer = selectedCustomer,
                MembershipTypes = membershipTypes //all types that view needs
            };

            return View("CustomerForm", customerVM); //change view 'New' to CustomerForm
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //to use antiForgeryToken() in CustomerForm
        public ActionResult Save(Customer customer)
        {                           //NewCustomerViewModel newCustomerVM ie customer property is used before

            //          VALIDATION - Reset the form with customer correct data and validation messages on incorrect fields
            if (!ModelState.IsValid)
            {
                var custFormVmodel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", custFormVmodel);
            }
            //          VALIDATION END

            if(customer.Id == 0)//if this customer object doesnt exist in context
            {   //id got from hiddenFor from view
                //add new customer to context(ie memory)
                _context.Customers.Add(customer);
            }
            else
            {   //get old corresponding customer obj from dbContext
                var customerInDB = _context.Customers.Single(c => c.Id == customer.Id);
                //set its properties on passing customer object properties
                customerInDB.Name = customer.Name;
                customerInDB.Birthdate = customer.Birthdate;
                customerInDB.MembershipTypeId = customer.MembershipTypeId;
                customerInDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            //then, look throw all object modifications, generate SQL statements and run them on DB
            _context.SaveChanges();
            //return View();
            return RedirectToAction("Index", "Customers");
        }
        public ActionResult Details(int? id)
        {
            //var customer = GetCustomers().SingleOrDefault(cust => cust.Id == id);
            //var customer = _context.Customers.SingleOrDefault(cust => cust.Id == id); //If Details View shows reference objects must have Include()
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(cust => cust.Id == id);

            if(customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }
     
        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer() { Id = 1, Name = "Peter Smidth" },
        //        new Customer() { Id = 2, Name = "Marko Koaler" }
        //    };
        //}
 
    }
}
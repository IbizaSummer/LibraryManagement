using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Details()
        {
            var customers = _customerService.GetAllCustomers();
            return View(customers);
        }

        public IActionResult SearchById(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                throw new CustomerNotFoundException($"User ID {id} Not Found！");
            }
            return View("Details", new List<Customer> { customer });
        }

        [HttpPost]
        public IActionResult Update(int id, Customer updatedCustomer)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound();
                }

                customer.Name = updatedCustomer.Name;  // 更新属性
                _customerService.UpdateCustomer(customer);
                return RedirectToAction("Details", new { id = customer.CustomerId });
            }
            return View("Details", updatedCustomer);
        }

        public IActionResult Delete(int id)
        {
            _customerService.DeleteCustomer(id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.CreateCustomer(customer);
                return RedirectToAction("Details", new { id = customer.CustomerId });
            }
            return View(customer);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerApiController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerApiController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// 获取所有客户
        /// </summary>
        /// <returns>客户列表</returns>
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            return Ok(customers);
        }

        /// <summary>
        /// 根据 ID 获取客户信息
        /// </summary>
        /// <param name="id">客户的唯一标识</param>
        /// <returns>指定 ID 的客户信息</returns>
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound(new { Message = $"Customer with ID {id} not found." });
            }
            return Ok(customer);
        }

        /// <summary>
        /// 创建新客户
        /// </summary>
        /// <param name="customer">客户数据</param>
        /// <returns>创建成功的客户信息</returns>
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _customerService.CreateCustomer(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }

        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="id">客户的唯一标识</param>
        /// <param name="customer">更新后的客户数据</param>
        /// <returns>无内容表示更新成功</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest(new { Message = "ID mismatch in request body and URL." });
            }

            var existingCustomer = _customerService.GetCustomerById(id);
            if (existingCustomer == null)
            {
                return NotFound(new { Message = $"Customer with ID {id} not found." });
            }

            _customerService.UpdateCustomer(customer);
            return NoContent();
        }

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="id">客户的唯一标识</param>
        /// <returns>无内容表示删除成功</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound(new { Message = $"Customer with ID {id} not found." });
            }

            _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}

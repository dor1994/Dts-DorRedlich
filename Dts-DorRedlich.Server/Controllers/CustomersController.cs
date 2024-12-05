using CustomersBarBer.CustomerServices.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dts_DorRedlich.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerModel customer)
        {
            return Ok(await _customerService.AddCustomerAsync(customer));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerModel customer)
        {
            await _customerService.UpdateCustomerAsync(customer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return Ok();
        }
    }
    }

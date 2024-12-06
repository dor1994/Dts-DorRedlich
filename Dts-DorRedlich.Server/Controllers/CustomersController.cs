using CustomersBarBer.CustomerServices.Interfaces;
using Data.Enums;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("getFilteredCustomers")]
        public async Task<IActionResult> GetFilteredCustomers([FromQuery] string? customerName, [FromQuery] DateTime? requestedTime)
        {
            var customers = await _customerService.GetFilterCustomersAsync(customerName, requestedTime);
            return Ok(customers);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerModel customer)
        {
            if(customer.RequestedTime < DateTime.Now)
            {
                return Ok(new ApiResponse<bool, EnumResponse> () { Status = false, Message = "Requested Time Must be Valid!"});
            }
            return Ok(await _customerService.AddCustomerAsync(customer));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerModel customer)
        {
            if (customer.RequestedTime < DateTime.Now)
            {
                return Ok(new ApiResponse<bool, EnumResponse>() { Status = false, Message = "Requested Time Must be Valid!" });
            }
            return Ok(await _customerService.UpdateCustomerAsync(customer));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            
            return Ok(await _customerService.DeleteCustomerAsync(id));
        }
    }
}

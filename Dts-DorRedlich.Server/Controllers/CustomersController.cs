using Microsoft.AspNetCore.Mvc;

namespace Dts_DorRedlich.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        //private readonly CustomerService _customerService;

        //public CustomersController(CustomerService customerService)
        //{
        //    _customerService = customerService;
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAllCustomers()
        //{
        //    var customers = await _customerService.GetAllCustomersAsync();
        //    return Ok(customers);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        //{
        //    await _customerService.AddCustomerAsync(customer);
        //    return Ok();
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        //{
        //    await _customerService.UpdateCustomerAsync(customer);
        //    return Ok();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCustomer(int id)
        //{
        //    await _customerService.DeleteCustomerAsync(id);
        //    return Ok();
        //}
        }
    }

using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerapiController : ControllerBase
    {
        public CustomerService cservice;
        public CustomerapiController(CustomerService c)
        {
            cservice = c;
        }
        [HttpGet]
        public IActionResult GetAdmin()
        {
            IEnumerable<Customer>? clist;
            try
            {
                clist = cservice.GetCustomer();
                return Ok(clist);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int CustomerId)
        {
            Customer c;
            try
            {
                c = cservice.GetCustomerById(CustomerId);
                return Ok(c);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> addAdmin([FromQuery] Customer c)
        {
            var adm = await cservice.AddCustomer(c);
            return CreatedAtAction(nameof(GetCustomerById), new { id = c.CustomerId, controller = "Customer" }, c);

        }
        [HttpDelete("{CustomerId}")]
        public async Task<IActionResult> Delete([FromRoute] int CustomerId)
        {
            var admid = cservice.RemoveCustomer(CustomerId);
            return Ok();
        }
    }
}

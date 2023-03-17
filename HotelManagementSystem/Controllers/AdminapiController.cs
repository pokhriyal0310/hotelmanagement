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
    public class AdminapiController : ControllerBase
    {
        public AdminService aservice;
        public AdminapiController(AdminService a)
        {
            aservice = a;
        }
        [HttpGet]
        public IActionResult GetAdmin()
        {
            IEnumerable<Admin>? alist;
            try
            {
                alist = aservice.GetAdmin();
                return Ok(alist);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("{AdminId}")]
        public async Task<IActionResult>GetAdminById([FromRoute] int AdminId)
        {
            Admin c;
            try
            {
                c = aservice.GetAdminById(AdminId);
                return  Ok(c);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("UpdateBooking")]
        public IActionResult UpdateAdmin(Admin? UpdAdmin)
        {
            Admin? UpdAdmins;
            try
            {
                UpdAdmin = aservice.UpdateAdmin(UpdAdmin);
                return Ok(UpdAdmin);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult>addAdmin([FromQuery] Admin a)
        {
            var adm = await aservice.AddAdmin(a);
            return CreatedAtAction(nameof(GetAdminById), new { id = a.AdminId, controller = "Admin" }, a);

        }
        [HttpDelete("{AdminId}")]
        public async Task<IActionResult> Delete([FromRoute] int AdminId)
        {
            var admid = aservice.RemoveAdmin(AdminId);
            return Ok();
        }

    }
}

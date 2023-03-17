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
    public class EmployeeapiController : ControllerBase
    {
        public EmployeeService eservice;
        public EmployeeapiController(EmployeeService e)
        {
            eservice = e;
        }
        [HttpGet]
        public IActionResult GetEmployee()
        {
            IEnumerable<Employee1>? elist;
            try
            {
                elist = eservice.GetRoom();
                return Ok(elist);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{EmpId}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int EmpId)
        {
            Employee1 e;
            try
            {
                e = eservice.GetEmployeeById(EmpId);
                return Ok(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdateRoom")]
        public IActionResult UpdateEmployee(Employee1? UpdEmployee)
        {
            Employee1? UpdEmployees;
            try
            {
                UpdEmployee = eservice.UpdateEmployee(UpdEmployee);
                return Ok(UpdEmployee);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> addRoom([FromQuery] Employee1 e)
        {
            var emp = await eservice.AddEmployee(e);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = e.EmpId, controller = "Employee" }, e);

        }
        [HttpDelete("{EmpId}")]
        public async Task<IActionResult> Delete([FromRoute] int EmpId)
        {
            var empid = eservice.RemoveEmployee(EmpId);
            return Ok();
        }
    }
}

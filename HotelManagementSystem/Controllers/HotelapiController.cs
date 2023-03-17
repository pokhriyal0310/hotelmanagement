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
    public class HotelapiController : ControllerBase
    {
        public HotelService hservice;
        public HotelapiController(HotelService h)
        {
            hservice = h;
        }
        [HttpGet]
        public IActionResult GetHotel()
        {
            IEnumerable<Hotel>? hlist;
            try
            {
                hlist = hservice.GetHotel();
                return Ok(hlist);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{HotelId}")]
        public async Task<IActionResult> GetHotelById([FromRoute] int HotelId)
        {
            Hotel c;
            try
            {
                c = hservice.GetHotelById(HotelId);
                return Ok(c);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateHotel")]
        public IActionResult UpdateHotel(Hotel? UpdHotel)
        {
            Hotel? UpdHotels;
            try
            {
                UpdHotel = hservice.UpdateHotel(UpdHotel);
                return Ok(UpdHotel);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> addHotel([FromQuery] Hotel h)
        {
            var ht = await hservice.AddHotel(h);
            return CreatedAtAction(nameof(GetHotelById), new { id = h.HotelId, controller = "Hotel" }, h);

        }
        [HttpDelete("{HotelId}")]
        public async Task<IActionResult> Delete([FromRoute] int HotelId)
        {
            var htid = hservice.RemoveHotel(HotelId);
            return Ok();
        }
    }
}

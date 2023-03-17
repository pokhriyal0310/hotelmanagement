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
    public class RoomapiController : ControllerBase
    {
        public RoomService rservice;
        public RoomapiController(RoomService a)
        {
            rservice = a;
        }
        [HttpGet]
        public IActionResult GetRoom()
        {
            IEnumerable<Room>? rlist;
            try
            {
                rlist = rservice.GetRoom();
                return Ok(rlist);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{RoomNo}")]
        public async Task<IActionResult> GetRoomById([FromRoute] int RoomNo)
        {
            Room r;
            try
            {
                r = rservice.GetRoomById(RoomNo);
                return Ok(r);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("UpdateRoom")]
        public IActionResult UpdateRoom(Room? UpdRoom)
        {
            Room? UpdRooms;
            try
            {
                UpdRoom = rservice.UpdateRoom(UpdRoom);
                return Ok(UpdRoom);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> addRoom([FromQuery] Room r)
        {
            var adm = await rservice.AddRoom(r);
            return CreatedAtAction(nameof(GetRoomById), new { id = r.RoomNo, controller = "Room" }, r);

        }
        [HttpDelete("{RoomId}")]
        public async Task<IActionResult> Delete([FromRoute] int RoomId)
        {
            var rmid = rservice.RemoveRoom(RoomId);
            return Ok();
        }

    }
}

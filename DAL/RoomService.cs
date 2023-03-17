using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RoomService
    {
        private HotelManagementContext db = new HotelManagementContext();

        public IEnumerable<Room> GetRoom()
        {
            IEnumerable<Room> rlist;
            try
            {
                rlist = db.Rooms.ToList();
                return rlist;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Room GetRoomById(int Id)
        {
            Room? room;
            try
            {
                room = db.Rooms.Find(Id);
                if (room != null)
                {
                    return room;
                }
                else
                {
                    throw new Exception("Record not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Room UpdateRoom(Room RoomRec)
        {
            try
            {
                if (RoomRec != null)
                {
                    db.Entry(RoomRec).State = EntityState.Modified;
                    db.SaveChanges();
                    return RoomRec;
                }
                else
                {
                    throw new Exception("Ui Error");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> AddRoom(Room room)
        {
            var Room1 = new Room()
            {
                RoomNo = room.RoomNo,
                RoomType = room.RoomType,
                RoomPrice = room.RoomPrice,
                HotelId = room.HotelId

            };
            db.Rooms.Add(Room1);
            await db.SaveChangesAsync();
            return (int)Room1.RoomNo;
        }
        public async Task RemoveRoom(int RoomNo)
        {
            Room rm = db.Rooms.Where((x) => x.RoomNo == RoomNo).FirstOrDefault();
            db.Rooms.Remove(rm);
            await db.SaveChangesAsync();
        }
    }
}

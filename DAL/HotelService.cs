using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HotelService
    {
        private HotelManagementContext db = new HotelManagementContext();

        public IEnumerable<Hotel> GetHotel()
        {
            IEnumerable<Hotel> hlist;
            try
            {
                hlist = db.Hotels.ToList();
                return hlist;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Hotel GetHotelById(int Id)
        {
            Hotel? hotel;
            try
            {
                hotel = db.Hotels.Find(Id);
                if (hotel != null)
                {
                    return hotel;
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
        public Hotel UpdateHotel(Hotel HotelRec)
        {
            try
            {
                if (HotelRec != null)
                {
                    db.Entry(HotelRec).State = EntityState.Modified;
                    db.SaveChanges();
                    return HotelRec;
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
        public async Task<int> AddHotel(Hotel hotel)
        {
            var Hotel1 = new Hotel()
            {
                HotelId = hotel.HotelId,
                HotelName = hotel.HotelName,
                HotelAddress = hotel.HotelAddress,
                Pincode = hotel.Pincode

            };
            db.Hotels.Add(Hotel1);
            await db.SaveChangesAsync();
            return (int)Hotel1.HotelId;
        }
        public async Task RemoveHotel(int HotelId)
        {
            Hotel ht = db.Hotels.Where((x) => x.HotelId == HotelId).FirstOrDefault();
            db.Hotels.Remove(ht);
            await db.SaveChangesAsync();
        }
    }
}

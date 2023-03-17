using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BookingService
    {
        private HotelManagementContext db = new HotelManagementContext();

        public IEnumerable<Booking> GetBooking()
        {
            IEnumerable<Booking> blist;
            try
            {
                blist = db.Bookings.ToList();
                return blist;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Booking GetBookingById(int Id)
        {
            Booking? booking;
            try
            {
                booking = db.Bookings.Find(Id);
                if (booking != null)
                {
                    return booking;
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
        public Booking UpdateBooking(Booking BookingRec)
        {
            try
            {
                if (BookingRec != null)
                {
                    db.Entry(BookingRec).State = EntityState.Modified;
                    db.SaveChanges();
                    return BookingRec;
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
        public async Task<int> AddBooking(Booking booking)
        {
            var book1 = new Booking()
            {
                 DateOfBooking= booking.DateOfBooking,
                 RoomNo= booking.RoomNo,
                 CustomerId= booking.CustomerId,
                 BookingId = booking.BookingId

            };
            db.Bookings.Add(book1);
            await db.SaveChangesAsync();
            return (int)book1.BookingId;
        }
        public async Task RemoveBooking(int BookingId)
        {
            Booking bk = db.Bookings.Where((x) => x.BookingId == BookingId).FirstOrDefault();
            db.Bookings.Remove(bk);
            await db.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Booking
    {
        public DateTime? DateOfBooking { get; set; }
        public int? RoomNo { get; set; }
        public int? CustomerId { get; set; }
        public int BookingId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Room RoomNoNavigation { get; set; }
    }
}

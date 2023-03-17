using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Hotel
    {
        public Hotel()
        {
            Customers = new HashSet<Customer>();
            Employee1s = new HashSet<Employee1>();
            Rooms = new HashSet<Room>();
        }

        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public int? Pincode { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee1> Employee1s { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}

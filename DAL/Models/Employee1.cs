using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Employee1
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpGrade { get; set; }
        public int? HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}

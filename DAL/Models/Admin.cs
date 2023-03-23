using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminType { get; set; }
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class VoucherDto
    {
        public Guid? ObjId { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string NoOfRides { get; set; }
        public string NoOfPeople { get; set; }
        public string AgeGroup { get; set; }
        public string OccasionId { get; set; }
        public string VoucherNo { get; set; }
        public string NamesId { get; set; }
        public string Status { get; set; }

        public string OccasionDate { get; set; }
        public string Category { get; set; }
    }
}
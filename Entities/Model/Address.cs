using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    [Table("Address")]
    public class Address :CommonModel
    {
        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid UserId { get; set; }
        public string Line1 { get; set; } = string.Empty;

        public string Line2 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public int Pincode { get; set; }

        public string State { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public long Delivery_Phone { get; set; }

    }
}

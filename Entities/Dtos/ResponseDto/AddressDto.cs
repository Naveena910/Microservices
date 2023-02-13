using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.ResponseDto
{
    public class AddressDto
    {
        public Guid Id { get; set; }

        public string Line1 { get; set; } = string.Empty;

        public string Line2 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public int Pincode { get; set; }

        public string State { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}

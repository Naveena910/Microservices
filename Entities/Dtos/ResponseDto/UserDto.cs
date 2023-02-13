using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.ResponseDto
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public long Phone { get; set; }

        public string UserType { get; set; } = string.Empty;

        public ICollection<AddressDto> Address { get; set; } = new List<AddressDto>();

        public ICollection<PaymentDto> Payment { get; set; } = new List<PaymentDto>();
    }
}

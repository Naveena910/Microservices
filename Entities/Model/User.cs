using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    [Table("User")]
    public class User : CommonModel
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public long Phone { get; set; }
        public string? Password { get; set; }
        public string UserType { get; set; } = string.Empty;
        public ICollection<Address>? Address { get; set; } = new List<Address>();

        public ICollection<Payment>? Payment { get; set; } = new List<Payment>();
    }
}

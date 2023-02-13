using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.ResponseDto
{
    public class PaymentDto
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Expiry { get; set; } = string.Empty;

        public string AccountNumber { get; set; }=string.Empty;
    }
}

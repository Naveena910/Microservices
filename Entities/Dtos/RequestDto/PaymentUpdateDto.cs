using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.RequestDto
{
    public class PaymentUpdateDto
    {
        
        [RegularExpression(@"^(UPI|DebitCard|CreditCard)$", ErrorMessage = "Must be the vaild type")]
        public string Type { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
       
        public string? Expiry { get; set; }
    }
}

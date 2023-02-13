using Entities.Dtos.Helpers;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.RequestDto
{
    public class PaymentForCreatingDto
    {
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^(UPI|DebitCard|CreditCard)$", ErrorMessage = "Must be the vaild type")]
        public string Type { get; set; } = string.Empty;
        [RegularExpression(@"^.{9,}$", ErrorMessage = "Minimum 9 characters required")]
        [Required(ErrorMessage = "Required")]
        [StringLength(18, MinimumLength = 9, ErrorMessage = "Maximum 18 characters")]
        public string AccountNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required")]
        [ValidDate]
        public string Expiry { get; set; }=string.Empty;

    }
}

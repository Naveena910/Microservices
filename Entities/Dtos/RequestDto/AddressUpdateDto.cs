using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.RequestDto
{
    public class AddressUpdateDto
    {
        
        [RegularExpression(@"^(Home|Work|Other)$", ErrorMessage = "The  field must be either 'Home' or'Work' 'Other'")]
        public string? Type { get; set; } = string.Empty;
        
        public string? Line1 { get; set; } = string.Empty;
        
        public string? Line2 { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;

        [RegularExpression(@"^\d{6}$", ErrorMessage = " Enter a valid pincode")]
        public int Pincode { get; set; }
        public string? State { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.RequestDto
{
    public class AddressForCreatingDto
    {
        

        [Required(ErrorMessage = "This field is required")]
        public string Type { get; set; } = string.Empty;
       [Required(ErrorMessage = "This field is required")]
        public string Line1 { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required")]
        public string Line2 { get; set; } = string.Empty;


       [Required(ErrorMessage = "This field is required")]
        public string City { get; set; } = string.Empty;


        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = " Enter a valid pincode")]
        public int Pincode { get; set; }


        [Required(ErrorMessage = "This field is required")]
        public string State { get; set; } = string.Empty;


        [Required(ErrorMessage = "This field is required")]
        public string Country { get; set; } = string.Empty;
    }
}

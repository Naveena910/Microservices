using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.RequestDto
{
    public class UserForCreatingDto
    {
        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required")]
        
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "This field is required")]
        [MinLength(8, ErrorMessage = "Password should be at least 8 characters long")]
       
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter a valid phone number")]
        public long Phone { get; set; }

        
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^(Customer|Admin)$", ErrorMessage = " Should  be either 'Customer' or 'Admin'")]
        public string UserType { get; set; } = string.Empty;


        public ICollection<AddressForCreatingDto>? Address { get; set; } = new List<AddressForCreatingDto>();
    }
}

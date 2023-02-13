using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.RequestDto
{
    public class UserUpdateDto
    {
        
        public string? FirstName { get; set; } = string.Empty;
        
        public string? LastName { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string? Email { get; set; } = string.Empty;
        [MinLength(8, ErrorMessage = "Password should be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Should contain atleast  one uppercase letter,one lowercase letter, one digit and one special characters")]
        public string? Password { get; set; } = string.Empty;
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter a valid phone number")]
        public long Phone { get; set; }

        [RegularExpression(@"^(Customer|Admin)$", ErrorMessage = " Should  be either 'Customer' or 'Admin'")]
        public string? UserType { get; set; } = string.Empty;


        public ICollection<AddressForCreatingDto>? Address { get; set; } = new List<AddressForCreatingDto>();
    }
}

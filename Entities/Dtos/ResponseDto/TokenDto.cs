using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.ResponseDto
{
    public class TokenDto
    {
        public string Token { get; set; } = string.Empty;

        public string TokenType { get; set; } = "Bearer";
    }
}

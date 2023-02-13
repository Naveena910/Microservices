using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.ResponseDto
{
    public class ErrorDto
    {
        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}

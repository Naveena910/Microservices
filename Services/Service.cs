using Contracts.IServices;
using Entities.Dtos.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class Service :IService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Service(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        ///<summary>
        /// Validates user entered data.
        ///</summary>
        ///<return>ErrorDTO</return>
        public ErrorDto ModelStateInvalid(ModelStateDictionary ModelState)
        {
            return new ErrorDto
            {
                ErrorMessage = ModelState.Keys.FirstOrDefault(),
                Description = ModelState.Values.Select(src => src.Errors.Select(src => src.ErrorMessage).FirstOrDefault()).FirstOrDefault(),
                StatusCode=400
               
            };
        }
        /// <summary>
        ///checks user or admin
        /// </summary>
        /// <param name="userId"></param>
        public void Typecheck()
        {
            string s = (_httpContextAccessor.HttpContext.User?.FindFirstValue("UserType"));
            if(s== "Customer") throw new ForbiddenException("Admins access only");
        }
    }
}

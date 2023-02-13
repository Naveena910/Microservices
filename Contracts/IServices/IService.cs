using Entities.Dtos.ResponseDto;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Contracts.IServices
{
    public interface IService
    {
        ///<summary>
        /// Validates user given  data.
        ///</summary>
        ///<return>ErrorDto</return>
        public ErrorDto ModelStateInvalid(ModelStateDictionary modelState);

        
        /// <summary>
        ///checks user or admin
        /// </summary>
        /// <param name="userId"></param>
        public void Typecheck();

    }
}

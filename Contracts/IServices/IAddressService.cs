using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IServices
{
    public interface IAddressService
    {
        /// <summary>
        /// creates delivery address of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public ResponseDto CreateUserAddress(AddressForCreatingDto address);
        /// <summary>
        /// Gets all the addresses of a user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<AddressDto> GetAddressByUserId();
        /// <summary>
        /// Get address of the user by address id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public AddressDto GetAddressById( Guid addressId);
        /// <summary>
        /// Update by address id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        /// <param name="updateAddress"></param>
        public void UpdateAddressById( Guid addressId, AddressUpdateDto updateAddress);
        /// <summary>
        /// Delete  by address id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        public void DeleteByAddressId( Guid addressId);
        /// <summary>
        /// Delete  by user id
        /// </summary>
        /// <param name="userId"></param>

        public void DeleteByUserId();



    }
}

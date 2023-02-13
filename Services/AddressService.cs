using AutoMapper;
using Contracts.IRepository;
using Contracts.IServices;
using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using Entities.Model;
using log4net;
using Microsoft.Extensions.Logging;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AddressService :IAddressService
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly IService _service;
        private readonly ILogger<UserService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AddressService (IMapper mapper, IAddressRepository addressRepository,
                                  IService service, ILogger<UserService> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _addressRepository = addressRepository;
            _service = service;
            _logger = logger;
           _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// creates delivery address of the user
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public ResponseDto CreateUserAddress(AddressForCreatingDto address)
        {
           Guid userId= _unitOfWork.GetuserId();
            Address userAddress = _mapper.Map<Address>(address);
            userAddress.UserId = userId;
           _unitOfWork.addresss.Add(userAddress);
           _unitOfWork.Save();

            return new ResponseDto { Id = userAddress.Id };
        }
        /// <summary>
        /// Gets all the addresses of a customer by user id
        /// </summary>
        /// <returns></returns>
        public List<AddressDto> GetAddressByUserId()
        {
            Guid userId= _unitOfWork.GetuserId();
            _unitOfWork.CurrentUser(userId);
         IEnumerable<Address> userAddress = _unitOfWork.addresss.GetAllById(x => x.UserId==userId);

            return _mapper.Map<List<AddressDto>>(userAddress);

        }
        /// <summary>
        /// Get address of the user by address id
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public AddressDto GetAddressById(Guid addressId)
        {
            Guid userId= _unitOfWork.GetuserId();
            _unitOfWork.CurrentUser(userId);
            Address userAddress =_unitOfWork.addresss.GetById(addressId);

            if (userAddress == null)
            {
                throw new NotFoundException("No address has been found");
            }
            return _mapper.Map<AddressDto>(userAddress);

        }
        /// <summary>
        /// Update by address id
        /// </summary>
        /// <param name="addressId"></param>
        /// <param name="updateAddress"></param>
        public void UpdateAddressById( Guid addressId, AddressUpdateDto updateAddress)
        {
            Guid userId= _unitOfWork.GetuserId();
            _unitOfWork.CurrentUser(userId);
            Address address = _unitOfWork.addresss.GetById(addressId);

            if (address == null)
            {
                throw new NotFoundException("Address not found");
            }
            address.Pincode = updateAddress.Pincode;
            address.Line1 = updateAddress.Line1;
            address.Line2 = updateAddress.Line2;
            address.State = updateAddress.State;
            address.City = updateAddress.City;
            address.Country = updateAddress.Country;
            address.UserId= userId;
            address.IsActive= true;
            address.DateCreated= address.DateCreated;
            address.DateUpdated = DateTime.Now;
            _unitOfWork.addresss.Update(address); 
            _unitOfWork.Save();
           
      }
        /// <summary>
        /// Delete  by address id
        /// </summary>
        /// <param name="addressId"></param>
        public void DeleteByAddressId( Guid addressId)
        {
            Guid userId= _unitOfWork.GetuserId();
            _unitOfWork.CurrentUser(userId);
            Address address = _unitOfWork.addresss.GetById(addressId);

            if (address == null)
            {
                throw new NotFoundException("No address has been found");
            }
           _unitOfWork.addresss.Delete(address);
            address.IsActive = false;
          _unitOfWork.Save();
        }
        /// <summary>
        /// Delete  by user id
        /// </summary>
      
        
        public void DeleteByUserId()
        {
            Guid userId= _unitOfWork.GetuserId();
            _unitOfWork.CurrentUser(userId);
            IEnumerable<Address> userAddress = _unitOfWork.addresss.GetAllById(x => x.UserId == userId);

            if (userAddress.Count()==0)
            {
                throw new NotFoundException("No address has been found");
            }
            _unitOfWork.DeleteAllAddress(userId);
            _unitOfWork. Save();
        }
        

    }
}

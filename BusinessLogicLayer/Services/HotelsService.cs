using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class HotelsService:IHotelService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<HotelsService> _logger;
        private readonly IHotelRepository _hotelRepository;
        private readonly IValidator<HotelAddRequest> _hotelAddRequestValidator;
        private readonly IValidator<HotelUpdateRequest> _hotelUpdateRequestValidator;
        private readonly IValidator<RoomAddRequestDTO> _roomAddRequestValidator;
        private readonly IValidator<RoomUpdateRequestDTO> _roomUpdateRequestValidator;
        private object _hoteRepository;

        public HotelsService(ILogger<HotelsService> logger,IHotelRepository hotelRepository, IValidator<HotelAddRequest> hotelAddRequestValidator,
            IValidator<HotelUpdateRequest> hotelUpdateRequestValidator, IValidator<RoomAddRequestDTO> roomAddRequestValidator,
            IValidator<RoomUpdateRequestDTO> roomUpdateRequestValidator,IMapper mapper)
        {
            _logger = logger;
            _hotelRepository = hotelRepository;
            _hotelAddRequestValidator = hotelAddRequestValidator;
            _hotelUpdateRequestValidator = hotelUpdateRequestValidator;
            _roomAddRequestValidator = roomAddRequestValidator;
            _roomUpdateRequestValidator = roomUpdateRequestValidator;
            _mapper = mapper;
        }

        public async Task<HotelResponse?> AddHotel(HotelAddRequest? hotelAddRequest)
        {
            if(hotelAddRequest == null)
            {
                _logger.LogError("Hotel Add Request is null");
                throw new  ArgumentNullException(nameof(hotelAddRequest));
            }
            ValidationResult validationResult = await _hotelAddRequestValidator.ValidateAsync(hotelAddRequest);
            if(!validationResult.IsValid)
            {
                _logger.LogError("Hotel Add Request Validation Failed: {Errors}", string.Join(", ",validationResult.Errors.Select(e => e.ErrorMessage)));
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errors);
            }
            //Validating rooms
            foreach (RoomAddRequestDTO roomAddRequest in hotelAddRequest.Rooms)
            {
                ValidationResult roomAddRequsetvalidationResult = await _roomAddRequestValidator.ValidateAsync(roomAddRequest);
                if (!roomAddRequsetvalidationResult.IsValid)
                {
                    _logger.LogError("Room Add Request Validation Failed: {Errors}", string.Join(", ", roomAddRequsetvalidationResult.Errors.Select(e => e.ErrorMessage)));
                    var errors = string.Join(", ", roomAddRequsetvalidationResult.Errors.Select(e => e.ErrorMessage));
                    throw new ArgumentException(errors);
                }
            }

            Hotel? addedHotel = await _hotelRepository.AddHotelAsync(_mapper.Map<Hotel>(hotelAddRequest));
            if(addedHotel == null)
            {
                _logger.LogError("Failed to add hotel to the repository");
                return null;
            }
            return _mapper.Map<HotelResponse>(addedHotel);
        }


        public async Task<HotelResponse?> UpdateHotel(HotelUpdateRequest hotelUpdateRequest)
        {
            if (hotelUpdateRequest == null)
            {
                _logger.LogError("Hotel Update Request is null");
                throw new ArgumentNullException(nameof(hotelUpdateRequest));
            }
            var validationResult = await _hotelUpdateRequestValidator.ValidateAsync(hotelUpdateRequest);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Hotel Update Request Validation Failed: {Errors}", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errors);
            }
            Hotel? updatedHotel = await _hotelRepository.UpdateHotelAsync(_mapper.Map<Hotel>(hotelUpdateRequest));
            if (updatedHotel == null)
            {
                _logger.LogError("Failed to update hotel in the repository");
                return null;
            }
            return _mapper.Map<HotelResponse>(updatedHotel);
        }



        public async Task<bool> DeleteHotel(Guid? hotelID)
        {
            if(hotelID == null)
            {        
                _logger.LogError("Hotel ID is null");
                throw new ArgumentNullException(nameof(hotelID));
            }
            Hotel? hotel = await _hotelRepository.GetHotelByConditionAsync(temp=>temp.HotelID == hotelID);
            if (hotel == null)
            {
                return false;
            }

            return await _hotelRepository.DeleteHotelAsync(hotelID.Value);
        }

        public async Task<HotelResponse> GetHotelByCondition(Expression<Func<Hotel, bool>> conditionExpression)
        {
            Hotel? hotel = await _hotelRepository.GetHotelByConditionAsync(conditionExpression);
            if (hotel == null)
            {
                return null;
            }
            return _mapper.Map<HotelResponse>(hotel);
        }

        public async Task<List<HotelResponse?>> GetHotels()
        {
           IEnumerable<Hotel?> hotels =  await _hotelRepository.GetAllHotelsAsync();

             return _mapper.Map<List<HotelResponse?>>(hotels);
        }

        public async Task<List<HotelResponse?>> GetHotelsByCondition(Expression<Func<Hotel, bool>> conditionExpression)
        {
            IEnumerable<Hotel?> hotel = await _hotelRepository.GetHotelsByConditionAsync(conditionExpression);
            return _mapper.Map<List<HotelResponse?>>(hotel);
        }

       
    }
}

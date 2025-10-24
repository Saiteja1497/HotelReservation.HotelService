using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace BusinessLogicLayer.ServiceContracts
{
    public interface IHotelService
    {
        Task<HotelResponse?> AddHotel(HotelAddRequest? hotelAddRequest);
        Task<HotelResponse?> UpdateHotel(HotelUpdateRequest hotelUpdateRequest);
        Task<bool> DeleteHotel(Guid? hotelID);
        Task<HotelResponse> GetHotelByCondition(Expression<Func<Hotel, bool>> conditionExpression);
        Task<List<HotelResponse?>> GetHotels();
        Task<List<HotelResponse?>> GetHotelsByCondition(Expression<Func<Hotel,bool>> conditionExpression);


    }
}

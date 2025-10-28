using DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace DataAccessLayer.RepositoryContracts
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel?>> GetAllHotelsAsync();
        Task<IEnumerable<Hotel?>> GetHotelsByConditionAsync(Expression<Func<Hotel, bool>> conditioExpretion);
        Task<Hotel?> GetHotelByConditionAsync(Expression<Func<Hotel, bool>> conditioExpretion);
        Task<Hotel?> AddHotelAsync(Hotel hotel);
        Task<Hotel?> UpdateHotelAsync(Hotel hotel);
        Task<bool> DeleteHotelAsync(Guid hotelId);


    }
}

using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HotelRepository> _logger;
        public HotelRepository(ApplicationDbContext context,ILogger<HotelRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Hotel?> AddHotelAsync(Hotel hotel)
        {

            if (hotel == null)
            {
                _logger.LogError("Attempted to add a null hotel entity.");
                throw new ArgumentNullException(nameof(hotel), "Hotel entity cannot be null.");
            }
            var rowsAffected = await _context.Hotels.AddAsync(hotel);
            if (rowsAffected != null)
            {
                await _context.SaveChangesAsync();
                    _logger.LogInformation("Hotel entity added successfully with ID: {HotelID}", hotel.HotelID);
                return hotel;
            }
            
            else
            {
                _logger.LogError("Failed to add hotel entity.");
                return null;
            }
        }

        public async Task<bool> DeleteHotelAsync(Guid hotelId)
        {
            if(hotelId == Guid.Empty)
            {
                _logger.LogError("Attempted to delete a hotel with an empty GUID.");
                throw new ArgumentException("Hotel ID cannot be an empty GUID.", nameof(hotelId));
            }
            Hotel? hotel = await _context.Hotels.FirstOrDefaultAsync(temp => temp.HotelID == hotelId);
            if(hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await  _context.SaveChangesAsync();
                _logger.LogInformation("Hotel entity with ID: {HotelID} deleted successfully.", hotelId);
                return true;
            }
            else
            {
                _logger.LogWarning("No hotel entity found with ID: {HotelID}. Deletion aborted.", hotelId);
                return false;
            }

        }

        public async Task<IEnumerable<Hotel?>> GetAllHotelsAsync()
        {
            return await _context.Hotels.ToListAsync();
        }

        public Task<Hotel?> GetHotelByConditionAsync(Expression<Func<Hotel, bool>> conditioExpretion)
        {
            return _context.Hotels.FirstOrDefaultAsync(conditioExpretion);
        }

        public async Task<IEnumerable<Hotel?>> GetHotelsByConditionAsync(Expression<Func<Hotel, bool>> conditioExpretion)
        {
            return await _context.Hotels.Where(conditioExpretion).ToListAsync();
        }

        public async Task<Hotel?> UpdateHotelAsync(Hotel hotel)
        {
            if(hotel == null)
            {
                _logger.LogError("Attempted to update a null hotel entity.");
                throw new ArgumentNullException(nameof(hotel), "Hotel entity cannot be null.");
            }

            Hotel? existingHotel = await _context.Hotels.FirstOrDefaultAsync(temp => temp.HotelID == hotel.HotelID);
            if(existingHotel != null)
            {
                existingHotel.HotelName = hotel.HotelName;
                existingHotel.HotelLocation = hotel.HotelLocation;
                existingHotel.HotelDescription = hotel.HotelDescription;
                existingHotel.Rooms = hotel.Rooms;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Hotel entity with ID: {HotelID} updated successfully.", hotel.HotelID);
                return existingHotel;
            }
            else
            {
                _logger.LogWarning("No hotel entity found with ID: {HotelID}. Update aborted.", hotel.HotelID);
                return null;
            }
        }
    }
}

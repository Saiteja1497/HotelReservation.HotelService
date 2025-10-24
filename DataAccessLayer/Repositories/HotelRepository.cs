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

            var rowsAffected = await _context.Hotel.AddAsync(hotel);
            foreach (var room in hotel.Rooms)
            {
                room.HotelID = hotel.HotelID;
                await _context.Room.AddAsync(room);
            }

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
            Hotel? hotel = await _context.Hotel.FirstOrDefaultAsync(temp => temp.HotelID == hotelId);
            if(hotel != null)
            {
                foreach(var room in _context.Room.Where(r => r.HotelID == hotelId))
                {
                    _context.Room.Remove(room);
                }
                _context.Hotel.Remove(hotel);
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
            List<Hotel>? hotels = await _context.Hotel.ToListAsync();
            foreach(var hotel in hotels)
            {
                hotel.Rooms = await _context.Room.Where(r => r.HotelID == hotel.HotelID).ToListAsync();
            }
            return hotels;
        }

        public async Task<Hotel?> GetHotelByConditionAsync(Expression<Func<Hotel, bool>> conditioExpretion)
        {
            Hotel? hotel = await _context.Hotel.FirstOrDefaultAsync(conditioExpretion);
            if(hotel != null)
            {
                hotel.Rooms = await _context.Room.Where(r => r.HotelID == hotel.HotelID).ToListAsync();
            }
            return hotel;
        }

        public async Task<IEnumerable<Hotel?>> GetHotelsByConditionAsync(Expression<Func<Hotel, bool>> conditioExpretion)
        {
            List<Hotel>? hotels =  await _context.Hotel.Where(conditioExpretion).ToListAsync();
            foreach (var hotel in hotels)
            {
                hotel.Rooms = await _context.Room.Where(r => r.HotelID == hotel.HotelID).ToListAsync();
            }
            return hotels;
        }

        public async Task<Hotel?> UpdateHotelAsync(Hotel hotel)
        {
            if(hotel == null)
            {
                _logger.LogError("Attempted to update a null hotel entity.");
                throw new ArgumentNullException(nameof(hotel), "Hotel entity cannot be null.");
            }

            Hotel? existingHotel = await _context.Hotel.FirstOrDefaultAsync(temp => temp.HotelID == hotel.HotelID);
            if(existingHotel != null)
            {
                existingHotel.HotelName = hotel.HotelName;
                existingHotel.HotelLocation = hotel.HotelLocation;
                existingHotel.HotelDescription = hotel.HotelDescription;
                List<Room> existingRooms = _context.Room.Where(r => r.HotelID == hotel.HotelID).ToList();
                foreach (var room in existingRooms)
                {
                    room.HotelID = hotel.HotelID;
                    room.RoomPrice = hotel.Rooms.FirstOrDefault(r => r.RoomID == room.RoomID)?.RoomPrice ?? room.RoomPrice;
                    room.RoomType = hotel.Rooms.FirstOrDefault(r => r.RoomID == room.RoomID)?.RoomType ?? room.RoomType;
                    room.IsAvailable = hotel.Rooms.FirstOrDefault(r => r.RoomID == room.RoomID)?.IsAvailable ?? room.IsAvailable;
                    _context.Room.Update(room);
                }
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

using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.RepositoryContracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class HotelService:IHotelService
    {
        private readonly ILogger<HotelService> _logger;
        private readonly IHotelRepository _hotelRepository;
        public HotelService(ILogger<HotelService> logger,IHotelRepository hotelRepository)
        {
            _logger = logger;
            _hotelRepository = hotelRepository;
        }
    }
}

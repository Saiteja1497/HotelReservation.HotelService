using Microsoft.Extensions.DependencyInjection;

namespace HotelReservation.HotelService.BusinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            // Register your business logic layer services here
            // services.AddScoped<IBusinessService, BusinessService>();
            return services;
        }
    }
}

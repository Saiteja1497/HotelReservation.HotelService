using BusinessLogicLayer.Mappers;
using BusinessLogicLayer.ServiceContracts;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservation.HotelService.BusinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            // Register your business logic layer services here
             services.AddScoped<IHotelService,HotelsService>();
            services.AddAutoMapper(cfg => { },typeof(HotelAddRequestToHotelMappingProfile).Assembly);
            services.AddValidatorsFromAssemblyContaining<HotelAddRequestValidator>();
            return services;
        }
    }
}

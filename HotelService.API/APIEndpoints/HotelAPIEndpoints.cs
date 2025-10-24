using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using BusinessLogicLayer.Validators;
using DataAccessLayer.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace HotelService.API.APIEndpoints
{
    public static class HotelAPIEndpoints
    {
        public static IEndpointRouteBuilder MapHotelAPIEndpoints(this IEndpointRouteBuilder app)
        {
            //Get - /api/hotels
            app.MapGet("/api/hotels", async (IHotelService hotelService) =>
            {
                List<HotelResponse?> hotels =  await hotelService.GetHotels();
                return Results.Ok(hotels);
            });

            //Get - /api/hotels/search/hotel-id/{hotelID}
            app.MapGet("/api/hotels/search/hotel-id/{hotelID:guid}", async (IHotelService hotelService, Guid hotelID) =>
            {
                HotelResponse? hotel = await hotelService.GetHotelByCondition(temp=>temp.HotelID == hotelID);
                return Results.Ok(hotel);
            });

            //Get - /api/hotels/search/{searchString}
            app.MapGet("/api/hotels/search/{searchString}", async (IHotelService hotelService, string searchString) =>
            {
                List<HotelResponse?> hotelByName = await hotelService.GetHotelsByCondition(temp => temp.HotelName!=null && temp.HotelName.Contains(searchString,StringComparison.OrdinalIgnoreCase));
                List<HotelResponse?> hotelByLocation = await hotelService.GetHotelsByCondition(temp => temp.HotelLocation != null && temp.HotelLocation.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                List<HotelResponse?> hotelByDescription = await hotelService.GetHotelsByCondition(temp => temp.HotelDescription != null && temp.HotelDescription.Contains(searchString, StringComparison.OrdinalIgnoreCase));

                var hotels = (hotelByName.Union(hotelByLocation)).Union(hotelByDescription);

                return Results.Ok(hotels);
            });


            //Post - /api/hotels
            app.MapPost("/api/hotels", async (IHotelService hotelService, IValidator<HotelAddRequest> hotelAddRequestValidator,
                HotelAddRequest hotelAddRequest) =>
            {
                if (hotelAddRequest == null)
                {
                    return Results.BadRequest("Hotel Add Request is null");
                }
                ValidationResult validationResult = await hotelAddRequestValidator.ValidateAsync(hotelAddRequest);
                if (!validationResult.IsValid)
                {
                    //var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }
                HotelResponse? hotelResponse = await hotelService.AddHotel(hotelAddRequest);
                if (hotelResponse == null)
                {
                    return Results.Problem("Failed to add hotel");
                }
                return Results.Created($"/api/hotels/search/hotel-id/{hotelResponse.HotelID}", hotelResponse);

            });

            //Put - /api/hotels
            app.MapPut("/api/hotels", async (IHotelService hotelService, IValidator<HotelUpdateRequest> hotelUpdateRequestValidator,
                HotelUpdateRequest hotelUpdateRequest) =>
            {
                if (hotelUpdateRequest == null)
                {
                    return Results.BadRequest("Hotel Upadte Request is null");
                }
                ValidationResult validationResult = await hotelUpdateRequestValidator.ValidateAsync(hotelUpdateRequest);
                if (!validationResult.IsValid)
                {
                    //var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }
                HotelResponse? hotelResponse = await hotelService.UpdateHotel(hotelUpdateRequest);
                if (hotelResponse == null)
                {
                    return Results.Problem("Failed to add hotel");
                }
                return Results.Ok(hotelResponse);

            });

            //Delete - /api/hotels/{hotelID}
            app.MapDelete("/api/hotels/{hotelID}", async (IHotelService hotelService,Guid? hotelID) =>
            {
                if (hotelID == null)
                {
                    return Results.BadRequest("Hotel Delete Request is failed due to null ID reference");
                }
                HotelResponse? hotel = await hotelService.GetHotelByCondition(temp => temp.HotelID == hotelID);
                if(hotel == null)
                {
                    return Results.NotFound("Hotel not found for the given ID");
                }
                bool isDeleted = await hotelService.DeleteHotel(hotelID);

                return Results.Ok(isDeleted);

            });


            return app;
        }
    }
}

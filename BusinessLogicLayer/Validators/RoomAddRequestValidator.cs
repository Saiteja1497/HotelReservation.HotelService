using BusinessLogicLayer.DTO;
using FluentValidation;

namespace BusinessLogicLayer.Validators
{
    public class RoomAddRequestValidator:AbstractValidator<RoomAddRequestDTO>
    {
        public RoomAddRequestValidator()
        {
            RuleFor(x => x.RoomPrice).NotEmpty().WithMessage("Room Price Can't be Empty")
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be a non-negative value.");
                
            //RuleFor(x => x.HotelID).NotEmpty().WithMessage("Hotel ID can't be Empty");
            RuleFor(x => x.NoOfRoomsAvailable).NotEmpty().WithMessage("Please mention No. Of Rooms that are available");
            RuleFor(x => x.RoomType)
                .IsInEnum().WithMessage("Room Typre must be a valid type")
                .NotEmpty().WithMessage("Room type can't be Empty");


        }
    }
}

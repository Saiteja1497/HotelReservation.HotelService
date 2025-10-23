using BusinessLogicLayer.DTO;
using FluentValidation;

namespace BusinessLogicLayer.Validators
{
    public class HotelUpdateRequestValidator:AbstractValidator<HotelUpdateRequest>
    {
        public HotelUpdateRequestValidator()
        {
            RuleFor(x => x.HotelName).NotEmpty().WithMessage("HotelName Can't be Empty");
            RuleFor(x => x.HotelLocation).NotEmpty().WithMessage("Hotel Location Can't be Empty");
            RuleFor(x => x.Rooms).NotEmpty().WithMessage("Rooms Can't be Empty");

        }
    }
}

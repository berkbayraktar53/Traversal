using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class DestinationValidator : AbstractValidator<DestinationListDto>
    {
        public DestinationValidator()
        {
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir boş geçilemez");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş geçilemez");
            RuleFor(x => x.DayNight).NotEmpty().WithMessage("Gece gündüz boş geçilemez");
            RuleFor(x => x.Capacity).NotEmpty().WithMessage("Kapasite boş geçilemez");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat boş geçilemez");
        }
    }
}
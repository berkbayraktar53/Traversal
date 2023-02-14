using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class DestinationValidator : AbstractValidator<DestinationListDto>
    {
        public DestinationValidator()
        {
            RuleFor(x => x.DestinationImage).NotEmpty().WithMessage("Resim boş geçilemez");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş geçilemez");
            RuleFor(x => x.Capacity).NotEmpty().WithMessage("Kapasite boş geçilemez");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat boş geçilemez");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");

            RuleFor(x => x.DayNight)
                .NotEmpty().WithMessage("Gece gündüz boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");
        }
    }
}
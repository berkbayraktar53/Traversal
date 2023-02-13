using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class GuideValidator : AbstractValidator<GuideListDto>
    {
        public GuideValidator()
        {
            RuleFor(x => x.Image).NotEmpty().WithMessage("Resim boş geçilemez");

            RuleFor(x => x.NameSurname)
                .NotEmpty().WithMessage("Ad soyad boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");

            RuleFor(x => x.InstagramLink)
                .NotEmpty().WithMessage("İnstragram link boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");

            RuleFor(x => x.TwitterLink)
                .NotEmpty().WithMessage("Twitter link boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");
        }
    }
}
using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
	public class AboutValidator : AbstractValidator<AboutListDto>
	{
        public AboutValidator()
        {
			RuleFor(x => x.Image).NotEmpty().WithMessage("Resim boş geçilemez");

			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("Başlık boş geçilemez")
				.MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
				.MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");

			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("Açıklama boş geçilemez")
				.MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
				.MaximumLength(1000).WithMessage("En fazla 1000 karakter girebilirsiniz");
		}
    }
}
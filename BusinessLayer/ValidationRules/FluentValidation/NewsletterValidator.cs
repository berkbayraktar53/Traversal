using FluentValidation;
using EntityLayer.Concrete;

namespace BusinessLayer.ValidationRules.FluentValidation
{
	public class NewsletterValidator : AbstractValidator<Newsletter>
	{
		public NewsletterValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email boş geçilemez")
				.MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
				.MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz")
				.EmailAddress().WithMessage("Lütfen email formatında giriniz");
		}
	}
}
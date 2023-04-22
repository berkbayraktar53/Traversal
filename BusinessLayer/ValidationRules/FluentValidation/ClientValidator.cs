using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
	public class ClientValidator : AbstractValidator<ClientListDto>
	{
		public ClientValidator()
		{
			RuleFor(x => x.ClientImage).NotEmpty().WithMessage("Resim boş geçilemez");

			RuleFor(x => x.NameSurname)
				.NotEmpty().WithMessage("Müşteri adı soyadı boş geçilemez")
				.MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
				.MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");

			RuleFor(x => x.ClientComment)
				.NotEmpty().WithMessage("Müşteri yorumu boş geçilemez")
				.MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
				.MaximumLength(500).WithMessage("En fazla 500 karakter girebilirsiniz");
		}
	}
}
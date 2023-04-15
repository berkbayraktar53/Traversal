using FluentValidation;
using EntityLayer.Concrete;

namespace BusinessLayer.ValidationRules.FluentValidation
{
	public class ReservationValidator : AbstractValidator<Reservation>
	{
		public ReservationValidator()
		{
			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("Açıklama boş geçilemez")
				.MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
				.MaximumLength(250).WithMessage("En fazla 250 karakter girebilirsiniz");

			RuleFor(x => x.PersonCount)
				.NotEmpty().WithMessage("Kişi sayısı boş geçilemez")
				.MinimumLength(1).WithMessage("En az 1 karakter girebilirsiniz")
				.MaximumLength(250).WithMessage("En fazla 250 karakter girebilirsiniz");

			RuleFor(x => x.Date)
				.NotEmpty().WithMessage("Tarih boş geçilemez");
		}
	}
}
using FluentValidation;
using EntityLayer.Concrete;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.NameSurname)
                .NotEmpty().WithMessage("Ad soyad boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş geçilemez")
                .EmailAddress().WithMessage("Email formatında girebilirsiniz")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Konu boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mesaj boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(500).WithMessage("En fazla 500 karakter girebilirsiniz");
        }
    }
}
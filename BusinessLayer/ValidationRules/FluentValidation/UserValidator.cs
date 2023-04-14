using EntityLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<UserListDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserImage).NotEmpty().WithMessage("Resim boş geçilemez");

            RuleFor(x => x.NameSurname)
                .NotEmpty().WithMessage("Adı soyadı boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");

            RuleFor(x => x.AboutUser)
                .NotEmpty().WithMessage("Hakkında boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(150).WithMessage("En fazla 150 karakter girebilirsiniz");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz")
                .EmailAddress().WithMessage("Lütfen email formatında giriniz");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");
        }
    }
}
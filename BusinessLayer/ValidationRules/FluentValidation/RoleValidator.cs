using FluentValidation;
using EntityLayer.Concrete;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Rol adı boş geçilemez")
                .MinimumLength(3).WithMessage("En az 3 karakter girebilirsiniz")
                .MaximumLength(30).WithMessage("En fazla 30 karakter girebilirsiniz");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Rol açıklaması boş geçilemez")
                .MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");
        }
    }
}
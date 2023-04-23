using FluentValidation;
using EntityLayer.Concrete;

namespace BusinessLayer.ValidationRules.FluentValidation
{
	public class CommentValidator : AbstractValidator<Comment>
	{
        public CommentValidator()
        {
			RuleFor(x => x.CommentContent)
				.NotEmpty().WithMessage("Yorum boş geçilemez")
				.MinimumLength(5).WithMessage("En az 5 karakter girebilirsiniz")
				.MaximumLength(150).WithMessage("En fazla 150 karakter girebilirsiniz");
		}
    }
}
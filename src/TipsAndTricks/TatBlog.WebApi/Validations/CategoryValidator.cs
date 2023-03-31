using FluentValidation;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryEditModel>
    {
        public CategoryValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Tên chuyên mục không được để trống")
                .MaximumLength(100)
                .WithMessage("Tên chuyên mục tối đa 100 ký tự");

            RuleFor(a => a.UrlSlug)
                .NotEmpty()
                .WithMessage("Urlslug không được để trống")
                .MaximumLength(100)
                .WithMessage("Urlslug tối đa 100 ký tự");

            RuleFor(a => a.Description)
              .MaximumLength(1000)
              .WithMessage("Mô tả tối đa 1000 ký tự");

        }
    }
}

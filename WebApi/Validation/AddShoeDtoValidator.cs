using Application.Models.DTOs.ShoesDTOs;
using FluentValidation;

namespace WebApi.Validation
{
    public class AddShoeDtoValidator : AbstractValidator<AddShoeDto>
    {
        public AddShoeDtoValidator()
        {
            RuleFor(x => x.Brend)
                .NotEmpty();

            RuleFor(x=> x.ShoeCountSizes)
                .NotNull();
            
        }
    }
}

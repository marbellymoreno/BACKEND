using BACKEND.DTOs;
using FluentValidation;

namespace BACKEND.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator() 
        { 
            RuleFor(X => X.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(X => X.Name).Length(2, 20).WithMessage("Debe ser entre 2 a 20 caracteres");
        }
    }
}
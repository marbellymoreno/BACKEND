using BACKEND.DTOs;
using FluentValidation;

namespace BACKEND.Validators
{
    public class BeerUpdateValidator : AbstractValidator<BeerUpdateDto>
    {
        public BeerUpdateValidator() 
        {
            RuleFor(X => X.Id).NotNull().WithMessage("El ID es obligatorio");
            RuleFor(X => X.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(X => X.Name).Length(2, 20).WithMessage("Debe ser entre 2 a 20 caracteres");
            RuleFor(X => X.BrandID).NotNull().WithMessage("Debe ingresar una marca de cerveza valida");
            RuleFor(X => X.BrandID).GreaterThan(0).WithMessage("La marca debe estar registrada");
            RuleFor(X => X.Al).GreaterThan(0).WithMessage(X => "El nivel de alcohol {PropertyName} debe ser legal");
        }
    }
}

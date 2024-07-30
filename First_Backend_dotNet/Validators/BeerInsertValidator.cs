﻿using First_Backend_dotNet.DTOs;
using FluentValidation;

namespace First_Backend_dotNet.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto> // se hereda de AbstractValidator (FluentValidator) y se le pasa el DTO que se va a validar
    {
        public BeerInsertValidator()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("El nombre es obligatorio!");
            RuleFor(b => b.Name).Length(2, 20).WithMessage("El nombre debe tener entre 2 y 20 caracteres!");
            RuleFor(b => b.BrandID).NotNull().WithMessage("La marca es obligatoria!");
            RuleFor(b => b.BrandID).GreaterThan(0).WithMessage("Error con el valor enviado de la marca!");
            RuleFor(b => b.Alcohol).NotNull().WithMessage("El contenido de alcohol es obligatorio!");
            RuleFor(b => b.Alcohol).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0!");
        }
    }
}

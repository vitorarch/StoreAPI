using API.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public List<string> ProductsName { get; set; }

        public ProductValidator()
        {
            ProductsName = new List<string>();

            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("Identificador obrigatório");
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Nome obrigtório")
                .Must(BeUnique)
                .WithMessage("Produto já adicionado");
            RuleFor(p => p.Value)
                .NotEmpty()
                .WithMessage("Valor obrigtório");
        }

        private bool BeUnique(string productName)
        {
            foreach (var name in ProductsName)
            {
                if (name == productName) return false;
            }
            return true;
        }
    }
}

using API.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Validators
{
    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public List<Guid> ProductsOrdered { get; set; }

        public OrderItemValidator()
        {
            RuleFor(oi => oi.Id)
                .NotEmpty()
                .WithMessage("Identificador obrigatório");
            RuleFor(oi => oi.ProductId)
                .NotEmpty()
                .WithMessage("Identificador do produto obrigatório");
            RuleFor(oi => oi.ProductName)
                .NotEmpty()
                .WithMessage("Nome do produto obrigatório");
            RuleFor(oi => oi.ProductValue)
                .NotEmpty()
                .WithMessage("Valor deve ser maior que zero");

        }

    }
}

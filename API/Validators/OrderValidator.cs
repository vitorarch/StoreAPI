using API.Models;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        private OrderItemValidator _orderItemValidator;
        public OrderValidator()
        {
            _orderItemValidator = new OrderItemValidator();

            RuleFor(o => o.Cpf)
                .NotEmpty()
                .WithMessage("Cpf obrigatório");
            RuleFor(o => o.TotalValue)
                .NotEmpty()
                .WithMessage("Valor deve ser maior que zero");
            RuleFor(o => o.Items)
                .Must(ValidateOrderItems)
                .WithMessage("Erro no produto inserido");
        }

        private bool ValidateOrderItems(List<OrderItem> items)
        {
            ValidationResult result = null;
            foreach (var item in items)
            {
                result = _orderItemValidator.Validate(item);
            }
            if (result.Errors.Count.Equals(0)) return true;
            return false;
        }

    }
}

using BusinessObject.RequestModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validate
{
    public class SubcriptionValidate : AbstractValidator<SubcriptionRequest>
    {
        public SubcriptionValidate()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Name is required");
            RuleFor(s => s.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(2000).WithMessage("Price is not lower than 2k");
            RuleFor(s => s.Duration)
                .NotEmpty().WithMessage("Duration is required")
                .GreaterThan(1).WithMessage("Duration is greater than 1 month");
        }
    }
}

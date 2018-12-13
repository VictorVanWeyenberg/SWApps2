using FluentValidation;
using SWApps2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWApps2.Validation
{
    public class RegisterEstablishmentValidator : AbstractValidator<Establishment>
    {
        public RegisterEstablishmentValidator()
        {
            RuleFor(e => e.Address).NotNull().WithMessage("Please provide an address");
            RuleFor(e => e.Address.Number).Must(n => n > 1).WithMessage("Please provide a valid address number");
            RuleFor(e => e.Address.Street).NotEmpty().WithMessage("Street cannot be empty")
                .Matches(@"^[a-zA-Z]+").WithMessage("Street must begin with a letter")
                .Matches(@"^[a-zA-Z]+[a-zA-Z\s.-]+").WithMessage("Street can only contain letters, dots, '-' or spaces");
            RuleFor(e => e.Name).NotEmpty().WithMessage("Name cannot be empty");
        }
    }
}

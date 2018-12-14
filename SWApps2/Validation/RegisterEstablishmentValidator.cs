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
            RuleFor(e => e.Address).SetValidator(new AddressValidator());
            RuleFor(e => e.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(e => e.Tags).Must(t => !t.Contains(string.Empty)).WithMessage("Tags cannot have empty elements");
        }
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(a => a).NotNull().WithMessage("Please provide an address");
            RuleFor(a => a.Number).Must(n => n >= 1).WithMessage("Number must be 1 or higher");
            RuleFor(a => a.Street).NotEmpty().WithMessage("Street cannot be empty")
                .Matches(@"^[a-zA-Z]+[a-zA-Z\s.-]+")
                .WithMessage("Street must begin with a letter and can only contain letters, dots, '-' or spaces");
        }
    }
}

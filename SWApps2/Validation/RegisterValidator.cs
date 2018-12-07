using FluentValidation;
using SWApps2.Model;
using SWApps2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Validation
{

    public class RegisterValidator : AbstractValidator<RegisterWrapper>
    {
        private const int fnMinlength = 2;
        private const int lnMinlength = 4;
        private const int passwordMinLength = 8;


        public RegisterValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name must not be empty or whitespace");
            RuleFor(x => x.FirstName).MinimumLength(fnMinlength).WithMessage($"First Name must be at least {fnMinlength} characters");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name must not be empty or whitespace");
            RuleFor(x => x.LastName).MinimumLength(lnMinlength).WithMessage($"Last Name must be at least {lnMinlength} characters");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Given email is not valid");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password should not be empty");
            RuleFor(x => x.Password).MinimumLength(passwordMinLength).WithMessage($"Password should be at least {passwordMinLength} characters long");
            RuleFor(x => x.PasswordRepeat).Equal(x => x.Password).WithMessage("Passwords should match");
        }
    }
}

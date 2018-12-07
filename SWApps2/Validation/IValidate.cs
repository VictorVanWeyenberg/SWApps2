using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Validation
{
    interface IValidate
    {
        void Validate();

        void ResetValidationErrors();

        void MapErrorToProperty(ValidationFailure fail);
    }
}

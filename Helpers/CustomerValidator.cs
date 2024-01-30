using CustomerApi.Models;
using FluentValidation;

namespace CustomerApi.Helpers
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            RuleFor(o => o.DateOfBirth).LessThan(o => DateTime.Now).WithErrorCode("Future birthdate is invalid*");
        }
    }
}

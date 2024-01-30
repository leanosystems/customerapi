using CustomerApi.Models;
using FluentValidation;

namespace CustomerApi.Helpers;

public class AccountValidator : AbstractValidator<AccountModel>
{
    public AccountValidator()
    {
        RuleFor(o => o.AccountType).IsEnumName(typeof(AccountType)).WithErrorCode("Account Type is invalid*");
        RuleFor(o => o.InitialDeposit).LessThanOrEqualTo(o => 1000000).WithErrorCode("Initial Deposit is must not be greater than 1,000,000.00*");
    }
}

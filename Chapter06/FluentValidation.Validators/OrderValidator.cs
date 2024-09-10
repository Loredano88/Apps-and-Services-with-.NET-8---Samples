using FluentValidation.Models;

namespace FluentValidation.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator() 
        {
            RuleFor(_ => _.OrderId).NotEmpty();
            RuleFor(_ => _.CustomerName)
                .NotNull()
                .WithName("Name");
            RuleFor(_ => _.CustomerName)
                .MinimumLength(5)
                .WithSeverity(Severity.Warning);
            RuleFor(_ => _.CustomerEmail)
                .NotEmpty()
                .EmailAddress();
            RuleFor(_ => _.CustomerLevel)
                .IsInEnum();
            RuleFor(_ => _.Total)
                .GreaterThan(0);
            RuleFor(_ => _.ShipDate)
                .GreaterThan(_ => _.OrderDate);
            When(_ => _.CustomerLevel == CustomerLevel.Gold, () =>
            {
                RuleFor(_ => _.Total).LessThan(50M);
                RuleFor(_ => _.Total).GreaterThanOrEqualTo(20M);
            }).Otherwise(() => 
            {
                RuleFor(_ => _.Total).LessThan(20M);
            });
        }
    }
}

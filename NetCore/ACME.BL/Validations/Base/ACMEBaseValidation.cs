
namespace ACME.BL.Validations
{
    using FluentValidation;
    public class ACMEBaseValidation<T> : AbstractValidator<T>
    {
        public void ACMERuleSet(string ruleSetName, Action action)
        {
            RuleSet(ruleSetName, action);
        }
        public IRuleBuilderInitial<T, TProperty> ACMERuleFor<TProperty>(System.Linq.Expressions.Expression<Func<T, TProperty>> expression)
        {
            return RuleFor(expression);
        }
        public IRuleBuilderInitialCollection<T, TProperty> ACMERuleForEach<TProperty>(System.Linq.Expressions.Expression<Func<T, System.Collections.Generic.IEnumerable<TProperty>>> expression)
        {
            return RuleForEach(expression);
        }
    }
}

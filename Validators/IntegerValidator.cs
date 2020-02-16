using System;
using FluentValidation;
using FluentValidation.Validators;

namespace NetApi.Validators
{
    public class IntegerValidator : PropertyValidator
    {
        private bool AllowEmpty { get; set; }
        public IntegerValidator(bool allowEmpty = false)
        : base("{PropertyName} must be a number")
        {
            this.AllowEmpty = allowEmpty;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var proprtyValue = context.PropertyValue.ToString();
            if (this.AllowEmpty &&
                string.IsNullOrEmpty(proprtyValue))
            {
                return true;
            }
            int value;
            bool result = int.TryParse(proprtyValue, out value);
            return result;
        }
    }

    public static class IsIntegerValidatorExtension
    {
        public static IRuleBuilderOptions<T, TProperty> IsInteger<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new IntegerValidator(false));
        }
    }
}

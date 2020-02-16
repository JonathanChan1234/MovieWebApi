using System;
using FluentValidation;
using FluentValidation.Validators;

namespace NetApi.Validators
{
    public class BooleanValidator : PropertyValidator
    {
        private bool AllowEmpty { get; set; }
        public BooleanValidator(bool allowEmpty = false)
        : base("{PropertyName} must be a date time")
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
            Boolean value;
            bool result = Boolean.TryParse(proprtyValue, out value);
            return result;
        }
    }

    public static class IsBooleanExtenstion
    {
        public static IRuleBuilderOptions<T, TProperty> IsBoolean<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new BooleanValidator(false));
        }
    }
}

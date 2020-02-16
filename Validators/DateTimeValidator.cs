using System;
using FluentValidation;
using FluentValidation.Validators;

namespace NetApi.Validators
{
    public class DateTimeValidator : PropertyValidator
    {
        private bool AllowEmpty { get; set; }
        public DateTimeValidator(bool allowEmpty = false)
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
            DateTime value;
            bool result = DateTime.TryParse(proprtyValue, out value);
            return result;
        }
    }

    public static class IsDateTimeValidatorExtenstion
    {
        public static IRuleBuilderOptions<T, TProperty> IsDateTime<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new DateTimeValidator(false));
        }
    }
}

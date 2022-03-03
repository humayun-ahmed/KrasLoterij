using Infrastructure.Validator.Contract;

namespace Infrastructure.Validator.Utility
{
    public static class FluentValidationExtensions
    {
        public static ValidationResult ToValidationResult(this FluentValidation.Results.ValidationResult validationResult)
        {
            var result = new ValidationResult();
            if (validationResult == null)
            {
                return result;
            }

            foreach (var validationFailure in validationResult.Errors)
            {
                result.AddError(validationFailure.ErrorMessage, validationFailure.PropertyName, validationFailure.ErrorCode, validationFailure.AttemptedValue);
            }

            return result;
        }
    }
}
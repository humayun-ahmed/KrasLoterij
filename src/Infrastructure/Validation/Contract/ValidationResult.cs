namespace Infrastructure.Validator.Contract
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<ValidationFailure>();
        }

        public ValidationResult(List<ValidationFailure> errors)
        {
            Errors = errors;
        }

        public List<ValidationFailure> Errors { get; set; }

        public bool IsValid
        {
            get { return !Errors.Any(); }
        }

        public void AddError(string errorMessage, string propertyName = "", string errorCode = "", object attemptedValue = null)
        {
            Errors.Add(new ValidationFailure
                {ErrorMessage = errorMessage, PropertyName = propertyName, ErrorCode = errorCode, AttemptedValue = attemptedValue});
        }

        public override string ToString()
        {
            var errorMessages = new StringBuilder();
            foreach (var error in Errors)
            {
                if (!string.IsNullOrEmpty(errorMessages.ToString()))
                {
                    errorMessages.Append(",\n");
                }

                errorMessages.Append(error.ErrorMessage);
            }

            return errorMessages.ToString();
        }
    }
}
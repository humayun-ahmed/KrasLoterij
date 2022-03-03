using System.Collections.Generic;
using FluentValidation.Results;
using Infrastructure.Validator.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infrastructure.ValidationTest
{
    [TestClass]
    public class FluentValidationExtensionsTest
    {
        [TestMethod]
        public void ToValidationResultMustSucceed()
        {
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new("ProductId", "Not be empty"),
                new("ProductName", "Not be empty")
            });

            var result = validationResult.ToValidationResult();
            Assert.AreEqual(2, result.Errors.Count);
        }
    }
}
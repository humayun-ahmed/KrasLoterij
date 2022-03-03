using System;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NederlandseLoterij.KrasLoterij.Service.Contracts.DTO;

namespace NederlandseLoterij.KrasLoterij.Validators.Test
{
    [TestClass]
    public class ScratchCommandValidatorTester
    {
        [TestMethod]
        public void Should_have_error_when_Id_is_0_or_less()
        {
            //Setup
            var validator = new ScratchCommandValidator();
            var request = new ScratchCommand { Id = 0, UserId = Guid.NewGuid() };

            //Act
            var result = validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [TestMethod]
        public void Should_have_error_when_UserId_is_null_or_empty_guid()
        {
            //Setup
            var validator = new ScratchCommandValidator();
            var request = new ScratchCommand { Id = 0, UserId = Guid.Empty };

            //Act
            var result = validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.UserId);
        }
    }
}
using HomeAssignment.API.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;



namespace HomeAssignment.UnitTests
{
    [TestFixture]
    public class ContactVMTests
    {
        [Test]
        public void FirstName_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var contact = new ContactVM
            {
                FirstName = null,
                LastName = "Doe",
                Email = "shashikant.azure@gmail.com"
            };

            // Act
            var validationResults = ValidateModel(contact);

            // Assert
            Assert.That(validationResults.IsValid, Is.False);
        }

        [Test]
        public void LastName_WhenNull_ShouldFailValidation()
        {
            // Arrange
            var contact = new ContactVM
            {
                FirstName = "Shashi",
                LastName = null,
                Email = "shashikant.azure@gmail.com"
            };

            // Act
            var validationResults = ValidateModel(contact);

            // Assert
            Assert.That(validationResults.IsValid, Is.False);
        }

        [Test]
        public void Email_WhenInvalid_ShouldFailValidation()
        {
            // Arrange
            var contact = new ContactVM
            {
                FirstName = "Shashi",
                LastName = "Nalavade",
                Email = "shashikant.azure"
            };

            // Act
            var validationResults = ValidateModel(contact);

            // Assert
            Assert.That(validationResults.IsValid, Is.False);
        }

        [Test]
        public void ValidContactVM_ShouldPassValidation()
        {
            // Arrange
            var contact = new ContactVM
            {
                FirstName = "Shashi",
                LastName = "Nalavade",
                Email = "shashikant.azure@gmail.com"
            };

            // Act
            var validationResults = ValidateModel(contact);

            // Assert
            Assert.That(validationResults.Errors.Count() == 0);
        }

        private ValidationResultCollection ValidateModel(ContactVM model)
        {
            var context = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(model, context, validationResults, true);
            return new ValidationResultCollection(isValid, validationResults);
        }

        private class ValidationResultCollection
        {
            public bool IsValid { get; }
            public IEnumerable<ValidationResult> Errors { get; }

            public ValidationResultCollection(bool isValid, IEnumerable<ValidationResult> errors)
            {
                IsValid = isValid;
                Errors = errors;
            }
        }
    }
}

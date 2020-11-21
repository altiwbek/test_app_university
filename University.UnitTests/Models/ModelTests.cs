using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace University.UnitTests.Models
{
    public abstract class ModelTests
    {
        protected IList<ValidationResult> ValidateModel(object model)
        {
            var validationResult = new List<ValidationResult>();
            var validContext = new ValidationContext(model);

            Validator.TryValidateObject(model, validContext, validationResult);
            return validationResult;
        }
    }
}

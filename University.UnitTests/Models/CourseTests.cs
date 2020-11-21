using System;
using System.Collections.Generic;
using System.Text;
using University.Models;
using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace University.UnitTests.Models
{
    public class CourseTests : ModelTests
    {
        [Fact]
        public void TestLastNameRequired()
        {
            Course course = new Course
            {
                Title = "A",
                Credits = 3
            };

            var result = ValidateModel(course);

            Assert.Contains(result, v => v.MemberNames.Contains("Title"));
            Assert.Contains(result, v => v.ErrorMessage.Contains("must be a string with a minimum length of 3"));
        }

    }
}

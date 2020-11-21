using System;
using System.Collections.Generic;
using System.Text;
using University.Models;
using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace University.UnitTests.Models
{
    public class StudentTests : ModelTests
    {
        [Fact]
        public void TestLastNameRequired()
        {
            Student student = new Student
            {
                FirstMidName = "John",
                EnrollmentDate = DateTime.Parse("2020-11-11")
            };

            /*Assert.Contains(
                ValidateModel(student), v => v.MemberNames.Contains("LastName") && 
                v.ErrorMessage.Contains("required")
                );*/

            var result = ValidateModel(student);
            Assert.Contains(result, v => v.MemberNames.Contains("LastName"));
            Assert.Contains(result, v => v.ErrorMessage.Contains("required"));
        }
    }
}

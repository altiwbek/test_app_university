using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Controllers;
using University.Data;
using University.Models;
using Xunit;

namespace University.UnitTests
{
    public class StudentsControllerTests
    {
        private StudentsController _studentsController;
        private SchoolContext _context;
        public StudentsControllerTests()
        {
            var options = new DbContextOptionsBuilder<SchoolContext>()
                              .UseInMemoryDatabase(databaseName: "schooldb")
                              .Options;
            _context = new SchoolContext(options);
            _studentsController = new StudentsController(_context);
        }

        [Fact]
        public async Task IndexActionReturnsListOfStudents()
        {
            var students = new Student[]
               {
                    new Student { FirstMidName = "Carson1111",   LastName = "Alexander",
                        EnrollmentDate = DateTime.Parse("2010-09-01") },
                    new Student { FirstMidName = "Meredith", LastName = "Alonso",
                        EnrollmentDate = DateTime.Parse("2012-09-01") },
                    new Student { FirstMidName = "Arturo",   LastName = "Anand",
                        EnrollmentDate = DateTime.Parse("2013-09-01") },
                    new Student { FirstMidName = "Gytis",    LastName = "Barzdukas",
                        EnrollmentDate = DateTime.Parse("2012-09-01") }
               };

            _context.Students.AddRange(students);
            _context.SaveChanges();
            var result = await _studentsController.Index(null, null, null, null) as ViewResult;

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PaginatedList<Student>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
            //Assert.Equal("Carson1111", model.First().FirstMidName);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task IndexActionTestSearchString()
        {
            var searchString = "Testname";
            var students = new Student[]
               {
                    new Student { FirstMidName = searchString,   LastName = "Alexander",
                        EnrollmentDate = DateTime.Parse("2010-09-01") },
                    new Student { FirstMidName = "Meredith", LastName = searchString,
                        EnrollmentDate = DateTime.Parse("2012-09-01") }
               };
            _context.Students.AddRange(students);
            _context.SaveChanges();

            var result = await _studentsController.Index(null, null, searchString, null) as ViewResult;

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PaginatedList<Student>>(viewResult.ViewData.Model);
            Assert.True( 
                model.Where(
                    s => s.FirstMidName == searchString || s.LastName == searchString
                    ).Count() >= 2 );
        }

        [Fact]
        public async Task TestCreate()
        {
            int beforeCount = _context.Students.Count();
            Student student = new Student
            {
                FirstMidName = "John",
                LastName = "Doe",
                EnrollmentDate = DateTime.Parse("2020-11-11")
            };
            var result = await _studentsController.Create(student);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal(beforeCount + 1, _context.Students.Count());

        }

        [Fact]
        public async Task TestCreateNotValidStudent()
        {
            int beforeCount = _context.Students.Count();
            Student student = new Student
            {
                FirstMidName = "John",
                EnrollmentDate = DateTime.Parse("2020-11-11")
            };
            _studentsController.ModelState.AddModelError("LastName", "Is required");

            var result = await _studentsController.Create(student);
            Assert.IsType<ViewResult>(result);            
            Assert.Equal(beforeCount, _context.Students.Count());

        }
    }
}

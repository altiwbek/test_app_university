using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using University.Controllers;
using Xunit;
using Microsoft.AspNetCore.Hosting;

namespace University.UnitTests
{
    public class HomeControllerTests
    {
        private HomeController _homeController;

        public HomeControllerTests()
        {
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _homeController = new HomeController(mockLogger.Object, mockWebHostEnvironment.Object);
        }

        [Fact]
        public void IndexActionReturnsIndexView()
        {
            var result = _homeController.Index() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexActionIsTypeViewResult()
        {
            var result = _homeController.Index() as ViewResult;
            Assert.IsType<ViewResult>(result);
        }



    }
}

using AutoFixture;
using Backend.Domain.Tests;
using FireDetection.Backend.API.Controllers;
using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.Entity;
using FluentAssertions;
using HotChocolate.Execution.Processing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.API.Tests.Controllers
{
    public class LocationControllerTest : SetupTest
    {
        private readonly LocationController _locationController;
        private readonly Mock<LinkGenerator> _linkGenerator;

        public LocationControllerTest()
        {
           _linkGenerator = new Mock<LinkGenerator>();
            _locationController = new LocationController(_locationService.Object,_linkGenerator.Object);
        }

        [Fact]
        public async Task GetLocation_ShouldReturnCorrectData()
        {
            // Arrange
            var urlHelperMock = new Mock<IUrlHelper>();
            var httpContext = new DefaultHttpContext();
            var controllerContext = new ControllerContext { HttpContext = httpContext };

            List<Location> locations = _fixture.CreateMany<Location>(3).ToList();

            // Mock URL helper setup
            urlHelperMock
                .Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("https://example.com");

            // Setup location service
            _locationService.Setup(x => x.GetLocation()).ReturnsAsync(locations.AsQueryable());

            // Set up the controller with the mock dependencies and HttpContext
            _locationController.ControllerContext = controllerContext;

            // Act
            var result = await _locationController.Get();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var restDto = Assert.IsType<RestDTO<IQueryable<Location>>>(okObjectResult.Value);

            // Assert values
            Assert.Equal("Get Location Successfully", restDto.Message);
            Assert.Same(locations.AsQueryable(), restDto.Data);

            // Additional assertions for Links if needed
            Assert.NotNull(restDto.Links);
            Assert.Single(restDto.Links);
        }
    }
}

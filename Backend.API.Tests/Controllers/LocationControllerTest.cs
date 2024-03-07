using AutoFixture;
using Backend.Domain.Tests;
using FireDetection.Backend.API.Controllers;
using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.Entity;
using FluentAssertions;
using HotChocolate.Execution.Processing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Backend.API.Tests.Controllers
{
    public class LocationControllerTest : SetupTest
    {
        private readonly LocationController _locationController;


        public LocationControllerTest()
        {

            _locationController = new LocationController(_locationServiceTest.Object, _linkGeneratorTest.Object);

        }


      //  [Fact]
      //  public async Task GetLocation_ShouldReturnCorrectData()
      //  {
      //      // Arrange
      //      List<Location> locations = _fixture.CreateMany<Location>(3).ToList();

      //      // Setup location service
      //      _locationServiceTest.Setup(x => x.GetLocation()).ReturnsAsync(locations.AsQueryable());

      //      // Set up the controller with the mock dependencies and HttpContext


      //      //   _urlHelperTest.Setup(u => u.Action(It.IsAny<UrlActionContext>())).Returns("/Location"); // Mock the URL path

      //      // _httpContextTest.SetupGet(c => c.Request.Scheme).Returns("http");


      //      _linkGeneratorTest.Setup(lg => LinkGeneratorWrapper.GetUriByAction(
      //    _linkGeneratorTest.Object,
      //    It.IsAny<HttpContext>(),
      //    It.IsAny<string>(),
      //    It.IsAny<string>(),
      //    It.IsAny<object>(),
      //    It.IsAny<string>(),
      //    It.IsAny<HostString>(),
      //    It.IsAny<PathString>(),
      //    It.IsAny<FragmentString>(),
      //    It.IsAny<LinkOptions>()))
      //.Returns("/location"); // Adjust the return value as needed
      //      // Use the wrapper method in your controller or service
      //      var uri = LinkGeneratorWrapper.GetUriByAction(
      //          _linkGeneratorTest.Object,
      //          It.IsAny<HttpContext>(), // Pass appropriate HttpContext instance
      //          "actionName",
      //          "controllerName",
      //          new { id = 1 });
      //      // Act

      //      _linkGeneratorTest.SetReturnsDefault(uri);
      //      var result = await _locationController.Get();

      //      // Assert

      //      Assert.NotNull(result);
      //  }
    }
}

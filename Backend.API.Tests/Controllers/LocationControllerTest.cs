using AutoFixture;
using Backend.Domain.Tests;
using FireDetection.Backend.API.Controllers;
using FireDetection.Backend.Domain.DTOs.Core;
using FireDetection.Backend.Domain.Entity;
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
        public LocationControllerTest()
        {
            _locationController = new LocationController(_locationServiceTest.Object);
        }

        [Fact]
        public async Task Get_Returns_Successful_Response()
        {
            //? arrange
            var data = _fixture.Build<Location>().CreateMany(10).ToList();

            _locationServiceTest.Setup(x => x.GetLocation()).ReturnsAsync(data.AsQueryable());

            var result = await _locationController.Get();

            Assert.Equal(data.Count(), result.Value.Data.Count());


        }

    }
}

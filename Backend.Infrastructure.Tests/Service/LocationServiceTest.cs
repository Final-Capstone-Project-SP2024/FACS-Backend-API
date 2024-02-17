using AutoFixture;
using Backend.Domain.Tests;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.Service.Serivces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Tests.Service
{
    public class LocationServiceTest : SetupTest
    {
        private readonly ILocationService _locationService;

        public LocationServiceTest()
        {
            _locationService = new LocationService(_unitOfWork.Object, _mapperConfig);
        }

        [Fact]
        public async Task GetLocation_ShouldReturnCorrectData()
        {
            var mock = _fixture.Build<Location>().CreateMany(10).ToList();

            _unitOfWork.Setup(x => x.LocationRepository.GetAll()).ReturnsAsync(mock.AsQueryable());

            var result = await _locationService.GetLocation();

            Assert.NotNull(result);
        }


        [Fact]
        public async Task AddNewLocation_ShouldReturnCorrectData()
        {
            var mock = _fixture.Build<AddLocationRequest>().Create();
            var location = _mapperConfig.Map<Location>(mock);
            _unitOfWork.Setup(x => x.LocationRepository.InsertAsync(location));
            _unitOfWork.Setup(x => x.SaveChangeAsync());


            _mapperConfig.Map<LocationInformationResponse>(location);
          
            
            var result = await _locationService.AddNewLocation(mock);

            Assert.Null(result);

        }


       

        [Fact]
        public async Task DeleteLocation_ShouldReturnTrue()
        {
            var mock = _fixture.Build<Location>().Create();

            
            _unitOfWork.Setup(x => x.LocationRepository.GetById(It.IsAny<Guid>())).ReturnsAsync(mock);

            var result = await _locationService.DeleteLocation(mock.Id);

            result.Should().BeTrue();
        }
    
    
    
    
    
    }
}

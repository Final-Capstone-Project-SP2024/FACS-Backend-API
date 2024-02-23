using AutoFixture;
using Backend.Domain.Tests;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.Service.Serivces;
using GreenDonut;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Record = FireDetection.Backend.Domain.Entity.Record;

namespace Backend.Infrastructure.Tests.Service
{
    public class CameraServiceTest : SetupTest
    {
        private readonly ICameraService _cameraService;
        public CameraServiceTest()
        {

            _cameraService = new CameraService(_unitOfWork.Object, _mapperConfig, _mediaRecordServiceTest.Object, _timerServiceTest.Object, _memoryCacheServiceTest.Object);
        }


        [Fact]
        public async Task Active_CameraExists_ShouldReturnCameInformationResponse()
        {
            var mock = _fixture.Build<Camera>().Create();

            _unitOfWork.Setup(x => x.CameraRepository.GetById(mock.Id)).ReturnsAsync(mock);

            mock.Status = "Active";
            mock.LastModified = DateTime.UtcNow;

            _unitOfWork.Setup(x => x.CameraRepository.InsertAsync(mock));
            _unitOfWork.Setup(x => x.SaveChangeAsync());

            _mapperConfig.Map<CameInformationResponse>(mock);

            var result = await _cameraService.Active(mock.Id);


            Assert.NotNull(result);
            Assert.Equal("Active", result.Status);
            Assert.Equal(mock.Id, result.CameraId);
        }


        [Fact]
        public async Task Active_CameraDoesNotExist_ShouldThrowException()
        {

            var mock = _fixture.Build<Camera>().Create();


            _unitOfWork.Setup(x => x.CameraRepository.GetById(mock.Id)).ReturnsAsync((Camera)null);

            await Assert.ThrowsAsync<Exception>(async () => await _cameraService.Active(mock.Id));
        }


        [Fact]
        public async Task Add_NonDuplicateDestination_ShouldReturnCameInformationResponse()
        {
            var mock = _fixture.Build<AddCameraRequest>().Create();
            var camera = _mapperConfig.Map<Camera>(mock);

            _unitOfWork.Setup(x => x.CameraRepository.InsertAsync(camera));
            _unitOfWork.Setup(x => x.SaveChangeAsync());

            _cameraServiceTest.Setup(x => x.GetCameraByName(mock.Destination)).ReturnsAsync(camera);
            _mapperConfig.Map<CameInformationResponse>(camera);
            var result = await _cameraService.Add(mock);

            Assert.Null(result);
        }

        [Fact]
        public async Task Inactive_CameraExists_ShouldReturnCameInformationResponse()
        {
            var mock = _fixture.Build<Camera>().Create();

            _unitOfWork.Setup(x => x.CameraRepository.GetById(mock.Id)).ReturnsAsync(mock);

            mock.Status = "Banned";
            mock.LastModified = DateTime.UtcNow;

            _unitOfWork.Setup(x => x.CameraRepository.InsertAsync(mock));
            _unitOfWork.Setup(x => x.SaveChangeAsync());

            _mapperConfig.Map<CameInformationResponse>(mock);

            var result = await _cameraService.Inactive(mock.Id);


            Assert.NotNull(result);
            Assert.Equal("Banned", result.Status);
            Assert.Equal(mock.Id, result.CameraId);
        }


        [Fact]
        public async Task Inactive_CameraDoesNotExist_ShouldThrowException()
        {

            var mock = _fixture.Build<Camera>().Create();


            _unitOfWork.Setup(x => x.CameraRepository.GetById(mock.Id)).ReturnsAsync((Camera)null);

            await Assert.ThrowsAsync<Exception>(async () => await _cameraService.Inactive(mock.Id));
        }

        [Fact]
        public async Task Update_CameraExists_ShouldReturnCameInformationResponse()
        {
            var mock = _fixture.Build<AddCameraRequest>().Create();

            var mock2 = _fixture.Build<Camera>().Create();
            _unitOfWork.Setup(x => x.CameraRepository.GetById(mock2.Id)).ReturnsAsync(mock2);

            mock2.CameraDestination = mock.Destination;
            mock2.Status = mock.Status;
            mock2.LastModified = DateTime.UtcNow;

            _unitOfWork.Setup(x => x.CameraRepository.Update(mock2));
            _unitOfWork.Setup(x => x.SaveChangeAsync());

            _unitOfWork.Setup(x => x.CameraRepository.GetById(mock2.Id)).ReturnsAsync(mock2);
            _mapperConfig.Map<CameInformationResponse>(mock2);

            var result = await _cameraService.Update(mock2.Id, mock);

            Assert.NotNull(result);

        }


        [Fact]
        public async Task DetectElectricalIncident_ShouldReturnDetectElectricalIncidentResponse()
        {
            var mock = _fixture.Build<TakeElectricalIncidentRequest>().Create();
            Guid id = Guid.NewGuid();
            
            Record record = _mapperConfig.Map<Record>(mock);
            record.CameraID = id;
            record.Id = new Guid();

            _unitOfWork.Setup(x => x.RecordRepository.InsertAsync(record));
            _unitOfWork.Setup(x => x.SaveChangeAsync());

            _mapperConfig.Map<DetectResponse>(record);

            var result = await _cameraService.DetectElectricalIncident(id);

            Assert.NotNull(result);
        }







    }
}

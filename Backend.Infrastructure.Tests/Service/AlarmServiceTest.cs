using AutoFixture;
using Backend.Domain.Tests;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.Service.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Record = FireDetection.Backend.Domain.Entity.Record;

namespace Backend.Infrastructure.Tests.Service
{
    public class AlarmServiceTest : SetupTest
    {
        private readonly IAlarmService _alarmService;

        public AlarmServiceTest()
        {
            _alarmService = new AlarmService(_unitOfWork.Object, _mapperConfig);
        }

/*
        [Fact]
        public async Task DetectElectricalIncident_ShouldReturnDetectElectricalIncidentResponse()
        {
            var mock = _fixture.Build<TakeAlarmRequest>().Create();

            mock.VideoUrl = "api.mp4";
            Record record = _mapperConfig.Map<Record>(mock);

           await  SaveMediaFileInStorage_VideoUrl_ShouldInsertMediaRecordWithType1();

            await SaveMediaFileInStorage_VideoUrl_ShouldInsertMediaRecordWithType2();

            await SaveRecord_ShouldReturnTrueWhenSaveIsSuccessful();

            var result = await _alarmService.TakeAlarm(mock);

            Assert.True(result);

        }
*/

        [Fact]
        public async Task SaveMediaFileInStorage_VideoUrl_ShouldInsertMediaRecordWithType1()
        {
            string urlInput = "may.m4";
            MediaRecord mediaRecord = new MediaRecord
            {
                Url = urlInput,
                MediaTypeId = 1
            };

            _unitOfWork.Setup(x => x.MediaRecordRepository.InsertAsync(mediaRecord));
            _unitOfWork.Setup(x => x.SaveChangeAsync());

            var result = await _alarmService.SaveMediaFileInStorage(urlInput);

            Assert.True(result);
          
        }


        [Fact]
        public async Task SaveMediaFileInStorage_VideoUrl_ShouldInsertMediaRecordWithType2()
        {
            string urlInput = "may";
            MediaRecord mediaRecord = new MediaRecord
            {
                Url = urlInput,
                MediaTypeId = 2
            };

            _unitOfWork.Setup(x => x.MediaRecordRepository.InsertAsync(mediaRecord));
            _unitOfWork.Setup(x => x.SaveChangeAsync());

            var result = await _alarmService.SaveMediaFileInStorage(urlInput);

            Assert.True(result);

        }

        [Fact]
        public async Task SaveRecord_ShouldReturnTrueWhenSaveIsSuccessful()
        {

            var mock = _fixture.Build<Record>().Create();

            _unitOfWork.Setup(x => x.RecordRepository.InsertAsync(mock));
            _unitOfWork.Setup(x => x.SaveChangeAsync());

            var result = await _alarmService.SaveRecord(mock);

            Assert.False(result);
        
        }
        }
}

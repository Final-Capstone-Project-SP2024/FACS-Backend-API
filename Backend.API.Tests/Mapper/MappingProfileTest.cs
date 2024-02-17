using AutoFixture;
using Backend.Domain.Tests;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Record = FireDetection.Backend.Domain.Entity.Record;

namespace Backend.API.Tests.Mapper
{
    public class MappingProfileTest : SetupTest
    {

        [Fact]
        public void User_CreateUserRequest()
        {
            var user = _fixture.Build<User>()
                .OmitAutoProperties()
                .With(x => x.Id)
                .Create();

            var result = _mapperConfig.Map<CreateUserRequest>(user);

            result.Phone.Should().Be(user.Phone);
        }


        [Fact]
        public void User_UserInformationResponse()
        {
            var user = _fixture.Build<User>()
                .OmitAutoProperties()
                .With(x => x.Id)
                .Create();

            var result = _mapperConfig.Map<UserInformationResponse>(user);

            result.Email.Should().Be(user.Email);
        }


        [Fact]
        public void Camera_AddCameraRequest()
        {
            var camera = _fixture.Build<Camera>()
                .OmitAutoProperties()
                .With(x => x.Id)
                .Create();

            var result = _mapperConfig.Map<AddCameraRequest>(camera);

            result.Destination.Should().Be(camera.CameraDestination);
        }


        [Fact]
        public void Camera_CameInformationResponse()
        {
            var camera = _fixture.Build<Camera>()
                .OmitAutoProperties()
                .With(x => x.Id)
                .Create();

            var result = _mapperConfig.Map<CameInformationResponse>(camera);

            result.CameraDestination.Should().Be(camera.CameraDestination);
        }


        [Fact]
        public void Location_LocationInformationResponse()
        {
            var location = _fixture.Build<Location>()
                .OmitAutoProperties()
                .With(x => x.Id)
                .Create();

            var result = _mapperConfig.Map<LocationInformationResponse>(location);

            result.LocationName.Should().Be(location.LocationName);

        }

        [Fact]
        public void Location_AddLocationRequest()
        {
            var location = _fixture.Build<Location>()
                  .OmitAutoProperties()
                  .With(x => x.Id)
                  .Create();

            var result = _mapperConfig.Map<AddLocationRequest>(location);

            result.LocationName.Should().Be(location.LocationName);

        }


        [Fact]
        public void Record_TakeAlarmRequest()
        {
            var record = _fixture.Build<Record>()
                .OmitAutoProperties()
                .With(x => x.Id)
                .Create();

            var result = _mapperConfig.Map<TakeAlarmRequest>(record);

            result.PredictedPercent.Should().Be(record.PredictedPercent);
        }


        [Fact]
        public void Record_TakeElectricalIncidentRequest()
        {
            var record = _fixture.Build<Record>()
                .OmitAutoProperties()
                .With(x => x.Id)
                .Create();

            var result = _mapperConfig.Map<TakeElectricalIncidentRequest>(record);

            result.Time.Should().Be(record.RecordTime);
        }


        [Fact]
        public void AlarmRate_RateAlarmRequest()
        {
            var alarmRate  = _fixture.Build<AlarmRate>()
             .OmitAutoProperties()
             .With(x => x.Id)
             .Create();

            var result = _mapperConfig.Map<RateAlarmRequest>(alarmRate);

            result.UserId.Should().Be(alarmRate.UserID);
        }


        [Fact]
        public void AddRecordActionRequest_RecordProcess()
        {
            var recordProcess = _fixture.Build<RecordProcess>()
             .OmitAutoProperties()
             .With(x => x.Id)
             .Create();

            var result = _mapperConfig.Map<AddRecordActionRequest>(recordProcess);

            result.ActionId.Should().Be(recordProcess.ActionTypeId);
        }

    }
}

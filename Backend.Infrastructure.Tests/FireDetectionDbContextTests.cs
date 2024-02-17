using AutoFixture;
using Backend.Domain.Tests;
using FireDetection.Backend.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Record = FireDetection.Backend.Domain.Entity.Record;

namespace Backend.Infrastructure.Tests
{
    public class FireDetectionDbContextTests : SetupTest , IDisposable
    {
        [Fact]
        public async Task AppDbContext_LocationDbSetShouldReturnCorrectData()
        {
            var mockData = _fixture.Build<Location>().CreateMany(10).ToList();
            await _dbContext.Locations.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Locations.ToListAsync();
           Assert.Equal(10, result.Count);

        }



        [Fact]
        public async Task AppDbContext_UserDbSetShouldReturnCorrectData()
        {

            var mockData = _fixture.Build<User>().CreateMany(10).ToList();

            await _dbContext.Users.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Users.ToListAsync();
            Assert.Equal(10, result.Count);
        }


        [Fact]
        public async Task AppDbContext_RecordDbSetShouldReturnCorrectData()
        {

            var mockData = _fixture.Build<Record>().CreateMany(10).ToList();

            await _dbContext.Records.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Records.ToListAsync();
            Assert.Equal(10, result.Count);
        }



        [Fact]
        public async Task AppDbContext_RecordProcessesDbSetShouldReturnCorrectData()
        {

            var mockData = _fixture.Build<RecordProcess>().CreateMany(10).ToList();


            await _dbContext.RecordProcesses.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.RecordProcesses.ToListAsync();
            Assert.Equal(10, result.Count);
        }


        [Fact]
        public async Task AppDbContext_MediaRecordDbSetShouldReturnCorrectData()
        {

            var mockData = _fixture.Build<MediaRecord>().CreateMany(10).ToList();

            await _dbContext.MediaRecords.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.MediaRecords.ToListAsync();
            Assert.Equal(10, result.Count);
        }


        [Fact]
        public async Task AppDbContext_ControlCamerasDbSetShouldReturnCorrectData()
        {

            var mockData = _fixture.Build<ControlCamera>().CreateMany(10).ToList();

            await _dbContext.ControlCameras.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.ControlCameras.ToListAsync();
            Assert.Equal(10, result.Count);
        }

        [Fact]
        public async Task AppDbContext_CameraDbSetShouldReturnCorrectData()
        {

            var mockData = _fixture.Build<Camera>().CreateMany(10).ToList();

            await _dbContext.Cameras.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Cameras.ToListAsync();
            Assert.Equal(10, result.Count);
        }


        [Fact]
        public async Task AppDbContext_AlarmRatesDbSetShouldReturnCorrectData()
        {

            var mockData = _fixture.Build<AlarmRate>().CreateMany(10).ToList();

            await _dbContext.AlarmRates.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.AlarmRates.ToListAsync();
            Assert.Equal(10, result.Count);
        }



    }
}

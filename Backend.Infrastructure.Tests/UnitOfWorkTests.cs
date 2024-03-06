using Backend.Domain.Tests;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Tests
{
    public class UnitOfWorkTests : SetupTest
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkTests()
        {
            _unitOfWork = new UnitOfWork(
                _dbContext,_userRepositoryTest.Object,
                _locationRepositoryTest.Object,_cameraRepositoryTest.Object,
                _mediaRecordRepositoryTest.Object,_recordRepositoryTest.Object,
                _controlCameraRepositoryTest.Object,
                _alarmRepositoryTest.Object,
                _recordProcessRepositoryTest.Object,
                _alarmrateRepositoryTest.Object,_notificationLogRepositoryTest.Object);
        }
    }
}

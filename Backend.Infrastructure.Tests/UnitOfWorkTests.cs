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
                _dbContext,_userRepository.Object,
                _locationRepository.Object,_cameraRepository.Object,
                _mediaRecordRepository.Object,_recordRepository.Object,
                _controlCameraRepository.Object,
                _alarmRepository.Object,
                _recordProcessRepository.Object,
                _alarmrateRepository.Object);
        }
    }
}

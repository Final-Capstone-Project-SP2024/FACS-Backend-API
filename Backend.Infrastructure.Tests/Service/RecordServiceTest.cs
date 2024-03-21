using Backend.Domain.Tests;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.Service.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Tests.Service
{
    public class RecordServiceTest :SetupTest
    {
        private readonly IRecordService _recordService;

        public RecordServiceTest()
        {
            _recordService = new RecordService(_unitOfWork.Object, _mapperConfig, _memoryCacheServiceTest.Object, _timerServiceTest.Object, _notificationLogServiceTest.Object);
        }
    }
}

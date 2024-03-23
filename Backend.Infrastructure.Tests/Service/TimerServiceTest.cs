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
    public class TimerServiceTest : SetupTest
    {
        private readonly ITimerService _timerService;

        public TimerServiceTest()
        {
            _timerService = new TimerService(_memoryCacheServiceTest.Object, _apiCallServiceTest.Object);
        }
    }
}

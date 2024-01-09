using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Infrastructure.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class AlarmService : IAlarmService
    {
        public Task<string> TakeAlarm(TakeAlarmRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

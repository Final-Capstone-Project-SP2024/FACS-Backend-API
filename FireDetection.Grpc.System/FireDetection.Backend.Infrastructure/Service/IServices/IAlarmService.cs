using FireDetection.Backend.Domain.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IAlarmService
    {
        public Task<string> TakeAlarm(TakeAlarmRequest request);
    }
}

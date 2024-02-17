using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IAlarmService
    {
        public Task<bool> TakeAlarm(TakeAlarmRequest request);

        public Task<string> TakeElectricalBreakdownAlarm(ElectricalBreakdownRequest request);

        public Task<string> RateAlarm(RateAlarmRequest request);

        public  Task<bool> SaveRecord(Record record);

        public Task<bool> SaveMediaFileInStorage(string urlInput);



    }
}

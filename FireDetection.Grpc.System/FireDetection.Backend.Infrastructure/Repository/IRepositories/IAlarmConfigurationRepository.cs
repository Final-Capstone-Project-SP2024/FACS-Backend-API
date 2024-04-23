using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.IRepositories
{
    public interface IAlarmConfigurationRepository
    {
        public Task<IEnumerable<AlarmConfiguration>> GetAlarmConfigurations();

        public Task<bool> AddAlarmConfiguration(AlarmConfiguration request);

        public Task<bool> UpdateAlarmConfiguration( AlarmConfiguration request);

        public Task<AlarmConfiguration> GetAlarmConfigurationDetail(int alarmConfigurationID);

    }
}

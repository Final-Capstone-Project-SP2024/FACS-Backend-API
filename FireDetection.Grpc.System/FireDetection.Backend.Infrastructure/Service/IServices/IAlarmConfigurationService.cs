using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IAlarmConfigurationService
    {
        public Task<IEnumerable<AlarmConfiguration>> GetAlarmConfigurations();

        public Task<bool> AddNewConfiguration(AddAlarmConfigurationRequest request);

        public Task<bool> UpdateAlarmConfiguration(int AlarmConfigurationId, AddAlarmConfigurationRequest request);
    }
}

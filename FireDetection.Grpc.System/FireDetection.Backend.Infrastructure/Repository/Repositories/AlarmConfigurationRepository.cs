using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class AlarmConfigurationRepository : IAlarmConfigurationRepository
    {
        private readonly FireDetectionDbContext _context;
        public AlarmConfigurationRepository(FireDetectionDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAlarmConfiguration(AlarmConfiguration request)
        {
            _context.AlarmConfigurations.Add(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<AlarmConfiguration> GetAlarmConfigurationDetail(int alarmConfigurationID)
        {
           return await _context.AlarmConfigurations.Where(x => x.AlarmConfigurationId == alarmConfigurationID).FirstOrDefaultAsync();
        }

        public  async Task<IEnumerable<AlarmConfiguration>> GetAlarmConfigurations()
        {
            return _context.AlarmConfigurations.AsEnumerable();
        }

        public async Task<bool> UpdateAlarmConfiguration(AlarmConfiguration alarm)
        {
            _context.AlarmConfigurations.Update(alarm);
            return await _context.SaveChangesAsync() > 0;

        }
    }
}

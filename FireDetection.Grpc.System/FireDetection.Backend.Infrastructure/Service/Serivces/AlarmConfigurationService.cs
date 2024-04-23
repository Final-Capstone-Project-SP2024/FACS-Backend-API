using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class AlarmConfigurationService : IAlarmConfigurationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AlarmConfigurationService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> AddNewConfiguration(AddAlarmConfigurationRequest request)
        {
            AlarmConfiguration alarm = _mapper.Map<AlarmConfiguration>(request);
          await _unitOfWork.AlarmConfigurationRepository.AddAlarmConfiguration(alarm);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<IEnumerable<AlarmConfiguration>> GetAlarmConfigurations()
        {
           return  await _unitOfWork.AlarmConfigurationRepository.GetAlarmConfigurations();
        }

        public async Task<bool> UpdateAlarmConfiguration(int AlarmConfigurationId, AddAlarmConfigurationRequest request)
        {
           AlarmConfiguration alarm = await _unitOfWork.AlarmConfigurationRepository.GetAlarmConfigurationDetail(AlarmConfigurationId);
           _mapper.Map<AlarmConfiguration,AddAlarmConfigurationRequest>(alarm, request);
            await _unitOfWork.AlarmConfigurationRepository.UpdateAlarmConfiguration(alarm);
            return true;
        }
    }
}

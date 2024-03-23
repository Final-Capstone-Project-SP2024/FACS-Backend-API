using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class AlarmService : IAlarmService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AlarmService(IUnitOfWork unitOfWork,IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<string> RateAlarm(RateAlarmRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> TakeAlarm(TakeAlarmRequest request)
        {
            Record record = _mapper.Map<Record>(request);
            //1. Push notifications 
            CloudMessagingHandlers.CloudMessaging();
            // 2. Save Media Record to System 
            await SaveMediaFileInStorage(request.PictureUrl.ToString());
            await SaveMediaFileInStorage(request.VideoUrl.ToString());
            await SaveRecord(record);

            return true;
        }

        public Task<string> TakeElectricalBreakdownAlarm(ElectricalBreakdownRequest request)
        {
            throw new NotImplementedException();
        }


        public  async Task<bool> SaveMediaFileInStorage(string urlInput)
        {
            if (urlInput.Contains(".mp4"))
            {
                MediaRecord mediaRecord = new MediaRecord()
                {
                    Url = urlInput,
                    MediaTypeId = 1
                };
                _unitOfWork.MediaRecordRepository.InsertAsync(mediaRecord);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                MediaRecord mediaRecord = new MediaRecord()
                {
                    Url = urlInput,
                    MediaTypeId = 2
                };
                _unitOfWork.MediaRecordRepository.InsertAsync(mediaRecord);
                await _unitOfWork.SaveChangeAsync();
            }

            return true;
        }


        public async Task<bool> SaveRecord(Record record)
        {
             _unitOfWork.RecordRepository.InsertAsync(record);
            return await _unitOfWork.SaveChangeAsync() > 0;

        }
    }
}

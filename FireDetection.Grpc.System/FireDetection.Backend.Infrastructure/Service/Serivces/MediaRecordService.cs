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
    public class MediaRecordService : IMediaRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MediaRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public async Task<Guid> AddImage(string urlImage,Guid recordId)
        {
            MediaRecord mediaRecord = new MediaRecord()
            {
                MediaTypeId = 2,
                Url = urlImage,
                RecordId = recordId
            };
            _unitOfWork.MediaRecordRepository.InsertAsync(mediaRecord);
            await _unitOfWork.SaveChangeAsync();

            return GetByUrl(urlImage).Result.Id;
        
        }

        public async Task<Guid> Addvideo(string urlVideo,Guid recordId)
        {
            MediaRecord mediaRecord = new MediaRecord()
            {
                MediaTypeId = 2,
                Url = urlVideo,
                RecordId = recordId
            };
            _unitOfWork.MediaRecordRepository.InsertAsync(mediaRecord);
            await _unitOfWork.SaveChangeAsync();

            return GetByUrl(urlVideo).Result.Id;
        }

        private async Task<MediaRecord> GetByUrl(string url)
        {
            return  _unitOfWork.MediaRecordRepository.Where(x => x.Url == url).FirstOrDefault();
        }
    }
}

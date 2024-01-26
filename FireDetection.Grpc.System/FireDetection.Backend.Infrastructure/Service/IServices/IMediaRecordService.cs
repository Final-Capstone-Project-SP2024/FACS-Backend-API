using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IMediaRecordService
    {
         Task<Guid> AddImage(string urlImage, Guid recordId);

        Task<Guid> Addvideo(string urlVideo, Guid recordId);

    }
}

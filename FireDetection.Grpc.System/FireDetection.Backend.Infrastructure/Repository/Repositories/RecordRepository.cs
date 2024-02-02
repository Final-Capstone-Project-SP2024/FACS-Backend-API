using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Response;
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
    public class RecordRepository : GenericRepository<Record>, IRecordRepository
    {
        private readonly FireDetectionDbContext _context;
        public RecordRepository(FireDetectionDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<RecordResponse> Get()
        {
            var result = CompiledQuery(_context);
            return result;
        }

        public RecordDetailResponse RecordDetailResponse(Guid recordId)
        {
           return GetRecordCompiledQuery(_context, recordId);
        }

        private static readonly Func<FireDetectionDbContext, IEnumerable<RecordResponse>> CompiledQuery =
            EF.CompileQuery(
                (FireDetectionDbContext context) =>
                    context.Cameras
                        .OrderByDescending(x => x.CreatedDate).Select(camera => new RecordResponse
                        {
                            CameraId = camera.Id,
                            cameraFollow = camera.Records.Select(record => new CameraFollow
                            {
                                CreatedDate = record.CreatedDate,
                                RecordId = record.Id,
                                RecordTypeId = record.RecordTypeID
                            }).ToList()
                        }));

        private static readonly Func<FireDetectionDbContext, Guid, RecordDetailResponse?> GetRecordCompiledQuery =
      EF.CompileQuery(
          (FireDetectionDbContext context, Guid recordId) =>
              context.Records
                  .Where(x => x.Id == recordId)
                  .Select(x => new RecordDetailResponse
                  {
                      CameraId = x.CameraID,
                      CameraDestination = x.Camera.CameraDestination,
                      RecordId = x.Id,
                      PredictedPercent = x.PredictedPercent,
                      UserRatingPercent = x.UserRatingPercent,
                      Status = x.Status,
                      ImageRecord = x.MediaRecords
                          .Where(m => m.MediaTypeId == 2 && m.RecordId == recordId)
                          .Select(m => new ImageRecord { VideoUrl = m.Url })
                          .FirstOrDefault(),
                      VideoRecord = x.MediaRecords
                          .Where(m => m.MediaTypeId == 1 && m.RecordId == recordId)
                          .Select(m => new VideoRecord { VideoUrl = m.Url })
                          .FirstOrDefault(),
                  })
                  .FirstOrDefault());

    }
}

using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.Service.Serivces;
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
            //var result = CompiledQuery(_context);
            return null; //result;
        }

        public async  Task<RecordDetailResponse> RecordDetailResponse(Guid recordId)
        {
            return GetRecordCompiledQuery(_context, recordId);
        }

        public async Task<IEnumerable<NotificationAlarmResponse>> NotificationAlarmResponse()
        {
            return GetRecordInAlarm(_context).AsEnumerable();
        }

        private static readonly Func<FireDetectionDbContext, IEnumerable<NotificationAlarmResponse>> GetRecordInAlarm =
             EF.CompileQuery(
         (FireDetectionDbContext context) =>
             context.Records.Include(x => x.Camera).Include(x => x.Camera.Location)
                 .Where(x => x.Status == RecordState.InAlram || x.Status  == RecordState.EndVote || x.Status == RecordState.InVote || x.Status == RecordState.InAction)
                 .Select(record => new NotificationAlarmResponse
                 {
                     CameraDestination = record.Camera.CameraDestination,
                     CameraName = record.Camera.CameraName,
                     LocationName = record.Camera.Location.LocationName,
                     RecordId = record.Id,
                     Status = record.Status
                 }));

        //private static readonly Func<FireDetectionDbContext, IEnumerable<RecordResponse>> CompiledQuery =
        //    EF.CompileQuery(
        //        (FireDetectionDbContext context) =>
        //            context.Cameras
        //                .OrderByDescending(x => x.CreatedDate).Select(camera => new RecordResponse
        //                {
        //                    CameraId = camera.Id,
        //                    RecordFollows = camera.Records.Select(record => new RecordFollows
        //                    {
        //                        CreatedDate = record.CreatedDate,
        //                        RecordId = record.Id,
        //                        RecordTypeId = record.RecordTypeID
        //                    }).ToList()
        //                }));

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
                      RatingResult = 1,
                      userRatings = x.AlarmRates
                                      .Where(x => x.RecordID == recordId)
                                      .Select(m => new UserRating { Rating = m.LevelID, userId = m.UserID })
                                      .ToList(),
                      userVoting = x.RecordProcesses
                                    .Where(x => x.RecordID == recordId)
                                    .Select(m => new UserVoting { userId = m.UserID, VoteLevel = m.ActionTypeId, VoteType = m.ActionType.ActionName })
                                    .ToList(),
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

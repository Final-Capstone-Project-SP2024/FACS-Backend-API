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

        public async Task<RecordDetailResponse> RecordDetailResponse(Guid recordId)
        {
            return GetRecordCompiledQuery(_context, recordId);
        }

        public async Task<IEnumerable<NotificationAlarmResponse>> NotificationAlarmResponse()
        {
            return GetRecordInAlarm(_context).AsEnumerable();
        }

        public async Task<IEnumerable<NotificationAlarmResponse>> NotificationDisconnected()
        {
            return GetDisconectedRecordInAlarm(_context).AsEnumerable();
        }

        public async Task<string> GetLocationName(Guid recordId)
        {
            return GetLocationNameDb(_context, recordId).FirstOrDefault().ToString();
        }

        private static readonly Func<FireDetectionDbContext, Guid, IEnumerable<string>> GetLocationNameDb = EF.CompileQuery(
           (FireDetectionDbContext context, Guid recordId) =>
           context.Records.Include(x => x.Camera).ThenInclude(x => x.Location).Where(x => x.Id == recordId).Select(x => x.Camera.Location.LocationName));

        private static readonly Func<FireDetectionDbContext, IEnumerable<NotificationAlarmResponse>> GetDisconectedRecordInAlarm =
        EF.CompileQuery(
    (FireDetectionDbContext context) =>
        context.Records.Include(x => x.Camera).Include(x => x.Camera.Location)
            .Where(x => x.RecordTypeID == 2)
            .Select(record => new NotificationAlarmResponse
            {
                RecordType = record.RecordTypeID,
                CameraId = record.CameraID,
                occurrenceTime = record.RecordTime.ToString("HH:mm:ss dd-MM-yyyy"),
                CameraDestination = record.Camera.CameraDestination,
                CameraName = record.Camera.CameraName,
                LocationName = record.Camera.Location.LocationName,
                RecordId = record.Id,
                Status = record.Status
            }));

        private static readonly Func<FireDetectionDbContext, IEnumerable<NotificationAlarmResponse>> GetRecordInAlarm =
             EF.CompileQuery(
         (FireDetectionDbContext context) =>
             context.Records.Include(x => x.Camera).Include(x => x.Camera.Location)
                 .Where(x => x.Status != RecordState.InFinish)
                 .Select(record => new NotificationAlarmResponse
                 {
                     CameraId = record.CameraID,
                     CameraDestination = record.Camera.CameraDestination,
                     CameraName = record.Camera.CameraName,
                     LocationName = record.Camera.Location.LocationName,
                     RecordId = record.Id,
                     Status = record.Status,
                     occurrenceTime = record.RecordTime.ToString("HH:mm:ss dd-MM-yyyy"),
                     RecordType = record.RecordTypeID
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
                      CameraName = x.Camera.CameraName,
                      RecommendAlarmLevel = x.RecommendAlarmLevel,
                      RecordTime = x.RecordTime.ToString("HH:mm:ss dd-MM-yyyy"),
                      FinishTime = x.FinishAlarmTime.ToString("HH:mm:ss dd-MM-yyyy"),
                      Status = x.Status,
                      RecordType = x.RecordTypeID,
                      userRatings = x.AlarmRates
                                      .Where(x => x.RecordID == recordId)
                                      .Select(m => new UserRating { SecurityCode = m.User.SecurityCode, Rating = m.LevelID, userId = m.UserID, Name = m.Level.Name })
                                      .ToList(),
                      userVoting = x.RecordProcesses
                                    .Where(x => x.RecordID == recordId)
                                    .Select(m => new UserVoting { SecurityCode = m.User.SecurityCode, userId = m.UserID, VoteLevel = m.ActionTypeId, VoteType = m.ActionType.ActionName })
                                    .ToList(),
                      ImageRecord = x.MediaRecords
                          .Where(m => m.MediaTypeId == 2 && m.RecordId == recordId)
                          .Select(m => new ImageRecord { ImageUrl = m.Url })
                          .FirstOrDefault(),
                      VideoRecord = x.MediaRecords
                          .Where(m => m.MediaTypeId == 1 && m.RecordId == recordId)
                          .Select(m => new VideoRecord { VideoUrl = m.Url })
                          .FirstOrDefault(),

                  })
                  .FirstOrDefault());

    }
}

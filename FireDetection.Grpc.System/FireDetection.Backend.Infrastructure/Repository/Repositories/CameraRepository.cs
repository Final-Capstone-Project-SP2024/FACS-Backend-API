﻿using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class CameraRepository : GenericRepository<Camera>, ICameraRepository
    {
        private readonly FireDetectionDbContext _context;
        public CameraRepository(FireDetectionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Guid>> GetUsersByCameraId(Guid cameraId)
        {
            var result = _context.Cameras
                                .Where(c => c.Id == cameraId)
                                .SelectMany(c => c.Location.ControlCameras.Select(cc => cc.UserID))
                                .ToList();

            return result;
        }

        public async Task<IQueryable<CameraInformationResponse>> GetAllViewModel()
        {
            return GetCameras(_context).AsQueryable();
        }

        public async Task<string> HighRiskFireDetectByCamera()
        {
            var mostFrequentCameraID = _context.Records
                                        .GroupBy(x => x.CameraID)
                                         .Select(g => new { CameraID = g.Key, Count = g.Count() }) // Count within the grouping
                                         .OrderByDescending(x => x.Count)
                                          .FirstOrDefault()?.CameraID;

            return _context.Cameras.Where(x => x.Id == mostFrequentCameraID ).FirstOrDefault().CameraName;
        }

        public async Task DeleteCamera(Guid locationId)
        {
            _context.Cameras.Where(x => x.LocationID == locationId).ExecuteUpdate(x => x.SetProperty(x => x.Status, CameraType.Disconnect));
            _context.ControlCameras.Where(x => x.LocationID == locationId).ExecuteDelete();
            await _context.SaveChangesAsync();
        }

        public async Task ActiveCamera(Guid locationId)
        {
            _context.Cameras.Where(x => x.LocationID == locationId).ExecuteUpdate(x => x.SetProperty(x => x.Status, CameraType.Connect));
            await _context.SaveChangesAsync();
        }

        private static readonly Func<FireDetectionDbContext, IEnumerable<CameraInformationResponse>>
            GetCameras =
            EF.CompileQuery(
                (FireDetectionDbContext context) =>
                context.Cameras.Select(x => new CameraInformationResponse
                {
                    CameraDestination = x.CameraDestination,
                    CameraId = x.Id,
                    CameraName = x.CameraName,
                    Status = x.Status,
                    CameraImage = x.CameraImage,
                    LocationName = x.Location.LocationName
                    
                }));
    }
}

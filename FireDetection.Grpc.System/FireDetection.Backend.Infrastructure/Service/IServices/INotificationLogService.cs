﻿using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface INotificationLogService
    {
        public Task SaveNotificationFireNotifyLog(Guid recordId, int count);
        public void SaveNotificationVotingRequire(Guid recordId,int count);
        public void SaveNotificationActionRequire(Guid recordId,int count ,CacheType type);
        public Task<FireDetectionAnalysis> Analysis();
        Task<string> GetInDay();

        Task<string> GetInMonth();

        Task<string> GetInYear();

        Task<string> GetInWeek();

        Task<string> GetLocationAnalysis();
    }
}

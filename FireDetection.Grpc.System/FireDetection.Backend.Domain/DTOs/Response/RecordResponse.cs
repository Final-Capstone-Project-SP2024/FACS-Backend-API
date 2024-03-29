﻿using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    //? base class
    public class RecordResponse
    {
        //public Guid CameraId { get; set; }
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime RecordTime { get; set; }
        public decimal UserRatingPercent { get; set; }
        public decimal PredictedPercent { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<AlarmRatesResponse> UserRatings { get; set; } = new List<AlarmRatesResponse>(); //UserRatings
        public List<RecordProcessResponse> UserVotings { get; set; } = new List<RecordProcessResponse>(); //UserVotings
        public RecordTypeResponse? RecordType { get; set; }
        public List<NotificationLogResponse>? NotificationLogs { get; set; } = new List<NotificationLogResponse>();
    }


}

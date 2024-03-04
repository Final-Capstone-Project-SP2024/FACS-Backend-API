﻿using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class RecordResponse
    {
        //public Guid CameraId { get; set; }
        public Guid Id { get; set; }
        public DateTime RecordTime { get; set; }
        public decimal UserRatingPercent { get; set; }
        public decimal PredictedPercent { get; set; }
        public DateTime CreatedDate { get; set; }
        public RecordTypeResponse? RecordType { get; set; }
    }

}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class RecordDetailResponse
    {
        public Guid CameraId { get; set; }

        public string CameraDestination { get; set; } = string.Empty;

        public int RatingResult { get; set; } = 0;
        public Guid RecordId { get; set; }

        public decimal UserRatingPercent { get; set; }

        public decimal PredictedPercent { get; set; }

        public string Status { get; set; }

        public VideoRecord VideoRecord { get; set; } = null!;

        public ImageRecord ImageRecord { get; set; } = null!;

        public List<UserRating> userRatings { get; set; } = null!;

        public List<UserVoting> userVoting { get; set; } = null!;
    }

    public class VideoRecord
    {
        public string? VideoUrl { get; set; }
    }


    public class ImageRecord
    {
        public string? VideoUrl { get; set; }
    }


    public class UserRating
    {
        public Guid userId { get; set; }

        public int Rating { get; set;}
    }


    public class UserVoting
    {
        public Guid userId { get; set; }

        public int VoteLevel { get; set; }

        public string VoteType { get; set; } = null!;
    }


    
}

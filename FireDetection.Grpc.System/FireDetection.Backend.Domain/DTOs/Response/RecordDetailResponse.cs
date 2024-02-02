using System;
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

        public Guid RecordId { get; set; }

        public decimal UserRatingPercent { get; set; }

        public decimal PredictedPercent { get; set; }

        public string Status { get; set; }

        public VideoRecord VideoRecord { get; set; } = null!;

        public ImageRecord ImageRecord { get; set; } = null!;
    }

    public class VideoRecord
    {
        public string? VideoUrl { get; set; }
    }


    public class ImageRecord
    {
        public string? VideoUrl { get; set; }
    }
}

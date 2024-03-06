using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class NotificationLogResponse
    {
        public int Count { get; set; }
        public NotificationTypeResponse? notificationType { get; set; }
    }
}

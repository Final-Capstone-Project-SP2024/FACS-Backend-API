using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class NotificationType
    {
        [Key]
        public int NotificationTypeId { get; set; }

        public string Name { get; set; }

        public ICollection<NotificationLog> Log { get; set; }
              
    }
}

using FireDetection.Backend.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.FluentAPIs
{
    public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationType>
    {
        public void Configure(EntityTypeBuilder<NotificationType> builder)
        {
            builder.HasData(new[]{new NotificationType { NotificationTypeId = 1, Name = "FireNotify" }, new NotificationType { NotificationTypeId = 2, Name = "Voting" },
                new NotificationType { NotificationTypeId = 3, Name = "Alarm Level 1" }, new NotificationType { NotificationTypeId = 4, Name = "Alarm Level 2" },
                new NotificationType { NotificationTypeId = 5, Name = "Alarm Level 3 " }, new NotificationType { NotificationTypeId = 6, Name = "Alarm Level 4" },
                new NotificationType { NotificationTypeId = 7, Name = "Alarm Level 5"}, new NotificationType { NotificationTypeId = 8, Name = "Fake Alarm"},
                new NotificationType { NotificationTypeId = 9 , Name ="Disconnect Camera"}});

            builder.HasMany(x => x.Log).WithOne(x => x.notificationType).HasForeignKey(x => x.NotificationTypeId);
        }
    }
}

using FireDetection.Backend.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain
{
    public class FireDetectionDbContext : DbContext
    {
        public FireDetectionDbContext(DbContextOptions<FireDetectionDbContext> options) : base(options)
        {

        }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Record> Records { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<RecordProcess> RecordProcesses { get; set; }

        public DbSet<MediaType> MediaTypes { get; set; }

        public DbSet<Level> Levels { get; set; }
        public DbSet<MediaRecord> MediaRecords { get; set; }

        public DbSet<ControlCamera> ControlCameras { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<AlarmRate> AlarmRates { get; set; }    
        public DbSet<NotificationLog>  NotificationLogs { get; set; }   
        public DbSet<RecordType> RecordTypes { get; set; }

        public DbSet<BugsReport> BugsReports { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<ManualPlan> ManualPlans { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}

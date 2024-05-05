using FireDetection.Backend.Domain.DTOs.State;
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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(book => book.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.ControlCameras).WithOne(x => x.User).HasForeignKey(x => x.UserID);
            builder.HasMany(x => x.RecordProcesses).WithOne(x => x.User).HasForeignKey(x => x.UserID);
            builder.HasMany(x => x.UserReponsibilities).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasData(new User
            {
                Name = "Admin",
                Password = "NJt3DCzVWSRDN7SigMcj+v/M8v+OWeZPBW/lApGrc+thCg3X",
                Email ="Admin@gmail.com",
                Id = Guid.Parse("3c9a2a1b-f4dc-4468-a89c-f6be8ca3b541"),
                Phone = "0902311453",
                RoleId = 1,
                SecurityCode = "XAD_000",
                IsDeleted = false,
                Status = UserState.Active
            }
                );


        }
    }
}

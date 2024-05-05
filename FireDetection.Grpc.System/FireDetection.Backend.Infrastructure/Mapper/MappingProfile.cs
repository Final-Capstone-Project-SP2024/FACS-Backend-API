using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using Location = FireDetection.Backend.Domain.Entity.Location;

namespace FireDetection.Backend.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CameraInLocation, Camera>()
                .ForMember(x => x.Id, src => src.MapFrom(x => x.CameraId))
                .ForMember(x => x.CameraName, src => src.MapFrom(x => x.CameraName))
                .ForMember(x => x.CameraDestination, src => src.MapFrom(x => x.CameraDestination))
                .ForMember(x => x.CameraImage, src => src.MapFrom(x => x.CameraImage))
                .ForMember(x => x.Status, src => src.MapFrom(x => x.CameraStatus))
                .ReverseMap();

            CreateMap<CreateUserRequest, User>()
                .ForMember(x => x.Email, src => src.MapFrom(x => x.Email))
                .ForMember(x => x.Phone, src => src.MapFrom(x => x.Phone))
                .ForMember(x => x.Name, src => src.MapFrom(x => x.Name))
                .ForMember(x => x.Password, src => src.MapFrom(x => x.Password))
                .ForMember(x => x.RoleId, src => src.MapFrom(x => x.UserRole))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.Status, src => src.MapFrom(x => "actived"))
                .ReverseMap();

            CreateMap<User, UserInformationResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SecurityCode, opt => opt.MapFrom(src => src.SecurityCode))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForPath(dest => dest.Role.RoleName,opt => opt.MapFrom(src => src.Role.RoleName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.ControlCameras.FirstOrDefault().Location.LocationName))
                .ReverseMap();


            CreateMap<User, UserRequest>().ReverseMap();

            CreateMap<UserInformationResponse, UserRequest>().ReverseMap();

            CreateMap<Role, RoleResponse>().ForMember(x => x.RoleName, opt => opt.MapFrom(src => src.RoleName)).ReverseMap();

            CreateMap<Record, RecordRequest>().ReverseMap();

            CreateMap<Record, RecordResponse>()
                .ForMember(x => x.UserVotings, src => src.MapFrom(x => x.RecordProcesses))
                .ReverseMap();

            CreateMap<RecordType, RecordTypeResponse>()
                .ForMember(x => x.RecordTypeId, src => src.MapFrom(x => x.RecordTypeId))
                .ForMember(x => x.RecordTypeName, src => src.MapFrom(x => x.Name))
                .ReverseMap();

            CreateMap<AddCameraRequest, Camera>()
                .ForMember(x => x.CameraDestination, src => src.MapFrom(x => x.Destination))
                .ForMember(x => x.CameraName, src => src.MapFrom(x => x.CameraName))
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
                .ForMember(x => x.LocationID, src => src.MapFrom(x => x.LocationId))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                 .ForMember(x => x.CameraImage, src => src.MapFrom(x => x.CameraImage.FileName.ToString()))
                .ReverseMap();
            CreateMap<AddCameraRequest, Camera>()
              .ForMember(x => x.CameraDestination, src => src.MapFrom(x => x.Destination))
              .ForMember(x => x.CameraName, src => src.MapFrom(x => x.CameraName))
              .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
              .ForMember(x => x.LocationID, src => src.MapFrom(x => x.LocationId))
              .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
              .ReverseMap();

            CreateMap<CameraInformationResponse, Camera>()
                .ForMember(x => x.CameraDestination, src => src.MapFrom(x => x.CameraDestination))
                .ForMember(x => x.CameraName, src => src.MapFrom(x => x.CameraName))
                .ForMember(x => x.Id, src => src.MapFrom(x => x.CameraId))
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.CameraImage, src => src.MapFrom(x => x.CameraImage))
                .ReverseMap();

            CreateMap<Location, LocationInformationResponse>()
                .ForMember(x => x.LocationId, src => src.MapFrom(x => x.Id))
                .ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.CreatedDate))
                .ReverseMap();

            CreateMap<AddLocationRequest, Location>()
                .ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName))
                 .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                  .ForMember(x => x.LocationImage, src => src.MapFrom(x => x.LocationImage.FileName.ToString()))
                .ReverseMap();

            CreateMap<UpdateLocationRequest, Location>()
               .ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
               .ReverseMap();


            CreateMap<TakeAlarmRequest, Record>()
                .ForMember(x => x.Status, src => src.MapFrom(x => "InAlarm"))
                .ForMember(x => x.PredictedPercent, src => src.MapFrom(x => x.PredictedPercent))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow.AddHours(7)))
                .ForMember(x => x.RecordTime, src => src.MapFrom(x => DateTime.UtcNow.AddHours(7)))
                 .ForMember(x => x.RecordTypeID, src => src.MapFrom(x => 1))
                .ReverseMap();

            CreateMap<DetectResponse, Record>()
                .ForMember(x => x.Id, src => src.MapFrom(x => x.RecordId))
                .ForMember(x => x.CameraID, src => src.MapFrom(x => x.CameraId))
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
                 .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.Createdate))
                  .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
                .ReverseMap();


            CreateMap<TakeElectricalIncidentRequest, Record>()
                 .ForMember(x => x.RecordTime, src => src.MapFrom(x => x.Time))
                   .ForMember(x => x.Status, src => src.MapFrom(x => "InAlarm"))
                     .ForMember(x => x.RecordTypeID, src => src.MapFrom(x => 2))
                  .ReverseMap();
            CreateMap<AddRecordActionRequest, RecordProcess>()
                .ForMember(x => x.ActionTypeId, src => src.MapFrom(x => x.ActionId))
                .ForMember(x => x.UserID, src => src.MapFrom(x => x.UserID))
                .ReverseMap();

            //CreateMap<AlarmRate, UserRating>
            //    .ForMember(x => x.LevelID, src => src.MapFrom(x => x.LevelRating))
            //    .ForMember(x => x.UserID, src => src.MapFrom(x => x.UserId))
            //    .ReverseMap();

            CreateMap<NotificationLog, NotificationLogResponse>()
                .ForMember(x => x.Count, src => src.MapFrom(x => x.Count))
                .ForMember(x => x.notificationType, src => src.MapFrom(x => x.notificationType))
                .ReverseMap();

            CreateMap<NotificationType, NotificationTypeResponse>()
                .ReverseMap();

     
            CreateMap<RecordProcess, RecordProcessResponse>()
                .ForMember(x => x.UserId, src => src.MapFrom(x => x.UserID))
                .ForMember(x => x.VoteLevel, src => src.MapFrom(x => x.ActionTypeId))
                .ForMember(x => x.VoteType, src => src.MapFrom(x => x.ActionType))
                .ReverseMap();

            CreateMap<ActionType, ActionProcessResponse>()
                .ForMember(x => x.ActionName, src => src.MapFrom(x => x.ActionName))
                .ReverseMap();

            //CreateMap<RecordProcess, RecordProcessResponse>()
            //    .ForMember(x => x.ActionId, src => src.MapFrom(x => x.ActionTypeId))

            CreateMap<UpdateUserRequest, User>()
                .ForMember(x => x.Name, src => src.MapFrom(x => x.Name))
                .ForMember(x => x.Email, src => src.MapFrom(x => x.Email))
                .ForMember(x => x.Phone, src => src.MapFrom(x => x.Phone))
                .ReverseMap();



            CreateMap<LocationGeneralResponse, Location>()
                   .ForMember(x => x.Id, src => src.MapFrom(x => x.LocationId))
                   .ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName))
                    .ForMember(x => x.LocationImage, src => src.MapFrom(x => x.LocationImage))
                   .ReverseMap();


            CreateMap<AddAlarmConfigurationRequest, AlarmConfiguration>()
                 .ForMember(x => x.Start, src => src.MapFrom(x => x.Start))
                  .ForMember(x => x.End, src => src.MapFrom(x => x.End))
                .ReverseMap();

            //CreateMap<Location, LocationResponse>().ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName)).ReverseMap();

        }
    }
}

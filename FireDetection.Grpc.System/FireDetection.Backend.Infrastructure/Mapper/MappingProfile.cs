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
                .ForMember(x => x.SecurityCode, src => src.MapFrom(x => x.SecurityCode))
                .ForMember(x => x.Email, src => src.MapFrom(x => x.Email))
                .ForMember(x => x.Name, src => src.MapFrom(x => x.Name))
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
                .ReverseMap();

            CreateMap<User, UserRequest>().ReverseMap();

            CreateMap<UserInformationResponse, UserRequest>().ReverseMap();

            CreateMap<Record, RecordRequest>().ReverseMap();
            CreateMap<Record, RecordResponse>().ReverseMap();
            CreateMap<RecordType, RecordTypeResponse>()
                .ForMember(x => x.RecordTypeId, src => src.MapFrom(x => x.RecordTypeId))
                .ForMember(x => x.RecordTypeName, src => src.MapFrom(x => x.Name))
                .ReverseMap();


            CreateMap<Camera, AddCameraRequest>()
                .ForMember(x => x.Destination, src => src.MapFrom(x => x.CameraDestination))
                .ForMember(x => x.CameraName, src => src.MapFrom(x => x.CameraName))
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
                .ForMember(x => x.LocationId, src => src.MapFrom(x => x.LocationID))
                .ReverseMap();

            CreateMap<CameraInformationResponse, Camera>()
                .ForMember(x => x.CameraDestination, src => src.MapFrom(x => x.CameraDestination))
                .ForMember(x => x.CameraName, src => src.MapFrom(x => x.CameraName))
                .ForMember(x => x.Id, src => src.MapFrom(x => x.CameraId))
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                .ReverseMap();

            CreateMap<Location, LocationInformationResponse>()
                .ForMember(x => x.LocationId, src => src.MapFrom(x => x.Id))
                .ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.CreatedDate))
                .ReverseMap();

            CreateMap<AddLocationRequest, Location>()
                .ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName))
                 .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                .ReverseMap();


            CreateMap<TakeAlarmRequest, Record>()
                .ForMember(x => x.Status, src => src.MapFrom(x => "InAlarm"))
                .ForMember(x => x.PredictedPercent, src => src.MapFrom(x => x.PredictedPercent))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.RecordTime, src => src.MapFrom(x => DateTime.UtcNow))
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


            CreateMap<RateAlarmRequest, AlarmRate>()
                 .ForMember(x => x.Time, src => src.MapFrom(x => DateTime.UtcNow))
                 .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                 .ForMember(x => x.LevelID, src => src.MapFrom(x => x.LevelRating))
                 .ForMember(x => x.UserID, src => src.MapFrom(x => x.UserId))
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

            //CreateMap<RecordProcess, RecordProcessResponse>()
            //    .ForMember(x => x.ActionId, src => src.MapFrom(x => x.ActionTypeId))
        }

    }
}

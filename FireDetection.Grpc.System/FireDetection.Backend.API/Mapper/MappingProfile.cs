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
                .ReverseMap();

            CreateMap<User, UserInformationResponse>()
                .ForMember(x => x.SecurityCode, src => src.MapFrom(x => x.SecurityCode))
                .ForMember(x => x.Email, src => src.MapFrom(x => x.Email))
                .ForMember(x => x.Name, src => src.MapFrom(x => x.Name))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.CreatedDate))
                .ReverseMap();


            CreateMap<Camera, AddCameraRequest>()
                .ForMember(x => x.Destination, src => src.MapFrom(x => x.CameraDestination))
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
                .ForMember(x => x.LocationId, src => src.MapFrom(x => x.LocationID))
                .ReverseMap();

            CreateMap<CameInformationResponse, Camera>()
                .ForMember(x => x.CameraDestination, src => src.MapFrom(x => x.CameraDestination))
                .ForMember(x => x.Id, src => src.MapFrom(x => x.CameraId))
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                .ReverseMap();

            CreateMap<Location, LocationInformationResponse>()
                .ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.CreatedDate))
                .ReverseMap();

            CreateMap<Location, AddLocationRequest>()
                .ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName))
                .ReverseMap();


            CreateMap<TakeAlarmRequest, Record>()
                .ForMember(x => x.CameraID, src => src.MapFrom(x => x.CameraId))
                .ForMember(x => x.Status, src => src.MapFrom(x => "InAlarm"))
                .ForMember(x => x.PredictedPercent, src => src.MapFrom(x => x.PredictedPercent))
                .ForMember(x => x.RecordTime, src => src.MapFrom(x => x.Time))
                 .ForMember(x => x.RecordTypeID, src => src.MapFrom(x => 1))
                .ReverseMap();

            CreateMap<DetectFireResponse, Record>()
                .ForMember(x => x.CameraID, src => src.MapFrom(x => x.CameraId))
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
                 .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.Createdate))
                  .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
                .ReverseMap();


            CreateMap<TakeElectricalIncidentRequest, Record>()
                 .ForMember(x => x.RecordTime, src => src.MapFrom(x => x.Time))
                  .ForMember(x => x.CameraID, src => src.MapFrom(x => x.CameraID))
                   .ForMember(x => x.Status, src => src.MapFrom(x => "InAlarm"))
                     .ForMember(x => x.RecordTypeID, src => src.MapFrom(x => 2))
                  .ReverseMap();


            CreateMap<RateAlarmRequest, AlarmRate>()
                 .ForMember(x => x.Time, src => src.MapFrom(x => DateTime.UtcNow))
                 .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                 .ForMember(x => x.LevelID, src => src.MapFrom(x => x.Level))
                 .ForMember(x => x.UserID, src => src.MapFrom(x => x.UserId))
                   .ReverseMap();

            CreateMap<AddRecordActionRequest, RecordProcess>()
                .ForMember(x => x.ActionTypeId, src => src.MapFrom(x => x.ActionId))
                .ForMember(x => x.UserID, src => src.MapFrom(x => x.UserID))
                .ReverseMap();
        }

    }
}

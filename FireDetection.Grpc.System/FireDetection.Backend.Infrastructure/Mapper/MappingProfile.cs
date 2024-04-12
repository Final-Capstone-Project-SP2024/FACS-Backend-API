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
                .ForMember(x => x.UserRatings, src => src.MapFrom(x => x.AlarmRates))
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

            CreateMap<AlarmRate, AlarmRatesResponse>()
                .ForMember(x => x.UserId, src => src.MapFrom(x => x.UserID))
                .ForMember(x => x.Rating, src => src.MapFrom(x => x.LevelID))
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

            CreateMap<AddContractRequest, Contract>()
                      .ForMember(x => x.StartDate, src => src.MapFrom(x => x.StartDate))
                        .ForMember(x => x.EndDate, src => src.MapFrom(x => x.EndDate))
                          .ForMember(x => x.isActive, src => src.MapFrom(x => x.IsActive))
                            .ForMember(x => x.isPaid, src => src.MapFrom(x => x.IsPaid))
                              .ForMember(x => x.TotalPrice, src => src.MapFrom(x => x.TotalPrice))
                                .ForMember(x => x.ContractImage, src => src.MapFrom(x => x.ContractImage))
                                  .ForMember(x => x.ManualPlanID, src => src.MapFrom(x => x.ManualPlanType))
                                    .ForMember(x => x.UserID, src => src.MapFrom(x => x.UserId))
                                      .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                    .ReverseMap();


            CreateMap<ContractDetailResponse, Contract>()
                .ForMember(x => x.StartDate, src => src.MapFrom(x => x.StartDate))
                .ForMember(x => x.EndDate, src => src.MapFrom(x => x.EndDate))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.CreatedDate))
                .ForMember(x => x.ManualPlanID, src => src.MapFrom(x => x.ManualTypeID))
                .ForMember(x => x.Id, src => src.MapFrom(x => x.ContractID))
                .ForMember(x => x.isActive, src => src.MapFrom(x => x.isActive))
                .ForMember(x => x.isPaid, src => src.MapFrom(x => x.isPaid))
                .ForMember(x => x.TotalPrice, src => src.MapFrom(x => x.TotalPrice))
                .ReverseMap();


            CreateMap<ContractGeneralResponse, Contract>()
              .ForMember(x => x.Id, src => src.MapFrom(x => x.ContractID))
              .ForMember(x => x.isActive, src => src.MapFrom(x => x.isActive))
              .ForMember(x => x.isPaid, src => src.MapFrom(x => x.isPaid))
              .ForMember(x => x.TotalPrice, src => src.MapFrom(x => x.TotalPrice))
              .ReverseMap();


            CreateMap<UpdateContractRequest, Contract>()
                  .ForMember(x => x.EndDate, src => src.MapFrom(x => x.EndDate))
                  .ForMember(x => x.isPaid, src => src.MapFrom(x => x.IsPaid))
                  .ForMember(x => x.TotalPrice, src => src.MapFrom(x => x.TotalPrice))
                  .ForMember(x => x.ManualPlanID, src => src.MapFrom(x => x.ManualPlanId))
                .ReverseMap();

            CreateMap<ManualPlanGeneralResponse, ManualPlan>()
                        .ForMember(x => x.ManualPlanId, src => src.MapFrom(x => x.ManualPlanId))
                        .ForMember(x => x.ManualPlanName, src => src.MapFrom(x => x.ManualPlanName))
                .ReverseMap();

            CreateMap<ManualPlanDetailResponse, ManualPlan>()
            .ForMember(x => x.ManualPlanId, src => src.MapFrom(x => x.ManualPlanId))
            .ForMember(x => x.ManualPlanName, src => src.MapFrom(x => x.ManualPlanName))
            .ForMember(x => x.LocationLimited, src => src.MapFrom(x => x.LocationLimited))
            .ForMember(x => x.UserLimited, src => src.MapFrom(x => x.UserLimited))
            .ForMember(x => x.CameraLimited, src => src.MapFrom(x => x.CameraLimited))
            .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.CreatedDate))
            .ReverseMap();


            CreateMap<AddFeedbackRequest, Feedback>()
                 .ForMember(x => x.Context, src => src.MapFrom(x => x.Context))
                 .ForMember(x => x.Rating, src => src.MapFrom(x => x.Rating))
                  .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                .ReverseMap();


            CreateMap<FeedbackResponse, Feedback>()
          .ForMember(x => x.Context, src => src.MapFrom(x => x.FeedbackContext))
          .ForMember(x => x.Rating, src => src.MapFrom(x => x.Rating))
           .ForMember(x => x.Id, src => src.MapFrom(x => x.FeedbackId))
           .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.CreatedDate))
         .ReverseMap();


            CreateMap<BugsReportRequest, BugsReport>()
                  .ForMember(x => x.Feature, src => src.MapFrom(x => x.Feature))
                   .ForMember(x => x.BugDescription, src => src.MapFrom(x => x.BugDescription))
                   .ForMember(x => x.CreatedDate, src => src.MapFrom(x => DateTime.UtcNow))
                    .ForMember(x => x.IsSolved, src => src.MapFrom(x => false))
                  .ReverseMap();

            CreateMap<BugsReportResponse, BugsReport>()
                .ForMember(x => x.Id, src => src.MapFrom(x => x.BugId))
          .ForMember(x => x.Feature, src => src.MapFrom(x => x.Feature))
           .ForMember(x => x.BugDescription, src => src.MapFrom(x => x.BugDescription))
           .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.CreatedDate))
            .ForMember(x => x.IsSolved, src => src.MapFrom(x => x.IsSolved))
          .ReverseMap();


            CreateMap<TransactionDetailResponse, Transaction>()
                .ForMember(x => x.UserID, src => src.MapFrom(x => x.UserID))
                .ForMember(x => x.ContractID, src => src.MapFrom(x => x.ContractID))
                .ForMember(x => x.ActionPlanTypeID, src => src.MapFrom(x => x.ActionPlanTypeID))
                .ForMember(x => x.PaymentTypeID, src => src.MapFrom(x => x.PaymentTypeID))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.CreatedDate))
                .ForMember(x => x.isPaid, src => src.MapFrom(x => x.isPaid))
                .ForMember(x => x.Price, src => src.MapFrom(x => x.Price))
                .ForMember(x => x.Id, src => src.MapFrom(x => x.TransactionId))
                .ReverseMap();

            CreateMap<UpdateUserRequest, User>()
                .ForMember(x => x.Name, src => src.MapFrom(x => x.Name))
                .ForMember(x => x.Email, src => src.MapFrom(x => x.Email))
                .ForMember(x => x.Phone, src => src.MapFrom(x => x.Phone))
                .ReverseMap();

            CreateMap<TransactionGeneralResponse, Transaction>()
                 .ForMember(x => x.Id, src => src.MapFrom(x => x.TransactionId))
                 .ForMember(x => x.Price, src => src.MapFrom(x => x.Price))
                 .ForMember(x => x.isPaid, src => src.MapFrom(x => x.isPaid))
                 .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.CreatedDate))
                .ReverseMap();


            CreateMap<LocationGeneralResponse, Location>()
                   .ForMember(x => x.Id, src => src.MapFrom(x => x.LocationId))
                   .ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName))
                   .ReverseMap();

            //CreateMap<Location, LocationResponse>().ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName)).ReverseMap();

        }
    }
}

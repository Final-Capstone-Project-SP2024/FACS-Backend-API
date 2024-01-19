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

            CreateMap<CreateUserRequest, User > ()
                .ForMember(x => x.Email, src => src.MapFrom(x => x.Email))
                .ForMember(x => x.SecurityCode, src => src.MapFrom(x => x.SecurityCode))
                .ForMember(x => x.Phone, src => src.MapFrom(x => x.Phone))
                .ForMember(x => x.Name, src => src.MapFrom(x => x.Name))
                .ForMember(x => x.Password, src => src.MapFrom(x => x.Password))
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
                .ReverseMap();

            CreateMap<Camera, CameInformationResponse>()
                .ForMember(x => x.CameraDestination,src => src.MapFrom(x => x.CameraDestination))
                .ForMember(x => x.CameraId, src => src.MapFrom(x => x.Id))
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status))
                .ReverseMap();

            CreateMap<Location, LocationInformationResponse>()
                .ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName))
                .ForMember(x => x.CreatedDate, src => src.MapFrom(x => x.CreatedDate))
                .ReverseMap();

            CreateMap<Location, AddLocationRequest>()
                .ForMember(x => x.LocationName, src => src.MapFrom(x => x.LocationName))
                .ReverseMap();





        }

    }
}

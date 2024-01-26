using FireDetection.Backend.Domain;
using  FireDetection.Backend.Domain.Entity;
using Location = FireDetection.Backend.Domain.Entity.Location;

namespace FireDetection.Backend.API.Middleware.GraphQL
{
    public class Query
    {
        [Serial]
        [UsePaging]
        [UseProjection]
        [UseSorting]
        public IQueryable<Location> GetLocations([Service] FireDetectionDbContext context) => context.Locations.AsQueryable();

        [Serial]
        [UsePaging]
        [UseProjection]
        [UseSorting]
        public IQueryable<User> GetUsers([Service] FireDetectionDbContext context) => context.Users.AsQueryable();


        [Serial]
        [UsePaging]
        [UseProjection]
        [UseSorting]
        public IQueryable<Camera> GetCameras([Service] FireDetectionDbContext context) => context.Cameras.AsQueryable();
    }
}

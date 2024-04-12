using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class LocationScopeService : ILocationScopeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LocationScopeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        char[] locations = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public async Task<List<Guid>> GetUserInLocation(string LocationCode, int fireLevel)
        {
            List<Guid> locationIdLisr = new List<Guid>();

            Guid locationId = _unitOfWork.LocationRepository.Where(x => x.LocationName == LocationCode).FirstOrDefault().Id;
            if (fireLevel == 1)
            {

                return _unitOfWork.ControlCameraRepository.Where(x => x.LocationID == locationId).Select(x => x.UserID).ToList();
            }
            else
            {
                return await FindUserNearbyInLevel(LocationCode, fireLevel);
            }


            return locationIdLisr;

        }
        //? with 2,3,4 level find 1,2,3 index can call be checkpoint

        internal async Task<List<Guid>> FindUserNearbyInLevel(string LocationCode, int fireLevel)
        {
            string pointIn = LocationCode.Split(' ')[1];
            int checkpoint = 0;
            object countLock = new object();
            int indexOf = Array.IndexOf(locations, char.Parse(pointIn));
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            List<Guid?> locationId = new List<Guid?>();


            for (int i = indexOf; i > 0; i--)
            {
                var location = await _unitOfWork.LocationRepository
                    .Where(x => x.LocationName == "Location " + locations[i]).FirstOrDefaultAsync();

                if (location != null)
                {
                    lock (countLock)
                    {
                        checkpoint++;
                        locationId.Add(location.Id);

                        if (checkpoint >= fireLevel)
                        {
                            cancellationTokenSource.Cancel();
                            break;
                        }
                    }
                }
            }
            for (int i = indexOf; i < 25; i++)
            {
                var location = await _unitOfWork.LocationRepository
                    .Where(x => x.LocationName == "Location " + locations[i]).FirstOrDefaultAsync();

                if (location != null)
                {
                    lock (countLock)
                    {
                        checkpoint++;
                        locationId.Add(location.Id);

                        if (checkpoint >= fireLevel)
                        {
                            cancellationTokenSource.Cancel();
                            break;
                        }
                    }
                }
            }




            // Dispose the cancellation token source
            cancellationTokenSource.Dispose();

            List<Guid> users = new List<Guid>();
            Console.WriteLine(locationId);
            foreach (var locate in locationId)
            {
                users.AddRange(_unitOfWork.ControlCameraRepository.Where(x => x.LocationID == locate).Select(x => x.UserID));
            }

            return users;
        }

        public async Task<List<UserInLocationResponse>> GetUserLocation(Guid locationId)
        {
            var usersInLocation = await _unitOfWork.ControlCameraRepository
                .Where(cc => cc.LocationID == locationId && cc.User != null)
                .Select(cc => new UserInLocationResponse
                {
                    UserID = cc.UserID,
                    Name = cc.User.Name
                })
                .ToListAsync();

            return usersInLocation;
        }
    }
}

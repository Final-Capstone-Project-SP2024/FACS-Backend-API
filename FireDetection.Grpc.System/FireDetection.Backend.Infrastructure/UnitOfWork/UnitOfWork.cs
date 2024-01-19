using FireDetection.Backend.Domain;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FireDetectionDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ICameraRepository _cameraRepository;


        public UnitOfWork(FireDetectionDbContext context,
            IUserRepository userRepository,
            ILocationRepository locationRepository,
            ICameraRepository cameraRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _cameraRepository = cameraRepository;
        }
        public IUserRepository UserRepository => _userRepository;

        public ILocationRepository LocationRepository => _locationRepository;

        public ICameraRepository CameraRepository => _cameraRepository;



        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}

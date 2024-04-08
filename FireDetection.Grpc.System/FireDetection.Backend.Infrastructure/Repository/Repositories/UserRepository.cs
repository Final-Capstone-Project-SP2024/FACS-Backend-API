using AutoMapper;
using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly FireDetectionDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(FireDetectionDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserInformationResponse>> GetUsersUnRegisterd()
        {
            List<UserInformationResponse> responses = new List<UserInformationResponse>();
            HashSet<Guid> users = _context.Users.Select(x => x.Id).ToHashSet();
            HashSet<Guid> usersInRegister = _context.ControlCameras.Select(x => x.Id).ToHashSet();

            var nonMatchingRegister = users.Except(usersInRegister).AsEnumerable();
            foreach (var item in nonMatchingRegister)
            {
                UserInformationResponse user = _mapper.Map<UserInformationResponse>(_context.Users.Where(x => x.Id == item).FirstOrDefault());
                responses.Add(user);
            }
            return responses.AsEnumerable();

        }

     
    }
}

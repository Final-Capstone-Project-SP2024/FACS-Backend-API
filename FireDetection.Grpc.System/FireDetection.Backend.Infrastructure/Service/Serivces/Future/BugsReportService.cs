using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces.Future
{
    public class BugsReportService : IBugsReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimService;

        public BugsReportService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;

        }
        public async Task<BugsReportResponse> Add(BugsReportRequest request)
        {
            var bugsReport = _mapper.Map<BugsReport>(request);
            bugsReport.UserID = _claimService.GetCurrentUserId;

            _unitOfWork.BugsReportRepository.InsertAsync(bugsReport);
            await _unitOfWork.SaveChangeAsync();

            return await GetByDes(request.BugDescription);
        }

        public async Task<IQueryable<BugsReportResponse>> GetAll()
        {
            var data = await _unitOfWork.BugsReportRepository.GetAll();
            var mappedData = data.Select(f => _mapper.Map<BugsReportResponse>(f));
            return mappedData.AsQueryable();
        }

        public async Task<BugsReportResponse> Solve(Guid bugId)
        {
            BugsReport bug = await _unitOfWork.BugsReportRepository.GetById(bugId);
            bug.IsSolved = true;

            _unitOfWork.BugsReportRepository.Update(bug);
            await _unitOfWork.SaveChangeAsync();


            return await GetByDes(bug.BugDescription);
        }


        internal async Task<BugsReportResponse> GetByDes(string description)
        {

            var data = _unitOfWork.BugsReportRepository.Where(x => x.BugDescription == description).FirstOrDefault();
            return _mapper.Map<BugsReportResponse>(data);
        }
    }
}

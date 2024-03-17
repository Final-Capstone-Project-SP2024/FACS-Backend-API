using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IBugsReportService
    {
        public  Task<BugsReportResponse> Add(BugsReportRequest request);
        public Task<IQueryable<BugsReportResponse>> GetAll();
        public Task<BugsReportResponse> Solve(Guid bugId);

    }
}

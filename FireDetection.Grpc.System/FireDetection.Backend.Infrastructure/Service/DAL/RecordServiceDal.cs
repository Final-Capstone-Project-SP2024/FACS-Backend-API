using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.Service.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.DAL
{
    public class RecordServiceDal
    {
       
        public async Task Add(IRecordService recordService,Guid recordId, int actionTypeId)
        {
            await  recordService.AutoAction(recordId, actionTypeId);
        }
    }
}

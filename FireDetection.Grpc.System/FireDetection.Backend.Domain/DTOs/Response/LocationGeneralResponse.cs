using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class LocationGeneralResponse
    {
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
    }
}

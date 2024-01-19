using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class LocationInformationResponse
    {
        public string LocationName { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }
    }
}

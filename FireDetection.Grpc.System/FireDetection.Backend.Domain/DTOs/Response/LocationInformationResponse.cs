using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class LocationInformationResponse
    {
        public Guid LocationId { get; set; } 
        public string LocationName { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public  ReadOnlyCollection<Guid> Users { get; set; } = null!;
    }
}

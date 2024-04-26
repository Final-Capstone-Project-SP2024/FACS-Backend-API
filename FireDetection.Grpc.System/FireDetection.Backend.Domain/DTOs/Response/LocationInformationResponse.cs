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

        public int UserQuantity { get; set; }
        public int CameraQuantity { get; set; }
        public DateTime? CreatedDate { get; set; }

        public   List<UserInLocation> Users { get; set; } = null!;

        public List<CameraInLocation> CameraInLocations { get; set; } = null!;
    }

    public class CameraInLocation
    {
        public Guid CameraId { get; set; }

        public string CameraName { get; set; }  

        public string CameraDestination { get; set; }

        public string CameraImage { get; set; }
    }

    public class UserInLocation
    {
        public Guid UserId { get; set;}
        public string UserName { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class GetLocationAnalysisData
    {
        public Dictionary<string ,List<CameraInLocationAnalysis>> Analysis { get; set;  } = new Dictionary<string, List<CameraInLocationAnalysis>>();
    }


    public class CameraInLocationAnalysis
    {
        public string CameraName { get; set; }

        public int Count { get; set; }
    }
}

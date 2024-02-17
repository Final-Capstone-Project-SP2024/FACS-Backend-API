using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class NotficationDetailResponse
    {
        public string Name { get; set; } = null!;

        public string Context { get; set; } = null!;


        public string Title { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Core
{
    public class RestDTO<T>
    {
        public string Message { get; set; } = null!;
        public T Data { get; set; } 

        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }
}

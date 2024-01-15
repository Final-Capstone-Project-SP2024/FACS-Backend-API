using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Core
{
    public class RestMetaDataDTO<T> : RestDTO<T>
    {
        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }

        public int? RecordCount { get; set; }
    }
}

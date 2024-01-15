using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Core
{
    public class LinkDTO
    {
        public LinkDTO(string href, string rel, string type)
        {
            Href = href;
            Rel = rel;
            Type = type;
        }
        
        public string Href { get; private set; }
        public string Rel { get; private set; }
        public string Type { get; private set; }

    }
}

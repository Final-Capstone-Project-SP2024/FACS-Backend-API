using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Utils
{
    public static class HandleTextUtil
    {
        public static string HandleTitle(string text, string location)
        {
            // Fire Detect in {destination}
            return text.Replace("location", location); ;
        }

        public static string HandleContext(string text, string location, string destination)
        {
            // Go to {destination} belong to {location}
            text = text.Replace("destination", destination);
            text = text.Replace("location", location);
            return text;
        }
    }
}

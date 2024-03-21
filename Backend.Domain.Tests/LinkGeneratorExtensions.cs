using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Tests
{
    public static class LinkGeneratorExtensions
    {
        public static string GetUriByAction(this LinkGenerator linkGenerator, HttpContext context, string action, string controller, object values = null, string protocol = null)
        {
            return ControllerLinkGeneratorExtensions.GetUriByAction(linkGenerator, context, action, controller, values, protocol);
        }
    }
}

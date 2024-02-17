using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Tests
{
    public static class LinkGeneratorWrapper
    {
        public static string GetUriByAction(LinkGenerator generator,
                                            HttpContext httpContext,
                                            string action,
                                            string controller,
                                            object values,
                                            string scheme = null,
                                            HostString host = default,
                                            PathString pathBase = default,
                                            FragmentString fragment = default,
                                            LinkOptions options = default)
        {
            return generator.GetUriByAction(httpContext, action, controller, values, scheme, host, pathBase, fragment, options);
        }
    }
}

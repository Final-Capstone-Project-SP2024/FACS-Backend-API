using FireDetection.Backend.API;
using FireDetection.Backend.Domain;
using FireDetection.Backend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.API.Tests
{
    public class DependencyInjectionTests
    {
        private readonly ServiceProvider _serviceProvider;
        public DependencyInjectionTests()
        {
            var service = new ServiceCollection();
            service.AddWebAPIService();
            service.AddInfrastructuresService("mock");
            service.AddDbContext<FireDetectionDbContext>(
                option => option.UseInMemoryDatabase("test"));
            _serviceProvider = service.BuildServiceProvider();
        }
    }
}

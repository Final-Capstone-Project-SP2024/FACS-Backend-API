using Grpc.Core;

namespace FireDetection.Grpc.System.Services
{
    public class FireDetectionService : FireDetectionGRPC.FireDetectionGRPCBase
    {
        private readonly ILogger<FireDetectionService> _logger;
        public FireDetectionService(ILogger<FireDetectionService> logger)
        {
            _logger = logger;
        }

        public override Task<ReturnResponse> TakeAlarm(GetRequest request, ServerCallContext context)
        {
            Console.WriteLine(request.CameraID);
            return Task.FromResult(new ReturnResponse
            {
                Message = "Take Alarm Successfully"
            });
            
        }
    }
}

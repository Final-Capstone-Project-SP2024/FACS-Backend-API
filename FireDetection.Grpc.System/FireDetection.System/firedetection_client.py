import firedetection_pb2_grpc
import firedetection_pb2
import time 
import grpc 

def run(cameraID , percentFire ,time ):
    with grpc.insecure_channel('localhost:5036') as channel:
        stub = firedetection_pb2_grpc.FireDetectionGRPCStub(channel)
        request = firedetection_pb2.GetRequest(cameraID  = cameraID, percentFire = percentFire,Time = time)
        response = stub.TakeAlarm(request)
        
        print("Received response: ")
        print(f"Message : {response.message}")
        
if __name__ == "__main__":
    run()
        
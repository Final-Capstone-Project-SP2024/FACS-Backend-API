from utils import   firebase_storage_handler, firebase_realtimedatabase_handler
from firedetection_client import run
from   utils.firebase_cloudmessaging_handler import sendPush


#firebase_storage_handler.add_video_in_firebase("comsuonhocmon1","../FireDetection.System/media/video1.mp4")
#firebase_realtimedatabase_handler.upload("","comsuohocmon.mp4")
#if __name__ == "__main__":
#    run(cameraID= "21" , percentFire= "80", time="12h")

sendPush()


print("Hello World")


import datetime
import pyrebase
import firebase_admin
from firebase_admin import credentials , messaging

firebaseConfig = {
  "apiKey": "AIzaSyBwKZniXvzvFC_emxVZaPR4FVJFI2AfFfo",
  "authDomain": "final-capstone-project-f8bdd.firebaseapp.com",
  "databaseURL": "https://final-capstone-project-f8bdd-default-rtdb.asia-southeast1.firebasedatabase.app/",
  "projectId": "final-capstone-project-f8bdd",
  "storageBucket": "final-capstone-project-f8bdd.appspot.com",
  "messagingSenderId": "348680274283",
  "appId": "1:348680274283:web:974751151c18ef32ca8c2e"
}



def sendPush():
    cred = credentials.Certificate('C:\Code\Practise_Coding\DemoFireDetection2\Demo1\FACS_Demo1\FireDetection.Grpc.System\FireDetection.System\json\credentials.json')
    firebase_admin.initialize_app(cred)
    message = messaging.MulticastMessage(
        notification = messaging.Notification(
            title = 'Com suon hoc mon',
            body = 'Com suon hoc mon'
        ),
        data = None,
        tokens =[ 'dEOCNL8DRsyu6d92SIXOKa:APA91bHw9YmvPBAVNSqtyTkytqUeu64evv4azkd6WJnoSSBMhP5A2GwJEEiKO52lYAN5nBbmlG1PCNfxqsEj6AUuMp1R76wbMkfVUIDnYpSumJ61ItfGLYlwGllyHPT9Fiw9WsSY5mGT']
    )
    response = messaging.send_multicast(message)
    print('Successfully sent message ', response)
    
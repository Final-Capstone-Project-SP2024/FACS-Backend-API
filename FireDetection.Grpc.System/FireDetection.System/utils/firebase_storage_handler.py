from pyrebase import pyrebase
import imghdr
import os
from moviepy.editor import VideoFileClip
import firebase_admin 
from firebase_admin import credentials , storage 

firebaseConfig = {
  "apiKey": "AIzaSyBwKZniXvzvFC_emxVZaPR4FVJFI2AfFfo",
  "authDomain": "final-capstone-project-f8bdd.firebaseapp.com",
  "databaseURL": "https://final-capstone-project-f8bdd-default-rtdb.asia-southeast1.firebasedatabase.app/",
  "projectId": "final-capstone-project-f8bdd",
  "storageBucket": "final-capstone-project-f8bdd.appspot.com",
  "messagingSenderId": "348680274283",
  "appId": "1:348680274283:web:974751151c18ef32ca8c2e"
}


def add_video_in_firebase(filename,content) :
    firebase = pyrebase.initialize_app(firebaseConfig)
    storage = firebase.storage()
    storage.child(filename+".mp4").put(content)
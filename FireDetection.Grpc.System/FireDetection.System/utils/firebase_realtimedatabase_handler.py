import pyrebase

firebaseConfig = {
  "apiKey": "AIzaSyBwKZniXvzvFC_emxVZaPR4FVJFI2AfFfo",
  "authDomain": "final-capstone-project-f8bdd.firebaseapp.com",
  "databaseURL": "https://final-capstone-project-f8bdd-default-rtdb.asia-southeast1.firebasedatabase.app/",
  "projectId": "final-capstone-project-f8bdd",
  "storageBucket": "final-capstone-project-f8bdd.appspot.com",
  "messagingSenderId": "348680274283",
  "appId": "1:348680274283:web:974751151c18ef32ca8c2e"
}
firebase  = pyrebase.initialize_app(firebaseConfig)
database = firebase.database()



def upload(personname, record):
    data = {
        "Camera": "Camera_21",
        "UserName": "ManVo",
        "Record": f"{record}"
    }

    # Assuming you want to store data under "Users" with the key being the person's name
    database.child("Users").child(personname).set(data)

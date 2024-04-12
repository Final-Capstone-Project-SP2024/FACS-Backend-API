import csv
import uuid
import random
from datetime import datetime, timedelta

# Function to generate a random GUID
def generate_guid():
    return str(uuid.uuid4())

# Function to generate a random datetime within a specific range
def generate_datetime(start_date, end_date):
    time_delta = end_date - start_date
    random_days = random.uniform(0, time_delta.days)
    random_time = timedelta(days=random_days)
    result_date = start_date + random_time
    return result_date.strftime("%Y-%m-%d %H:%M:%S")

# Function to generate a random percentage
def generate_percentage():
    return round(random.uniform(0, 100), 2)
def generate_vote():
    return round(random.uniform(1, 5))

# Specify the desired date range
start_date = datetime(2023, 10, 1)
end_date = datetime(2024, 4, 1)

# Generate 10,000 data records
data = []
for _ in range(200):
    camera_ids = [
        "6f88f3aa-209f-4dc0-9f9c-af425b482a88",
        "19b5d49c-4230-4a6f-8cc8-bbcc16d82554",
        "036d2831-dd20-4bf0-9637-22de96cb9785",
        "f5d26584-58cf-46b1-9097-82a6e966dd55"
    ]
    
    record = {
        'Id': generate_guid(),
        'RecordTime': generate_datetime(start_date, end_date),
        'Status': random.choice(['InFinish']),
        'UserRatingPercent': generate_vote(),
        'PredictedPercent': generate_percentage(),
        'CameraID': random.choice(camera_ids),
        'RecordTypeID': 1,
        'CreatedDate': generate_datetime(start_date, end_date),
        'CreatedBy': "00000000-0000-0000-0000-000000000000",
        'FinishAlarmTime' : generate_datetime(start_date, end_date)
    }
    data.append(record)

# Specify the file path
filename = 'C:\\Code\\code\\FACS-Backend-API\\FireDetection.Grpc.System\\recordList.csv'

# Write to CSV file
with open(filename, 'w', newline='') as file:
    writer = csv.DictWriter(file, fieldnames=data[0].keys())
    writer.writeheader()
    writer.writerows(data)

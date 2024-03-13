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

# Specify the desired date range
start_date = datetime(2022, 1, 1)
end_date = datetime(2024, 1, 1)

# Generate 10,000 data records
data = []
for _ in range(10000):
    camera_ids = [
        "3cf118d9-d39b-4784-b343-be7622d69095",
        "47629cbc-46ca-4131-9677-b183023785a8",
        "de33572f-311d-45bf-a378-a1798d1ed719",
        "b7576d81-48c9-4d87-946b-16b80d458e14",
        "d5e9ffac-920d-4a4b-bc0e-facfb1c484e8",
        "aac2e7aa-d7d4-4f21-8edb-56299ee53651"
    ]
    
    record = {
        'Id': generate_guid(),
        'RecordTime': generate_datetime(start_date, end_date),
        'Status': random.choice(['Active', 'Inactive']),
        'UserRatingPercent': generate_percentage(),
        'PredictedPercent': generate_percentage(),
        'CameraID': random.choice(camera_ids),
        'RecordTypeID': random.randint(1, 2),
        'CreatedDate': generate_datetime(start_date, end_date),
        'CreatedBy': "fa886056-0e02-4078-b046-66f4f712d07b",
    }
    data.append(record)

# Specify the file path
filename = 'FireDetection.Grpc.System/recordList.csv'

# Write to CSV file
with open(filename, 'w', newline='') as file:
    writer = csv.DictWriter(file, fieldnames=data[0].keys())
    writer.writeheader()
    writer.writerows(data)

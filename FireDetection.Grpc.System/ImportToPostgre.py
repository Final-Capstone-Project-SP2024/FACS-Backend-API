import csv
import psycopg2
from psycopg2 import sql

# Replace these values with your PostgreSQL database details
db_params = {
    'host': 'dpg-cncaqtuct0pc73fq4n5g-a.singapore-postgres.render.com',
    'port': '5432',
    'database': 'facs_database',
    'user': 'ducanhnt22',
    'password': 'qn6QrBJznvfq8AdlEEY7qMrWNqbFqjOf'
}

# File path to the CSV file
csv_file_path = 'C:\\Code\\code\\FACS-Backend-API\\FireDetection.Grpc.System\\recordList.csv'

# PostgreSQL table name
table_name = 'Records'

# Connect to the PostgreSQL database
try:
    conn = psycopg2.connect(**db_params)
    cursor = conn.cursor()

    # Read data from CSV and insert into the PostgreSQL table
    with open(csv_file_path, 'r') as file:
        reader = csv.DictReader(file)
        for row in reader:
            insert_query = sql.SQL("""
                INSERT INTO {} ({})
                VALUES ({})
            """).format(
                sql.Identifier(table_name),
                sql.SQL(', ').join(map(sql.Identifier, row.keys())),
                sql.SQL(', ').join(sql.Placeholder() * len(row))
            )
            cursor.execute(insert_query, list(row.values()))

    conn.commit()

except psycopg2.Error as e:
    print("Error: Unable to connect to the database.")
    print(e)

finally:
    # Close the database connection
    if conn is not None:
        conn.close()
        print("Connection closed.")

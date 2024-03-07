import os
import re
import json

# Define the regular expression pattern to extract values between parentheses
pattern = r"\((.*?)\)"

# Function to parse SQL insert statement and convert to dictionary
def parse_insert_to_dict(sql_insert):
    print(sql_insert)
    match = re.findall(pattern, sql_insert)
    data = []
    for line in match:
        columns = ["ItemId", "Name", "Department", "Category", "UPC", "SKU", "Price", "Discountable", "Taxable", "TrackingInventory", "Cost", "AssignedCost", "Quantity", "ReorderTrigger", "RecommendedOrder", "LastSoldDate", "Supplier", "LiabilityItem", "LiabilityRedemptionTender", "TaxGroupOrRate"]
        data_dict = dict(zip(columns, line))

        data.append(data_dict)
    return data

# Function to parse SQL file and convert to list of dictionaries
def parse_sql_file(file_path):
    with open(file_path, "r") as file:
        sql_lines = file.readlines()

    json_data = []
    sql_statement = ""
    for line in sql_lines:
        sql_statement += line.strip()
        if ";" in line:
            data_dict = parse_insert_to_dict(sql_statement[:-1])
            if data_dict:
                json_data.append(data_dict)
            sql_statement = ""

    return json_data

# Define the path to your SQL file
sql_file_path = "items.sql"
output_file_path = os.path.splitext(sql_file_path)[0] + "_output.json"

# Parse the SQL file and convert to list of dictionaries
json_data = parse_sql_file(sql_file_path)

# Write the JSON data to a file in the same directory
with open(output_file_path, "w") as output_file:
    json.dump(json_data, output_file)

print(f"JSON data written to: {output_file_path}")

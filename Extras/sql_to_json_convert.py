import re
import json
import os

#pattern = r"\('([^']*)', '([^']*)', '([^']*)', '([^']*)', '([^']*)', '([^']*)', '([^']*)', (\d+), (\d+), (\d+), '([^']*)', '([^']*)', (\d+), (\d+), (\d+), '([^']*)', '([^']*)', (\d+), '([^']*)', '([^']*)'\)"
pattern = r"\((\d+), '([^']*)'\)"
sql_file = "modifiers.sql"

sql_string = ""

with open(sql_file, 'r') as file:
    lines = file.readlines()

    for line in lines:
        sql_string += line.strip()

file.close()

print(sql_string)
#["ItemId", "Name", "Department", "Category", "UPC", "SKU", "Price", "Discountable", "Taxable", "TrackingInventory", "Cost", "AssignedCost", "Quantity", "ReorderTrigger", "RecommendedOrder", "LastSoldDate", "Supplier", "LiabilityItem", "LiabilityRedemptionTender", "TaxGroupOrRate"]
values = re.findall(pattern, sql_string)
#json_array = [{'ItemId':v[0], 'Name':v[1], 'Department':v[2], 'Category':v[3], 'UPC':v[4], 'SKU':v[5], 'Price':v[6], 'Discountable':v[7], 'Taxable':v[8], 'TrackingInventory':v[9], 'Cost':v[10], 'AssignedCost':v[11], 'Quantity':v[12], 'ReorderTrigger':v[13], 'RecommendedOrder':v[14], 'LastSoldDate':v[15], 'Supplier':v[16], 'LiabilityItem':v[17], 'LiabilityRedemptionTender':v[18], 'TaxGroupOrRate':v[19]} for v in values]

json_array = [{'ModifierId': v[0], 'ItemId': v[1]} for v in values]

json_output = json.dumps(json_array, indent=4)

print(json_output)

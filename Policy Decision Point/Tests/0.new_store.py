import requests
import json



headers = {
    'Content-Type': 'application/json',
}

new_store_request = {
    "name": "iam4cc"
}

response = requests.post( "http://192.168.1.2:6002/api/config/stores", data=json.dumps(new_store_request), headers=headers)
print(response.text)
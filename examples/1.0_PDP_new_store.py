import requests
import json
import base64

rebac_url = "http://192.168.1.18:6002"

headers = {
    'Content-Type': 'application/json',
}

new_store_request = {
    "name": "iam4cc"
}

response = requests.post( rebac_url + "/api/admin/stores", data=json.dumps(new_store_request), headers=headers)
print(response.status_code)
print(response.text)
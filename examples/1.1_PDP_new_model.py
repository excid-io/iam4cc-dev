import requests
import json
import base64

rebac_url = "http://192.168.1.18:6002"

with open('iam4cc.json', 'r') as file:
    model = json.load(file)


headers = {
    'Content-Type': 'application/json',
}

new_model_request = {
    "model":model
}

response = requests.post( rebac_url + "/api/admin/AuthorizationModels", data=json.dumps(new_model_request), headers=headers)
print(response.text)
import requests
import json

with open('iot-use-case.json', 'r') as file:
    model = json.load(file)


headers = {
    'Content-Type': 'application/json',
}

new_model_request = {
    "storeId": "01J6HTDRN7XC94E92DT5PV7HQ9",
    "model":model
}

response = requests.post( "http://192.168.1.2:6002/api/config/AuthorizationModels", data=json.dumps(new_model_request), headers=headers)
print(response.text)
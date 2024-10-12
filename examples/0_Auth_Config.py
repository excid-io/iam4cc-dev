import requests
import json

admin_url = "http://192.168.1.2:6000/"


headers = {
    'Content-Type': 'application/json',
}

payload = {
    "client_id": "admin",
    "client_secret":"admin-secret",
    "grant_types": [
        "client_credentials"
    ],
    "access_token_strategy":"jwt",
    "audience":["https://iot-data.space"],
    "scope":"admin"
}

response = requests.request("POST", admin_url + "clients", headers=headers, data=json.dumps(payload))
print(response.text)

payload = {
    "client_id": "alice",
    "client_secret":"alice-secret",
    "grant_types": [
        "client_credentials"
    ],
    "access_token_strategy":"jwt",
    "audience":["https://iot-data.space"],
    "scope":"user"
}

response = requests.request("POST", admin_url + "clients", headers=headers, data=json.dumps(payload))
print(response.text)
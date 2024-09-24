import requests
import json

admin_url = "http://192.168.1.2:6000/"


headers = {
    'Content-Type': 'application/json',
}

payload = {
    "client_id": "client-app-1",
    "client_secret":"client-app-1-secret",
    "grant_types": [
        "urn:ietf:params:oauth:grant-type:jwt-bearer",
        "client_credentials"
    ],
    "access_token_strategy":"jwt",
    "audience":["https://registry.excid.io"]
}

response = requests.request("POST", admin_url + "clients", headers=headers, data=json.dumps(payload))
print(response.text)

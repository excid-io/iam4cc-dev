import requests
import json
import base64

token_url = "http://192.168.1.18:6001/oauth2/token"
rebac_url = "http://192.168.1.18:6002"

client_id =  "admin"
client_secret = "admin-secret"

authorization_header = client_id + ":" + client_secret
headers={
    "Content-Type":"application/x-www-form-urlencoded",
    "Authorization": "Basic " + base64.b64encode(authorization_header.encode()).decode()
}

data={
    "client_id":"admin",
    "audience":"https://iot-data.space",
    "grant_type":"client_credentials",
    "scope":"admin"
}

response = requests.post(token_url, headers = headers, data = data)
print(response.text)
json_response = json.loads(response.text)

access_token = json_response['access_token']

headers = {
    'Content-Type': 'application/json',
    "Authorization":"Bearer " + access_token,
}

new_model_request = {
    "tuples":[
        {
        'user': 'device:meter1',
        'relation': 'parent',
        'object': 'resource:switch1',
        },
        {
        'user': 'device_group:meters',
        'relation': 'parent',
        'object': 'device:meter1',
        },
        {
        'user': 'user:alice',
        'relation': 'can_read',
        'object': 'resource:switch1',
        }
    ]
}

response = requests.post( rebac_url + "/api/admin/Tuples", data=json.dumps(new_model_request), headers=headers)
print(response.text)
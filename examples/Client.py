import requests
import json
import base64

import requests
import json
import base64

token_url = "http://192.168.1.2:6001/oauth2/token"
registry_url = " http://192.168.1.2:6004/"

client_id =  "user"
client_secret = "user-secret"

authorization_header = client_id + ":" + client_secret

headers={
    "Content-Type":"application/x-www-form-urlencoded",
    "Authorization": "Basic " + base64.b64encode(authorization_header.encode()).decode()
}

data={
    "client_id":"user",
    "audience":"https://iot-data.space",
    "grant_type":"client_credentials",
    "scope":"user"
}


response = requests.post(token_url, headers = headers, data = data)
print(response.text)

json_response = json.loads(response.text)
access_token = json_response['access_token']

'''
headers={
    "Authorization":"Bearer " + access_token
}

response = requests.get(registry_url + "api/Issue/Jwt", headers = headers)

access_token = response.text
'''
headers={
     "Authorization":"Bearer " + access_token,
}
print(response.text)

endpoint_url = "http://192.168.1.2:6003/item/switch1"
response = requests.get(endpoint_url, headers = headers)
print(response.text)

'''
headers={
     "Authorization":"Bearer " + access_token,
     "Content-Type":"application/json"
}

data={
    "id":"1"
}

endpoint_url = "http://localhost:6003/test"

response = requests.post(endpoint_url, headers = headers, data = json.dumps(data))
print(response.text)
'''
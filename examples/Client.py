import requests
import json
import base64

import requests
import json
import base64

token_url = "http://192.168.1.18:6001/oauth2/token"
endpoint = "http://192.168.1.18:6003"

client_id =  "alice"
client_secret = "alice-secret"

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


print(response.text)
'''

headers={
     "Authorization":"Bearer " + access_token,
}
endpoint_url = endpoint + "/item/switch1"
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
import requests
import json
import base64

token_url = "http://192.168.1.2:6001/oauth2/token"
registry_url = " http://192.168.1.2:6004/"

client_id =  "client-app-1"
client_secret = "client-app-1-secret"

authorization_header = client_id + ":" + client_secret
headers={
    "Content-Type":"application/x-www-form-urlencoded",
    "Authorization": "Basic " + base64.b64encode(authorization_header.encode()).decode()
}

data={
    "client_id":"client-app-1",
    "audience":"https://registry.excid.io",
    "grant_type":"client_credentials"
}


response = requests.post(token_url, headers = headers, data = data)
print(response.text)
json_response = json.loads(response.text)

access_token = json_response['access_token']

headers={
    "Authorization":"Bearer " + access_token
}

response = requests.get(registry_url + "api/Issue/Jwt", headers = headers)
print(response.text)
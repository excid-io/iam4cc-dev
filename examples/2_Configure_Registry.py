import requests
import json
import base64

token_url = "http://192.168.1.18:6001/oauth2/token"
rebac_url = "http://192.168.1.18:6002"

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



#-------New Relationship------

headers={
    "Authorization":"Bearer " + access_token,
    "Content-Type":"application/json"
}
'''
data={
    "description":"User client-app-1 is member of it group",
    "User": "user:client-app-1",
    "Relation": "member",
    "Object": "user_group:it"
}
'''
data={
    "description":"It group has read access to meters",
    "User": "user_group:it#member",
    "Relation": "can_read",
    "Object": "device_group:meters"
}

response = requests.post(rebac_url + "api/Relationships/Create", headers = headers, data = json.dumps(data))
print(response.status_code)
print(response.text)

#-------List------------
headers={
    "Authorization":"Bearer " + access_token
}

response = requests.get(registry_url + "api/Relationships/List", headers = headers)
print(response.text)
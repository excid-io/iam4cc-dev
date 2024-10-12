import requests
import json

with open('iot-use-case.json', 'r') as file:
    model = json.load(file)


headers = {
    'Content-Type': 'application/json',
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
        'user': 'user_group:it#member',
        'relation': 'can_read',
        'object': 'device_group:meters',
        }
    ]
}

response = requests.post( "http://192.168.1.2:6002/api/admin/Tuples", data=json.dumps(new_model_request), headers=headers)
print(response.text)
import requests
import json
import base64

rebac_url = "http://192.168.1.18:6002"


headers = {
    'Content-Type': 'application/json',
}

new_model_request = {
    "tuples":[
        {
            'user': 'asset:meter1',
            'relation': 'parent',
            'object': 'resource:switch1',
        },
        {
            'user': 'asset_group:meters',
            'relation': 'parent',
            'object': 'asset:meter1',
        },
        {
            'user': 'university:TUA',
            'relation': 'organization',
            'object': 'trusted_organizations:universities',
        },
        {
            'user': 'trusted_organizations:universities#users',
            'relation': 'writer',
            'object': 'asset_group:meters',
        }
    ]
}

response = requests.post( rebac_url + "/api/admin/Tuples", data=json.dumps(new_model_request), headers=headers)
print(response.text)

'''
# Contextual Tuples


{
'user': 'user:alice',
'relation': 'cs_department',
'object': 'university:TUA',
},
{
'user': 'user:alice',
'relation': 'phd_student',
'object': 'university:TUA',
}

assertion
{
user:alice
writer
resource:switch1
}

model
  schema 1.1

type trusted_organizations
  relations
    define organization:[university]
    define users:is_authorized from organization
    
type university
  relations
    define cs_department: [user]
    define phd_student:[user]
    define is_authorized: cs_department and phd_student
    
type user

type asset_group
  relations
    define writer: [trusted_organizations#users]
    define reader: [trusted_organizations#users] or writer

type asset
  relations
    define writer: [trusted_organizations#users] or writer from parent
    define reader: [trusted_organizations#users] or writer or reader from parent
    define parent: [asset_group]

type resource
  relations
    define writer: [trusted_organizations#users] or writer from parent
    define reader: [trusted_organizations#users] or writer or reader from parent
    define parent: [asset]

'''
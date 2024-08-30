import requests
import json

model = {
  "schema_version": "1.1",
  "type_definitions": [
    {
      "type": "user"
    },
    {
      "type": "document",
      "relations": {
        "reader": {
          "this": {}
        },
        "writer": {
          "this": {}
        },
        "owner": {
          "this": {}
        }
      },
      "metadata": {
        "relations": {
          "reader": {
            "directly_related_user_types": [
              {
                "type": "user"
              }
            ]
          },
          "writer": {
            "directly_related_user_types": [
              {
                "type": "user"
              }
            ]
          },
          "owner": {
            "directly_related_user_types": [
              {
                "type": "user"
              }
            ]
          }
        }
      }
    }
  ]
}

headers = {
    'Content-Type': 'application/json',
}

new_model_request = {
    "storeId": "01J6HGEJWDMWM034WTYZ5ST4BM",
    "model":model
}

response = requests.post( "http://192.168.1.2:6002/api/config/AuthorizationModels", data=json.dumps(new_model_request), headers=headers)
print(response.text)
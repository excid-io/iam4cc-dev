import requests
import json

access_token = "eyJhbGciOiJSUzI1NiIsInR5cCI6ImF0K2p3dCIsImtpZCI6InlvMVZESW81Ymx2VFBXMVNEOWNBbCJ9.eyJpc3MiOiJodHRwczovL2V4Y2lkLmV1LmF1dGgwLmNvbS8iLCJzdWIiOiJRMElUNjJSNkJGNDlFYmhJM1oyTVRJUkxMYjZTNXZKUEBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly9yZWdpc3RyeS5leGNpZC5pbyIsImlhdCI6MTcyMTczMTkwNSwiZXhwIjoxNzIxODE4MzA1LCJqdGkiOiI4OFZQdHBnblJnSzJQVVBZdGtqeW02IiwiY2xpZW50X2lkIjoiUTBJVDYyUjZCRjQ5RWJoSTNaMk1USVJMTGI2UzV2SlAifQ.LfBTTKqpeIP8N3wsk-kbzYvAsL8kCaCmKSQSfNa53Qw94DLn-kYQk5OoUTYQFu7noXaiWYcjmfVfyc_6Dea3PgW7t-57rzvI1HBJegffdhAf9e7e_2gGBZnK3R3SB9tvShShs10pdRTCptHQwl0_KNnu3oJ4s6sR56Frs5ARH5ReyN-cMxkE8NtSNw22W_VBFR_g_glwzh3lWia0OhgFrFyYdYjh9utS2j835urPTIUpnCE6lbJ83sLR9Vm5Zgvsv7CXsULeJMlU4_B4mhFnEhrKi85mi-h2z1fWWlFoRRkx4Shk-Xybnf6u3t_5S5OfXZJBILeZp-rqRZRsY4scYA"

headers={
     "Authorization":"Bearer " + access_token,
}

endpoint_url = "http://localhost:6003/item/14"

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
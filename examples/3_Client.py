import requests
import json
import base64

import requests
import json
import base64

import requests
import urllib3

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

CRED_ID = "did:fabric:bD6eebUPfgX3aVV4aiGIUdgCzU6OJJPr0ZGaOnXq_JY5912527"
USER_AGENT = "https://localhost:8082"
endpoint = "http://192.168.1.18:6003"

def get_verifiable_credential(device_agent_url, cred_id):
    print("Fetch the Verifiable Credential")
    payload = {"credId": cred_id}
    response = requests.post(f"{device_agent_url}/fluidos/idm/getVCredential", json=payload, verify=False)
    response.raise_for_status()
    print ("Received Verifiable Credential \n", response.json())
    return response.json()

def generate_verifiable_presentation(device_agent_url, cred_id, frame):
    print("Generate a Verifiable Presentation")
    payload = {
        "credId": cred_id,
        "querybyframe": {"frame": frame}
    }
    response = requests.post(f"{device_agent_url}/fluidos/idm/generateVP", json=payload, verify=False)
    response.raise_for_status()
    print("Generated Verifiable Presentation: \n", response.json())
    return response.json()

credential = get_verifiable_credential(USER_AGENT, CRED_ID)

# Generate Verifiable Presentation
frame = {
    "@context": [
        "https://www.w3.org/2018/credentials/v1",
        "https://www.w3.org/2018/credentials/examples/v1",
        "https://ssiproject.inf.um.es/security/psms/v1",
        "https://ssiproject.inf.um.es/poc/context/v1"
    ],
    "type": ["VerifiableCredential", "FluidosCredential"],
    "@explicit": True,
    "credentialSubject": {"@explicit": True, "fluidosRole": {}, "holderName": {}, "orgIdentifier":{"@value": "cs_department"}
    }
}
vp = generate_verifiable_presentation(USER_AGENT, CRED_ID, frame)
print("---------------------")
print(json.dumps(vp['results'][0],indent=4))

'''

headers={
     "Authorization":"Bearer " + access_token,
}
endpoint_url = endpoint + "/item/switch1"
response = requests.get(endpoint_url, headers = headers)
print(response.text)


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
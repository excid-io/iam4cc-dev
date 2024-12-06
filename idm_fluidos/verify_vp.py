import requests
import json

# Verify the Presentation
def verify_presentation(third_party_agent_url, vp):
    vc = vp['results'][0]
    formatted_vc = json.dumps(vc).replace('"', '\\"')
    print("Verify the Verifiable Presentation")
    payload = {"credential": formatted_vc}
    response = requests.post(f"{third_party_agent_url}/fluidos/idm/verifyCredential", json=payload, verify=False)
    response.raise_for_status()
    print(response.json())
    return response.json()
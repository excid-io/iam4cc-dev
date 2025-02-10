import requests
import urllib3

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

USER_AGENT = "https://localhost:8082" # User agent URL

def generate_did(url, name, keys):
    print("Generate DID for", name)
    payload = {
        "keys": keys,
        "name": name
    }
    response = requests.post(f"{url}/fluidos/idm/generateDID", json=payload, verify=False)
    response.raise_for_status()
    print(response.json())
    return response.json()["didDoc"]["id"]

try:
    # Generate DIDs
    holder_did = generate_did(USER_AGENT, "Bob", [{"keyType": {"keytype": "Ed25519VerificationKey2018"}, "purpose": "Authentication"}])

except requests.exceptions.RequestException as e:
    print("An error occurred:", e)
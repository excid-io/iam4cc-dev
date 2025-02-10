import requests
import urllib3

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

USER_AGENT = "https://localhost:8082"
ORGANIZATION_AGENT = "https://localhost:9082" # issuer URL


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

def enroll_user(device_agent_url, id_proofs):
    print("Perform enrollment to get a credential")
    payload = {
        "url": "https://issuer:9082",
        "idProofs": id_proofs
    }
    response = requests.post(f"{device_agent_url}/fluidos/idm/doEnrolment", json=payload, verify=False)
    print(response.json())
    response.raise_for_status()
    print("Enrollment was successful: credential storage id is", response.json()["credStorageId"])
    return response.json()["credStorageId"]

try:
    # Generate DIDs
    user_did = generate_did(USER_AGENT, "Alice12", [{"keyType": {"keytype": "Ed25519VerificationKey2018"}, "purpose": "Authentication"}])
    organization_did = generate_did(ORGANIZATION_AGENT, "Techical University13", [
        {"keyType": {"keytype": "Ed25519VerificationKey2018"}, "purpose": "Authentication"},
        {"keyType": {"keytype": "Bls12381G1Key2022", "attrs": ["3"]}, "purpose": "AssertionMethod"}
    ])
    id_proofs = [
        {"attrName": "holderName", "attrValue": "Alice"},
        {"attrName": "fluidosRole", "attrValue": "phd_student"},
        {"attrName": "orgIdentifier", "attrValue": "cs_department"},
    ]
    cred_id = enroll_user (USER_AGENT, id_proofs)
    print("cred_id=", cred_id)

except requests.exceptions.RequestException as e:
    print("An error occurred:", e)
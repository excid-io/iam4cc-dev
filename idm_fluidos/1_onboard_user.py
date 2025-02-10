import requests
import urllib3

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)


COMPANY_AGENT = "https://localhost:9082" # issuer URL
USER_AGENT = "https://localhost:8082"

def generate_did(url, name, keys):
    print("Generate DID for", name)
    payload = {
        "keys": keys,
        "name": name
    }
    response = requests.post(f"{url}/fluidos/idm/generateDID", json=payload, verify=False)
    response.raise_for_status()
    return response.json()["didDoc"]["id"]

def enroll_user(device_agent_url, issuer_url, id_proofs):
    print("Perform enrollment to get a credential")
    payload = {
        "url": issuer_url,
        "idProofs": id_proofs
    }
    response = requests.post(f"{device_agent_url}/fluidos/idm/doEnrolment", json=payload, verify=False)
    print(response.json())
    response.raise_for_status()
    print("Enrollment was successful: credential storage id is", response.json()["credStorageId"])
    return response.json()["credStorageId"]

try:
    # Generate DIDs
    '''
    issuer_did = generate_did(COMPANY_AGENT, "Techical University", [
        {"keyType": {"keytype": "Ed25519VerificationKey2018"}, "purpose": "Authentication"},
        {"keyType": {"keytype": "Bls12381G1Key2022", "attrs": ["5"]}, "purpose": "AssertionMethod"}
    ])
    '''
    # Enrollment and Credential Retrieval
    id_proofs = [
        {"attrName": "holderName", "attrValue": "FluidosNode"},
        {"attrName": "fluidosRole", "attrValue": "Customer"},
        {"attrName": "deviceType", "attrValue": "Server"},
        {"attrName": "orgIdentifier", "attrValue": "FLUIDOS_id_23241231412"},
        {"attrName": "physicalAddress", "attrValue": "50:80:61:82:ab:c9"}
    ]
    cred_id = enroll_user(USER_AGENT, f"https://issuer:9082", id_proofs)
    print("cred_id=", cred_id)

except requests.exceptions.RequestException as e:
    print("An error occurred:", e)
import requests
import issue_vc
import generate_vp
import verify_vp
import urllib3

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

DEVICE_AGENT = "https://localhost:8082" # holder URL
ISSUER_AGENT = "https://localhost:9082" # issuer URL
THIRD_PARTY_AGENT = "https://localhost:7082" # verifier URL

def generate_did(url, name, keys):
    print("Generate DID for", name)
    payload = {
        "keys": keys,
        "name": name
    }
    response = requests.post(f"{url}/fluidos/idm/generateDID", json=payload, verify=False)
    response.raise_for_status()
    return response.json()["didDoc"]["id"]

try:
    # Generate DIDs
    holder_did = generate_did(DEVICE_AGENT, "holderDevice2", [{"keyType": {"keytype": "Ed25519VerificationKey2018"}, "purpose": "Authentication"}])
    issuer_did = generate_did(ISSUER_AGENT, "Fluidos Issuer Node2", [
        {"keyType": {"keytype": "Ed25519VerificationKey2018"}, "purpose": "Authentication"},
        {"keyType": {"keytype": "Bls12381G1Key2022", "attrs": ["5"]}, "purpose": "AssertionMethod"}
    ])
    verifier_did = generate_did(THIRD_PARTY_AGENT, "thirdPartyDevice2", [{"keyType": {"keytype": "Ed25519VerificationKey2018"}, "purpose": "Authentication"}])

    # Enrollment and Credential Retrieval
    id_proofs = [
        {"attrName": "holderName", "attrValue": "FluidosNode"},
        {"attrName": "fluidosRole", "attrValue": "Customer"},
        {"attrName": "deviceType", "attrValue": "Server"},
        {"attrName": "orgIdentifier", "attrValue": "FLUIDOS_id_23241231412"},
        {"attrName": "physicalAddress", "attrValue": "50:80:61:82:ab:c9"}
    ]
    cred_id = issue_vc.enroll_holder(DEVICE_AGENT, f"https://issuer:9082", id_proofs)
    print("cred_id=", cred_id)
    credential = issue_vc.get_verifiable_credential(DEVICE_AGENT, cred_id)

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
        "identifier": {},
        "issuer": {},
        "issuanceDate": {},
        "credentialSubject": {"@explicit": True, "fluidosRole": {}, "holderName": {}}
    }
    vp = generate_vp.generate_verifiable_presentation(DEVICE_AGENT, cred_id, frame)

    # Verify Presentation
    verification_result = verify_vp.verify_presentation(THIRD_PARTY_AGENT, vp)
    print("Verification Result:", verification_result)

except requests.exceptions.RequestException as e:
    print("An error occurred:", e)
import requests

# Enroll holder with the issuer before obtaining a Verifiable Credential
def enroll_holder(device_agent_url, issuer_url, id_proofs):
    print("Perform enrollment to get a credential")
    payload = {
        "url": issuer_url,
        "idProofs": id_proofs
    }
    response = requests.post(f"{device_agent_url}/fluidos/idm/doEnrolment", json=payload, verify=False)
    response.raise_for_status()
    print("Enrollment was successful: credential storage id is", response.json()["credStorageId"])
    return response.json()["credStorageId"]

# Receive Verifiable Credential
def get_verifiable_credential(device_agent_url, cred_id):
    print("Fetch the Verifiable Credential")
    payload = {"credId": cred_id}
    response = requests.post(f"{device_agent_url}/fluidos/idm/getVCredential", json=payload, verify=False)
    response.raise_for_status()
    print ("Received Verifiable Credential \n", response.json())
    return response.json()

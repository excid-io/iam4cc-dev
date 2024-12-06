import requests

# Create a Verifiable Presentation using the issued Verifiable Credential
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
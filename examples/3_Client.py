import requests
import json
import base64

endpoint="http://192.168.1.18:6003"

vp = {
    "@context": [
        "https://www.w3.org/2018/credentials/v1"
    ],
    "type": [
        "VerifiablePresentation"
    ],
    "verifiableCredential": [
        {
            "@context": [
                "https://www.w3.org/2018/credentials/v1",
                "https://www.w3.org/2018/credentials/examples/v1",
                "https://ssiproject.inf.um.es/security/psms/v1",
                "https://ssiproject.inf.um.es/poc/context/v1"
            ],
            "credentialSubject": {
                "fluidosRole": "phd_student",
                "holderName": "alice",
                "id": "urn:bnid:_:c14n0",
                "orgIdentifier": "cs_department"
            },
            "id": "did:fabric:bD6eebUPfgX3aVV4aiGIUdgCzU6OJJPr0ZGaOnXq_JY5912527",
            "issuanceDate": "2025-02-10T20:49:28.999169169Z",
            "issuer": "did:fabric:bD6eebUPfgX3aVV4aiGIUdgCzU6OJJPr0ZGaOnXq_JY",
            "proof": {
                "created": "2025-02-10T20:49:29.053421098Z",
                "nonce": "MjAyNS0wMi0xMCAyMjowMjo0My4yMjQxMzY0MjQgKzAwMDAgVVRDIG09Kzg1NTQuOTk1NjUyMTI1",
                "proofPurpose": "assertionMethod",
                "proofValue": "AAQdAQQCcoVpkzVtlbZHa7fWEpK/yr1IgHAytZpL+uhOAWlcj0yi+LVEdWSO7qD6mmC39JkGMo5tBbBJqRekAHMAQwz5JJ+skbJGuhc4ntYJvUbj4ccNd1cNlrUxN0RrPialbi8TBRFYR9GIXzkxU4bf3IxCGRUZ64tKviPKEqWi5CQQvPH5xTVm4dj93UjyGNLh11gX5Eu2ju7LPyjQ4pGOSpwajrPY3tdGDJ0HsqYy6IbPpHlI7pUsCfh9davqMvtqYEUEALK3ie/kEsxnx1btVU4PD1RgGHisTnVoNBQPaiEoaadzmQfp0qzUC01GUDdjUG2CEKNP+2+yKtnbjnUXne4YmMx57i6He1ieUfi7JL2SATRliZ3hL51PWvgAc739alPfFYLCh25TPdxS/J1V3/sQ1MfjgHAjuImJo5XvncP+PJ/qQNiP8WiR4oAZglfV46XoFu2cfG//+c75PqG2nH7vvPrmX4Pg6ujcBFxyzuRp5+eBSgWCQBhpmS2JDQ+RrFpSFfWfBrdPT+3c3yQHqHHWOOXqDiqt7Fbjmj8jFNiuSHAcgfHY2+1u7dF4j9CnMNX+AAAAAAAAAAAAAAAAAAAAAC6NNCiPObd8HqXvKjqHhGG2yyb73Sx0ypxImOLQfksYAAAAAAAAAAAAAAAAAAAAAACvqbLKURY962eVolZhOfa7pcUQ1IQi4DzQTUkUe5WTAAAAAAAAAAAAAAAAAAAAAE0ioPSKuMFQkLcUtaMU7QHCwk/JDrroZVKHbgvLzaIZ",
                "type": "PsmsBlsSignatureProof2022",
                "verificationMethod": "did:fabric:bD6eebUPfgX3aVV4aiGIUdgCzU6OJJPr0ZGaOnXq_JY#TRhml1mcPNL6E9uBJs8qtmqh5gx_c-cLT_9w62FMdeE"
            },
            "type": [
                "FluidosCredential",
                "VerifiableCredential"
            ]
        }
    ]
}


headers={
     "Authorization":"Bearer " + base64.b64encode(json.dumps(vp).encode()).decode(),
}
endpoint_url = endpoint + "/item/switch1"
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
# IAM4CC Dev

This is the development repository of the IAM4CC project

## Components

### Policy Enforcement Point 
As a Policy Enforcement Point (PEP), IAM4CC uses [Ory oathkeeper](https://www.ory.sh/docs/oathkeeper).
Oathkeeper is configured with the [Bearer token authenticator](https://www.ory.sh/docs/oathkeeper/pipeline/authn#bearer_token).

## Execution
From the compose directory execute:

```bash
docker-compose -f iam4cc.yml up --build
```
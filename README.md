# IAM4CC Dev

This is the development repository of the IAM4CC project

## Components

### ReBAC
Policy Administration Point (PAP) for OpenFGA and Policy Decision Point (PDP) for 
ory oathkeeper. Build the docker image by executing

```bash
docker build -t rebac -f API/Dockerfile  .
```

### Policy Enforcement Point 
As a Policy Enforcement Point (PEP), IAM4CC uses [Ory oathkeeper](https://www.ory.sh/docs/oathkeeper).
Oathkeeper is configured with the [remote authorizer](https://www.ory.sh/docs/oathkeeper/pipeline/authz#remote).


#### Build without docker

```
dotnet-ef migrations add InitialCreate --project .\API\API.csproj
dotnet ef database update --project .\API\API.csproj
```

## Helm
Add the necessary repositories
* PEP `helm repo add ory https://k8s.ory.sh/helm/charts`
* OpenFGA `helm repo add openfga https://openfga.github.io/helm-charts`

### PEP
From the ReBAC directory run:

```
helm install oathkeeper ory/oathkeeper \
    --set-file "oathkeeper.accessRules=./helm/config/oathkeeper/rules.json" \
    -f "./helm/config/oathkeeper/config.yaml" \
     --set 'maester.enabled=false' 
    
```

Execute in another window

`kubectl port-forward --address 192.168.1.18 deployment/oathkeeper 6003:6003`

### ReBAC
First run OpenFGA:
```
helm install openfga openfga/openfga \
    --set 'service.ports.port=8080'
```
Execute in another window

`kubectl port-forward --address 192.168.1.18 deployment/openfga 6005:8080`

Make sure you have built the rebac image and you have pulled to you Kubernetes cluster
e.g., 
```
minikube image load rebac:latest
```

Then, from the `iam4cc-dev` directory execute
```
helm install rebac ./helm/ReBAC \
     --set 'openfga.apiURL=http://192.168.1.18:6005' 
```

Execute in another window

`kubectl port-forward --address 192.168.1.18 deployment/rebac 6002:6002`
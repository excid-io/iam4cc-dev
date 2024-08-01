# IAM4CC Dev

This is the development repository of the IAM4CC project

## Components

### Policy Enforcement Point 
As a Policy Enforcement Point (PEP), IAM4CC uses [Ory oathkeeper](https://www.ory.sh/docs/oathkeeper).
Oathkeeper is configured with the [remote authorizer](https://www.ory.sh/docs/oathkeeper/pipeline/authz#remote).

### Policy Decision Point
Policy Decision Point (PDP) for ory oathkeeper. Build the docker image by executing

```bash
docker build -t pdp -f API/Dockerfile --build-arg LISTENING_PORT="6002" .
```

### Registry
An authorization registry used for defining relationships between users and objects.
Build the docker image by executing

```bash
docker build -t registry -f API/Dockerfile --build-arg LISTENING_PORT="6004" .
```

#### Build without docker

```
dotnet-ef migrations add InitialCreate --project .\API\API.csproj
dotnet ef database update --project .\API\API.csproj
```
## Set up
From the compose directory execute only onceQ

```bash
docker-compose -f setup.yml up --build
```

## Execution
From the compose directory execute:

```bash
docker-compose -f iam4cc.yml up --build
```

This first time this script it is executed it will take some more time.


#!/bin/bash

# Add the ORY Helm repository
echo "Adding ORY Helm repository..."
helm repo add ory https://k8s.ory.sh/helm/charts

# Update the Helm repository
echo "Updating Helm repository..."
helm repo update

# Install Hydra using Helm
echo "Installing Hydra with Helm..."
helm install hyd ory/hydra \
    --set 'hydra.config.secrets.system={'$(LC_ALL=C tr -dc 'A-Za-z0-9' < /dev/urandom | base64 | head -c 32)'}'\
    --set 'hydra.config.dsn=memory' \
    --set 'hydra.config.urls.self.issuer=http://public.hydra.localhost/' \
    --set 'hydra.config.urls.self.admin=http://admin.hydra.localhost/' \
    --set 'ingress.public.enabled=true' \
    --set 'ingress.admin.enabled=true' \
    --set 'hydra.serve.dangerous.forceHttp=true' \
    --set 'hydra.serve.admin.forceHttp=true' \
    --set 'hydra.dev=true'\
    --set 'maester.enabled=false'

# Wait for pods to be ready
echo "Waiting for Hydra pods to be ready..."
kubectl wait --for=condition=ready pod -l app.kubernetes.io/instance=hyd --timeout=120s

# Forward the service on port 6000
echo "Forwarding Hydra admin service to port 6000..."
kubectl port-forward svc/hyd-hydra-admin 6000:4445 &

echo "Hydra setup complete!"


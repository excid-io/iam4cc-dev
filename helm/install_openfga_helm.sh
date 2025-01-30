#!/bin/bash

# Install OpenFGA using Helm
echo "Installing OpenFGA with Helm..."
helm repo add openfga https://openfga.github.io/helm-charts

helm install openfga openfga/openfga \
    --set 'service.ports.port=8080'

# Wait for OpenFGA pods to be ready
echo "Waiting for OpenFGA pods to be ready..."
kubectl wait --for=condition=ready pod -l app.kubernetes.io/name=openfga --timeout=120s

# Port-forward the service
echo "Port-forwarding OpenFGA to localhost:6005..."
kubectl port-forward svc/openfga 6005:8080 &

echo "OpenFGA setup complete!"


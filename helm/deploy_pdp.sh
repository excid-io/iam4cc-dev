#!/bin/bash

# Enable error handling
set -e

# Set Minikube Docker environment
echo "Switching to Minikube's Docker daemon..."
eval $(minikube docker-env)
eval $(minikube -p minikube docker-env)

# Navigate to the PDP project directory
echo "Navigating to the Policy Decision Point directory..."
cd 'Policy Decision Point'

# Build the Docker image directly in Minikube's Docker daemon
echo "Building the PDP Docker image..."
docker build -t pdp:latest -f API/Dockerfile --build-arg LISTENING_PORT="6002" .

# Return to the root directory
cd ..

# Lint the Helm chart to check for issues
# echo "Linting the Helm chart for PDP..."
# helm lint ./helm/PDP

# Perform a dry-run to simulate installation
# echo "Performing Helm dry-run..."
# helm install --dry-run --debug my-release ./helm/PDP

# Install the Helm chart for PDP
echo "Installing PDP Helm chart..."
helm install pdp ./helm/PDP

# Wait for the PDP pods to be ready
echo "Waiting for PDP pods to be ready..."
kubectl wait --for=condition=ready pod -l app=pdp-pdp --timeout=120s

# Port-forward the PDP service
echo "Port-forwarding PDP service to localhost:6002..."
kubectl port-forward svc/pdp-pdp 6002:6002 &

echo "PDP deployment completed successfully!"


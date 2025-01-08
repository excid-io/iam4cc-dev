#!/bin/bash

# Exit immediately if a command exits with a non-zero status
set -e

# Ensure we are using Minikube's Docker daemon
echo "Switching to Minikube Docker daemon..."
eval $(minikube docker-env)
eval $(minikube -p minikube docker-env)

# Build the Docker image for Registry
echo "Building the Docker image for Registry..."
cd Registry
docker build -t registry -f API/Dockerfile --build-arg LISTENING_PORT="6004" .
cd ..

# Create ConfigMap for AppSettings
echo "Creating ConfigMap for appsettings.Production.json..."
kubectl create configmap registry-appsettings --from-file=./helm/config/registry/appsettings.Production.json --dry-run=client -o yaml | kubectl apply -f -
kubectl get configmap registry-appsettings -o yaml

# Create Secret for Keys
echo "Creating Secret for keys..."
kubectl create secret generic registry-keys --from-file=./helm/config/registry/keys/ --dry-run=client -o yaml | kubectl apply -f -
kubectl get secret registry-keys -o yaml

# Package the Helm chart
echo "Packaging the Helm chart for Registry..."
helm package ./helm/Registry

# Deploy the Helm chart
echo "Deploying the Helm chart for Registry..."
helm install registry ./helm/Registry

# Wait for the Registry pods to be ready
echo "Waiting for Registry pods to be ready..."
sleep 5
kubectl wait --for=condition=ready pod -l app=registry --timeout=600s

# Port-forward the service
echo "Setting up port-forwarding for Registry service..."
kubectl port-forward svc/registry 6004:6004 &

echo "Deployment completed successfully!"


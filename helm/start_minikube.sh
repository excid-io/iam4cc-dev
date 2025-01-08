#!/bin/bash

# Start Minikube
echo "Starting Minikube..."
minikube start

# Get the cluster's IP address
MINIKUBE_IP=$(minikube ip)
echo "Cluster IP Address: $MINIKUBE_IP"

# Enable the ingress addon
echo "Enabling Ingress addon..."
minikube addons enable ingress

echo "Minikube setup complete!"

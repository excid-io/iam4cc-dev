#!/bin/bash

# Stop Minikube
echo "Stopping Minikube..."
minikube stop

# Delete Minikube
echo "Deleting Minikube..."
minikube delete

echo "Minikube cluster stopped and deleted!"

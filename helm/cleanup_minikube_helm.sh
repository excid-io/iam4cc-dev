#!/bin/bash

# Exit immediately if a command exits with a non-zero status
set -e

# Stop using Minikube's Docker daemon
echo "Stopping Minikube's Docker daemon..."
eval $(minikube docker-env -u)

# Uninstall our Helm releases
echo "Uninstalling the Helm releases related to the IAM4CC project..."

# Define the releases to uninstall
RELEASES=("pdp" "registry" "openfga" "hyd" "oathkeeper")

for release in "${RELEASES[@]}"; do
  if helm list --short | grep -q "^$release\$"; then
    echo "Uninstalling Helm release: $release"
    helm uninstall "$release"
  else
    echo "Helm release $release not found. Skipping..."
  fi
done

# Verify that no Helm releases remain, but do not uninstall in case there are others not related to IAM4CC
if [ -z "$(helm list --short)" ]; then
  echo "All specified Helm releases have been successfully uninstalled."
else
  echo "Some Helm releases may still exist. Please check manually:"
  helm list
fi

# Confirmation message
echo "Cleanup completed successfully!"


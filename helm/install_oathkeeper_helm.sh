#!/bin/bash

# Define relative paths for configuration files
CONFIG_PATH="./config/oathkeeper/config.yaml"
RULES_PATH="./config/oathkeeper/rules.json"

# Check if configuration files exist
if [ ! -f "$CONFIG_PATH" ]; then
  echo "Error: Oathkeeper config file not found at $CONFIG_PATH"
  exit 1
fi

if [ ! -f "$RULES_PATH" ]; then
  echo "Error: Access rules file not found at $RULES_PATH"
  exit 1
fi

# Install Oathkeeper using Helm
# Note: Use the debug flag (--debug \) to see all the details of the installations and ensure that the configuration and rules where set correctly.
echo "Installing Oathkeeper with Helm..."
helm install oathkeeper ory/oathkeeper \
    -f "$CONFIG_PATH" \
    --set-file "oathkeeper.accessRules=$RULES_PATH"

# Wait for Oathkeeper pods to be ready
echo "Waiting for Oathkeeper pods to be ready..."
kubectl wait --for=condition=ready pod -l app.kubernetes.io/name=oathkeeper --timeout=120s

# Port-forward the service
echo "Port-forwarding Oathkeeper API to localhost:6003..."
kubectl port-forward svc/oathkeeper-api 6003:4456 &

echo "Oathkeeper setup complete!"


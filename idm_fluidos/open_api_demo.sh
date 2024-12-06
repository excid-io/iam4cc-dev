make clean

# Stop and remove all Docker containers
echo "Stopping all Docker containers..."
docker stop $(docker ps -a -q)
echo "Removing all Docker containers..."
docker rm $(docker ps -a -q)

# Stop the OpenAPI demo if running
make stop-openapi-demo
make run-openapi-demo
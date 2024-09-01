#!/bin/bash

# Stop and remove the container
docker-compose down 

# Step 2: Build the Docker images and start the containers with the latest changes
docker-compose up --build -d
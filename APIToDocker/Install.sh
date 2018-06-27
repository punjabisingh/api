#!/bin/bash

docker stop API
docker rmi apitodocker --force
docker rm API
docker build -t apitodocker .
docker run -d -p 8080:3000 --name API apitodocker
docker network connect simple-network API
#!/bin/bash

#docker stop API
#docker rmi apitodocker --force
#docker rm API
#docker build -t apitodocker .
#docker run -d -p 8080:3100 --name API apitodocker
#docker network connect simple-network API
pushd ../
docker-compose build
docker-compose up --detach
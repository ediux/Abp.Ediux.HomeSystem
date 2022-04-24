@echo off
set TAG=%1
docker build -t "ediuxcity/ediuxhomesystemapihost:%TAG%" -f .\Dockerfile_APIHost .. --build-arg BUILDMODE=%2 --no-cache
docker build -t "ediuxcity/ediuxhomesystemblazor:%TAG%" -f .\Dockerfile_Client .. --build-arg BUILDMODE=%2 --no-cache
docker login
docker push "ediuxcity/ediuxhomesystemapihost:%TAG%"
docker push "ediuxcity/ediuxhomesystemblazor:%TAG%"
//Those are commands to build the docker image 
docker build -t library .

docker run -d -p 80:80 library

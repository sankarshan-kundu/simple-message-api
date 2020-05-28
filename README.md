# simple-message-api
Simple RESTful API with . NET Core

## Build and run the Docker image

* `docker build -t simple-message-api .` 
* `docker run --env-file ./.env -d -p 8080:80 --name message-api simple-message-api`

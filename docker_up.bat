docker build -t it-api-service --progress=plain -f ./build/Dockerfile.Api .
docker build -t it-storage-service --progress=plain -f ./build/Dockerfile.Storage .

docker run --rm -d --name rabbitui -p 8083:15672 -p 5672:5672 --hostname rabbitui rabbitmq:3.9.9-management
docker run --rm -d --name postgres -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=postgres -p 5432:5432 postgres:14.1
docker run --rm -d --name pgadmin --rm -p 8084:80 -e PGADMIN_DEFAULT_EMAIL=user@domain.com -e PGADMIN_DEFAULT_PASSWORD=SuperSecret dpage/pgadmin4:6.2
docker run --rm -d --name api-service --rm -p 8081:80 it-api-service
docker run --rm -d --name storage-service --rm -p 8082:80 it-storage-service

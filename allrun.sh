docker stop $(docker ps -a -q)
docker rm $(docker ps -a -q)
docker rmi -f docker-dotnetcore-redis
docker build -t docker-dotnetcore-redis .
docker-compose up -d
docker tag docker-dotnetcore-redis vdonthireddy/docker-dotnetcore-redis:1.0
docker push vdonthireddy/docker-dotnetcore-redis:1.0
docker rmi -f $(docker images -f "dangling=true" -q)
echo '3...'
sleep 1
echo '2...'
sleep 1
echo '1...'
sleep 1
curl http://localhost:8435/api/employee

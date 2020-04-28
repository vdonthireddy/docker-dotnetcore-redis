docker stop $(docker ps -a -q)
docker rm $(docker ps -a -q)
docker rmi -f docker-dotnetcore-redis
docker build -t docker-dotnetcore-redis .
docker-compose up -d
echo '5...'
sleep 1
echo '4...'
sleep 1
echo '3...'
sleep 1
echo '2...'
sleep 1
echo '1...'
sleep 1
curl http://localhost:8435/api/employee

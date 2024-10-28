@echo off
docker-compose -f .\docker-compose.yml -f .\docker-compose-dev.yml up auth_service --build --force-recreate
pause
# För att bygga och push:a images till Docker Hub

🟢


```bash
docker-compose build
docker-compose push
```

# För att köra utan .env filen med standardvärden

```bash
docker-compose up -d
```

# För att köra med specifika värden direkt från kommandoraden

```bash
docker-compose up -d \
  -e DB_USERNAME=admin \
  -e DB_PASSWORD=secure_password \
  -e BACKEND_DB_URL=jdbc:h2:file:/app/data/production_db
´´´

# Alternativt, ange alla variabler på samma rad

```bash
DB_USERNAME=admin DB_PASSWORD=secure_password BACKEND_DB_URL=jdbc:h2:file:/app/data/production_db docker-compose up -d
```

# För deployment till en server, skapa en deploy.sh script

```bash
docker-compose \
  -e DOCKER_REGISTRY=yourcompany \
  -e TAG=v1.0 \
  -e DB_USERNAME=production_user \
  -e DB_PASSWORD=production_pass \
  -e BACKEND_DB_URL=jdbc:h2:file:/app/data/prod_db \
  up -d
```

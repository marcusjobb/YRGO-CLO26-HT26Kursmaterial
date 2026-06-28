---

title: Docker Cross-Platform Build Guide
author: Marcus Ackre Medina
type: example
topic: cloud
difficulty: 1
language: bash
status: adapted
marcus_voice: true
source: "Old_courses/2025/java/8_cloud_integration/lectures/15_demo_azure_devops/docs/m1-m2-docker-build.md"
description: "- M1/M2 Macs har ARM64 (Apple Silicon) arkitektur"
tags: ["bash", "build", "cloud", "cross-platform", "docker", "git", "installation", "visual-studio"]
week_fit: []
---

# Docker Cross-Platform Build Guide

🟢


---

## För M1/M2 Mac till x64/amd64 (Azure)

### 🎯 Problemet
- M1/M2 Macs har ARM64 (Apple Silicon) arkitektur
- Azure Container Instances kör x64/amd64 arkitektur
- Images byggda på M1/M2 fungerar inte i Azure utan cross-platform build

---

## Metod 1: Docker Buildx (Rekommenderad)

### Steg 1: Aktivera Docker Buildx
```bash
# Kontrollera att buildx finns
docker buildx version

# Skapa en ny builder instance som stödjer multi-platform
docker buildx create --name multiplatform --use

# Starta builder
docker buildx inspect --bootstrap

# Verifiera att både linux/amd64 och linux/arm64 stöds
docker buildx ls
```

### Steg 2: Bygg för x64/amd64
```bash
# Backend - Bygg OCH pusha direkt till ACR (--push krävs för multi-platform)
docker buildx build \
  --platform linux/amd64 \
  --tag catapiacr.azurecr.io/cat-api-backend:v1 \
  --push \
  ./cat_mvc_auth_swagger_lomboc

# Frontend - Bygg OCH pusha direkt till ACR
docker buildx build \
  --platform linux/amd64 \
  --tag catapiacr.azurecr.io/cat-api-frontend:v1 \
  --push \
  ./cat_mvc_frontend
```

### Steg 3: För lokal testning (ladda till lokal Docker)
```bash
# Om du vill testa lokalt först (tar längre tid)
docker buildx build \
  --platform linux/amd64 \
  --tag cat-api-backend:test \
  --load \
  ./cat_mvc_auth_swagger_lomboc

# Verifiera arkitektur
docker inspect cat-api-backend:test | grep Architecture
# Ska visa: "Architecture": "amd64"
```

---

## Metod 2: Traditionell Docker Build med Platform Flag

### För Docker Desktop 4.x+
```bash
# Sätt default platform för alla builds
export DOCKER_DEFAULT_PLATFORM=linux/amd64

# Eller specifikt per build
docker build --platform linux/amd64 \
  -t catapiacr.azurecr.io/cat-api-backend:v1 \
  ./cat_mvc_auth_swagger_lomboc

docker build --platform linux/amd64 \
  -t catapiacr.azurecr.io/cat-api-frontend:v1 \
  ./cat_mvc_frontend

# Push till ACR
docker push catapiacr.azurecr.io/cat-api-backend:v1
docker push catapiacr.azurecr.io/cat-api-frontend:v1
```

---

## Metod 3: Azure ACR Build (Bygger i molnet)
*Bästa alternativet - bygger direkt på Azure's x64 servrar*

```bash
# Backend - Bygg direkt i ACR (ingen lokal Docker behövs!)
az acr build \
  --registry catapiacr \
  --image cat-api-backend:v1 \
  --platform linux/amd64 \
  ./cat_mvc_auth_swagger_lomboc

# Frontend - Bygg direkt i ACR
az acr build \
  --registry catapiacr \
  --image cat-api-frontend:v1 \
  --platform linux/amd64 \
  ./cat_mvc_frontend
```

**Fördelar med ACR Build:**
- ✅ Ingen lokal Docker build
- ✅ Garanterat rätt arkitektur
- ✅ Snabbare på M1/M2
- ✅ Ingen "platform mismatch" risk

---

## 🔧 Troubleshooting

### Problem: "exec format error" i Azure
**Orsak:** Image byggd för ARM64 istället för AMD64
```bash
# Lösning: Rebuild med --platform linux/amd64
docker buildx build --platform linux/amd64 --push ...
```

### Problem: "--load" fungerar inte med multi-platform
**Orsak:** Docker kan bara ladda en arkitektur åt gången lokalt
```bash
# Lösning: Använd --push direkt till registry istället
docker buildx build --platform linux/amd64 --push ...
```

### Problem: Buildx saknas
```bash
# Installera/uppdatera Docker Desktop till senaste version
# Eller installera buildx manuellt:
docker buildx install
```

### Problem: Slow builds on M1/M2
**Orsak:** Emulering av x64 på ARM
```bash
# Lösning 1: Använd Azure ACR Build istället (bygger i molnet)
az acr build --registry catapiacr ...

# Lösning 2: Använd GitHub Actions för att bygga
# (se github-actions-build.yml nedan)
```

---

## Optimering för M1/M2 Utvecklare

### Docker Compose för Multi-Platform
```yaml
# docker-compose.build.yml
version: '3.8'
services:
  backend:
    build:
      context: ./cat_mvc_auth_swagger_lomboc
      dockerfile: Dockerfile
      platforms:
        - "linux/amd64"
        - "linux/arm64"
    image: catapiacr.azurecr.io/cat-api-backend:v1

  frontend:
    build:
      context: ./cat_mvc_frontend
      dockerfile: Dockerfile
      platforms:
        - "linux/amd64"
        - "linux/arm64"
    image: catapiacr.azurecr.io/cat-api-frontend:v1
```

### Build Script för M1/M2
```bash
#!/bin/bash
# build-for-azure.sh

echo "🏗️ Building x64 images for Azure on M1/M2 Mac..."

# Logga in till ACR
az acr login --name catapiacr

# Använd buildx för multi-platform
docker buildx use multiplatform || docker buildx create --name multiplatform --use

# Build och push backend
echo "📦 Building backend..."
docker buildx build \
  --platform linux/amd64 \
  --tag catapiacr.azurecr.io/cat-api-backend:latest \
  --tag catapiacr.azurecr.io/cat-api-backend:$(git rev-parse --short HEAD) \
  --push \
  ./cat_mvc_auth_swagger_lomboc

# Build och push frontend
echo "📦 Building frontend..."
docker buildx build \
  --platform linux/amd64 \
  --tag catapiacr.azurecr.io/cat-api-frontend:latest \
  --tag catapiacr.azurecr.io/cat-api-frontend:$(git rev-parse --short HEAD) \
  --push \
  ./cat_mvc_frontend

echo "✅ Build complete! Images pushed to ACR"
```

---

## GitHub Actions för M1/M2 Utvecklare

Om builds är för långsamma lokalt, använd GitHub Actions:

```yaml
# .github/workflows/build-and-push.yml
name: Build and Push to ACR

on:
  push:
    branches: [ main ]
  workflow_dispatch:

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    
    - name: Login to ACR
      uses: azure/docker-login@v1
      with:
        login-server: catapiacr.azurecr.io
        username: ${{ secrets.ACR_USERNAME }}
        password: ${{ secrets.ACR_PASSWORD }}
    
    - name: Build and push Backend
      run: |
        docker build ./cat_mvc_auth_swagger_lomboc \
          -t catapiacr.azurecr.io/cat-api-backend:latest \
          -t catapiacr.azurecr.io/cat-api-backend:${{ github.sha }}
        docker push catapiacr.azurecr.io/cat-api-backend --all-tags
    
    - name: Build and push Frontend
      run: |
        docker build ./cat_mvc_frontend \
          -t catapiacr.azurecr.io/cat-api-frontend:latest \
          -t catapiacr.azurecr.io/cat-api-frontend:${{ github.sha }}
        docker push catapiacr.azurecr.io/cat-api-frontend --all-tags
```

---

## 📋 Checklista för M1/M2 Utvecklare

- [ ] Docker Desktop uppdaterat till senaste version
- [ ] Buildx aktiverat och konfigurerat
- [ ] ALLTID använd `--platform linux/amd64` för Azure
- [ ] Överväg ACR Build för snabbare builds
- [ ] Testa images med `docker inspect` före deployment
- [ ] Använd GitHub Actions för production builds

---

## 🎓 För undervisning

**Förklara för studenter:**
1. **Arkitektur-skillnader**: ARM64 (Apple) vs x64/amd64 (Intel/AMD)
2. **Container portabilitet**: Inte alltid "build once, run anywhere"
3. **Build strategies**: Lokal vs moln, tid vs resurser
4. **Real-world scenario**: Många utvecklare har M1/M2, production kör x64

**Demo:**
```bash
# Visa arkitektur-problemet
docker build -t test:arm ./cat_mvc_auth_swagger_lomboc
docker inspect test:arm | grep Architecture
# Output: "Architecture": "arm64"

# Visa lösningen
docker build --platform linux/amd64 -t test:amd ./cat_mvc_auth_swagger_lomboc  
docker inspect test:amd | grep Architecture
# Output: "Architecture": "amd64"
```

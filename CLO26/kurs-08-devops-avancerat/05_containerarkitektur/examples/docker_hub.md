# Deploying images to Docker Hub

🟢


## Prerequisite: Log in to Docker Hub

```bash
docker login
```

## Option 1: Build and push in separate steps

### 1. Build the image

```bash
# For standard architecture
docker build -t deskavaenkelt/calculator-app:1.0.0 .

# For cross-platform builds (separate)
# Arm to x86
docker buildx create --use
docker buildx build --platform linux/amd64 -t deskavaenkelt/calculator-app:1.0.0 .

# x86 to Arm
docker buildx create --use
docker buildx build --platform linux/arm64 -t deskavaenkelt/calculator-app:1.0.0 .
```

### 2. Push the image to Docker Hub

```bash
docker push deskavaenkelt/calculator-app:1.0.0
```

## Option 2: Build and push in a single step

```bash
# Standard architecture (build and push in one command)
docker buildx build -t deskavaenkelt/calculator-app:1.0.0 --push .

# Cross-platform build and push
# Arm to x86
docker buildx create --use
docker buildx build --platform linux/amd64 -t deskavaenkelt/calculator-app:1.0.0 --push .

# x86 to Arm
docker buildx create --use
docker buildx build --platform linux/arm64 -t deskavaenkelt/calculator-app:1.0.0 --push .

# Multi-platform (both architectures)
docker buildx create --use
docker buildx build --platform linux/amd64,linux/arm64 -t deskavaenkelt/calculator-app:1.0.0 --push .
```

## Verify deployment

```bash
# Check local images
docker images

# Pull the image to verify it's on Docker Hub
docker pull deskavaenkelt/calculator-app:1.0.0
```

## Run container

```bash
docker run -d -p 8080:8080 deskavaenkelt/calculator-app:1.0.0
```



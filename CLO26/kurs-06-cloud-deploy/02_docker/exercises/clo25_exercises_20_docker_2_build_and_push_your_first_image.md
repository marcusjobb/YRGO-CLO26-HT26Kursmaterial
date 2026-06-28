---

title: Build and Push Your First Image
author: Marcus Ackre Medina
type: exercise
topic: docker
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/exercises/20-docker/2-build-and-push-your-first-image.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-"
tags: ["azure", "build", "cloud", "docker", "dotnet", "exercise", "first", "git", "image", "linux"]
week_fit: []
---

Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-
Server Foundation-
Network Foundation-
Deployment-
Cloud Databases-
Webapp Development-
Code Collaboration-
Docker-- Run Your First Container-- Build and Push Your First Image-- Containerize CloudSoft Recruitment-- Docker Compose -- Local Development Stack-- Multi-Platform BuildsTutorials

# Build and Push Your First Image

🟡


# Build and Push Your First Image

## Goal

Write a Dockerfile that packages a static HTML page into a custom nginx image, build and tag the image, then push it to Docker Hub so anyone can pull and run it.

> **What you’ll learn:**
>
> * How to write a Dockerfile with `FROM`, `COPY`, and `EXPOSE`
> * How image layers work and appear during the build process
> * How to tag images with your Docker Hub username
> * How to push and pull images from a container registry

## Prerequisites

> **Before starting, ensure you have:**
>
> * ✓ Docker Desktop installed and running
> * ✓ Comfortable running containers with `docker run` and port mapping
> * ✓ A text editor (VS Code recommended)

## Exercise Steps

### Overview

1. **Create the Project Files**
2. **Write the Dockerfile**
3. **Build and Run Your Image**
4. **Push to Docker Hub**
5. **Verify Your Published Image**

### **Step 1:** Create the Project Files

Before writing a Dockerfile, you need the files that will go into your image. In this step you will create a simple HTML page that represents a CloudSoft company landing page. This is the content your custom nginx image will serve.

1. **Create** a project directory:

   ```
   mkdir exercise-2
   ```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
2. **Create** a file named `index.html` in that directory with the following content:

   > `exercise-2/index.html`

   ```
   <!DOCTYPE html>
   <html lang="en">
   <head>
       <meta charset="UTF-8">
       <meta name="viewport" content="width=device-width, initial-scale=1.0">
       <title>CloudSoft Recruitment Portal</title>
       <style>
           * { margin: 0; padding: 0; box-sizing: border-box; }
           body {
               font-family: 'Segoe UI', sans-serif;
               min-height: 100vh;
               background: linear-gradient(135deg, #1a1a2e, #16213e, #0f3460);
               color: white;
               display: flex;
               flex-direction: column;
               align-items: center;
               justify-content: center;
           }
           .hero { text-align: center; padding: 2rem; }
           h1 { font-size: 3rem; margin-bottom: 1rem; }
           .subtitle { font-size: 1.3rem; opacity: 0.8; margin-bottom: 2rem; }
           .badge {
               display: inline-block;
               background: rgba(255,255,255,0.1);
               border: 1px solid rgba(255,255,255,0.2);
               padding: 0.5rem 1.5rem;
               border-radius: 2rem;
               font-size: 0.9rem;
           }
       </style>
   </head>
   <body>
       <div class="hero">
           <h1>CloudSoft Recruitment</h1>
           <p class="subtitle">Find your next opportunity in cloud technology</p>
           <span class="badge">Served from a Docker container</span>
       </div>
   </body>
   </html>
   ```

> ✓ **Quick check:** The file `index.html` exists in `exercise-2/`

### **Step 2:** Write the Dockerfile

A Dockerfile is a text file with instructions for building an image. Each instruction creates a **layer** in the image. Docker caches layers to speed up subsequent builds. In this step you will write a minimal Dockerfile that takes the nginx base image and adds your HTML page.

1. **Create** a file named `Dockerfile` (no extension) in the same directory:

   > `exercise-2/Dockerfile`

   ```
   FROM nginx:alpine

   COPY index.html /usr/share/nginx/html/index.html

   EXPOSE 80
   ```

> ℹ **Concept Deep Dive**
>
> Each Dockerfile instruction has a specific purpose:
>
> * `FROM nginx:alpine` — Sets the **base image**. Every Dockerfile starts with `FROM`. The `alpine` tag means a minimal Linux distribution (~40MB vs ~190MB for the full image). This base image already has nginx installed and configured.
> * `COPY index.html /usr/share/nginx/html/index.html` — Copies your file into the image as a new **layer**. This replaces nginx’s default welcome page with your custom page.
> * `EXPOSE 80` — Documents that the container listens on port 80. This is informational — it does not actually publish the port. You still need `-p` when running the container.
>
> Each instruction creates a layer. Docker caches layers, so if you change only `index.html`, Docker reuses the cached `FROM` layer and only rebuilds from `COPY` onward.
>
> ⚠ **Common Mistakes**
>
> * The filename must be exactly `Dockerfile` (capital D, no extension). `dockerfile` works but `Dockerfile` is the convention.
> * Copying to the wrong path inside the container. For nginx, static files go in `/usr/share/nginx/html/`.
> * Forgetting that `EXPOSE` does not publish ports — you still need `-p` at runtime.
>
> ✓ **Quick check:** `Dockerfile` exists alongside `index.html` with three instructions

### **Step 3:** Build and Run Your Image

Now you will turn your Dockerfile into an image with `docker build` and then run a container from it. Pay attention to the build output — it shows each layer being created.

1. **Make sure** you are in the `exercise-2` directory
2. **Build** the image:

   ```
   docker build -t cloudsoft-static .
   ```
3. **Observe** the build output — each step corresponds to a Dockerfile instruction:

   ```
   Step 1/3 : FROM nginx:alpine
   Step 2/3 : COPY index.html /usr/share/nginx/html/index.html
   Step 3/3 : EXPOSE 80
   ```
4. **Run** a container from your new image:

   ```
   docker run --name cloudsoft-static -d -p 8080:80 cloudsoft-static
   ```
5. **Open** `http://localhost:8080` and **confirm** your CloudSoft page appears
6. **Check** the image size:

   ```
   docker images cloudsoft-static
   ```

> ℹ **Concept Deep Dive**
>
> The `docker build` command:
>
> * `-t cloudsoft-static` **tags** the image with a name. Without a tag after the colon, Docker defaults to `:latest`.
> * `.` is the **build context** — the directory Docker sends to the build engine. Docker can only `COPY` files from within this context. A `.dockerignore` file works like `.gitignore` to exclude files from the context.
>
> Notice the image is very small (around 45MB) because we used `nginx:alpine` as the base. Image size matters for deployment speed, storage costs, and security — fewer packages means fewer potential vulnerabilities.
>
> ✓ **Quick check:** Browser shows your CloudSoft page at `http://localhost:8080`

### **Step 4:** Push to Docker Hub

Docker Hub is the default public registry for Docker images. By pushing your image there, anyone in the world can pull and run it. In this step you will create a Docker Hub account, tag your image with your username, and push it.

1. **Create** a Docker Hub account at <https://hub.docker.com> (if you do not have one)
2. **Log in** from the terminal:

   ```
   docker login
   ```
3. **Tag** your image with your Docker Hub username:

   ```
   docker tag cloudsoft-static YOUR_DOCKERHUB_USERNAME/cloudsoft-static:1.0
   ```

   Replace `YOUR_DOCKERHUB_USERNAME` with your actual username.
4. **Push** the image:

   ```
   docker push YOUR_DOCKERHUB_USERNAME/cloudsoft-static:1.0
   ```
5. **Verify** the image is visible at `https://hub.docker.com/r/YOUR_DOCKERHUB_USERNAME/cloudsoft-static`

> ⚠ **Platform Awareness**
>
> On Docker Hub, notice the **OS/ARCH** column next to your image tag. If you built the image on a Mac with Apple Silicon (M1/M2/M3/M4), it will show `linux/arm64`. This means the image will only run correctly on ARM-based hosts. If you deploy it to a server running `linux/amd64` (which most cloud VMs use, including Azure), the container will either fail to start or run through slow emulation.
>
> This is a real deployment pitfall — your image works perfectly on your Mac but breaks in production. We will solve this in a later exercise with **multi-platform builds**.

> ℹ **Concept Deep Dive**
>
> Docker Hub image names follow the format `username/repository:tag`:
>
> * **username** — Your Docker Hub account (or an organization name)
> * **repository** — The image name
> * **tag** — A version label. If omitted, Docker defaults to `latest`
>
> When you `docker push`, Docker uploads only the layers that do not already exist in the registry. Since `nginx:alpine` is a public image, its layers are already on Docker Hub — only your custom layer (the HTML file) gets uploaded.
>
> In production, you would use a private registry like **Azure Container Registry** instead of Docker Hub. The workflow is the same: tag, login, push.
>
> ⚠ **Common Mistakes**
>
> * Forgetting to `docker login` before pushing results in `denied: requested access to the resource is denied`
> * The tag must include your username prefix. `docker push cloudsoft-static` (without username) will fail because Docker tries to push to the official library
> * Docker Hub free accounts have rate limits for pulls. For classroom use this is rarely an issue
>
> ✓ **Quick check:** `docker push` completes successfully and the image appears on Docker Hub

### **Step 5:** Verify Your Published Image

The real test of a registry image is whether someone else can pull and run it. In this step you will delete your local copy and pull it fresh from Docker Hub to prove it works end-to-end.

1. **Stop and remove** the running container:

   ```
   docker rm -f cloudsoft-static
   ```
2. **Remove** the local image:

   ```
   docker rmi YOUR_DOCKERHUB_USERNAME/cloudsoft-static:1.0
   ```
3. **Pull** the image from Docker Hub:

   ```
   docker pull YOUR_DOCKERHUB_USERNAME/cloudsoft-static:1.0
   ```
4. **Run** it:

   ```
   docker run -d -p 8080:80 YOUR_DOCKERHUB_USERNAME/cloudsoft-static:1.0
   ```
5. **Verify** the page loads at `http://localhost:8080`
6. **Note the platform:** Check which architecture the image was built for:

   ```
   docker image inspect YOUR_DOCKERHUB_USERNAME/cloudsoft-static:1.0 \
     --format '{{.Architecture}}'
   ```

   This will show `amd64` or `arm64` depending on your machine. If a classmate with a different CPU architecture pulls your image, it may not run correctly. This is a limitation we will address in a later exercise with multi-platform builds.
7. **Clean up** containers when done:

   ```
   docker rm -f $(docker ps -aq)
   ```
8. **Remove** all images as well:

   ```
   docker rmi -f $(docker images -aq)
   ```

> ✓ **Success indicators:**
>
> * Image pulled successfully from Docker Hub
> * Container runs and serves your CloudSoft page
> * You can see the image architecture matches your machine
>
> ✓ **Final verification checklist:**
>
> * ☐ Dockerfile created with `FROM`, `COPY`, `EXPOSE`
> * ☐ Image built and tagged with your username
> * ☐ Image pushed to Docker Hub
> * ☐ Image pulled from Docker Hub and runs correctly
> * ☐ You understand that the image only works on your machine’s architecture

## Common Issues

> **If you encounter problems:**
>
> **“denied: requested access to the resource is denied”:** You are not logged in or the image tag does not include your username. Run `docker login` and check your tag.
>
> **“manifest unknown”:** The image or tag does not exist on Docker Hub. Verify the exact name and tag you used when pushing.
>
> **Build fails with “COPY failed”:** The file you are trying to copy does not exist in the build context. Make sure `index.html` is in the same directory as your `Dockerfile`.
>
> **Image runs but shows nginx default page:** The `COPY` destination path is wrong. It must be `/usr/share/nginx/html/index.html`.
>
> **Still stuck?** Run `docker build` again and read the output carefully — it will show which step fails and why.

## Summary

You’ve successfully created a custom Docker image and published it which:

* ✓ Demonstrates how Dockerfiles define images layer by layer
* ✓ Uses `FROM`, `COPY`, and `EXPOSE` — the three most fundamental instructions
* ✓ Publishes to Docker Hub so others can use your image
* ✓ Reveals a platform limitation that multi-platform builds solve

> **Key takeaway:** A Dockerfile is a recipe for building an image. Each instruction adds a layer, and Docker caches layers for fast rebuilds. Docker Hub (or any container registry) is where you publish images so they can be deployed anywhere — but by default, images only work on the architecture they were built on.

## Going Deeper (Optional)

> **Want to explore more?**
>
> * Create a `.dockerignore` file to exclude files from the build context
> * Run `docker history cloudsoft-static` to see each layer’s size and creation command
> * Rebuild with `docker build --no-cache -t cloudsoft-static .` and compare build times with and without cache
> * Try adding a second HTML page and a CSS file to your image

## Done! 🎉

Great job! You’ve written your first Dockerfile, built a custom image, and published it to Docker Hub. Next you will containerize a real .NET application using multi-stage builds.

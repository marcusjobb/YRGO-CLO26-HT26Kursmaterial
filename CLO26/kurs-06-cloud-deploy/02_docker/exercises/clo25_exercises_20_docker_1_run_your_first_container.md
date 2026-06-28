---

title: Run Your First Container
author: Marcus Ackre Medina
type: exercise
topic: docker
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/exercises/20-docker/1-run-your-first-container.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-"
tags: ["cloud", "container", "docker", "exercise", "first", "linux", "networking", "run", "your"]
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

# Run Your First Container

🟡


# Run Your First Container

## Goal

Run a container from a public image to understand how containers work, how port mapping connects your browser to a containerized application, and how bind mounts share files between your machine and a container.

> **What you’ll learn:**
>
> * How containers run isolated processes from images
> * How port mapping connects host ports to container ports
> * How bind mounts share files between host and container
> * Essential lifecycle commands: `docker ps`, `docker stop`, `docker rm`

## Prerequisites

> **Before starting, ensure you have:**
>
> * ✓ Docker Desktop installed and running (verify with `docker --version`)
> * ✓ A terminal (Terminal on Mac, PowerShell on Windows)
> * ✓ A text editor (VS Code recommended)
> * ✓ A web browser

## Exercise Steps

### Overview

1. **Pull and Run an nginx Container**
2. **Explore Port Mapping**
3. **Serve a Custom Page with Bind Mounts**
4. **Persist Data with Volume Mounts**
5. **Manage Container Lifecycle**
6. **Verify Your Understanding**

### **Step 1:** Pull and Run an nginx Container

Docker Hub is a public registry with thousands of pre-built images ready to use. In this step you will pull the official nginx web server image and run it as a container. This is the fastest way to get a web server running — no installation, no configuration, just one command.

1. **Open** your terminal
2. **List** the images currently on your machine:

   ```
   docker images
   ```

   You should see an empty list (or no nginx image). This confirms you are starting from a clean state.
3. **Pull** the nginx image from Docker Hub:

   ```
   docker pull nginx
   ```
4. **List** images again to confirm nginx was downloaded:

   ```
   docker images
   ```

   You should now see the `nginx` image in the list.
5. **Run** a container from the image:

   ```
   docker run --name my-nginx -d -p 8080:80 nginx
   ```
6. **Verify** the container is running:

   ```
   docker ps
   ```

   You should see `my-nginx` listed with status `Up` and port mapping `0.0.0.0:8080->80/tcp`.
7. **Open** your browser and **navigate to** `http://localhost:8080`
8. **Confirm** you see the “Welcome to nginx!” default page

> ℹ **Concept Deep Dive**
>
> Let’s break down the `docker run` command:
>
> * `--name my-nginx` gives the container a human-readable name
> * `-d` runs the container in **detached mode** (in the background)
> * `-p 8080:80` maps port 8080 on your machine to port 80 inside the container
> * `nginx` is the image name (pulled from Docker Hub if not already local)
>
> An **image** is a read-only template — like a class in programming. A **container** is a running instance of that image — like an object. You can create multiple containers from the same image.
>
> ⚠ **Common Mistakes**
>
> * If Docker Desktop is not running, you will see `Cannot connect to the Docker daemon`
> * If port 8080 is already in use, you will see `Bind for 0.0.0.0:8080 failed: port is already allocated`
>
> ✓ **Quick check:** Browser shows “Welcome to nginx!” at `http://localhost:8080`

### **Step 2:** Explore Port Mapping

Port mapping is how you expose a containerized service to your host machine. The container runs in its own isolated network — without port mapping, nothing outside the container can reach it. In this step you will stop the current container and re-run it with a different port to see how the mapping works.

1. **Stop** the running container:

   ```
   docker stop my-nginx
   ```
2. **Remove** the stopped container:

   ```
   docker rm my-nginx
   ```
3. **Run** a new container with a different host port:

   ```
   docker run --name my-nginx -d -p 9090:80 nginx
   ```
4. **Navigate to** `http://localhost:9090` in your browser
5. **Confirm** the nginx welcome page now appears on port 9090

> ℹ **Concept Deep Dive**
>
> The `-p` flag syntax is `HOST_PORT:CONTAINER_PORT`. The container always listens on port 80 (nginx’s default). You choose which port on your machine maps to it. This means you could run multiple nginx containers on different host ports:
>
> ```
> docker run -d -p 8080:80 --name web1 nginx
> docker run -d -p 8081:80 --name web2 nginx
> docker run -d -p 8082:80 --name web3 nginx
> ```
>
> Each container is an isolated instance with its own port mapping.
>
> ✓ **Quick check:** `docker ps` shows `my-nginx` with port mapping `9090->80`

### **Step 3:** Serve a Custom Page with Bind Mounts

So far you have been seeing nginx’s default welcome page. In this step you will create your own HTML page and use a bind mount to make it available inside the container. This lets you serve custom content without building a new image.

1. **Create** a project directory:

   ```
   mkdir exercise-1
   ```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
2. **Create** a file named `index.html` in that directory with the following content:

   > `exercise-1/index.html`

   ```
   <!DOCTYPE html>
   <html lang="en">
   <head>
       <meta charset="UTF-8">
       <meta name="viewport" content="width=device-width, initial-scale=1.0">
       <title>CloudSoft</title>
       <style>
           body {
               font-family: 'Segoe UI', sans-serif;
               display: flex;
               justify-content: center;
               align-items: center;
               min-height: 100vh;
               margin: 0;
               background: linear-gradient(135deg, #0078d4, #00b4d8);
               color: white;
           }
           .container { text-align: center; }
           h1 { font-size: 3rem; margin-bottom: 0.5rem; }
           p { font-size: 1.2rem; opacity: 0.9; }
       </style>
   </head>
   <body>
       <div class="container">
           <h1>CloudSoft Recruitment</h1>
           <p>Running in a Docker container</p>
       </div>
   </body>
   </html>
   ```
3. **Stop and remove** all existing containers (including web1, web2, web3 if you tried the example above):

   ```
   docker rm -f $(docker ps -aq)
   ```
4. **Run** a new container with a bind mount:

   ```
   docker run --name my-nginx -d -p 8080:80 \
     -v ./exercise-1:/usr/share/nginx/html:ro \
     nginx
   ```
5. **Open** `http://localhost:8080` and **confirm** you see your custom CloudSoft page
6. **Edit** the `index.html` file — change the heading text to something else
7. **Refresh** the browser and **confirm** the change appears immediately

> ℹ **Concept Deep Dive**
>
> The `-v` flag creates a **bind mount** that maps a directory on your host to a directory inside the container:
>
> `-v HOST_PATH:CONTAINER_PATH:OPTIONS`
>
> * The host path (`./exercise-1`) is your local directory
> * The container path (`/usr/share/nginx/html`) is where nginx serves files from
> * The `:ro` option makes the mount **read-only** — the container can read files but cannot modify them
>
> Bind mounts are useful during development because changes on your host are immediately visible inside the container. In production, you would typically copy files into the image instead (which you will learn in the next exercise).
>
> ⚠ **Common Mistakes**
>
> * On Windows, use forward slashes or the full path: `-v C:/Users/you/project/exercise-1:/usr/share/nginx/html:ro`
> * Forgetting the container path (`/usr/share/nginx/html`) will not mount anything
> * Typos in the host path will create an empty mount — nginx will return a 403 Forbidden error
>
> ✓ **Quick check:** Browser shows your custom page, and live edits reflect immediately on refresh

### **Step 4:** Persist Data with Volume Mounts

In Step 3 you used a bind mount where you controlled the files on your host. In this step you will use a **named volume** — storage managed by Docker. The key difference: data in a named volume persists even after the container is removed. You will copy your custom page into a volume, destroy the container, and see the page survive in a new container.

1. **Stop and remove** the existing container:

   ```
   docker rm -f my-nginx
   ```
2. **Create** a named volume:

   ```
   docker volume create web-content
   ```
3. **Run** a new container with the named volume:

   ```
   docker run --name my-nginx -d -p 8080:80 \
     -v web-content:/usr/share/nginx/html \
     nginx
   ```
4. **Open** `http://localhost:8080` and **confirm** you see the default nginx welcome page

   The volume is empty on creation, but Docker copies nginx’s default files into it on first use.
5. **Copy** your custom page into the running container:

   ```
   docker cp exercise-1/index.html my-nginx:/usr/share/nginx/html/index.html
   ```
6. **Refresh** the browser and **confirm** your custom CloudSoft page appears
7. **Remove** the container:

   ```
   docker rm -f my-nginx
   ```
8. **Run** a new container with the same volume:

   ```
   docker run --name my-nginx -d -p 8080:80 \
     -v web-content:/usr/share/nginx/html \
     nginx
   ```
9. **Open** `http://localhost:8080` and **confirm** your custom page is still there — it survived the container removal

> ℹ **Concept Deep Dive**
>
> A **named volume** is storage managed entirely by Docker. Unlike a bind mount, you do not choose a host directory — Docker handles the storage location for you.
>
> |  | Bind Mount | Named Volume |
> | --- | --- | --- |
> | **Host path** | You specify it | Docker manages it |
> | **Use case** | Development — live editing | Production — persistent data |
> | **Survives container removal** | Files are on your host regardless | Volume persists until explicitly removed |
> | **Syntax** | `-v ./my-dir:/container/path` | `-v my-volume:/container/path` |
>
> `docker cp` copies files between your host and a running container. This is useful for one-off file transfers but is not a replacement for volumes or bind mounts in a workflow.
>
> ⚠ **Common Mistakes**
>
> * Forgetting to remove the volume after cleanup — orphaned volumes take up disk space. Use `docker volume ls` to list them
> * Confusing bind mounts and volumes: if the `-v` value starts with `/` or `./`, it is a bind mount. If it is just a name, it is a volume
>
> ✓ **Quick check:** Your custom page survived container removal — the named volume preserved the data

### **Step 5:** Manage Container Lifecycle

Containers are designed to be disposable — you create them, use them, and throw them away. Understanding the lifecycle commands is essential for working with Docker. In this step you will practice the full lifecycle: list, stop, start, and remove containers.

1. **List** running containers:

   ```
   docker ps
   ```
2. **Stop** the container:

   ```
   docker stop my-nginx
   ```
3. **List** all containers (including stopped ones):

   ```
   docker ps -a
   ```
4. **Start** the stopped container again:

   ```
   docker start my-nginx
   ```
5. **Verify** it is running again at `http://localhost:8080`
6. **Force-remove** the running container (stop + remove in one command):

   ```
   docker rm -f my-nginx
   ```
7. **Confirm** it is gone:

   ```
   docker ps -a
   ```
8. **Remove** the named volume from the previous step:

   ```
   docker volume rm web-content
   ```
9. **List** the images still on your machine:

   ```
   docker images
   ```
10. **Remove** the nginx image to reset completely:

```
docker rmi nginx
```

> ℹ **Concept Deep Dive**
>
> Key lifecycle concepts:
>
> * **Running → Stopped:** `docker stop` sends SIGTERM, then SIGKILL after 10 seconds
> * **Stopped → Running:** `docker start` restarts a stopped container with its original configuration
> * **Stopped → Removed:** `docker rm` deletes the container. Running containers must be stopped first, or use `docker rm -f`
> * **Images persist** even after all containers are removed. Use `docker rmi nginx` to remove the image itself
>
> A stopped container still exists on disk — it just is not running. Data inside its writable layer is preserved until the container is removed.
>
> ✓ **Quick check:** `docker ps -a` shows no containers, `docker volume ls` shows no volumes, `docker images` shows no nginx image — you are completely reset

### **Step 6:** Verify Your Understanding

Put your knowledge to the test by running nginx on a new port with different content, without referring to the previous steps.

1. **Create** a new HTML file at `exercise-1/test.html` with any content you like
2. **Run** an nginx container with these requirements:

   * Name it `test-nginx`
   * Map host port 3000 to container port 80
   * Bind mount your exercise directory as read-only
3. **Verify** your page loads at `http://localhost:3000/test.html`
4. **Check** the container is running with `docker ps`
5. **Clean up:** stop and remove the container

> ✓ **Success indicators:**
>
> * Your custom page loads on port 3000
> * `docker ps` shows the container with correct port mapping
> * After cleanup, `docker ps -a` shows no remaining containers
>
> ✓ **Final verification checklist:**
>
> * ☐ You can pull and run images from Docker Hub
> * ☐ You understand the `-p HOST:CONTAINER` port mapping syntax
> * ☐ You can serve custom content with bind mounts
> * ☐ You can stop, start, and remove containers
> * ☐ You understand the difference between images and containers

## Common Issues

> **If you encounter problems:**
>
> **“Cannot connect to the Docker daemon”:** Docker Desktop is not running. Start it from your applications menu and wait for it to initialize.
>
> **“Port is already allocated”:** Another process (or container) is using that port. Either stop the other process, or choose a different host port.
>
> **403 Forbidden from nginx:** The bind mount path is wrong or the directory is empty. Double-check the host path in your `-v` flag.
>
> **Container exits immediately:** Run `docker logs my-nginx` to see the error output. Missing configuration files or permission issues are common causes.
>
> **Still stuck?** Run `docker ps -a` to see all containers and their status. Use `docker logs <container-name>` to inspect output from any container.

## Summary

You’ve successfully run your first Docker containers which:

* ✓ Demonstrates how images become running containers
* ✓ Uses port mapping to expose services to your host
* ✓ Shares files between host and container using bind mounts
* ✓ Manages container lifecycle with `ps`, `stop`, `start`, and `rm`

> **Key takeaway:** Containers are lightweight, disposable runtime environments created from images. Port mapping and bind mounts are the two primary ways a container interacts with the outside world. These concepts are the foundation for everything else in Docker.

## Going Deeper (Optional)

> **Want to explore more?**
>
> * Run `docker exec -it my-nginx bash` to open a shell inside a running container and explore its filesystem
> * Try `docker logs my-nginx` to see the access logs when you refresh the browser
> * Run `docker inspect my-nginx` to see the full container configuration as JSON
> * Explore `docker stats` to see real-time CPU and memory usage of running containers

## Done! 🎉

Great job! You’ve learned how to pull images, run containers, map ports, and mount files. This foundation is what every other Docker skill builds on — next you will create your own custom images with a Dockerfile.

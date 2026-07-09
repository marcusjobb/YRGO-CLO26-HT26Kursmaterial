Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-
Server Foundation-
Network Foundation-
Deployment-
Cloud Databases-
Webapp Development-
Code Collaboration-
Docker-- Run Your First Container-- Build and Push Your First Image-- Containerize CloudSoft Recruitment-- Docker Compose -- Local Development Stack-- Multi-Platform BuildsTutorials

# Docker Compose -- Local Development Stack

🟡


# Docker Compose – Local Development Stack

## Goal

Starting from a simple job recruitment CRUD application with in-memory and MongoDB repositories, containerize the app, run it with Docker Compose, and progressively add MongoDB and Mongo Express to build a complete local development environment.

> **What you’ll learn:**
>
> * How to dockerize a .NET application with a multi-stage Dockerfile
> * How to define single and multi-container applications with `docker-compose.yml`
> * How environment variables override `appsettings.json` to toggle feature flags
> * How Docker Compose networking lets containers communicate by service name
> * How named volumes persist database data across container restarts
> * How to inspect a MongoDB database visually with Mongo Express

## Prerequisites

> **Before starting, ensure you have:**
>
> * ✓ Docker Desktop installed and running
> * ✓ Comfortable writing multi-stage Dockerfiles (from exercise 3)
> * ✓ The starter application — a simple job recruitment CRUD app with `InMemoryJobRepository` and `MongoDbJobRepository`, toggled by the `FeatureFlags:UseMongoDB` setting
> * ✓ A text editor for YAML files

## Exercise Steps

### Overview

1. **Verify the Application Runs Locally**
2. **Dockerize the Application**
3. **Move to Docker Compose (App Only)**
4. **Add MongoDB to the Stack**
5. **Test Data Persistence with Volumes**
6. **Add Mongo Express for Database Inspection**

### **Step 1:** Verify the Application Runs Locally

Before containerizing, run the application on your machine to confirm it works. The app defaults to in-memory mode, so no database is needed yet.

1. **Place** the starter application at `exercise-4/CloudSoft.Web`
2. **Run** the application:

   ```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
   dotnet run --project exercise-4/CloudSoft.Web
   ```
3. **Open** the URL shown in the terminal (typically `http://localhost:5150`)
4. **Navigate** to the Jobs page and **create a test job** to confirm CRUD works
5. **Stop** the application with `Ctrl+C`

> ℹ **Concept Deep Dive**
>
> The starter application is structured in three layers:
>
> * **Controller** (`JobController`) — handles HTTP requests
> * **Service** (`JobService`) — business logic, returns `OperationResult`
> * **Repository** (`IJobRepository`) — data access, swappable implementation
>
> `Program.cs` reads the `FeatureFlags:UseMongoDB` setting and registers either `InMemoryJobRepository` or `MongoDbJobRepository` as the `IJobRepository`. This is the pattern we will toggle later from Docker Compose without changing any code.
>
> ✓ **Quick check:** You can create, edit, and delete jobs through the UI

### **Step 2:** Dockerize the Application

Write a multi-stage Dockerfile following the restore-first pattern from exercise 3. The image will run in in-memory mode by default — no database configuration needed yet.

1. **Create** `exercise-4/CloudSoft.Web/Dockerfile`:

   > `exercise-4/CloudSoft.Web/Dockerfile`

   ```
   FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
   WORKDIR /src

   COPY CloudSoft.Web.csproj .
   RUN dotnet restore

   COPY . .
   RUN dotnet publish -c Release -o /app/publish

   FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
   WORKDIR /app
   COPY --from=build /app/publish .
   EXPOSE 8080
   ENTRYPOINT ["dotnet", "CloudSoft.Web.dll"]
   ```
2. **Navigate** to the project directory:

   ```
   cd exercise-4/CloudSoft.Web
   ```
3. **Build** the image:

   ```
   docker build -t cloudsoft-web .
   ```
4. **Run** a container:

   ```
   docker run --name cloudsoft-web -d -p 5000:8080 cloudsoft-web
   ```
5. **Open** `http://localhost:5000` — the app should work exactly as it did locally, still in in-memory mode
6. **Clean up** before moving to Compose:

   ```
   docker rm -f cloudsoft-web
   ```

> ℹ **Concept Deep Dive**
>
> The application is containerized, but running it manually with `docker run` has limitations:
>
> * Long commands with flags for ports, names, environment variables
> * Adding another service (like a database) means another `docker run` command and manual networking
> * No single source of truth for the development environment
>
> Docker Compose solves this by defining everything in a single YAML file.
>
> ✓ **Quick check:** The app runs at `http://localhost:5000` from a Docker container

### **Step 3:** Move to Docker Compose (App Only)

Before adding a database, start with a minimal `docker-compose.yml` that contains only the application. This shows the value of Compose even for a single service: no more long `docker run` commands.

1. **Create** `exercise-4/docker-compose.yml`:

   > `exercise-4/docker-compose.yml`

   ```
   services:
     cloudsoft-web:
       build:
         context: ./CloudSoft.Web
       container_name: cloudsoft-web
       restart: unless-stopped
       ports:
         - "127.0.0.1:5000:8080"
   ```
2. **Navigate** to the exercise-4 directory:

   ```
   cd exercise-4
   ```
3. **Build and start** the service:

   ```
   docker compose up -d --build
   ```
4. **Check** it is running:

   ```
   docker compose ps
   ```
5. **Open** `http://localhost:5000` and **verify** the app works

> ℹ **Concept Deep Dive**
>
> Reading the Compose file:
>
> * `services:` — defines each container. Each key (`cloudsoft-web`) is a **service name**.
> * `build: context: ./CloudSoft.Web` — tells Compose to build the image from the `Dockerfile` in that directory (instead of pulling from a registry).
> * `ports: "127.0.0.1:5000:8080"` — binds the host port only to `localhost`, preventing external access. Useful for local development.
> * `restart: unless-stopped` — automatically restarts the container if it crashes, unless you explicitly stop it.
>
> YAML formatting is strict: use **spaces** (not tabs), and indentation matters. Two spaces per level is the convention.
>
> ⚠ **Common Mistakes**
>
> * Using tabs instead of spaces causes parsing errors
> * The command is `docker compose` (with a space, Compose V2), not `docker-compose` (hyphen, legacy V1)
> * Forgetting `--build` after changing source code — Compose reuses the previously built image
>
> ✓ **Quick check:** `docker compose ps` shows `cloudsoft-web` running; browser shows the app at `http://localhost:5000`

### **Step 4:** Add MongoDB to the Stack

Now add MongoDB as a second service and switch the application from in-memory to MongoDB mode — without changing any code. The switch happens through environment variables that override `appsettings.json`.

1. **Update** `exercise-4/docker-compose.yml` to add MongoDB and configure the app to use it:

   > `exercise-4/docker-compose.yml`

   ```
   services:
     cloudsoft-web:
       build:
         context: ./CloudSoft.Web
       container_name: cloudsoft-web
       restart: unless-stopped
       ports:
         - "127.0.0.1:5000:8080"
       environment:
         - FeatureFlags__UseMongoDB=true
         - MongoDb__ConnectionString=mongodb://mongodb:27017
         - MongoDb__DatabaseName=cloudsoft
       depends_on:
         - mongodb

     mongodb:
       image: mongo:latest
       container_name: mongodb
       restart: unless-stopped
       ports:
         - "127.0.0.1:27017:27017"
       volumes:
         - mongo-data:/data/db

   volumes:
     mongo-data:
   ```
2. **Rebuild and restart**:

   ```
   docker compose up -d --build
   ```
3. **Check** both services are running:

   ```
   docker compose ps
   ```
4. **Open** `http://localhost:5000` and **verify** the app still works
5. **Check** the application logs to confirm MongoDB is being used:

   ```
   docker compose logs cloudsoft-web
   ```

   Look for the line `Using MongoDB repository` in the output.

> ℹ **Concept Deep Dive**
>
> Two important concepts appear here:
>
> * **Compose networking:** Docker Compose creates a shared network for all services. Each service name becomes a DNS hostname. So `mongodb://mongodb:27017` resolves to the MongoDB container’s IP automatically. No IP configuration needed.
> * **Environment variable naming:** ASP.NET Core uses `__` (double underscore) as a hierarchy separator. `FeatureFlags__UseMongoDB=true` maps to `FeatureFlags:UseMongoDB` in configuration. This is how environment variables override values from `appsettings.json`.
>
> Other details:
>
> * `depends_on: mongodb` ensures MongoDB starts before the app. Note: this only waits for the container to start, not for MongoDB to be ready to accept connections.
> * `volumes: mongo-data:/data/db` creates a **named volume** that stores MongoDB data outside the container. The top-level `volumes:` section declares the volume.
>
> ⚠ **Common Mistakes**
>
> * Using `localhost` instead of `mongodb` in the connection string. Inside a container, `localhost` refers to the container itself, not your host machine.
> * Forgetting the top-level `volumes:` section at the bottom means the named volume is not declared.
> * Forgetting `--build` after changing environment variables that require a rebuild.
>
> ✓ **Quick check:** App loads at `http://localhost:5000`, logs show “Using MongoDB repository”

### **Step 5:** Test Data Persistence with Volumes

Named volumes are what make databases useful in Docker — without them, all data is lost when a container is recreated. In this step you will experience data loss firsthand, then confirm that the named volume prevents it.

1. **Create** a job listing through the application at `http://localhost:5000`
2. **Verify** the job appears in the list
3. **Bring everything down** (but keep volumes):

   ```
   docker compose down
   ```
4. **Bring everything back up:**

   ```
   docker compose up -d --build
   ```
5. **Check** that the job you created is still there at `http://localhost:5000`

   The data persists because MongoDB’s data is stored in the `mongo-data` named volume, which survives `docker compose down`.
6. **Now bring everything down AND remove volumes:**

   ```
   docker compose down -v
   ```
7. **Start again:**

   ```
   docker compose up -d --build
   ```
8. **Check** `http://localhost:5000` — the job listing is gone. The `-v` flag removed the named volume, which deleted all MongoDB data.

> ℹ **Concept Deep Dive**
>
> Volume behavior with `docker compose down`:
>
> * `docker compose down` — stops and removes containers and the default network. **Named volumes are preserved.**
> * `docker compose down -v` — also removes named volumes. **All data is deleted.**
>
> Without the named volume, MongoDB would store data inside the container’s writable layer, which is deleted whenever the container is removed. Named volumes live outside the container lifecycle.
>
> In production, databases are typically managed services (like Azure Cosmos DB) rather than containers. But for local development, containerized databases with named volumes are convenient and disposable.
>
> ⚠ **Common Mistakes**
>
> * Running `docker compose down -v` accidentally destroys all data. Use this intentionally when you want a clean slate.
>
> ✓ **Quick check:** Data persists through `down` + `up`, but is lost with `down -v` + `up`

### **Step 6:** Add Mongo Express for Database Inspection

Mongo Express is a web-based admin UI for MongoDB. Adding it to your Compose stack gives you a visual way to inspect collections and documents without running `mongosh` commands.

1. **Update** `exercise-4/docker-compose.yml` to add Mongo Express:

   > `exercise-4/docker-compose.yml`

   ```
   services:
     cloudsoft-web:
       build:
         context: ./CloudSoft.Web
       container_name: cloudsoft-web
       restart: unless-stopped
       ports:
         - "127.0.0.1:5000:8080"
       environment:
         - FeatureFlags__UseMongoDB=true
         - MongoDb__ConnectionString=mongodb://mongodb:27017
         - MongoDb__DatabaseName=cloudsoft
       depends_on:
         - mongodb

     mongodb:
       image: mongo:latest
       container_name: mongodb
       restart: unless-stopped
       ports:
         - "127.0.0.1:27017:27017"
       volumes:
         - mongo-data:/data/db

     mongo-express:
       image: mongo-express:latest
       container_name: mongo-express
       restart: unless-stopped
       ports:
         - "127.0.0.1:8081:8081"
       environment:
         - ME_CONFIG_MONGODB_SERVER=mongodb
         - ME_CONFIG_BASICAUTH=false
       depends_on:
         - mongodb

   volumes:
     mongo-data:
   ```
2. **Start** the updated stack:

   ```
   docker compose up -d --build
   ```
3. **Verify** all three services are running:

   ```
   docker compose ps
   ```
4. **Open** `http://localhost:8081` to access Mongo Express
5. **Navigate** to the `cloudsoft` database and **inspect** the `jobs` collection
6. **Create** a new job through the app at `http://localhost:5000`, then **refresh** Mongo Express to see it appear in the collection

> ℹ **Concept Deep Dive**
>
> Mongo Express connects to MongoDB using the same service-name DNS as the application:
>
> * `ME_CONFIG_MONGODB_SERVER=mongodb` — the MongoDB service name on the Compose network
> * `ME_CONFIG_BASICAUTH=false` — disables the login prompt (only safe for local development)
>
> This is a typical pattern for development stacks: one service for the application, one for the database, and one for a database admin UI. In production, you would never expose a database admin UI publicly — use it only for local development.
>
> ✓ **Quick check:** Jobs created through the app show up in the `jobs` collection in Mongo Express

## Common Issues

> **If you encounter problems:**
>
> **YAML syntax error:** Check indentation — YAML requires spaces, not tabs. Each level is indented two spaces. Use a YAML validator if unsure.
>
> **“Connection refused” from the app to MongoDB:** The connection string must use the service name (`mongodb`), not `localhost`. Inside a container, `localhost` is the container itself.
>
> **App starts before MongoDB is ready:** Occasionally the app starts faster than MongoDB initializes. If you see connection errors, run `docker compose restart cloudsoft-web` to restart just the app.
>
> **Port conflicts:** If another service is using port 5000, 27017, or 8081, either stop that service or change the host port in `docker-compose.yml`.
>
> **Old image used after code changes:** Always pass `--build` when running `docker compose up` after changing source code or the Dockerfile.
>
> **Still stuck?** Run `docker compose logs` to see output from all services, or `docker compose logs <service-name>` for a specific service.

## Summary

You’ve successfully built a multi-container development environment which:

* ✓ Dockerizes a .NET application with a multi-stage Dockerfile
* ✓ Starts with a minimal single-service Compose file and builds up progressively
* ✓ Toggles feature flags through environment variables — no code changes
* ✓ Uses Compose networking so containers communicate by service name
* ✓ Persists database data with named volumes across container restarts
* ✓ Adds Mongo Express for visual database inspection

> **Key takeaway:** Docker Compose turns multi-container environments into a single command. Start small, add services as you need them. Environment variables let you configure the same image for different environments without rebuilding. Named volumes are essential for any stateful service like a database.

## Going Deeper (Optional)

> **Want to explore more?**
>
> * Try `docker compose logs -f cloudsoft-web` to tail logs in real time
> * Use `docker compose exec mongodb mongosh` as a shortcut to open the MongoDB shell
> * Run `docker compose top` to see which processes are running inside each container
> * Research Docker Compose profiles to selectively start subsets of services (e.g., include `mongo-express` only in a `dev` profile)

## Done! 🎉

Great job! You’ve built a complete local development stack with Docker Compose. Your application, database, and admin UI all run together with a single command. This is the workflow professional development teams use to ensure consistent development environments across the team.

---

title: Containerize CloudSoft Recruitment
author: Marcus Ackre Medina
type: exercise
topic: cloud
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/exercises/20-docker/3-containerize-cloudsoft-recruitment.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-"
tags: ["cloud", "cloudsoft", "containerize", "docker", "dotnet", "exercise", "git", "networking", "recruitment", "security"]
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

# Containerize CloudSoft Recruitment

🟡


# Containerize CloudSoft Recruitment

## Goal

Containerize the CloudSoft Recruitment application using first a simple single-stage Dockerfile to understand the problem with oversized images, then refactor to a multi-stage build that separates build-time tools from the production runtime.

> **What you’ll learn:**
>
> * How to containerize a .NET application with `dotnet restore` and `dotnet publish`
> * Why single-stage builds produce unnecessarily large images
> * How multi-stage builds separate build-time dependencies from runtime
> * The restore-first pattern for efficient Docker layer caching

## Prerequisites

> **Before starting, ensure you have:**
>
> * ✓ Docker Desktop installed and running
> * ✓ Comfortable writing Dockerfiles (`FROM`, `COPY`, `EXPOSE`)
> * ✓ .NET 10 SDK installed locally (verify with `dotnet --version`)

## Exercise Steps

### Overview

1. **Create the Project**
2. **Verify the Application Runs Locally**
3. **Write a Single-Stage Dockerfile**
4. **Build and Run the Single-Stage Image**
5. **Refactor to a Multi-Stage Dockerfile**
6. **Build, Run, and Compare Image Sizes**

### **Step 1:** Create the Project

Before writing any Dockerfiles, you need an application to containerize. In this step you will scaffold a new ASP.NET Core MVC application and customize the landing page to match the CloudSoft theme.

1. **Create** a project directory:

   ```
   mkdir exercise-3
   ```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
2. **Scaffold** a new MVC application:

   ```
   dotnet new mvc -n CloudSoft.Web -o exercise-3/CloudSoft.Web
   ```
3. **Create** a `.gitignore` file for the .NET project:

   ```
   cd exercise-3 && dotnet new gitignore && cd ..
   ```

   This excludes `bin/`, `obj/`, and other .NET build artifacts from version control.
4. **Replace** the contents of `exercise-3/CloudSoft.Web/Views/Home/Index.cshtml` with:

   > `exercise-3/CloudSoft.Web/Views/Home/Index.cshtml`

   ```
   @{
       ViewData["Title"] = "CloudSoft Recruitment";
   }

   <div class="text-center" style="padding: 4rem 2rem;">
       <h1 class="display-3" style="color: #1a1a2e; font-weight: 700;">CloudSoft Recruitment</h1>
       <p class="lead" style="color: #555; margin-bottom: 2rem;">Find your next opportunity in cloud technology</p>
       <span style="display: inline-block; background: #1a1a2e; color: white; padding: 0.5rem 1.5rem; border-radius: 2rem; font-size: 0.9rem;">
           Powered by ASP.NET Core
       </span>
   </div>
   ```

> ✓ **Quick check:** `exercise-3/CloudSoft.Web/` contains a `.csproj` file and the modified `Index.cshtml`

### **Step 2:** Verify the Application Runs Locally

Before containerizing an application, make sure it runs correctly on your machine first. This gives you a known-good baseline to compare against when running in a container.

1. **Run** the application:

   ```
   dotnet run --project exercise-3/CloudSoft.Web
   ```
2. **Open** `http://localhost:5106` (or the port shown in the terminal output)
3. **Confirm** the application loads and you can see the CloudSoft Recruitment landing page
4. **Stop** the application with `Ctrl+C`

> ℹ **Concept Deep Dive**
>
> When you run `dotnet run`, the SDK performs several steps behind the scenes:
>
> 1. **Restore** — downloads NuGet packages listed in the `.csproj` file (same as `dotnet restore`)
> 2. **Build** — compiles all `.cs` and `.cshtml` files into a DLL in Debug mode (same as `dotnet build`)
> 3. **Run** — starts the compiled application
>
> This is convenient for local development, but not how you deploy. In a Dockerfile you instead use `dotnet publish -c Release`, which restores, compiles in **Release mode** (optimized, no debug symbols), and collects all files needed to run into a single output folder. That folder is what you copy into the runtime image and start with `dotnet CloudSoft.Web.dll`.
>
> When containerizing, you also need to know four things about your application:
>
> * **SDK version:** .NET 10 — determines which build image to use (`mcr.microsoft.com/dotnet/sdk:10.0`)
> * **Runtime:** ASP.NET Core — determines which runtime image to use (`mcr.microsoft.com/dotnet/aspnet:10.0`)
> * **Project file location:** `exercise-3/CloudSoft.Web/CloudSoft.Web.csproj` — what to copy and restore
> * **Port:** ASP.NET Core defaults to port 8080 inside containers (set by the `ASPNETCORE_HTTP_PORTS` environment variable in the base image)
>
> ✓ **Quick check:** Application starts and the CloudSoft landing page loads in the browser

### **Step 3:** Write a Single-Stage Dockerfile

Start with the simplest possible approach — use the .NET SDK image for everything. This image contains the compiler, build tools, NuGet package manager, and the runtime. It works, but you will see why this is not ideal for production.

1. **Create** a file named `Dockerfile` inside the `exercise-3/CloudSoft.Web/` directory:

   > `exercise-3/CloudSoft.Web/Dockerfile`

   ```
   FROM mcr.microsoft.com/dotnet/sdk:10.0
   WORKDIR /app

   COPY . .
   RUN dotnet restore
   RUN dotnet publish -c Release -o /app/publish

   WORKDIR /app/publish
   EXPOSE 8080
   ENTRYPOINT ["dotnet", "CloudSoft.Web.dll"]
   ```

> ℹ **Concept Deep Dive**
>
> Walking through each instruction:
>
> * `FROM mcr.microsoft.com/dotnet/sdk:10.0` — Uses the full .NET SDK image as the base. This image is approximately 900MB because it includes the compiler (`dotnet build`), NuGet cache, and all build tools.
> * `WORKDIR /app` — Sets the working directory inside the container. Created automatically if it does not exist.
> * `COPY . .` — Copies everything from the build context into the container.
> * `RUN dotnet restore` — Downloads NuGet packages.
> * `RUN dotnet publish -c Release -o /app/publish` — Compiles the application in Release mode and outputs to `/app/publish`.
> * `ENTRYPOINT` — Defines the command that runs when the container starts. Unlike `CMD`, `ENTRYPOINT` cannot be easily overridden.
>
> This works, but the final image contains the entire SDK — compilers, build tools, NuGet cache — none of which are needed at runtime.
>
> ⚠ **Common Mistakes**
>
> * Forgetting `WORKDIR` before `ENTRYPOINT` means the DLL path must be absolute
> * Using `CMD` instead of `ENTRYPOINT` is fine but has different override behavior
>
> ✓ **Quick check:** `Dockerfile` exists at `exercise-3/CloudSoft.Web/Dockerfile`

### **Step 4:** Build and Run the Single-Stage Image

Build the image and run it to verify the application works in a container. Then check the image size to see the problem with the single-stage approach.

1. **Navigate** to the project directory:

   ```
   cd exercise-3/CloudSoft.Web
   ```
2. **Build** the image:

   ```
   docker build -t cloudsoft-web:single .
   ```
3. **Run** a container:

   ```
   docker run --name cloudsoft-single -d -p 5000:8080 cloudsoft-web:single
   ```
4. **Open** `http://localhost:5000` and **confirm** the application loads
5. **Confirm** the CloudSoft landing page appears
6. **Check** the image size:

   ```
   docker images cloudsoft-web
   ```
7. **Note** the size — it will be approximately 900MB or more

> ℹ **Concept Deep Dive**
>
> The `docker build` command:
>
> * `-t cloudsoft-web:single` tags the image with name `cloudsoft-web` and tag `single`
> * `.` is the **build context** — the current directory, which Docker sends to the build engine. Docker looks for a `Dockerfile` in this directory by default and can only `COPY` files from within it.
>
> A 900MB image is problematic:
>
> * **Slow deployments:** More data to transfer over the network
> * **Higher costs:** More storage on the registry and in production
> * **Larger attack surface:** The SDK includes tools an attacker could exploit
>
> ⚠ **Common Mistakes**
>
> * Running `docker build` from the wrong directory. The build context path must match what the Dockerfile expects to `COPY`.
> * Port 5000 already in use. Stop other containers or choose a different host port.
>
> ✓ **Quick check:** App runs at `http://localhost:5000`, image size is ~900MB

### **Step 5:** Refactor to a Multi-Stage Dockerfile

Multi-stage builds use multiple `FROM` instructions. Each `FROM` starts a new stage. You can copy files between stages but only the final stage becomes the image. This lets you use the SDK to build and the much smaller runtime image to run.

1. **Stop and remove** the single-stage container:

   ```
   docker rm -f cloudsoft-single
   ```
2. **Replace** the contents of `exercise-3/CloudSoft.Web/Dockerfile` with:

   > `exercise-3/CloudSoft.Web/Dockerfile`

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

> ℹ **Concept Deep Dive**
>
> This Dockerfile has two stages:
>
> **Stage 1 — `build`** (SDK image, ~900MB):
>
> * Uses the full SDK to compile the application
> * `COPY CloudSoft.Web.csproj .` then `RUN dotnet restore` — this is the **restore-first pattern**
> * `COPY . .` then `RUN dotnet publish` — compiles the source code
>
> **Stage 2 — `runtime`** (ASP.NET runtime image, ~220MB):
>
> * Uses the much smaller ASP.NET runtime image — only what is needed to run the app
> * `COPY --from=build /app/publish .` — copies only the published output from the build stage
> * The entire build stage (SDK, NuGet cache, source code) is discarded
>
> **Why the restore-first pattern matters:**
>
> Docker caches each layer. When you rebuild an image, Docker compares each instruction to what it has cached. As soon as something changes, Docker invalidates that layer **and every layer after it** — it rebuilds from that point onward.
>
> This is why the order of `COPY` instructions matters:
>
> ```
> COPY CloudSoft.Web.csproj .    ← changes rarely (only when you add/remove packages)
> RUN dotnet restore              ← cached as long as .csproj hasn't changed
> COPY . .                        ← changes often (every time you edit code)
> RUN dotnet publish              ← must rebuild when source code changes
> ```
>
> If you copied everything at once (`COPY . .` followed by `dotnet restore` and `dotnet publish`), then even a one-line change in a `.cshtml` file would invalidate the `COPY` layer, forcing Docker to re-download all NuGet packages from scratch. By copying the `.csproj` first and restoring before the source code, the restore layer stays cached through code-only changes. In practice this cuts rebuild times from minutes to seconds.
>
> ⚠ **Common Mistakes**
>
> * Using `sdk` instead of `aspnet` for the runtime stage defeats the purpose of multi-stage builds
> * Wrong path in `COPY --from=build` — must match the `-o` path from `dotnet publish`
> * Forgetting `AS build` in the first `FROM` — the second stage needs this name to reference it
>
> ✓ **Quick check:** Dockerfile has two `FROM` instructions, one tagged `AS build` and one tagged `AS runtime`

### **Step 6:** Build, Run, and Compare Image Sizes

Build the multi-stage image, verify the application still works identically, and compare the image sizes side by side. Then test the layer caching to see the restore-first pattern in action.

1. **Make sure** you are in the `exercise-3/CloudSoft.Web` directory
2. **Build** the multi-stage image:

   ```
   docker build -t cloudsoft-web:multi .
   ```
3. **Run** a container from the multi-stage image:

   ```
   docker run --name cloudsoft-multi -d -p 5000:8080 cloudsoft-web:multi
   ```
4. **Verify** the app at `http://localhost:5000` — it should work identically to the single-stage version
5. **Compare** image sizes:

   ```
   docker images cloudsoft-web
   ```

   You should see something like:

   ```
   REPOSITORY      TAG      SIZE
   cloudsoft-web   multi    ~220MB
   cloudsoft-web   single   ~900MB
   ```
6. **Test layer caching:** Make a small change to `Views/Home/Index.cshtml`, then rebuild:

   ```
   docker build -t cloudsoft-web:multi .
   ```

   Notice that the `dotnet restore` step shows `CACHED` — only the `COPY . .` and `dotnet publish` steps run again.
7. **Clean up:**

   ```
   docker rm -f cloudsoft-multi
   ```

> ✓ **Success indicators:**
>
> * Both images run the same application with identical behavior
> * Multi-stage image is 60-75% smaller than single-stage
> * Rebuilds after code-only changes skip the restore step (cached)
>
> ✓ **Final verification checklist:**
>
> * ☐ Single-stage Dockerfile works but produces a ~900MB image
> * ☐ Multi-stage Dockerfile produces a ~220MB image
> * ☐ Application runs correctly from the multi-stage image
> * ☐ Layer caching works — `dotnet restore` is cached on code-only changes
> * ☐ You understand the restore-first pattern and why it speeds up builds

## Common Issues

> **If you encounter problems:**
>
> **“COPY failed: file not found”:** The build context does not contain the file. Make sure you are in the `exercise-3/CloudSoft.Web` directory when running `docker build`. The `.` context must be the directory containing `CloudSoft.Web.csproj` and the `Dockerfile`.
>
> **Application starts but crashes immediately:** Run `docker logs cloudsoft-multi` to see the error. Common causes: missing configuration, wrong `WORKDIR`, or the DLL name does not match.
>
> **Image is still large after multi-stage:** Verify the runtime stage uses `aspnet:10.0`, not `sdk:10.0`. The SDK image is much larger.
>
> **Restore is not cached on rebuild:** Changes to the `.csproj` file invalidate the restore cache. This is expected — the cache only helps when you change source code without changing dependencies.
>
> **Still stuck?** Add `RUN ls -la` instructions in your Dockerfile to inspect the filesystem at each stage during the build.

## Summary

You’ve successfully containerized a .NET application which:

* ✓ Demonstrates why single-stage builds produce oversized images
* ✓ Uses multi-stage builds to separate build-time from runtime dependencies
* ✓ Applies the restore-first pattern for efficient layer caching
* ✓ Reduces image size by 60-75% compared to the naive approach

> **Key takeaway:** Multi-stage builds are essential for production Docker images. The build stage has the tools to compile your code, and the runtime stage has only what is needed to run it. The restore-first pattern keeps your rebuilds fast by caching dependency resolution separately from source code compilation.

## Going Deeper (Optional)

> **Want to explore more?**
>
> * Add a `.dockerignore` file to exclude `bin/`, `obj/`, and `.git/` from the build context
> * Add a `USER` instruction in the runtime stage to run as a non-root user for better security
> * Try the `HEALTHCHECK` instruction to let Docker monitor if your app is responding
> * Compare the layers with `docker history cloudsoft-web:multi` to see exactly what each layer contains and its size

## Done! 🎉

Excellent work! You’ve containerized a real .NET application with an optimized multi-stage Dockerfile. The image is small, builds are fast thanks to layer caching, and the application runs identically to the local version. Next you will use Docker Compose to add a database and build a complete local development environment.

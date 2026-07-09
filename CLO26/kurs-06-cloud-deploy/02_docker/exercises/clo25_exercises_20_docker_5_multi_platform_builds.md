Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-
Server Foundation-
Network Foundation-
Deployment-
Cloud Databases-
Webapp Development-
Code Collaboration-
Docker-- Run Your First Container-- Build and Push Your First Image-- Containerize CloudSoft Recruitment-- Docker Compose -- Local Development Stack-- Multi-Platform BuildsTutorials

# Multi-Platform Builds

🟡


# Multi-Platform Builds

## Goal

Build a Docker image that runs on both AMD64 (Intel/AMD processors) and ARM64 (Apple Silicon, ARM servers) using `docker buildx`, and push it to Docker Hub as a multi-platform manifest so anyone can pull and run it regardless of their CPU architecture.

> **What you’ll learn:**
>
> * Why single-platform images cause compatibility problems in mixed teams
> * How `docker buildx` builds images for multiple architectures
> * How multi-platform manifests work in container registries
> * How to verify and inspect multi-platform images

## Prerequisites

> **Before starting, ensure you have:**
>
> * ✓ Docker Desktop installed and running (includes buildx)
> * ✓ A Docker Hub account with login configured (`docker login`)
> * ✓ A working multi-stage Dockerfile from the previous exercise
> * ✓ Understanding of image tags and Docker Hub pushing

## Exercise Steps

### Overview

1. **Understand the Platform Problem**
2. **Set Up a Buildx Builder**
3. **Build and Push a Multi-Platform Image**
4. **Verify the Multi-Platform Manifest**
5. **Test Cross-Platform Compatibility**

### **Step 1:** Understand the Platform Problem

Docker images contain compiled binaries for a specific CPU architecture. An image built on Apple Silicon contains ARM64 binaries that cannot run natively on an AMD64 server — and vice versa. In this step you will identify your machine’s architecture and understand why this matters.

1. **Check** your machine’s CPU architecture:

   ```
   uname -m
   ```

   This shows `x86_64` (AMD64) or `arm64` (Apple Silicon / ARM).
2. **Check** what Docker reports:

   ```
   docker info --format '{{.Architecture}}'
   ```
3. **Recall** from a previous exercise: the image you pushed to Docker Hub was built for your machine’s architecture only. If a classmate with a different architecture pulls it, it will either fail or run through slow emulation.

> ℹ **Concept Deep Dive**
>
> Why this matters in practice:
>
> * **Mixed development teams:** Some developers use MacBooks with Apple Silicon (ARM64), others use Intel-based machines (AMD64). A single-platform image breaks for half the team.
> * **Cloud deployment:** Most cloud servers (Azure, AWS, GCP) run AMD64, but ARM instances (like AWS Graviton) are increasingly popular for cost savings. A multi-platform image lets you deploy to either.
> * **CI/CD pipelines:** GitHub Actions runners are typically AMD64. If you build there and deploy to ARM, you need multi-platform support.
>
> The solution is a **manifest list** — a registry concept that maps platform+architecture to specific image digests. When someone runs `docker pull`, the Docker client checks the manifest list and automatically downloads the correct platform-specific image.
>
> ✓ **Quick check:** You know whether your machine is `amd64` or `arm64`

### **Step 2:** Set Up a Buildx Builder

The default Docker builder only supports your native platform. To build for multiple platforms, you need to create a new buildx builder that uses QEMU emulation for cross-compilation.

1. **Create** a new buildx builder:

   ```
   docker buildx create --name multiplatform --use
   ```
2. **Initialize** the builder and verify supported platforms:

   ```
   docker buildx inspect --bootstrap
   ```
3. **Confirm** the builder supports both platforms in the output:

   ```
   Platforms: linux/amd64, linux/arm64, ...
   ```
4. **List** all builders to see the new one is active:

   ```
   docker buildx ls
   ```

> ℹ **Concept Deep Dive**
>
> How buildx handles cross-compilation:
>
> * `docker buildx create` creates a new builder instance using Docker’s BuildKit backend
> * `--name multiplatform` gives it a human-readable name
> * `--use` sets it as the active builder for subsequent `docker buildx build` commands
> * `--bootstrap` starts the builder and downloads QEMU emulation binaries
>
> **QEMU** is an open-source emulator that allows running code compiled for one CPU architecture on another. Docker Desktop includes QEMU support out of the box. Building for a non-native platform is slower because each instruction is emulated, but the resulting image contains native binaries for the target platform.
>
> ⚠ **Common Mistakes**
>
> * Forgetting `--use` means the default builder is still active and `docker buildx build` will not use the new builder
> * If QEMU is not available, the builder will only show your native platform. Restarting Docker Desktop usually fixes this.
>
> ✓ **Quick check:** `docker buildx ls` shows the `multiplatform` builder with both `linux/amd64` and `linux/arm64` platforms

### **Step 3:** Build and Push a Multi-Platform Image

Build the CloudSoft Recruitment image for both AMD64 and ARM64 in a single command. Multi-platform images must be pushed directly to a registry — they cannot be stored in the local image store.

1. **Navigate** to the project directory:

   ```
   cd exercise-4/CloudSoft.Web
   ```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
2. **Build and push** the multi-platform image:

   ```
   docker buildx build \
     --platform linux/amd64,linux/arm64 \
     -t YOUR_DOCKERHUB_USERNAME/cloudsoft-web:2.0 \
     --push \
     .
   ```

   Replace `YOUR_DOCKERHUB_USERNAME` with your actual Docker Hub username.
3. **Wait** for the build to complete — it will take longer than a single-platform build because it builds twice (once for each platform, with the non-native one emulated)

> ℹ **Concept Deep Dive**
>
> The build command explained:
>
> * `--platform linux/amd64,linux/arm64` — Build for both architectures. Each platform gets its own image.
> * `-t YOUR_DOCKERHUB_USERNAME/cloudsoft-web:2.0` — Tag for the registry.
> * `--push` — Push directly to the registry. **This is required** — multi-platform images are stored as a manifest list on the registry, not locally. You cannot use `docker images` to see them.
> * The build runs the Dockerfile once per platform. For the non-native platform, each `RUN` instruction executes through QEMU emulation.
>
> The resulting manifest list on Docker Hub contains pointers to two platform-specific images. When someone runs `docker pull`, the Docker client automatically selects the right one.
>
> ⚠ **Common Mistakes**
>
> * Forgetting `--push` will cause an error: multi-platform images cannot be loaded into the local Docker image store
> * Not being logged in to Docker Hub (`docker login`) will cause a push failure
> * Typo in the platform string (e.g., `linux/arm` instead of `linux/arm64`) will fail
>
> ✓ **Quick check:** Build completes for both platforms and the push succeeds without errors

### **Step 4:** Verify the Multi-Platform Manifest

After pushing, verify that Docker Hub has a manifest list with entries for both architectures. This confirms that users on either platform will get the correct image.

1. **Inspect** the manifest using buildx:

   ```
   docker buildx imagetools inspect YOUR_DOCKERHUB_USERNAME/cloudsoft-web:2.0
   ```
2. **Look for** both platform entries in the output:

   ```
   Name:  docker.io/YOUR_DOCKERHUB_USERNAME/cloudsoft-web:2.0
   MediaType: ...
   Manifests:
     Name:  ...@sha256:abc123...
     Platform: linux/amd64

     Name:  ...@sha256:def456...
     Platform: linux/arm64
   ```
3. **Verify** on Docker Hub’s web UI at `https://hub.docker.com/r/YOUR_DOCKERHUB_USERNAME/cloudsoft-web/tags` — the tag page shows the OS/Architecture for each variant

> ℹ **Concept Deep Dive**
>
> A **manifest list** (also called a “fat manifest”) is a registry-level concept:
>
> * It is not an image — it is an index that points to platform-specific images
> * Each entry has a digest (SHA256 hash) that uniquely identifies the platform-specific image
> * The Docker client reads the manifest list, matches its own platform, and pulls the correct image
> * This is completely transparent to the user — `docker pull` just works
>
> This is the same mechanism official images like `nginx`, `mongo`, and `mcr.microsoft.com/dotnet/aspnet` use to support multiple platforms from a single tag.
>
> ✓ **Quick check:** `imagetools inspect` shows both `linux/amd64` and `linux/arm64` in the manifest

### **Step 5:** Test Cross-Platform Compatibility

The final test is proving that the image works on a different platform. You can either ask a classmate with a different architecture to pull your image, or use Docker’s `--platform` flag to force pulling a specific variant.

1. **Ask a classmate** with a different CPU architecture to run:

   ```
   docker pull YOUR_DOCKERHUB_USERNAME/cloudsoft-web:2.0
   docker run -d -p 5000:8080 YOUR_DOCKERHUB_USERNAME/cloudsoft-web:2.0
   ```

   They should see the CloudSoft Recruitment app running correctly without any special flags.
2. **If testing alone**, force Docker to pull the non-native platform:

   ```
   docker run --platform linux/amd64 -d -p 5001:8080 \
     YOUR_DOCKERHUB_USERNAME/cloudsoft-web:2.0
   ```

   This runs through QEMU emulation (slower) but proves the AMD64 image works.
3. **Verify** the app loads at the appropriate port
4. **Clean up** when done:

   ```
   docker rm -f $(docker ps -aq)
   docker buildx rm multiplatform
   ```

> ✓ **Success indicators:**
>
> * Manifest list contains both `linux/amd64` and `linux/arm64`
> * Image runs on a different platform than it was built on
> * A classmate on a different architecture can pull and run the same tag
>
> ✓ **Final verification checklist:**
>
> * ☐ Buildx builder created with multi-platform support
> * ☐ Image built for both AMD64 and ARM64
> * ☐ Image pushed to Docker Hub with a manifest list
> * ☐ `imagetools inspect` shows both platforms
> * ☐ Image runs correctly on a non-native platform

## Common Issues

> **If you encounter problems:**
>
> **“multiple platforms feature is currently not supported for docker driver”:** You are using the default builder. Run `docker buildx create --name multiplatform --use` to create a proper builder.
>
> **QEMU errors or missing platform support:** Restart Docker Desktop. QEMU support is built in but sometimes needs a restart to initialize.
>
> **Build is very slow:** Cross-compilation through QEMU is significantly slower than native builds. This is expected. The ARM64 build on an AMD64 machine (or vice versa) takes several minutes.
>
> **Push fails with “denied”:** Make sure you are logged in (`docker login`) and the tag includes your Docker Hub username.
>
> **Still stuck?** Run `docker buildx inspect --bootstrap` to verify the builder is running and shows both platforms.

## Summary

You’ve successfully built and published a multi-platform Docker image which:

* ✓ Runs on both AMD64 (Intel/AMD) and ARM64 (Apple Silicon) machines
* ✓ Uses a manifest list so `docker pull` automatically gets the right variant
* ✓ Solves the platform incompatibility problem from earlier exercises
* ✓ Follows the same pattern used by official images on Docker Hub

> **Key takeaway:** Multi-platform builds with `docker buildx` ensure your images work everywhere — across different developer machines and cloud providers. The manifest list is transparent to users: they pull a single tag and get the right image for their architecture automatically.

## Going Deeper (Optional)

> **Want to explore more?**
>
> * Research how GitHub Actions builds multi-platform images in CI/CD using `docker/build-push-action`
> * Try `docker buildx build --cache-to` and `--cache-from` to speed up CI builds with external caches
> * Explore `docker buildx bake` for building multiple images in a single command
> * Compare ARM64 and AMD64 image sizes — they can differ slightly due to different base image layers

## Done! 🎉

Excellent work! You’ve mastered Docker’s multi-platform build system. Your images now work on any machine regardless of CPU architecture — exactly how professional container workflows operate. This is the last piece of the Docker puzzle: you can build, optimize, compose, and distribute container images across platforms.

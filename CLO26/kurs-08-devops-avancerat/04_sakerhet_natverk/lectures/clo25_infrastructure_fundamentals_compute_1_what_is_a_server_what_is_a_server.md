---

title: What is a Server?
author: Marcus Ackre Medina
type: lecture
topic: infrastructure
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/infrastructure-fundamentals/compute/1-what-is-a-server/what-is-a-server.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure Fundamentals-"
tags: ["azure", "cloud", "docker", "infrastructure", "linux", "networking", "server", "server?", "what"]
week_fit: []
---

Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure Fundamentals-
Compute-- What is a Server?-- Common Server Roles-- Inside a Physical Server-- Inside a Virtual Server-- Azure VM Sizing and Cost (Azure)-
Network-
Storage-
IaCExercisesTutorials

# What is a Server?

🟡


[Watch the presentation](/presentations/infrastructure-fundamentals/compute/1-what-is-a-server.html)

[Se presentationen på svenska](/presentations/infrastructure-fundamentals/compute/1-what-is-a-server-swe.html)

---

Applications that serve multiple users require centralized compute resources accessible over a network. The **server** fills this role, providing processing power, storage, and services that clients can access. Understanding what a server is—and the various forms it takes—enables informed decisions about infrastructure architecture.

## The Server Concept

The term “server” has two related meanings. In hardware terms, a server is a physical computer optimized for continuous operation and network accessibility. In software terms, a server is any program that listens for requests and sends responses. These definitions coexist: a database server refers both to the PostgreSQL process handling queries and to the machine running that process.

This dual nature matters because infrastructure decisions involve both layers. Choosing a deployment platform (hardware concern) differs from choosing server software (application concern), though both affect system behavior.

### The Client-Server Model

Servers operate within the **client-server model**, where one system (the server) provides services that other systems (clients) consume. A web browser requests a page from a web server. A mobile app queries an API server. A backend application retrieves data from a database server.

This model enables centralized resource management. Rather than distributing data across every client, a single server maintains the authoritative copy. Clients connect when they need access, and the server controls that access. This architecture scales because adding clients does not require duplicating the core service—only the server’s capacity to handle concurrent requests.

## Server Deployment Models

The same application logic can run on different infrastructure foundations. Each deployment model abstracts more of the underlying hardware, trading direct control for operational simplicity.

### Physical Servers

A **physical server** (also called bare-metal) provides dedicated hardware without any virtualization layer. The operating system runs directly on the CPU, memory, and storage.

Physical servers deliver maximum performance because no abstraction layer consumes resources. They also provide complete control over the hardware environment, which matters for workloads with specific requirements—certain database configurations, specialized hardware accelerators, or compliance constraints that prohibit shared infrastructure.

The trade-off is operational overhead. Physical servers require procurement, rack installation, power and cooling, hardware maintenance, and eventual replacement. Provisioning takes days or weeks rather than minutes. Scaling requires purchasing and installing additional machines.

### Virtual Machines

A **virtual machine** (VM) emulates a complete computer in software. A **hypervisor** layer manages hardware resources and allocates them to multiple VMs running on the same physical host. Each VM operates its own operating system instance, isolated from other VMs on the same hardware.

Virtualization improves hardware utilization. A physical server often sits idle; multiple VMs can share that capacity. VMs also enable rapid provisioning—creating a new VM takes minutes, not weeks. Cloud platforms like Azure build on this model, offering VM instances on demand with consumption-based pricing.

VMs introduce a performance overhead, typically 2-10%, because the hypervisor mediates hardware access. They also carry the weight of a full operating system per instance, consuming memory and requiring OS-level maintenance (patching, configuration) for each VM.

### Containers

A **container** packages an application with its dependencies and runs as an isolated process on a shared operating system. Unlike VMs, containers do not include a full operating system—they share the host’s kernel while maintaining process isolation.

This architecture makes containers lightweight. They start in milliseconds (the application process starts, not an entire OS) and consume less memory than VMs. Container images define the exact runtime environment, ensuring consistency across development, testing, and production.

Containers suit applications designed as smaller, independent services. They scale efficiently because new instances spin up quickly and consume minimal resources. Orchestration platforms like Kubernetes automate deployment, scaling, and management across clusters of container hosts.

The trade-off is reduced isolation. Containers share the host kernel, so a kernel vulnerability affects all containers on that host. Some workloads require the stronger isolation of separate operating systems that VMs provide.

### Serverless Functions

**Serverless computing** abstracts the server entirely. Code executes in response to events—an HTTP request, a message on a queue, a scheduled timer—and the platform manages all underlying infrastructure. Resources scale automatically with demand, and billing reflects actual execution time.

Serverless functions suit event-driven workloads with variable traffic. An image processing function that runs occasionally costs less than a VM running continuously. The operational burden shifts entirely to the platform provider: no servers to provision, patch, or scale.

The constraints are meaningful. Serverless functions have execution time limits (typically 5-15 minutes maximum). They start with some latency as the platform provisions execution context. They work best for stateless operations; maintaining state between invocations requires external storage.

## Choosing a Deployment Model

Each model presents trade-offs along several dimensions:

| Model | Control | Operational Overhead | Startup Time | Cost Model |
| --- | --- | --- | --- | --- |
| Physical Server | Complete | High | Days/weeks | Capital expense |
| Virtual Machine | High | Medium | Minutes | Hourly/reserved |
| Container | Medium | Low | Milliseconds | Per-pod or underlying VM |
| Serverless | Minimal | None | Sub-second (with cold start) | Per-execution |

When you need maximum performance and complete environment control, physical servers or dedicated VMs make sense. When you need rapid scaling and minimal operational overhead, containers and serverless functions offer advantages. Most production architectures combine models—VMs for databases requiring specific configurations, containers for stateless application services, serverless for event processing.

The choice depends on workload characteristics: resource requirements, scaling patterns, isolation needs, operational capacity, and cost constraints. Understanding what each model provides and requires enables matching the infrastructure to the application.

## Server Roles

Beyond deployment model, servers are categorized by the services they provide. Each role handles a specific part of application delivery, and production systems typically combine multiple roles to serve users.

Many server roles exist—mail servers, DNS servers, proxy servers, cache servers, message queue servers, and others. The following sections describe four roles commonly encountered in web application infrastructure, but these represent examples rather than an exhaustive list.

### Web Servers

A **web server** accepts HTTP and HTTPS requests from clients and returns responses. The response might be a static file (HTML, CSS, JavaScript, images) retrieved from disk, or the request might be forwarded to an application server for dynamic processing.

Web servers excel at handling many concurrent connections efficiently. They manage SSL/TLS termination, compress responses, cache frequently requested content, and distribute requests across multiple backend servers. Common web server software includes nginx and Apache HTTP Server.

In a typical deployment, the web server sits at the edge of the infrastructure, receiving all incoming traffic. It serves static assets directly and proxies application requests to backend services. This separation allows each component to be optimized for its specific task.

### Application Servers

An **application server** executes business logic and generates dynamic content. When a user submits a form or requests data, the application server processes that request—validating input, applying business rules, querying databases, and constructing the response.

Application servers run the actual application code. For Python applications, servers like Gunicorn or uWSGI manage worker processes that execute the Flask or Django code. Each worker handles requests independently, and the application server manages process lifecycle, restarts failed workers, and distributes incoming requests.

The application server layer scales horizontally. When traffic increases, additional instances can be deployed behind a load balancer. Stateless application design—where each request contains all necessary information—enables this scaling pattern.

### Database Servers

A **database server** manages persistent data storage and retrieval. Applications store user information, transaction records, and application state in databases, then query that data when needed.

Database servers provide more than storage. They enforce data integrity through constraints and transactions, optimize query execution through indexing and query planning, and manage concurrent access from multiple clients. Relational databases like PostgreSQL and MySQL use SQL for data manipulation; other databases use different query models suited to specific use cases.

Database servers typically scale differently than application servers. While applications scale horizontally by adding instances, databases often scale vertically—increasing CPU, memory, and storage on a single server. Read replicas can distribute query load, but write operations usually flow through a primary server to maintain consistency.

### File Servers

A **file server** provides centralized storage that multiple clients can access over a network. Rather than storing files locally on each machine, clients read and write to shared storage.

File servers implement access controls, determining which users can read, modify, or delete specific files. Network protocols like NFS (Network File System) for Linux environments and SMB (Server Message Block) for Windows environments handle the communication between clients and the file server.

Cloud environments often use object storage services instead of traditional file servers. Object storage provides high durability and availability for unstructured data like images, videos, and backups, accessed through HTTP APIs rather than filesystem protocols.

### Combining Server Roles

A typical web application involves multiple server roles working together. A request arrives at the web server, which forwards it to the application server. The application server processes the request, queries the database server for data, constructs a response, and returns it through the web server to the client.

These roles may run on separate infrastructure or coexist on a single machine. Small applications might run nginx, Gunicorn, and PostgreSQL on one VM. High-traffic applications separate these roles across multiple servers, allowing each to scale independently based on its resource demands.

## Summary

A server provides compute resources that process requests and deliver services over a network. The term describes both physical hardware and the software running on it. Servers can be deployed on physical machines, virtual machines, containers, or serverless platforms—each model offering different balances of control, overhead, and operational characteristics. Understanding these options enables selecting the appropriate foundation for an application’s requirements.

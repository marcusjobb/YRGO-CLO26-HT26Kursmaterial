---

title: Azure VM Sizing and Cost (Azure)
author: Marcus Ackre Medina
type: lecture
topic: infrastructure
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/infrastructure-fundamentals/compute/5-azure-vm-sizing-and-cost.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure Fundamentals-"
tags: ["(azure)", "azure", "cloud", "cost", "infrastructure", "linux", "networking", "sizing"]
week_fit: []
---

Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure Fundamentals-
Compute-- What is a Server?-- Common Server Roles-- Inside a Physical Server-- Inside a Virtual Server-- Azure VM Sizing and Cost (Azure)-
Network-
Storage-
IaCExercisesTutorials

# Azure VM Sizing and Cost (Azure)

🟡


Selecting a virtual machine size determines both the performance characteristics and the cost of cloud infrastructure. An undersized VM cannot handle the workload; an oversized VM wastes money on unused capacity. Understanding how VM resources map to workload requirements—and how cloud platforms price these resources—enables matching infrastructure to application needs efficiently.

## VM Resource Dimensions

Every virtual machine provides three primary resources that determine its capabilities.

### Processing Power (vCPUs)

The number of virtual CPUs determines how much parallel computation the VM can perform. More vCPUs enable handling more concurrent operations—more web requests, more database queries, more data processing threads.

CPU-intensive workloads—compiling code, processing data, rendering, scientific computation—benefit from additional vCPUs. Web servers handling many concurrent requests also benefit, as each request can execute on a separate core. A single-threaded application gains nothing from additional vCPUs beyond one.

### Memory (RAM)

Memory capacity determines how much data the VM can hold in fast-access RAM. Applications access RAM in nanoseconds; accessing disk takes milliseconds—a difference of several orders of magnitude.

Memory-intensive workloads include databases (holding indexes and hot data in memory), caching systems (Redis, Memcached), and applications with large working datasets. A VM that exhausts its memory begins swapping to disk, degrading performance severely.

### Storage (Disks)

Storage provides persistent capacity for the operating system, applications, and data. Two characteristics matter: capacity (how much data) and performance (how fast operations complete).

Storage performance is measured in IOPS (input/output operations per second) and throughput (megabytes per second). Database servers and applications with heavy disk access require high IOPS; workloads transferring large files require high throughput.

## Azure VM Series

Azure organizes VMs into series based on resource ratios and intended use cases. Each series provides a specific balance of vCPU, memory, and storage optimized for different workload types.

### B-Series: Burstable

B-series VMs provide a baseline CPU performance level and accumulate credits during idle periods. When the workload demands more CPU, the VM bursts above baseline using accumulated credits.

This model suits workloads with variable CPU usage—development environments, small websites, applications with occasional spikes. During idle periods, the VM accumulates credits at low cost. During activity spikes, it uses those credits for full CPU performance.

| Size | vCPUs | Memory | Baseline CPU | Approximate Monthly Cost |
| --- | --- | --- | --- | --- |
| Standard\_B1s | 1 | 1 GB | 10% | ~$8 |
| Standard\_B1ms | 1 | 2 GB | 20% | ~$15 |
| Standard\_B2s | 2 | 4 GB | 40% | ~$30 |
| Standard\_B2ms | 2 | 8 GB | 60% | ~$60 |

B-series is not suitable for workloads requiring sustained high CPU usage—the credits deplete and performance drops to baseline. For continuous high-CPU workloads, use D-series or F-series instead.

### D-Series: General Purpose

D-series VMs provide a balanced ratio of vCPU to memory (approximately 1:4) with consistent performance. No burstable credits—the VM delivers its full rated performance continuously.

This series suits most production workloads: web servers, application servers, development environments with consistent load, small to medium databases.

| Size | vCPUs | Memory | Approximate Monthly Cost |
| --- | --- | --- | --- |
| Standard\_D2s\_v5 | 2 | 8 GB | ~$70 |
| Standard\_D4s\_v5 | 4 | 16 GB | ~$140 |
| Standard\_D8s\_v5 | 8 | 32 GB | ~$280 |

D-series is the default choice for workloads without specific optimization requirements.

### E-Series: Memory Optimized

E-series VMs provide a high memory-to-vCPU ratio (approximately 1:8), optimized for workloads that require large amounts of RAM relative to compute power.

This series suits databases holding large datasets in memory, in-memory caching systems, and analytics workloads processing large data volumes.

| Size | vCPUs | Memory | Approximate Monthly Cost |
| --- | --- | --- | --- |
| Standard\_E2s\_v5 | 2 | 16 GB | ~$90 |
| Standard\_E4s\_v5 | 4 | 32 GB | ~$180 |
| Standard\_E8s\_v5 | 8 | 64 GB | ~$360 |

### F-Series: Compute Optimized

F-series VMs provide a high vCPU-to-memory ratio with the fastest per-core performance, optimized for compute-intensive workloads.

This series suits batch processing, scientific modeling, gaming servers, and other CPU-bound applications that benefit from raw compute power rather than large memory.

*Costs vary by region. Check the Azure pricing calculator for current rates in your region.*

## Selecting VM Size

Matching VM size to workload requirements involves identifying the workload type, estimating resource needs, and right-sizing based on actual usage.

### Identify Workload Type

Different workloads have different resource profiles:

| Workload Type | Primary Resource Need | Recommended Series |
| --- | --- | --- |
| Development/Test | Variable CPU, cost-sensitive | B-series |
| Web server | Balanced CPU and memory | D-series |
| Application server | Balanced CPU and memory | D-series |
| Database server | Memory, storage I/O | E-series |
| Batch processing | CPU | F-series |
| Caching | Memory | E-series |

### Estimate Resource Requirements

For a simple web application (Flask with nginx):

* vCPUs: 1-2 (handles typical traffic patterns)
* Memory: 1-2 GB (Flask processes are lightweight)
* Storage: 30 GB Standard SSD

A Standard\_B1s or Standard\_B1ms suits development; Standard\_D2s\_v5 provides headroom for production.

For a production database server:

* vCPUs: 4-8 (handles concurrent queries)
* Memory: 32-64 GB (holds indexes and working data)
* Storage: Premium SSD (low-latency queries)

A Standard\_E4s\_v5 or Standard\_E8s\_v5 provides the memory-to-CPU ratio databases require.

### Start Small and Scale

Predicting exact resource requirements before deployment is difficult. A better approach: start with a smaller size, monitor actual resource usage, and scale based on observed metrics.

1. Deploy with a conservative size estimate
2. Monitor CPU, memory, and storage metrics
3. Scale up if utilization consistently exceeds 70-80%
4. Scale down if utilization remains consistently low

Azure allows resizing VMs with a brief restart. This flexibility enables adjusting size based on actual usage rather than predictions.

## Understanding VM Costs

VM costs comprise multiple components beyond the compute charge.

### Cost Components

| Component | Description | Billing Model |
| --- | --- | --- |
| Compute | vCPU and memory | Per hour or per second |
| OS Disk | Boot disk storage | Per GB per month |
| Data Disks | Additional storage | Per GB per month |
| Public IP | Static IP address | Per hour |
| Network Egress | Outbound data transfer | Per GB |

### Example Cost Calculation

A Standard\_B1s in North Europe:

| Component | Specification | Monthly Cost |
| --- | --- | --- |
| Compute | 1 vCPU, 1 GB RAM | ~$8 |
| OS Disk | 30 GB Standard SSD | ~$5 |
| Public IP | Static | ~$3 |
| **Total** |  | **~$16/month** |

Network egress adds to this cost when users download data from the VM. Ingress (data uploaded to the VM) is free.

### Storage Tiers

Storage type significantly affects both performance and cost:

| Tier | Use Case | IOPS | Relative Cost |
| --- | --- | --- | --- |
| Standard HDD | Backups, archives | 500 | $ |
| Standard SSD | General workloads | 500-6000 | $$ |
| Premium SSD | Production databases | 120-20000 | $$$ |

Premium SSD costs approximately 2-3x more than Standard SSD for the same capacity, but delivers substantially higher IOPS for I/O-intensive workloads.

### OS Disks and Data Disks

Azure VMs use two categories of disks with different purposes.

The **OS disk** contains the operating system and is created automatically when provisioning a VM. For Linux VMs, this disk is typically 30 GB. The OS disk persists with the VM—deleting the VM can optionally delete the OS disk.

**Data disks** are additional storage attached to a VM for application data, databases, or other content that should be stored separately from the operating system. Data disks have their own lifecycle—they can be detached from one VM and attached to another, or preserved when a VM is deleted.

Separating data from the OS disk provides several benefits:

* Data persists independently of the VM lifecycle
* Data disks can be resized without affecting the OS
* Different performance tiers can be applied (Premium SSD for database, Standard SSD for logs)
* Backups and snapshots can target specific disks

For production workloads, store application data on data disks rather than the OS disk.

### Managed Disks

Azure offers two disk management models: Managed Disks and unmanaged disks (storage account blobs).

**Always use Managed Disks** for new deployments. Managed Disks provide:

* Automatic storage account management (no storage account limits to monitor)
* Higher availability through automatic placement across fault domains
* Simpler configuration and management
* Support for disk snapshots and Azure Backup integration

Unmanaged disks are a legacy approach that required manually managing storage accounts and their limits. There is no advantage to unmanaged disks for new deployments.

## Cost Optimization

Cloud infrastructure costs accumulate quickly across multiple VMs and services. Several strategies reduce costs without sacrificing required performance.

### Right-Size VMs

Over-provisioning “just in case” wastes money continuously. Monitor actual resource usage and downsize VMs that consistently show low utilization. A VM running at 10% CPU utilization could likely run on a smaller, cheaper size.

Use B-series for variable workloads instead of paying for consistent capacity that goes unused most of the time.

### Use Reserved Instances

Reserved Instances provide significant discounts (up to 72%) in exchange for committing to 1-year or 3-year terms. If you know a VM will run continuously for an extended period, reserved pricing reduces costs substantially.

Reserved instances suit stable production workloads. They are not appropriate for temporary or experimental deployments where the commitment cannot be honored.

### Stop VMs When Not Needed

Development and test VMs do not need to run continuously. Stopping (deallocating) a VM stops compute charges while preserving the disk and configuration. Only storage and static IP costs continue.

```
# Stop and deallocate VM (stops compute billing)
az vm deallocate --resource-group MyResourceGroup --name MyVM

# Start VM when needed
az vm start --resource-group MyResourceGroup --name MyVM
```

Azure Automation or Azure DevTest Labs can automate start/stop schedules for development environments.

### Choose Appropriate Storage

Match storage tier to actual requirements. Premium SSD costs significantly more than Standard SSD—use it only for workloads that require the additional IOPS. Archive infrequently accessed data to Standard HDD or blob storage rather than keeping it on expensive disk tiers.

### Monitor Costs

Azure Cost Management provides visibility into spending by resource, resource group, and subscription. Set budgets and alerts to notify when spending approaches limits. Review spending regularly to identify optimization opportunities.

The Azure Advisor service provides specific recommendations for cost optimization, including identifying underutilized VMs and recommending reserved instance purchases.

### Estimate Costs Before Deployment

The **Azure Pricing Calculator** (<https://azure.microsoft.com/pricing/calculator/>) enables estimating costs before deploying resources. Configure VM size, storage, networking, and region to see projected monthly costs. Compare different configurations to find the best balance of performance and cost for your requirements.

## Network Cost Considerations

Network costs can be significant for applications that transfer substantial data.

**Ingress is free**: Data uploaded to Azure (user form submissions, API requests) incurs no network charges.

**Egress is charged**: Data leaving Azure (web pages served to users, files downloaded, API responses) is billed per GB. Rates vary by volume and region.

To minimize egress costs:

* Keep related resources in the same region (intra-region traffic is free or very cheap)
* Compress responses (gzip reduces transfer size)
* Use CDN for static content (CDN egress is often cheaper than VM egress)
* Cache content at the edge to reduce origin requests

## Practical Recommendations

### Development and Learning

* **Size**: Standard\_B1s or Standard\_B1ms
* **Disk**: 30 GB Standard SSD
* **IP**: Dynamic (free) or static if needed
* **Cost**: ~$8-20/month

Stop VMs when not actively using them. Delete resource groups when projects complete. Use free tier credits if available.

### Production Web Application

* **Size**: Standard\_D2s\_v5 (scale up based on monitoring)
* **Disk**: Premium SSD for database, Standard SSD for application
* **IP**: Static public IP
* **Cost**: ~$80-150/month

Monitor metrics and right-size based on actual usage. Consider reserved instances for stable long-term workloads. Implement auto-scaling for variable traffic patterns.

## Summary

Selecting VM size requires matching workload resource requirements to VM capabilities. B-series suits variable workloads; D-series provides general-purpose balance; E-series optimizes for memory; F-series optimizes for compute. Start with conservative sizing, monitor actual usage, and adjust based on metrics.

VM costs include compute, storage, public IP, and network egress. Cost optimization involves right-sizing based on utilization, using reserved instances for stable workloads, stopping idle VMs, and choosing appropriate storage tiers. Monitoring costs and setting budgets prevents unexpected spending.

The Standard\_B1s provides adequate resources for development and learning at minimal cost. Production workloads typically start with D-series general-purpose VMs, scaling or changing series as monitoring reveals actual resource requirements.

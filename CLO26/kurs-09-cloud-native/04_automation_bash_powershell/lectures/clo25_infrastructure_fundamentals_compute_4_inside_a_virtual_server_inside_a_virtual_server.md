---

title: Inside a Virtual Server
author: Marcus Ackre Medina
type: lecture
topic: infrastructure
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/infrastructure-fundamentals/compute/4-inside-a-virtual-server/inside-a-virtual-server.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure Fundamentals-"
tags: ["azure", "cloud", "infrastructure", "inside", "linux", "networking", "security", "server", "virtual"]
week_fit: []
---

Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure Fundamentals-
Compute-- What is a Server?-- Common Server Roles-- Inside a Physical Server-- Inside a Virtual Server-- Azure VM Sizing and Cost (Azure)-
Network-
Storage-
IaCExercisesTutorials

# Inside a Virtual Server

🟡


[Watch the presentation](/presentations/infrastructure-fundamentals/compute/4-inside-a-virtual-server.html)

[Se presentationen på svenska](/presentations/infrastructure-fundamentals/compute/4-inside-a-virtual-server-swe.html)

---

Physical servers sit idle much of the time. A server sized for peak load runs at low utilization during normal operation, wasting purchased capacity. Virtual servers address this inefficiency by allowing multiple isolated computing environments to share the same physical hardware, each appearing to be a complete independent server.

Understanding virtual server architecture—how virtual components map to physical resources—clarifies the trade-offs between virtualization options and informs decisions about VM sizing and configuration.

## The Virtualization Layer

A **virtual server** (or virtual machine, VM) is a software-based emulation of a physical computer. The VM runs its own operating system and applications, isolated from other VMs on the same physical host. From the perspective of software running inside, the VM appears to be a dedicated physical machine.

The **hypervisor** makes this possible. The hypervisor is software that manages physical hardware resources and allocates them to virtual machines. It creates the abstraction layer between physical hardware and virtual environments.

Two types of hypervisors exist:

**Type 1 (bare-metal) hypervisors** run directly on the physical hardware, with no underlying operating system. They provide better performance and are used in production environments. VMware ESXi, Microsoft Hyper-V, and KVM (Kernel-based Virtual Machine) are Type 1 hypervisors.

**Type 2 (hosted) hypervisors** run as applications on top of a conventional operating system. They are easier to set up but add overhead. VirtualBox and VMware Workstation are Type 2 hypervisors, commonly used for development and testing.

Cloud platforms use Type 1 hypervisors to host customer VMs. Azure uses a customized version of Hyper-V; AWS uses a modified version of KVM.

## Virtual Components

Each virtual server contains virtualized versions of the components found in physical servers. The hypervisor maps these virtual components to physical resources on the host.

### Virtual CPUs (vCPUs)

A **vCPU** represents processing capacity allocated to the virtual machine. The hypervisor schedules vCPU execution on the physical CPU cores of the host.

A VM with 4 vCPUs can execute four threads simultaneously—but these vCPUs share the physical cores with vCPUs from other VMs on the same host. The hypervisor time-slices access to physical cores, giving each VM its allocated share of processing time.

More vCPUs allow a VM to handle more concurrent operations. CPU-intensive workloads—data processing, compilation, encryption—benefit from additional vCPUs. The right number depends on the workload: a simple web server may need only 1-2 vCPUs, while a database server processing many concurrent queries may need 8 or more.

### Virtual Memory

Virtual memory is a portion of the host’s physical RAM allocated to the VM. The hypervisor isolates this memory—a VM cannot access memory allocated to other VMs.

The amount of virtual memory determines how much data the VM can hold in fast-access RAM versus slower disk storage. Memory-intensive applications—databases, caching systems, applications with large working datasets—require more virtual memory. A VM running out of memory will use disk swap, severely degrading performance.

Unlike vCPUs which are time-sliced, memory allocation is more rigid. Memory allocated to a VM is unavailable to other VMs, even if the VM is not fully using it. Some hypervisors support memory overcommitment and ballooning to reclaim unused memory, but these techniques add complexity.

### Virtual Storage

Virtual storage appears to the VM as disk drives, but the underlying implementation is files on the host’s storage system. A **virtual disk** is typically a single large file (VMDK for VMware, VHD/VHDX for Hyper-V, qcow2 for KVM) that contains the VM’s filesystem.

Virtual disk performance depends on the underlying physical storage. A virtual disk backed by SSD performs better than one backed by HDD. Cloud platforms offer different storage tiers—standard HDD, standard SSD, premium SSD—with corresponding performance and cost characteristics.

In Azure, **Managed Disks** handle the underlying storage accounts automatically. You specify the disk type and size; Azure manages placement and replication. Disk types include:

* **Standard HDD**: Lowest cost, suitable for backups and infrequently accessed data
* **Standard SSD**: Balanced performance and cost for general workloads
* **Premium SSD**: High performance for production databases and I/O-intensive applications

### Virtual Network Interfaces

A **virtual NIC (vNIC)** connects the VM to virtual networks. The hypervisor or cloud platform routes traffic between vNICs and physical network infrastructure.

Each VM receives its own IP address and appears as a distinct host on the network. Network isolation between VMs is enforced at the hypervisor level—VMs can only communicate according to configured network rules.

In Azure, VMs connect to **Virtual Networks (VNets)**, which provide isolated network environments. Network Security Groups (NSGs) control traffic flow, allowing specific ports and protocols while blocking others.

## Comparing Physical and Virtual Servers

Physical and virtual servers differ in several important dimensions.

### Resource Allocation

A physical server dedicates all its resources to a single environment. A 32-core server with 128 GB RAM provides exactly that capacity to its workload.

Virtual servers share underlying physical resources. A host with 32 cores might run ten VMs with 4 vCPUs each. The hypervisor schedules these virtual CPUs on physical cores, multiplexing access. This sharing improves utilization but means VMs compete for physical resources.

### Isolation

Physical servers provide complete isolation—separate hardware, separate failure domains. A hardware fault affects only that server.

Virtual servers share hardware, so a host failure affects all VMs on that host. However, the hypervisor provides strong isolation between VMs on the same host. One VM cannot access another VM’s memory or storage; a crash in one VM does not affect others.

### Provisioning Speed

Physical servers require procurement, shipping, rack installation, and configuration—a process taking days or weeks.

Virtual servers provision in minutes. Cloud platforms maintain pools of physical capacity; creating a VM allocates resources from this pool and boots the operating system. This speed enables elastic scaling and rapid deployment.

### Scaling

Scaling physical infrastructure requires purchasing and installing new hardware. Lead times are long and capacity changes are coarse-grained.

Virtual servers scale by changing configuration. Vertical scaling (adding vCPUs or memory) requires a restart but no hardware changes. Horizontal scaling (adding more VMs) happens by provisioning additional instances. Cloud platforms automate this scaling based on metrics like CPU utilization.

### Cost Structure

Physical servers require capital expenditure: purchasing hardware, paying for data center space, power, and cooling. Costs are fixed regardless of utilization.

Virtual servers in cloud platforms use operational expenditure: pay for resources consumed, billed hourly or per-second. Costs scale with usage. This model converts fixed costs to variable costs and eliminates upfront capital requirements.

## Virtual Servers in Azure

Azure Virtual Machines exemplify cloud-based virtualization. When you create an Azure VM, you specify:

* **Region**: The Azure data center location
* **Size**: The vCPU and memory configuration (more on this in VM sizing)
* **Image**: The operating system (Ubuntu, Windows Server, etc.)
* **Storage**: OS disk type and additional data disks
* **Networking**: Virtual network, subnet, and public IP configuration

Azure manages the underlying physical infrastructure—the hypervisors, physical servers, networking, and storage systems. You manage from the VM level upward: operating system, applications, and data.

Azure provides different VM series optimized for different workloads:

* **B-series**: Burstable VMs for variable workloads
* **D-series**: General-purpose balanced compute and memory
* **E-series**: Memory-optimized for databases and caching
* **F-series**: Compute-optimized for CPU-intensive workloads

Each series offers multiple sizes with different vCPU and memory configurations. Selecting the appropriate series and size requires understanding your workload’s resource requirements.

## Trade-offs of Virtualization

Virtualization provides efficiency, flexibility, and rapid provisioning, but introduces trade-offs.

**Performance overhead**: The hypervisor consumes some resources and adds latency to hardware access. This overhead is typically 2-10% compared to bare-metal, though it varies by workload. I/O-intensive workloads see more overhead than compute-intensive ones.

**Noisy neighbors**: VMs on the same physical host compete for resources. If one VM consumes excessive CPU or I/O, others may experience degraded performance. Cloud platforms mitigate this through resource isolation and limits, but the shared nature of infrastructure remains.

**Abstraction limits**: Some workloads require direct hardware access—specialized network cards, GPU computing, specific CPU features. Virtualization may not expose these capabilities, or may expose them with limitations.

For most workloads, these trade-offs are acceptable given the benefits of virtualization. When they are not acceptable, dedicated hosts or bare-metal cloud instances provide physical isolation at higher cost.

## Summary

Virtual servers emulate physical computers in software, with virtual CPUs, memory, storage, and networking mapped to physical resources by a hypervisor. This abstraction enables efficient hardware utilization, rapid provisioning, and flexible scaling—but introduces performance overhead and shared-resource considerations. Understanding virtual server architecture informs decisions about VM sizing, storage configuration, and when virtualization trade-offs matter for specific workloads.

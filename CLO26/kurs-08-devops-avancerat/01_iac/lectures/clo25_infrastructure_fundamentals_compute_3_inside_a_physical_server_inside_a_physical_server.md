Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure Fundamentals-
Compute-- What is a Server?-- Common Server Roles-- Inside a Physical Server-- Inside a Virtual Server-- Azure VM Sizing and Cost (Azure)-
Network-
Storage-
IaCExercisesTutorials

# Inside a Physical Server

🟡


[Watch the presentation](/presentations/infrastructure-fundamentals/compute/3-inside-a-physical-server.html)

[Se presentationen på svenska](/presentations/infrastructure-fundamentals/compute/3-inside-a-physical-server-swe.html)

---

Servers that handle production workloads operate continuously, often for years without shutdown. This continuous operation requires hardware designed for reliability, serviceability, and performance under sustained load. Understanding the components inside a physical server clarifies how these requirements translate into hardware design and informs decisions about capacity planning and maintenance.

## Core Components

A physical server contains the same fundamental components as a desktop computer—processor, memory, storage, and networking—but in configurations optimized for server workloads. Each component plays a specific role in processing requests and maintaining system availability.

### Central Processing Unit (CPU)

The **CPU** executes instructions that constitute all server operations. Server-grade processors differ from desktop processors in several ways: they support more cores, more memory, and features like error-correcting code (ECC) memory support.

Server CPUs contain multiple cores—8, 16, 32, or more—allowing parallel execution of tasks. A web server handling many concurrent requests benefits from more cores because each request can execute on a separate core simultaneously. CPU-intensive workloads like video encoding or data analysis also benefit from high core counts.

Intel Xeon and AMD EPYC processors dominate the server market. These processors support dual-socket configurations, where two physical CPUs operate in the same server, doubling available cores and memory bandwidth.

### Random Access Memory (RAM)

**RAM** provides temporary storage for data the CPU is actively processing. Server RAM capacity directly affects how many concurrent operations the server can handle and how much data can remain in fast-access memory rather than slower disk storage.

Servers typically use **ECC (Error-Correcting Code) memory**, which detects and corrects single-bit memory errors automatically. In a server running continuously for months, random bit flips from cosmic rays or electrical interference become statistically likely. ECC memory prevents these errors from causing crashes or data corruption.

Server RAM capacity ranges from 16 GB for small workloads to multiple terabytes for large database servers. Memory-intensive applications like in-memory databases or caching layers require substantial RAM to hold working datasets entirely in memory.

### Storage Drives

Storage holds the operating system, applications, and persistent data. Servers use two primary storage technologies with different performance characteristics.

**Hard Disk Drives (HDDs)** use spinning magnetic platters and mechanical read/write heads. They offer high capacity at low cost—several terabytes per drive—but mechanical movement limits their speed. HDDs suit archival storage and workloads that access data sequentially.

**Solid-State Drives (SSDs)** use flash memory with no moving parts. They deliver much faster read and write speeds and lower latency than HDDs, but cost more per gigabyte. SSDs suit workloads requiring fast random access, including databases and operating system drives.

Servers often use **RAID (Redundant Array of Independent Disks)** configurations that combine multiple drives for redundancy or performance. RAID 1 mirrors data across two drives—if one fails, the other contains a complete copy. RAID 10 combines mirroring with striping for both redundancy and performance. These configurations allow a server to survive drive failures without data loss or downtime.

### Motherboard

The **motherboard** provides the physical and electrical connections between all components. It contains the CPU sockets, memory slots, storage interfaces, and expansion slots that allow components to communicate.

Server motherboards differ from desktop motherboards in their support for multiple CPUs, larger amounts of RAM (often 12 or more memory slots), and more expansion slots for additional network interfaces or storage controllers.

The motherboard also hosts the **BMC (Baseboard Management Controller)**, a separate processor that monitors hardware health and provides remote management capabilities even when the main system is powered off.

### Power Supply Unit (PSU)

The **PSU** converts AC power from the data center to the DC voltages required by server components. Server power supplies are rated by wattage (how much power they can deliver) and efficiency (how much input power converts to usable output versus waste heat).

Servers typically include **redundant power supplies**—two or more PSUs that can each power the entire server independently. If one PSU fails, the others continue operating without interruption. Hot-swappable designs allow replacing a failed PSU without shutting down the server.

Efficiency ratings like 80 PLUS Platinum or Titanium indicate that the PSU converts at least 90-94% of input power to usable output. Higher efficiency reduces electricity costs and heat generation in data centers where thousands of servers operate.

### Cooling Systems

Server components generate substantial heat during operation. Without adequate cooling, temperatures rise until components throttle performance or fail. Cooling systems maintain safe operating temperatures.

**Fans** move air across heat-generating components. Server fans are larger and spin faster than desktop fans, moving more air but generating more noise. Fan speed adjusts automatically based on temperature sensor readings.

**Heat sinks** are metal blocks with fins that attach directly to the CPU. They conduct heat away from the processor and dissipate it into the airflow. Some servers use liquid cooling, circulating coolant through channels in the heat sink for more efficient heat transfer.

Data center design complements server cooling. Hot aisle/cold aisle arrangements direct cool air into server intakes and exhaust hot air away from them, maintaining consistent airflow patterns.

### Network Interface Cards (NICs)

**NICs** connect the server to networks, enabling communication with clients and other servers. Server NICs support higher speeds than typical desktop network adapters—1 Gbps, 10 Gbps, 25 Gbps, or faster.

Servers often include multiple NICs for different purposes: one for production traffic, another for management access, another for storage network communication. Multiple NICs also enable redundancy—if one network path fails, traffic routes through another.

Some NICs include hardware offload capabilities that handle network processing tasks (checksums, segmentation) on the NIC rather than the CPU, freeing CPU cycles for application work.

### Expansion Slots

**PCIe (Peripheral Component Interconnect Express) slots** allow adding specialized hardware: additional NICs, GPU accelerators, storage controllers, or hardware security modules. PCIe provides high-bandwidth, low-latency connections between expansion cards and the system.

Servers typically include multiple PCIe slots of varying sizes (x4, x8, x16) corresponding to different bandwidth capabilities. The x16 slots provide the highest bandwidth for demanding cards like GPUs.

## Design for Continuous Operation

Server hardware design prioritizes reliability and serviceability—the ability to maintain and repair systems with minimal downtime.

**Hot-swappable components** can be replaced while the server continues operating. Hot-swap drive bays allow replacing failed disks without shutdown. Hot-swap power supplies allow replacing failed PSUs immediately. This capability is essential for systems that cannot tolerate downtime.

**Rack-mountable chassis** fit into standard 19-inch server racks. Servers are measured in rack units (U)—1U is 1.75 inches tall. A 2U server provides more internal space for drives and expansion cards than a 1U server. Rack mounting enables high-density deployments in data centers.

**Remote management interfaces** like IPMI, iLO (HP), or iDRAC (Dell) provide out-of-band access to server hardware. Administrators can power servers on and off, access console output, and monitor hardware health remotely—even when the operating system is not running. This capability is essential for managing servers in remote data centers.

## Summary

Physical servers contain the same fundamental components as other computers—CPU, RAM, storage, networking—but in configurations designed for continuous operation, reliability, and serviceability. Redundant power supplies survive PSU failures; ECC memory prevents bit errors; RAID storage tolerates drive failures; hot-swap designs enable repairs without downtime. Understanding these components and their server-specific characteristics informs capacity planning and infrastructure decisions.

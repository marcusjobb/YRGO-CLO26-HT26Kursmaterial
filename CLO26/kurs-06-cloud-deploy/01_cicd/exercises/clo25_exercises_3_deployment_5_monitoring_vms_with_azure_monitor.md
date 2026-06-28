---

title: 5. Monitoring Azure VMs with Azure Monitor
author: Marcus Ackre Medina
type: exercise
topic: deployment
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/exercises/3-deployment/5-monitoring-vms-with-azure-monitor.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-"
tags: ["azure", "cloud", "deployment", "devops", "docker", "dotnet", "exercise", "git", "iac", "linux"]
week_fit: []
---

Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-
Server Foundation-
Network Foundation-
Deployment-- 1. Deploy HTML with SCP-- 2. Deploy .NET MVC App with Systemd-- 3. Use Github Actions to Deploy your App-- 4. Implementing Azure Key Vault with MongoDB on Azure VM-- 5. Monitoring Azure VMs with Azure Monitor-
Cloud Databases-
Webapp Development-
Code Collaboration-
DockerTutorials

# 5. Monitoring Azure VMs with Azure Monitor

🟡


## Goal

Configure monitoring for an Azure Virtual Machine (VM) using Azure Monitor to gain visibility into system performance and application logs.

> **What you’ll learn:**
>
> * How to create a Log Analytics Workspace as a central data store
> * How to enable Azure Monitor Insights for VM performance metrics (CPU, Memory, Disk)
> * How to collect Linux Syslog via Data Collection Rules
> * How to redirect Nginx access logs to Syslog for centralized analysis
> * How to query and analyze logs using KQL (Kusto Query Language)

## Prerequisites

> **Before starting, ensure you have:**
>
> * ✓ An active Azure subscription (student or pay-as-you-go)
> * ✓ Access to the [Azure Portal](https://portal.azure.com/)
> * ✓ Basic familiarity with the Azure Portal navigation
> * ✓ An SSH key pair (or willingness to generate one during the exercise)
> * ✓ Basic knowledge of Linux commands and SSH

## Exercise Steps

### Overview

1. **Log in to Azure Portal**
2. **Create a Log Analytics Workspace (LAWS)**
3. **Provision a Virtual Machine**
4. **Enable Insights on the VM**
5. **Configure Syslog Collection**
6. **Install Nginx and Redirect Access Logs to Syslog**

### **Step 1:** Log in to Azure Portal

All resources in this exercise are created through the Azure Portal. Start by signing in to your account.

1. **Open** a web browser and **navigate to** the [Azure Portal](https://portal.azure.com/)
2. **Sign in** using your Azure account credentials

### **Step 2:** Create a Log Analytics Workspace (LAWS)

A Log Analytics Workspace is the central data store for Azure Monitor. All metrics, logs, and diagnostic data from your monitored resources flow into this workspace, where you can query and analyze them using KQL. You need to create this first because the monitoring components you configure later need a destination for the data they collect.

1. **Search for** `Log Analytics workspaces` in the Azure Portal
2. **Click** `+ Create`
3. **Fill out** the following details:

   * **Subscription**: Select your subscription
   * **Resource Group**: Create new `MonitoringRG`
   * **Name**: `MonitoringLAWS`
   * **Region**: `North Europe`
4. **Click** `Review + Create` and then `Create`

> ℹ **Concept Deep Dive**
>
> A Log Analytics Workspace (LAWS) is essentially a time-series database purpose-built for operational data. It stores logs and metrics from multiple Azure resources and on-premises systems, enabling cross-resource querying with KQL. Think of it as the central nervous system of your monitoring setup — every other component you configure in this exercise sends data here.
>
> ⚠ **Common Mistakes**
>
> * Creating the workspace in a different region than your VM adds latency and may incur cross-region data transfer costs
> * The workspace name must be globally unique across all Azure subscriptions
>
> ✓ **Quick check:** The Log Analytics Workspace shows status `Succeeded` in the Azure Portal

### **Step 3:** Provision a Virtual Machine

Now you need a VM to monitor. This step creates an Ubuntu Linux VM that will serve as your monitoring target throughout the exercise. You place it in the same resource group and region as your workspace to keep things organized and minimize latency.

1. **Search for** `Virtual Machines` in the Azure Portal and **click** `+ Create`
2. **Configure** the **Basics** tab:

   * **Subscription**: Select your subscription
   * **Resource group**: `MonitoringRG`
   * **Name**: `MonitoredVM`
   * **Region**: `North Europe`
   * **Image**: Choose **Ubuntu Server 24.04 LTS**
   * **Size**: Standard B1s
   * **Authentication Type**: Select **SSH public key**
     + **Username**: `azureuser`
     + If using SSH public key: alt 1) paste your public key; alt 2) download a new key
   * **Select inbound ports**: 80 and 22
3. **Click** `Review + create`, then `Create` to provision the VM

> ℹ **Concept Deep Dive**
>
> **Standard B1s** is a burstable VM size ideal for light workloads and testing. It provides 1 vCPU and 1 GiB RAM at low cost, accumulating CPU credits during idle periods that can be spent during bursts. This is sufficient for running Nginx and the monitoring agent.
>
> **SSH key authentication** is more secure than passwords. It uses asymmetric cryptography — a public key is placed on the server while your private key stays on your machine. Azure can generate a key pair for you if you don’t have one.
>
> **Opening ports 22 and 80** creates Network Security Group (NSG) rules allowing SSH access (for administration) and HTTP traffic (for Nginx later in the exercise).
>
> ⚠ **Common Mistakes**
>
> * Forgetting to open port 80 will prevent HTTP traffic to Nginx in Step 6
> * Choosing a different region than your LAWS adds unnecessary complexity
> * If you download a new SSH key, save it securely — you cannot retrieve it later
>
> ✓ **Quick check:** VM shows `Running` status in the Azure Portal

### **Step 4:** Enable Insights on the VM

VM Insights provides a pre-built monitoring experience for CPU, memory, disk, and network metrics. Enabling it triggers several actions behind the scenes: Azure assigns a system-managed identity to the VM, installs the Azure Monitor Agent (AMA) as an extension, creates a Data Collection Rule (DCR) for performance counters, and associates the DCR with your VM.

1. **Go to** the VM in the Azure Portal
2. **Navigate to** the left menu: Monitoring > Insights

   * **Click** `Enable`
   * Enable guest performance: check (will Configure Performance Counters)
   * **Data collection rule**: Create New
     + **Data collection rule name**: `MonitoredVM` (same as the VM)
     + **Log Analytics workspaces**: `MonitoringLAWS`
     + **Create**
3. **Click** `Configure` (this creates a DCR and installs AMA, including managed identity, on the VM)

> ℹ **Concept Deep Dive**
>
> **Azure Monitor Agent (AMA)** is the modern, unified agent that replaces the older Log Analytics Agent (MMA/OMS). It uses Data Collection Rules for configuration, supports multi-homing (sending data to multiple destinations), and has better resource consumption. AMA requires a managed identity on the VM to authenticate with Azure services without storing credentials.
>
> **Data Collection Rules (DCR)** define *what* data to collect, *how* to transform it, and *where* to send it. By separating the collection configuration from the agent, DCRs enable centralized, flexible management. The Insights DCR created here collects performance counters like CPU %, available memory, disk I/O, and network throughput.
>
> **Managed Identity** is an Azure AD identity automatically assigned to your VM. It allows the AMA agent to authenticate securely with Azure Monitor without any stored secrets or passwords — a key security best practice.
>
> ✓ **Quick check:** The Insights page shows `Monitoring agent: Connected` after a few minutes
>
> ⚠ **Timing Expectations**
>
> A brand-new Log Analytics Workspace has a warm-up period. The first data batch takes significantly longer to appear than subsequent data. Expect:
>
> * **Heartbeat**: ~5–10 minutes
> * **Syslog**: ~10–15 minutes
> * **InsightsMetrics** (performance counters): ~15–20 minutes
>
> This is normal — don’t wait for data here. Continue with Steps 5 and 6, then come back to verify once everything is set up. By that time, data should be flowing.
>
> ✅ **Verification Step: Insights Data**
>
> 1. **Navigate to** VM > Insights > Performance (you might have to refresh the browser — not just the refresh button)
>    * Ensure metrics for **CPU**, **Memory**, and **Disk** are visible (this can take up to 15–20 minutes on a new workspace)
> 2. **Go to** VM > Logs and run the following queries:
>    * `Heartbeat`
>    * `InsightsMetrics`
> 3. **Go to** Data collection rules
>    * Select: `MSVMI-MonitoredVM`
>    * Go to Configuration > Data sources. Verify *Performance Counters*
>    * Go to Configuration > Resources. Verify *MonitoredVM*

### **Step 5:** Configure Syslog Collection

Syslog is the standard logging protocol on Linux systems. Every Linux service — from the kernel to SSH to cron jobs — writes structured messages to syslog with a *facility* (category) and *severity level*. By creating a separate Data Collection Rule for Syslog, you enable Azure Monitor to ingest these system logs and make them queryable alongside your performance metrics.

1. **In the Azure Portal**, create another Data Collection Rule:

   * **Basic Tab:**

     + **Name**: `MonitoredVMSyslogDCR`
     + Resource group: `MonitoringRG`
     + Region: `North Europe`
     + Platform Type: `Linux`
     + Next: Resources
   * **Resources Tab:**

     + **Add resource**: `MonitoredVM`
     + **Apply**
     + Next: Collect and deliver
   * **Collect and deliver Tab:**

     + **Add data source**: Linux Syslog (check all facilities and set LOG\_INFO)
     + Next: Destination
     + **Destination**: Select `MonitoringLAWS`
     + Add data source
2. **Click** `Review + create`, then `Create`

> ℹ **Concept Deep Dive**
>
> **Syslog facilities** categorize log sources. Standard facilities include `kern` (kernel), `auth` (authentication), `daemon` (background services), and `cron` (scheduled tasks). The facilities `local0` through `local7` are reserved for custom applications — Nginx uses `local7` by default, which becomes important in the next step.
>
> **Log levels** follow a severity hierarchy: Debug < Info < Notice < Warning < Error < Critical < Alert < Emergency. Setting the minimum to `LOG_INFO` captures everything from Info upward, filtering out verbose Debug messages that would generate excessive data and cost.
>
> We create a **separate DCR** for syslog rather than adding it to the Insights DCR. This keeps collection rules modular — you can enable or disable syslog collection independently without affecting performance monitoring.
>
> ⚠ **Common Mistakes**
>
> * Setting log level to Debug generates excessive data volume and increases costs
> * Forgetting to add the VM as a resource in the Resources tab means no data will be collected
> * The DCR association can take a few minutes to become active
>
> ✓ **Quick check:** The new DCR shows status `Succeeded` with MonitoredVM listed under Resources
>
> ⚠ **Timing Expectations**
>
> After creating the Syslog DCR, the Azure Monitor Agent needs 2–5 minutes to receive the new configuration and start collecting syslog data. The data then takes another 5–10 minutes to appear in Log Analytics queries. Continue with Step 6 while waiting.
>
> ✅ **Verification Step: Syslog Data**
>
> 1. **Go to** Monitoring > Logs on the VM
> 2. **Run** a query to check Syslog data (allow 10–15 minutes after DCR creation for data to appear):
>
>    ```
>    Syslog | take 10
>    ```

### **Step 6:** Install Nginx and Redirect Access Logs to Syslog

Now you install a web server and configure it to send its access logs through syslog instead of only writing to a file. This creates a unified log pipeline: Nginx writes to syslog, the Azure Monitor Agent picks up syslog entries, and they flow into your Log Analytics Workspace — enabling centralized querying of both system logs and web traffic data.

1. **Connect** to the VM using SSH:

   ```
   ssh azureuser@<VM-public-IP>
   ```
2. **Update** the package list and **install** Nginx:

   ```
   sudo apt update && sudo apt install -y nginx
   ```

> ✅ **Verification Step: Nginx**
>
> 1. **Verify** Nginx is serving the default page:
>
>    ```
>    curl localhost
>    ```
> 2. **Tail** the Nginx access log, then browse to your Nginx site in a browser and verify the logs appear:
>
>    ```
>    tail -f /var/log/nginx/access.log
>    ```
>
>    Press `Ctrl-C` to exit

Now configure Nginx to send access logs to syslog:

1. **Open** the default Nginx configuration file:

   ```
   sudo nano /etc/nginx/sites-enabled/default
   ```
2. **Add** the `access_log` directive into the server block:

   ```
   access_log syslog:server=unix:/dev/log combined;
   ```
3. **Test** and **restart** Nginx:

   ```
   sudo nginx -t
   sudo systemctl restart nginx
   ```

> ℹ **Concept Deep Dive**
>
> The directive `access_log syslog:server=unix:/dev/log combined` tells Nginx to send access log entries to the local syslog daemon via a Unix socket (`/dev/log`). The `combined` format includes the client IP, timestamp, HTTP request, status code, response size, referrer, and user agent — all the fields you need for traffic analysis.
>
> This does **not** disable the default file-based logging in `/var/log/nginx/access.log`. Nginx supports multiple `access_log` directives simultaneously, so you get both file-based logs for local debugging and syslog-based logs for centralized monitoring.
>
> Nginx sends syslog messages using facility `local7`, which is why you can filter for it in KQL queries using `Facility == "local7"`.
>
> ⚠ **Common Mistakes**
>
> * Forgetting to run `sudo nginx -t` before restarting can leave Nginx in a broken state if there’s a syntax error
> * The syslog directive must be inside the `server` block, not outside it
> * Not restarting Nginx after changing the configuration means the changes have no effect
>
> ✓ **Quick check:** `sudo nginx -t` outputs `syntax is ok` and `test is successful`
>
> ✅ **Verification Step: Nginx Logs in Syslog**
>
> 1. **Tail** the system log:
>
>    ```
>    tail -f /var/log/syslog
>    ```
> 2. **Browse** to your Nginx site and verify the logs appear in syslog
> 3. **Run** a query in VM > Logs to analyze Nginx logs (allow 5–10 minutes for the data pipeline to deliver them to the workspace):
>
>    ```
>    Syslog | where Facility == "local7" | count
>    ```
>
> ⚠ **Timing Expectations**
>
> If this is your first time querying after setting up the full pipeline, all three data types (Heartbeat, Syslog, InsightsMetrics) should now be available. If `local7` returns 0 results, make sure you have browsed to the Nginx site to generate traffic, then wait a few more minutes and retry. The end-to-end pipeline latency from Nginx request to queryable data is typically 5–15 minutes.

## Common Issues

> **If you encounter problems:**
>
> **No data in Insights after enabling:** A brand-new workspace has a warm-up period. Heartbeat data appears after ~5–10 minutes, Syslog after ~10–15 minutes, and InsightsMetrics (performance counters) after ~15–20 minutes. Refresh the full browser page (not just the refresh button) and be patient. Continue with the next steps while waiting.
>
> **Syslog query returns no results:** Verify the DCR has the VM listed under Resources and that the status is Succeeded. After DCR creation, the agent needs 2–5 minutes to pick up the new configuration, plus another 5–10 minutes for data to appear in queries.
>
> **Cannot SSH into the VM:** Check that port 22 is open in the NSG. Verify you’re using the correct public IP and the right SSH key.
>
> **Nginx not responding on port 80:** Ensure port 80 is open in the NSG and that Nginx is running (`sudo systemctl status nginx`).
>
> **`nginx -t` reports a syntax error:** The `access_log` directive must be placed inside the `server { }` block, not at the top level of the file.
>
> **Facility “local7” returns no results in KQL:** Browse to the Nginx site first to generate some access log entries, then wait 2–5 minutes for the data pipeline to deliver them to the workspace.

## Summary

You’ve successfully configured a complete monitoring pipeline for an Azure VM:

* ✓ Created a Log Analytics Workspace as the central data store
* ✓ Enabled VM Insights for performance metrics (CPU, Memory, Disk)
* ✓ Configured Syslog collection via Data Collection Rules
* ✓ Set up a unified log pipeline from Nginx through Syslog to Azure Monitor
* ✓ Queried and analyzed logs using KQL

> **Key takeaway:** Azure Monitor’s architecture separates *collection* (AMA + DCRs) from *storage* (Log Analytics Workspace) from *analysis* (KQL). This modular design means you can add new data sources or monitoring targets without redesigning your pipeline. The same pattern scales from a single VM to hundreds of resources.

## Going Deeper: KQL Log Analysis (Optional)

> **Want to explore more?**
>
> The KQL queries below demonstrate various analyses you can perform on Nginx access logs flowing through Syslog. Try these in VM > Logs to gain deeper insights into web traffic, user behavior, and security.

### Traffic Analysis

* **Requests Over Time** — analyze traffic patterns by minute:

  ```
  Syslog | where Facility == "local7"
  | summarize RequestCount = count() by bin(TimeGenerated, 1m)
  ```
* **Top Requested URLs** — identify the most accessed pages:

  ```
  Syslog | where Facility == "local7"
  | extend URL = extract("\"[A-Z]+ (/.*?) HTTP", 1, SyslogMessage)
  | summarize Count = count() by URL
  | sort by Count desc
  | take 10
  ```

### User Behavior

* **Top User Agents** — identify the browsers and devices your users are using:

  ```
  Syslog | where Facility == "local7"
  | extend UserAgent = extract(".*\"[^\"]+\"[ ]+\"([^\"]+)\"", 1, SyslogMessage)
  | summarize Count = count() by UserAgent
  | sort by Count desc
  | take 10
  ```

### Security Insights

* **Top 404 (Not Found) Errors** — identify missing pages or incorrect links:

  ```
  Syslog | where Facility == "local7"
  | extend StatusCode = extract("\" ([0-9]{3}) ", 1, SyslogMessage)
  | where StatusCode == "404"
  | extend URL = extract("\"[A-Z]+ (/.*?) HTTP", 1, SyslogMessage)
  | summarize Count = count() by URL
  | sort by Count desc
  | take 10
  ```
* **Frequent Client Errors (4xx)** — monitor bad requests or unauthorized access attempts:

  ```
  Syslog | where Facility == "local7"
  | extend StatusCode = extract("\" ([0-9]{3}) ", 1, SyslogMessage)
  | where StatusCode startswith "4"
  | summarize Count = count() by StatusCode
  ```
* **Top IPs Causing Errors** — detect potential malicious actors or misconfigured clients:

  ```
  Syslog | where Facility == "local7"
  | extend StatusCode = extract("\" ([0-9]{3}) ", 1, SyslogMessage)
  | where StatusCode startswith "4" or StatusCode startswith "5"
  | extend IP = extract("nginx: ([0-9.]+)", 1, SyslogMessage)
  | summarize Count = count() by IP
  | sort by Count desc
  ```

### Content and Resource Analysis

* **Top File Types** — understand what types of files are being requested most often:

  ```
  Syslog | where Facility == "local7"
  | extend FileType = extract("\\.(\\w+)$", 1, SyslogMessage)
  | summarize Count = count() by FileType
  | sort by Count desc
  ```

### Error Monitoring

* **Error Rate** — monitor the proportion of error responses over total responses:

  ```
  Syslog | where Facility == "local7"
  | extend StatusCode = extract("\" ([0-9]{3}) ", 1, SyslogMessage)
  | summarize TotalRequests = count(), Errors = countif(StatusCode startswith "4" or StatusCode startswith "5")
  | extend ErrorRate = Errors * 100 / TotalRequests
  ```

## Done

Happy Cloud! You’ve learned how to set up a complete monitoring pipeline from VM metrics and system logs through to centralized log analytics. This foundation applies to any Azure workload — from a single VM to a fleet of microservices.

---

## Appendix: Azure CLI Approach

This section provides the equivalent Azure CLI commands, letting you complete the exercise from the terminal in an Infrastructure as Code manner. All resources are created through a single script that you build up incrementally, re-running it as you add each section.

> ℹ **Concept Deep Dive**
>
> The portal walkthrough above teaches the *concepts* — what each resource does and how they connect. This appendix teaches the *automation* — how to express the same infrastructure as code. In production, you would always use a scripted approach (CLI, Bicep, or Terraform) for reproducibility, version control, and disaster recovery.

Create the following four files in the same directory.

### `cloud-init.yaml`

This cloud-config installs Nginx and configures syslog access logging at VM creation time, replacing manual Step 6.

```
#cloud-config
packages:
  - nginx

runcmd:
  - sed -i '/server_name _;/a\    access_log syslog:server=unix:/dev/log combined;' /etc/nginx/sites-enabled/default
  - nginx -t && systemctl restart nginx
```

> ℹ **Concept Deep Dive**
>
> **cloud-init** is the industry standard for VM initialization. Azure passes this YAML file to the VM at creation time, and cloud-init executes it on first boot. The `packages` section installs software through apt, and `runcmd` runs arbitrary shell commands after package installation. This eliminates the need to SSH in and configure manually.

### `dcr-insights.json`

Data Collection Rule for performance counters (CPU, memory, disk, network). The placeholder `__WORKSPACE_RESOURCE_ID__` is replaced by the script at runtime.

```
{
  "location": "northeurope",
  "kind": "Linux",
  "properties": {
    "dataSources": {
      "performanceCounters": [
        {
          "name": "perfCounterDataSource",
          "streams": ["Microsoft-InsightsMetrics"],
          "samplingFrequencyInSeconds": 60,
          "counterSpecifiers": [
            "\\Processor Information(_Total)\\% Processor Time",
            "\\Memory\\Available MBytes",
            "\\Memory\\% Used Memory",
            "\\LogicalDisk(_Total)\\% Free Space",
            "\\LogicalDisk(_Total)\\Disk Transfers/sec",
            "\\Network(_Total)\\Bytes Transmitted/sec",
            "\\Network(_Total)\\Bytes Received/sec"
          ]
        }
      ]
    },
    "destinations": {
      "logAnalytics": [
        {
          "workspaceResourceId": "__WORKSPACE_RESOURCE_ID__",
          "name": "logAnalyticsDest"
        }
      ]
    },
    "dataFlows": [
      {
        "streams": ["Microsoft-InsightsMetrics"],
        "destinations": ["logAnalyticsDest"]
      }
    ]
  }
}
```

> ℹ **Concept Deep Dive**
>
> The `counterSpecifiers` use Windows-style performance counter paths (e.g., `\Memory\Available MBytes`), even on Linux. The Azure Monitor Agent translates these internally to Linux equivalents. The `samplingFrequencyInSeconds: 60` means a data point every minute — a good balance between granularity and data volume for most workloads.

### `dcr-syslog.json`

Data Collection Rule for Syslog collection (all facilities, Info level and above).

```
{
  "location": "northeurope",
  "kind": "Linux",
  "properties": {
    "dataSources": {
      "syslog": [
        {
          "name": "syslogDataSource",
          "streams": ["Microsoft-Syslog"],
          "facilityNames": [
            "auth", "authpriv", "cron", "daemon", "kern", "syslog",
            "user", "local0", "local1", "local2", "local3", "local4",
            "local5", "local6", "local7", "mail", "news", "uucp",
            "lpr", "ftp", "ntp", "audit", "alert", "clock", "mark"
          ],
          "logLevels": [
            "Info", "Notice", "Warning", "Error",
            "Critical", "Alert", "Emergency"
          ]
        }
      ]
    },
    "destinations": {
      "logAnalytics": [
        {
          "workspaceResourceId": "__WORKSPACE_RESOURCE_ID__",
          "name": "logAnalyticsDest"
        }
      ]
    },
    "dataFlows": [
      {
        "streams": ["Microsoft-Syslog"],
        "destinations": ["logAnalyticsDest"]
      }
    ]
  }
}
```

### `setup-monitoring.sh`

Build up this script section by section. Re-running is safe — commands that create already-existing resources will print an error and continue.

```
#!/bin/bash

# --- Variables ---
RG="MonitoringRG"
LOCATION="northeurope"
WORKSPACE="MonitoringLAWS"
VM="MonitoredVM"

# ============================================================
# Step 2: Resource Group and Log Analytics Workspace
# ============================================================
az group create --name $RG --location $LOCATION

az monitor log-analytics workspace create \
  --resource-group $RG \
  --name $WORKSPACE \
  --location $LOCATION

# ============================================================
# Step 3 + 6: Provision VM with cloud-init (includes Nginx)
# ============================================================
az vm create \
  --resource-group $RG \
  --name $VM \
  --location $LOCATION \
  --image Ubuntu2404 \
  --size Standard_B1s \
  --admin-username azureuser \
  --generate-ssh-keys \
  --custom-data cloud-init.yaml

az vm open-port --resource-group $RG --name $VM --port 80

# ============================================================
# Step 4: Enable Monitoring — Insights
# ============================================================
WORKSPACE_RESOURCE_ID=$(az monitor log-analytics workspace show \
  --resource-group $RG --name $WORKSPACE --query id -o tsv)

VM_RESOURCE_ID=$(az vm show \
  --resource-group $RG --name $VM --query id -o tsv)

# Managed identity (required for Azure Monitor Agent)
az vm identity assign --resource-group $RG --name $VM

# Install Azure Monitor Agent
az vm extension set \
  --name AzureMonitorLinuxAgent \
  --publisher Microsoft.Azure.Monitor \
  --vm-name $VM \
  --resource-group $RG \
  --enable-auto-upgrade true

# Create and associate performance counters DCR
sed "s|__WORKSPACE_RESOURCE_ID__|$WORKSPACE_RESOURCE_ID|g" \
  dcr-insights.json > /tmp/dcr-insights.json

az monitor data-collection rule create \
  --resource-group $RG \
  --location $LOCATION \
  --name "${VM}-insights-dcr" \
  --rule-file /tmp/dcr-insights.json

DCR_INSIGHTS_ID=$(az monitor data-collection rule show \
  --resource-group $RG --name "${VM}-insights-dcr" --query id -o tsv)

az monitor data-collection rule association create \
  --name "${VM}-insights-association" \
  --rule-id "$DCR_INSIGHTS_ID" \
  --resource "$VM_RESOURCE_ID"

# ============================================================
# Step 5: Configure Syslog Collection
# ============================================================
sed "s|__WORKSPACE_RESOURCE_ID__|$WORKSPACE_RESOURCE_ID|g" \
  dcr-syslog.json > /tmp/dcr-syslog.json

az monitor data-collection rule create \
  --resource-group $RG \
  --location $LOCATION \
  --name "${VM}-syslog-dcr" \
  --rule-file /tmp/dcr-syslog.json

DCR_SYSLOG_ID=$(az monitor data-collection rule show \
  --resource-group $RG --name "${VM}-syslog-dcr" --query id -o tsv)

az monitor data-collection rule association create \
  --name "${VM}-syslog-association" \
  --rule-id "$DCR_SYSLOG_ID" \
  --resource "$VM_RESOURCE_ID"

# ============================================================
# Done
# ============================================================
echo "Setup complete!"
echo "VM public IP:"
az vm show --resource-group $RG --name $VM -d --query publicIps -o tsv
```

> ℹ **Concept Deep Dive**
>
> The `sed` command replaces the placeholder `__WORKSPACE_RESOURCE_ID__` in the JSON files with the actual workspace resource ID retrieved from Azure. The pipe character `|` is used as the sed delimiter instead of `/` to avoid conflicts with the forward slashes in Azure resource IDs. The modified files are written to `/tmp/` so the original JSON templates remain reusable.
>
> ⚠ **Timing: Agent Restart May Be Needed**
>
> In the CLI flow, the Azure Monitor Agent is installed *before* the DCR associations are created. The agent checks for associations at startup and may cache a “no configuration” state. If queries return no data after 15 minutes, SSH into the VM and restart the agent:
>
> ```
> sudo systemctl restart azuremonitoragent
> ```
>
> This forces the agent to re-fetch its configuration and pick up the DCR associations. In the portal flow this is less likely because Insights creates the DCR and association in a single operation.

### Verification from the CLI

You can query Log Analytics directly from the terminal:

```
WORKSPACE_ID=$(az monitor log-analytics workspace show \
  --resource-group MonitoringRG --name MonitoringLAWS \
  --query customerId -o tsv)

az monitor log-analytics query \
  --workspace "$WORKSPACE_ID" \
  --analytics-query "Syslog | where Facility == 'local7' | count"
```

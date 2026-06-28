---

title: 5. Provisioning a VM Using AZ CLI, Configuring Nginx, and Allowing HTTP Traffic
author: Marcus Ackre Medina
type: exercise
topic: infrastructure
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/exercises/1-server-foundation/2-command-line-interface/5-provisioning-vm-az-cli.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-"
tags: ["allowing", "azure", "cli", "cli,", "cloud", "configuring", "docker", "exercise", "git", "http"]
week_fit: []
---

Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-
Server Foundation--
1. Portal Interface--
2. Command Line Interface--- 4. Creating a Resource Group Using AZ CLI--- 5. Provisioning a VM Using AZ CLI, Configuring Nginx, and Allowing HTTP Traffic--- 6. Automating VM Creation, Nginx Installation, and Port Configuration Using a Bash Script--
3. ARM and Bicep-
Network Foundation-
Deployment-
Cloud Databases-
Webapp Development-
Code Collaboration-
DockerTutorials

# 5. Provisioning a VM Using AZ CLI, Configuring Nginx, and Allowing HTTP Traffic

🟡


## Goal

Provision a Virtual Machine using Azure CLI, install Nginx web server, and configure network security to allow HTTP traffic.

> **What you’ll learn:**
>
> * How to create and manage Azure resources using the Azure CLI
> * How to generate and use SSH keys for secure VM authentication
> * How to configure Azure Network Security Groups to control traffic

## Prerequisites

> **Before starting, ensure you have:**
>
> * ✓ Azure CLI installed and configured
> * ✓ VSCode with integrated terminal (Git Bash for Windows users)
> * ✓ An active Azure subscription

## Exercise Steps

### Overview

1. **Set up the environment**
2. **Create a resource group**
3. **Create a VM**
4. **Log in to the VM**
5. **Install Nginx**
6. **Open port 80 for HTTP traffic**
7. **Clean up resources (optional)**

### **Step 1:** Set Up the Environment

Before creating Azure resources, you need to ensure your local environment is properly configured. This includes verifying your Azure CLI installation and authenticating with your Azure account to establish the connection between your terminal and Azure’s management plane.

1. **Open** VSCode and launch the **Integrated Terminal**:

   * On Windows, use **Git Bash** inside VSCode.
   * On Mac or Linux, use the default terminal.
2. **Verify** that Azure CLI is installed by running:

   ```
   az --version
   ```

   * If Azure CLI is not installed, follow the [official installation guide](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli).
3. **Log in** to your Azure account:

   ```
   az login
   ```

   * Follow the on-screen instructions to complete the authentication.

### **Step 2:** Create a Resource Group

Resource groups serve as logical containers for all related Azure resources. By creating a dedicated resource group for this exercise, you can easily manage, monitor, and delete all associated resources together when you’re finished.

1. **Set** variables in the terminal session for the resource group and VM name:

   ```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
   resource_group="MyCLIVMGroup"
   vm_name="MyCLIVM"
   ```
2. **Create** the resource group in the `northeurope` location:

   ```
   az group create --name $resource_group --location northeurope
   ```

> ✓ **Quick check:** Review the output to confirm successful creation. You should see a JSON response similar to:

```
{
  "id": "/subscriptions/<subscription-id>/resourceGroups/MyCLIDemoGroup",
  "location": "eastus",
  "name": "MyCLIVMGroup",
  "properties": {
    "provisioningState": "Succeeded"
  }
}
```

### **Step 3:** Create a VM

Creating a VM through Azure CLI gives you precise control over the machine’s configuration. The command below provisions a cost-effective B1s instance with Ubuntu 24.04 and automatically configures SSH key-based authentication, which is more secure than password-based access.

1. **Create** a VM using the default SSH keys:

   ```
   az vm create \
      --resource-group $resource_group \
      --location northeurope \
      --name $vm_name \
      --image Ubuntu2404 \
      --size Standard_B1s \
      --admin-username azureuser \
      --generate-ssh-keys
   ```

> ℹ **Concept Deep Dive**
>
> This command:
>
> * Creates a VM named `MyCLIVM`.
> * Uses the latest Ubuntu 22.04 image.
> * Generates SSH keys if not already available in `~/.ssh/id_rsa`.
>
> If you don’t have default SSH keys (`~/.ssh/id_rsa`), Azure CLI will generate them automatically. You can also create them manually using:

```
ssh-keygen -t rsa -b 2048
```

> This generates:
>
> * `~/.ssh/id_rsa` (private key)
> * `~/.ssh/id_rsa.pub` (public key)
>
> ✓ **Quick check:** Review the output to confirm successful creation. You should see a JSON response similar to:

```
{
  "fqdns": "",
  "id": "/subscriptions/ca0a7799-8e2e-4237-8616-8cc0e947ecd5/resourceGroups/MyCLIVMGroup/providers/Microsoft.Compute/virtualMachines/MyCLIVM",
  "location": "northeurope",
  "macAddress": "60-45-BD-DD-A5-5F",
  "powerState": "VM running",
  "privateIpAddress": "10.0.0.4",
  "publicIpAddress": "13.74.100.155",
  "resourceGroup": "MyCLIVMGroup",
  "zones": ""
}
```

### **Step 4:** Log in to the VM

SSH (Secure Shell) provides encrypted communication between your local machine and the VM. By retrieving the public IP address and connecting via SSH, you establish a secure remote session that allows you to configure the server directly.

1. **Retrieve** the public IP address of the VM:

   ```
   vm_ip=$(az vm show --resource-group $resource_group --name $vm_name --show-details --query publicIps -o tsv)
   echo $vm_ip
   ```
2. **Log in** to the VM using your default SSH key:

   ```
   ssh azureuser@$vm_ip
   ```

### **Step 5:** Install Nginx

Nginx is a high-performance web server commonly used to serve static content and act as a reverse proxy. Installing it on your VM transforms the machine from a basic compute instance into a functional web server capable of responding to HTTP requests.

1. **Update** the system and install Nginx:

   ```
   sudo apt update
   sudo apt install nginx -y
   ```

> ✓ **Quick check:**

1. Run the command `curl localhost`. Ensure you get some HTML code back. This means that it runs correctly regardless of firewall settings.
2. Open a browser and go to the `<VM_Public_IP>`. You should **NOT** see the Nginx default page. This is because we haven’t opened the firewall on port 80 yet.

> ℹ **Concept Deep Dive**
> Azure VMs block HTTP traffic on port 80 by default for security reasons. This defense-in-depth approach means you need to explicitly allow HTTP traffic through the Network Security Group before external users can access your web server.

### **Step 6:** Open Port 80 for HTTP Traffic

Network Security Groups (NSGs) act as virtual firewalls for your Azure resources. By default, Azure blocks incoming traffic on common service ports. Opening port 80 creates an inbound security rule that permits HTTP traffic to reach your Nginx server.

1. **Exit** back to your laptop in the Terminal:

   ```
   exit
   ```
2. **Allow** traffic on port 80:

   ```
   az vm open-port --resource-group $resource_group --name $vm_name --port 80
   ```

> ✓ **Quick check:** Verify the rule is applied:

```
az network nsg rule list --resource-group $resource_group --nsg-name ${vm_name}NSG --output table
```

* Ensure a rule exists for port 80 allowing inbound traffic.

> Return to the browser and refresh the page. Confirm the default Nginx page loads in the browser, indicating port 80 is open and traffic is allowed.

### **Step 7:** Clean Up Resources (Optional)

Deleting unused resources prevents unexpected charges from accumulating on your Azure subscription. Since we placed all resources in a single resource group, you can remove everything with a single command.

1. **Delete** the resource group to avoid unnecessary costs:

   ```
   az group delete --name $resource_group --no-wait --yes
   ```

> ⚠ **Common Mistakes**
>
> * Forgetting to delete test resources leads to ongoing charges
> * The `--no-wait` flag returns immediately while deletion continues in background

## Common Issues

> **If you encounter problems:**
>
> **SSH connection refused:** Verify the VM is running and you’re using the correct public IP address
>
> **Nginx page not loading:** Ensure port 80 is open in the NSG and Nginx service is running
>
> **Azure CLI not authenticated:** Run `az login` again if your session has expired

## Summary

You’ve successfully provisioned a VM using Azure CLI which:

* ✓ Created a resource group to organize Azure resources
* ✓ Deployed an Ubuntu VM with SSH key authentication
* ✓ Installed and configured Nginx web server
* ✓ Modified Network Security Group rules to allow HTTP traffic

> **Key takeaway:** Azure CLI enables scriptable, repeatable infrastructure provisioning that forms the foundation for infrastructure automation and Infrastructure as Code practices.

## Going Deeper (Optional)

> **Want to explore more?**
>
> * Create a bash script that automates the entire VM provisioning process
> * Configure HTTPS with a self-signed certificate on port 443
> * Explore Azure CLI commands for scaling and monitoring your VM

## Done 🎉

You have successfully provisioned a VM using Azure CLI, logged in with SSH, installed and tested Nginx, and configured port 80 for HTTP traffic. This exercise demonstrates the fundamental workflow for command-line infrastructure management in Azure.

---

title: 2. Provisioning a VM with SSH Keys and Exploring Linux
author: Marcus Ackre Medina
type: exercise
topic: infrastructure
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/exercises/1-server-foundation/1-portal-interface/2-provisioning-vm-ssh-keys.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-"
tags: ["azure", "cloud", "docker", "exercise", "exploring", "git", "iac", "infrastructure", "keys", "linux"]
week_fit: []
---

Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-
Server Foundation--
1. Portal Interface--- 1. Provisioning a VM via Azure Portal--- 2. Provisioning a VM with SSH Keys and Exploring Linux--- 3. Automating Nginx Installation with Custom Data Scripts--
2. Command Line Interface--
3. ARM and Bicep-
Network Foundation-
Deployment-
Cloud Databases-
Webapp Development-
Code Collaboration-
DockerTutorials

# 2. Provisioning a VM with SSH Keys and Exploring Linux

🟡


## Goal

Provision an Ubuntu virtual machine with secure SSH key authentication and explore fundamental Linux filesystem navigation and bash scripting.

> **What you’ll learn:**
>
> * How to create and use SSH key pairs for secure VM authentication
> * How to connect to remote servers using SSH from your local terminal
> * How to navigate the Linux filesystem and understand its hierarchical structure
> * How to create and execute bash scripts to automate server configuration

## Prerequisites

> **Before starting, ensure you have:**
>
> * ✓ An active Azure account with an available subscription
> * ✓ A terminal application (Git Bash on Windows, Terminal on Mac/Linux)
> * ✓ Completed Exercise 1 or familiarity with Azure Portal navigation

## Exercise Steps

### Overview

1. **Log in to Azure Portal**
2. **Create a Virtual Machine with SSH Key Pair**
3. **Secure the SSH Private Key**
4. **Connect to the VM**
5. **Explore the Linux Filesystem**
6. **Create and Execute a Bash Script to Install Nginx**
7. **Clean Up Resources**

### **Step 1:** Log in to Azure Portal

Before creating any resources, you need to access the Azure management interface. The Azure Portal provides a web-based dashboard for managing all your cloud resources.

1. **Open** a web browser and navigate to the [Azure Portal](https://portal.azure.com/).
2. **Sign in** using your Azure account credentials.

### **Step 2:** Create a Virtual Machine with SSH Key Pair

This step creates your Ubuntu server with secure SSH key authentication. Unlike password authentication, SSH keys provide cryptographic security that is virtually impossible to brute-force attack.

1. **Navigate to** **Virtual Machines** and click **Create** > **Azure Virtual machine**.
2. **Configure** the VM:
   * **Subscription**: Select your subscription.
   * **Create a new Resource Group**: `LabSSHResourceGroup`. (You can create a new resource group here if you don´t have one already)
   * **Virtual Machine Name**: `LabSSHVM`.
   * **Region**: Same as the resource group.
   * **Zone options**: Select *Azure-selected zone*
   * **Image**: Select `Ubuntu Server 24.04 LTS`.
   * **Size**: Choose `Standard_B1s`.
   * **Authentication Type**: Select **SSH Public Key**.
   * **Username**: `azureuser`.
3. **Select** **Generate new key pair** under **SSH Public Key Source**.
4. **Configure** inbound ports:
   * Check **Allow selected ports** and select **HTTP (80)** and **SSH (22)**.
5. **Note** the name for your key pair (e.g., `LabSSHKey`) and click **Download private key and create resource**.

> ℹ **Concept Deep Dive**
>
> The **Generate new key pair** option allows you to securely create an SSH key pair. The private key will be downloaded to your computer and should be stored securely. Azure automatically applies the corresponding public key to the VM.

### **Step 3:** Secure the SSH Private Key (Only Mac and Linux Users)

SSH requires that private key files have restricted permissions. This security measure prevents other users on your system from reading your private key, which would compromise your server’s security.

1. **Locate** the downloaded private key file (e.g., `LabSSHKey.pem`) on your local machine.
2. **Open** the Terminal and change file permissions:

   ```
   chmod 400 ~/Downloads/LabSSHKey.pem
   ```

> ℹ **Concept Deep Dive**
>
> The `chmod 400` command restricts permissions on the private key file, ensuring it is readable only by the file’s owner. This is required for secure SSH connections. Failure to set these permissions will result in SSH errors.
>
> Make sure the key is in the Downloads folder. Otherwise you need to adjust the command above accordingly.

### **Step 4:** Connect to the VM

Now you will establish a secure shell connection to your virtual machine. SSH encrypts all traffic between your local computer and the remote server, protecting your commands and data from interception.

1. **Navigate to** your VM’s **Overview** tab in the Azure Portal and copy the public IP address of `LabSSHVM`.
2. **Open** your terminal:

   * **Windows**: Use **Git Bash**.
   * **Mac/Linux**: Use your default terminal.
3. **Connect** to the VM using SSH:

   ```
   ssh -i ~/Downloads/LabSSHKey.pem azureuser@<VM_Public_IP>
   ```

   * Replace `<VM_Public_IP>` with the public IP address you copied.
   * On Windows you might need to use the absolute path for the key. You can usually do that by drag-and-drop the key `LabSSHKey.pem` from the file explorer into the Terminal

> ✓ **Quick check:** Ensure you are logged into the VM and see the `azureuser@LabSSHVM` prompt.

### **Step 5:** Explore the Linux Filesystem

Understanding the Linux filesystem structure is essential for server administration. The filesystem follows a hierarchical tree structure starting from the root directory, with each directory serving a specific purpose.

1. **List** the contents of the root directory:

   ```
   ls /
   ```

   * Key directories:
     + `/home`: User home directories.
     + `/etc`: Configuration files.
     + `/var`: Logs and variable data.
2. **Check** your current working directory:

   ```
   pwd
   ```

> ℹ **Concept Deep Dive**
>
> The Linux filesystem is hierarchical, starting from `/` (root).

### **Step 6:** Create and Execute a Bash Script to Install Nginx

Bash scripts automate repetitive tasks and ensure consistent server configuration. By writing installation steps as a script, you can reproduce the same setup across multiple servers and document exactly what was done.

1. **Create** the Bash Script:

   * While logged into the VM, create a new script file:

     ```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
     nano install_nginx.sh
     ```
   * **Add** the following content to the file:

     ```
     #!/bin/bash
     apt update
     apt install nginx -y
     ```
   * **Save** and exit by pressing `CTRL+X`, `y`, `Enter`.
2. **Make** the Script Executable:

   ```
   chmod +x install_nginx.sh
   ```
3. **Run** the Script with Elevated Privileges:

   ```
   sudo ./install_nginx.sh
   ```
4. **Verify** the Installation:

   * Check if Nginx is running:

     ```
     systemctl status nginx
     ```

     Ensure the status shows `active (running)`.
   * Test Nginx using `curl`:

     ```
     curl localhost
     ```

     The output should include the default Nginx HTML content, such as `Welcome to nginx!`.

> ℹ **Concept Deep Dive**
>
> **Systemctl and the Init System**
>
> The `systemctl` command is used to interact with the system’s **init system** (often `systemd` on modern Linux distributions). It is responsible for managing services, such as starting, stopping, enabling, or checking the status of services like Nginx.
>
> * `start`: Starts the service immediately.
> * `enable`: Configures the service to start automatically at boot.
> * `status`: Displays the current status of the service.

### **Step 7:** Clean Up Resources (Optional)

To avoid unnecessary charges, you should delete resources when they are no longer needed. Deleting the entire resource group removes all associated resources in one operation.

1. **Navigate to** **Resource Groups** in the Azure Portal and locate `LabSSHResourceGroup`.
2. **Click** **Delete resource group**.

## Common Issues

> **If you encounter problems:**
>
> **Permission denied (publickey):** Ensure you ran `chmod 400` on your private key file and that you’re using the correct path to the key.
>
> **Connection timed out:** Verify that SSH (port 22) is allowed in your VM’s network security group inbound rules.
>
> **Key file path not found:** On Windows, use the absolute path by dragging the key file into Git Bash, or navigate to the Downloads folder first.

## Summary

You’ve successfully provisioned an Ubuntu VM with SSH key authentication and explored Linux fundamentals which:

* ✓ Created a secure SSH key pair using Azure’s built-in key generation
* ✓ Established encrypted SSH connections from your local terminal
* ✓ Navigated the Linux filesystem structure
* ✓ Automated Nginx installation using a bash script

> **Key takeaway:** SSH keys provide cryptographic authentication that is far more secure than passwords, and bash scripts enable repeatable, documented server configuration.

## Going Deeper (Optional)

> **Want to explore more?**
>
> * Move your private key to `~/.ssh/` and create an SSH config file for easier connections
> * Explore other Linux directories like `/var/log` for system logs
> * Modify the bash script to also enable Nginx to start on boot with `systemctl enable nginx`
> * Try connecting from a different device using the same key pair

## Done 🎉

You have successfully provisioned a VM using Azure’s Generate Key Pair feature, connected securely using SSH, and explored the Linux filesystem and basic commands.

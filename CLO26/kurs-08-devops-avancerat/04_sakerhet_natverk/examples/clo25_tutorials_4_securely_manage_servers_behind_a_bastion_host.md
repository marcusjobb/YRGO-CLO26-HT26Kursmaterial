---

title: Securely Manage Servers Behind a Bastion Host
author: Marcus Ackre Medina
type: tutorial
topic: infrastructure
difficulty: 2
language: swedish
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/tutorials/4-securely-manage-servers-behind-a-bastion-host.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercisesTutorials- How To Write A Tutorial- Configure Azure VM DNS Labels- Securely Manage Servers Behind a Bastion Hos"
tags: ["azure", "bastion", "behind", "cloud", "git", "host", "infrastructure", "linux", "manage", "networking"]
week_fit: []
---

Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercisesTutorials- How To Write A Tutorial- Configure Azure VM DNS Labels- Securely Manage Servers Behind a Bastion Host

# Securely Manage Servers Behind a Bastion Host

🟡


## Introduction

This tutorial is aimed at IT professionals and system administrators who wish to securely manage their servers on Azure, especially when these servers are not directly exposed to the internet and are located behind a bastion host. By following this guide, får du lära dig how to create a secure environment, ensuring that your web servers and other critical infrastructure are safely accessible for management and deployment tasks.

## Prerequisites

* An Azure account. If you don’t have one, sign up [here](https://azure.microsoft.com/).
* Basic knowledge of cloud computing and familiarity with command-line interfaces (CLI).
* For Windows users: GitBash or similar terminal (Mac and Linux users can use the pre-installed terminal)

## Step-by-Step Guide

### 1. Provisioning Infrastructure

* **Log into Azure Portal**:

  + Navigate to [Azure Portal](https://portal.azure.com/) and sign in.
* **Create a Resource Group `NetworkDemoRG`**:

  + Find “Resource groups” and select “Create”.
  + Enter the name `NetworkDemoRG`, choose your subscription, and select a region close to you.
  + Click “Review + create”, then “Create”.
* **Set Up Virtual Network `DemoNetwork`**:

  + Search for “Virtual networks” and choose “Create”.
  + Select your new resource group, name the network `DemoNetwork`, specify address space, and create a new subnet.
* **Deploy Two VMs**: `BastionHost` and `WebServer`.

  + Go to “Virtual machines”, click “Create” -> “Azure virtual machine”.
  + Repeat the process for both VMs, ensuring they’re attached to `DemoNetwork`.
  + For “Image”, select Ubuntu Server 22.04 LTS. Choose a VM size.
  + Set “Authentication type” to “SSH public key”, configure the inbound port rules to allow SSH (port 22), and link to your SSH key.
  + Complete the creation process for both VMs.

### 2. Establishing Secure Access

* **Secure SSH Keys**:

  ```
  chmod 400 WebServer_key.pem
  chmod 400 BastionHost_key.pem
  ```
* **Initial Login Test**:

  Test SSH access to both VMs using their public IPs:

  ```
  ssh -i WebServer_key.pem azureuser@<WebServer_IP>
  ssh -i BastionHost_key.pem azureuser@<BastionHost_IP>
  ```
* **Disable Direct SSH Access to WebServer**:

  Remove the SSH Network Security Group (NSG) rule on the WebServer to simulate a secure environment where direct access is not permitted.

### 3. Using SSH Agent for Secure Access

* **Start SSH Agent**:

  ```
  eval $(ssh-agent)
  ssh-add ~/Downloads/WebServer_key.pem
  ssh-add ~/Downloads/BastionHost_key.pem
  ssh-add -l
  ```
* **SSH Jump via BastionHost**:

  ```
  ssh -A azureuser@<BastionHost_IP>
  ```

  Inside BastionHost, attempt to SSH into WebServer using its internal IP:

  ```
  ssh azureuser@<WebServer_InternalIP>
  ```

#### SSH Agent Commands

> **`eval $(ssh-agent)`**:
>
> * This command starts the SSH agent in the background. The `ssh-agent` is a program that holds private keys used for public key authentication (SSH). The `eval $(ssh-agent)` command initializes the environment variables that allow the SSH client to locate the `ssh-agent`, so you don’t have to enter your passphrase every time you use your SSH keys.
>
> **`ssh-add -l`**:
>
> * The `ssh-add -l` command lists all the private keys that are currently loaded into the SSH agent. Det här är praktiskt for verifying which keys are already available to the SSH client for authentication.
>
> **`ssh-add ~/Downloads/WebServer_key.pem`**:
>
> * This command adds the private key `WebServer_key.pem` from the Downloads directory to the SSH agent. By adding a private key, you won’t have to specify the key file explicitly when connecting to the server that recognizes the corresponding public key. This step is necessary for authenticating to the web server without entering the passphrase for the key (if one is set) every time.
>
> **`ssh -A azureuser@<BastionHost_IP>`**:
>
> * This command establishes an SSH connection to the bastion host with the IP address `<BastionHost_IP>`, logging in as the user `azureuser`. The `-A` option enables forwarding of the SSH agent connection. Det betyder att the SSH agent running on your local machine (which now holds your added private keys) will be accessible during the SSH session to the bastion host. Agent forwarding allows you to use your local SSH keys for further SSH connections initiated from the bastion host, such as when you connect from the bastion host to the web server, without having to store your private keys on the bastion host itself.

### 4. Establishing an SSH Tunnel for File Transfer

* **Setup SSH Tunnel**:

  On a new terminal, establish an SSH tunnel to the WebServer via BastionHost:

  ```
  ssh -A -N -L 2222:<WevServer_privateIP>:22 azureuser@<BastionHost_IP>
  ```

  > The terminal will now “get stuck”. So in order to continue use a second terminal. Add private keys to the SSH agent as before.
* **File Transfer Through Tunnel**:

  Create and copy a file, through the Bastion Host, using the tunnel:

  ```
  touch myfile.txt
  scp -P 2222 myfile.txt azureuser@localhost:~/
  ```
* **Verify File Transfer**:

  ```
  ssh -A azureuser@<BastionHost_IP>
  ```

  Inside the BastionHost

  ```
  ssh azureuser@<WebServer_InternalIP>
  ```

  Inside the WebServer

  ```
  ls
  ```

#### SSH Tunnel Command

> **`-A`**: Enables forwarding of the SSH authentication agent connection. If you have an SSH agent running on your client machine and you use SSH keys to authenticate, this option allows the remote machine to use the same keys to authenticate further SSH connections (e.g., to another server). It’s useful when you need to hop from one server to another without transferring your private keys to any of the servers.
>
> **`-N`**: Tells SSH that no remote commands will be executed, and is used when forwarding ports. This option is useful when you’re only interested in forwarding ports (creating a tunnel) and not in executing commands on the remote system.
>
> **`-L 2222:10.0.0.5:22`**: Specifies local port forwarding. This option is followed by three parameters separated by colons (`:`):
>
> * `2222`: The local port on the client machine. This is the port you will connect to locally, which gets forwarded to the destination.
> * `10.0.0.5`: The destination address, which is the IP address of the target machine you want to connect to from the remote server. This target machine is typically on the same network as the SSH server or otherwise reachable by it.
> * `22`: The port on the destination machine. Port 22 is the default port for SSH connections.
>
> **`azureuser@40.113.57.172`**: Specifies the user and the IP address of the SSH server (the remote machine you’re directly connecting to). In this case, `azureuser` is the username, and `40.113.57.172` is the IP address of the SSH server.

#### SCP Command

> * **`scp`**: Stands for Secure Copy Protocol, a network protocol that supports file transfers between hosts on a network. `scp` uses SSH for data transfer, ensuring that both authentication and the data itself are encrypted and secure.
> * **`-P 2222`**: Specifies the port on the local machine to connect to. The uppercase `-P` flag is used with `scp` to indicate the port. In this context, `2222` is the local port that’s been forwarded (via SSH tunneling) to the remote host’s SSH port (typically port 22). This means you’re instructing `scp` to send the file through the SSH tunnel established on port 2222.
> * **`myfile.txt`**: The name of the file you want to copy. This should be replaced with the actual path and filename of the file you’re transferring. If the file is not in the current directory from which you’re running the command, you’ll need to specify the full path to the file.
> * **`azureuser@localhost`**: The destination for the file transfer. `azureuser` is the username on the remote machine to which you’re transferring the file. `localhost` refers to your local machine, but in the context of this command, it’s used because the SSH tunnel redirects the connection to the remote machine. Essentially, you’re connecting to the local end of the SSH tunnel, which then forwards the connection to the remote machine.
> * **`:~/`**: Specifies the directory on the remote machine where the file should be copied. `~/` is a shorthand for the user’s home directory. Thus, `:~/` means the file will be placed in the home directory of `azureuser` on the remote machine.

## Final Thoughts

By following this tutorial, you’ve learned how to securely manage servers behind a bastion host on Azure. This setup enhances your security solution by limiting direct access to your infrastructure, utilizing SSH keys for authentication, and leveraging SSH tunnels for secure data transfer.

## Don’t Forget

* Always monitor Azure service costs and manage resources effectively to avoid unexpected charges.
* Consider deleting resources you no longer need to save costs.

# Happy Secure Managing! 🚀

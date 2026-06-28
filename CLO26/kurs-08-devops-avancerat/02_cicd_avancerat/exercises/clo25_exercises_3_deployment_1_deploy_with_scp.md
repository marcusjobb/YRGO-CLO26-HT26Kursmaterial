---

title: 1. Deploy HTML with SCP
author: Marcus Ackre Medina
type: exercise
topic: deployment
difficulty: 3
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/exercises/3-deployment/1-deploy-html-with-scp.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-"
tags: ["azure", "cloud", "deploy", "deployment", "devops", "docker", "dotnet", "exercise", "git", "html"]
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

# 1. Deploy HTML with SCP

🔴


## Goal

Deploy a simple HTML file to an nginx web server on an Azure Ubuntu VM using SCP (Secure Copy Protocol), establishing the foundation for manual web application deployment.

> **What you’ll learn:**
>
> * How to create and transfer files to remote servers using SCP
> * When to use manual deployment vs automated pipelines
> * Best practices for deploying to nginx web servers

## Prerequisites

> **Before starting, ensure you have:**
>
> * ✓ An Azure Ubuntu VM with a public IP address
> * ✓ SSH key pair configured (`~/.ssh/id_rsa` and `~/.ssh/id_rsa.pub`)
> * ✓ nginx installed and running on the VM
> * ✓ Terminal access on your local machine (macOS, Linux, or WSL)

## Exercise Steps

### Overview

1. **Create a Simple HTML File**
2. **Transfer the File with SCP**
3. **Move the File to nginx Document Root**
4. **Verify the Deployment**

### **Step 1:** Create a Simple HTML File

Create a basic HTML file locally that will serve as your web page. This file will demonstrate successful deployment when you view it in a browser later. Starting with a simple file helps isolate deployment issues from application complexity.

1. **Open** your terminal
2. **Navigate to** a convenient working directory
3. **Create** a new file named `index.html`:

   ```
   nano index.html
   ```
4. **Add** the following HTML content:

   > `index.html`

   ```
   <!DOCTYPE html>
   <html lang="en">
   <head>
       <meta charset="UTF-8">
       <meta name="viewport" content="width=device-width, initial-scale=1.0">
       <title>My First Deployment</title>
       <style>
           body {
               font-family: Arial, sans-serif;
               display: flex;
               justify-content: center;
               align-items: center;
               min-height: 100vh;
               margin: 0;
               background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
               color: white;
           }
           .container {
               text-align: center;
               padding: 2rem;
           }
           h1 {
               font-size: 3rem;
               margin-bottom: 1rem;
           }
           p {
               font-size: 1.2rem;
               opacity: 0.9;
           }
       </style>
   </head>
   <body>
       <div class="container">
           <h1>Hello from Azure!</h1>
           <p>Successfully deployed with SCP</p>
       </div>
   </body>
   </html>
   ```
5. **Save and exit** the editor (in nano: `Ctrl+O`, `Enter`, `Ctrl+X`)

> ℹ **Concept Deep Dive**
>
> SCP (Secure Copy Protocol) uses SSH for data transfer, providing encrypted file transfers between your local machine and remote servers. Unlike FTP, SCP doesn’t require additional server software beyond SSH, making it a reliable choice for secure file deployment.
>
> ⚠ **Common Mistakes**
>
> * Creating the file with Windows line endings can cause issues on Linux servers
> * Forgetting to save the file before attempting to transfer it
> * Using special characters in filenames that may not transfer correctly
>
> ✓ **Quick check:** Run `cat index.html` to verify the file was created with correct content

### **Step 2:** Transfer the File with SCP

Use SCP to securely copy your HTML file to the Azure VM. We’ll initially transfer to the `/tmp` directory because it’s universally writable, avoiding permission issues during the initial transfer.

1. **Identify** your VM’s public IP address (from Azure Portal or your notes)
2. **Execute** the SCP command to transfer the file:

   ```
   scp index.html azureuser@<YOUR_VM_IP>:/tmp/
   ```
3. **Replace** `<YOUR_VM_IP>` with your actual VM’s public IP address
4. **Confirm** the SSH key fingerprint if prompted (first connection only)

> ℹ **Concept Deep Dive**
>
> The SCP command syntax is `scp <source> <user>@<host>:<destination>`. We transfer to `/tmp` first because this directory allows all users to write files, whereas the nginx document root (`/var/www/html`) requires elevated permissions. This two-step approach is a common deployment pattern.
>
> ⚠ **Common Mistakes**
>
> * Using the wrong username (Azure default is `azureuser`, not `ubuntu` or `root`)
> * Forgetting the colon (`:`) before the destination path causes the command to fail silently
> * SSH key permissions being too open (`chmod 400 ~/.ssh/id_rsa` to fix)
> * Firewall blocking SSH (port 22) on the VM
>
> ✓ **Quick check:** Successful transfer shows the filename and 100% progress indicator

### **Step 3:** Move the File to nginx Document Root

Execute remote commands over SSH to move the file from `/tmp` to the nginx document root. This approach lets you run commands on the VM without starting an interactive session, which is useful for scripting deployments.

1. **Move** the file to the nginx document root:

   ```
   ssh azureuser@<YOUR_VM_IP> 'sudo mv /tmp/index.html /var/www/html/'
   ```
2. **Set** correct file ownership:

   ```
   ssh azureuser@<YOUR_VM_IP> 'sudo chown www-data:www-data /var/www/html/index.html'
   ```
3. **Verify** the file is in place:

   ```
   ssh azureuser@<YOUR_VM_IP> 'ls -la /var/www/html/'
   ```

> ℹ **Concept Deep Dive**
>
> Running commands over SSH with `ssh user@host 'command'` executes the command remotely without opening an interactive shell. This is the foundation of automated deployment scripts. The `/var/www/html` directory is the default document root for nginx on Ubuntu. The `www-data` user is the service account that nginx runs under, so correct ownership ensures nginx can read the files.
>
> ⚠ **Common Mistakes**
>
> * Forgetting quotes around the remote command causes local shell interpretation issues
> * Missing `sudo` results in “Permission denied” errors
> * Moving to wrong directory (e.g., `/var/www/` instead of `/var/www/html/`)
> * Not checking if an existing `index.html` will be overwritten
>
> ✓ **Quick check:** The `ls` command output shows `index.html` with `www-data` ownership

### **Step 4:** Verify the Deployment

Test that your deployment was successful by accessing the page through a web browser. This confirms both the file transfer and nginx configuration are working correctly.

1. **Open** a web browser on your local machine
2. **Navigate to:** `http://<YOUR_VM_IP>/`
3. **Verify** you see:

   * The purple gradient background
   * “Hello from Azure!” heading
   * “Successfully deployed with SCP” message
4. **Check** nginx status if the page doesn’t load:

   ```
   ssh azureuser@<YOUR_VM_IP> 'sudo systemctl status nginx'
   ```
5. **Review** nginx error logs if needed:

   ```
   ssh azureuser@<YOUR_VM_IP> 'sudo tail /var/log/nginx/error.log'
   ```

> ✓ **Success indicators:**
>
> * Page loads without errors in the browser
> * Custom styling (gradient background) displays correctly
> * No nginx error messages in logs
> * Browser shows HTTP (not HTTPS) connection to your VM IP
>
> ✓ **Final verification checklist:**
>
> * ☐ HTML file created locally with correct content
> * ☐ File transferred successfully via SCP to /tmp
> * ☐ File moved to /var/www/html with correct permissions
> * ☐ Web page accessible via browser at VM’s public IP
> * ☐ nginx serving the custom page (not default welcome page)

## Common Issues

> **If you encounter problems:**
>
> **“Connection refused” or “Connection timed out”:** Verify the VM’s Network Security Group allows inbound traffic on port 22 (SSH) and port 80 (HTTP)
>
> **“Permission denied (publickey)”:** Check your SSH key is in `~/.ssh/id_rsa` and has correct permissions (`chmod 600 ~/.ssh/id_rsa`)
>
> **Page shows nginx default page instead of your content:** Your file may not have been placed in `/var/www/html/` or nginx may need a restart with `sudo systemctl restart nginx`
>
> **“403 Forbidden” error:** File permissions issue. Run `sudo chmod 644 /var/www/html/index.html` to fix
>
> **Still stuck?** Verify nginx is running with `sudo systemctl status nginx` and check `/var/log/nginx/error.log` for details

## Summary

You’ve successfully deployed a web page to an Azure VM using SCP which:

* ✓ Establishes a manual deployment workflow for web content
* ✓ Demonstrates secure file transfer using SSH infrastructure
* ✓ Prepares you for understanding automated deployment pipelines

> **Key takeaway:** SCP provides a simple, secure way to deploy files to remote servers using existing SSH infrastructure. While manual SCP deployment works for small projects and learning, production environments typically use CI/CD pipelines that automate this process. Understanding manual deployment helps you troubleshoot automated systems.

## Going Deeper (Optional)

> **Want to explore more?**
>
> * Try deploying multiple files using `scp -r` for recursive directory transfer
> * Research rsync as an alternative that only transfers changed files
> * Set up a simple deployment script that combines these commands
> * Configure nginx to serve from a different directory

## Done! 🎉

Well done! You’ve learned how to deploy web content to a cloud server using SCP. This fundamental skill forms the basis for understanding more advanced deployment strategies like CI/CD pipelines and container orchestration.

## TL;DR

Complete deployment script (assumes `index.html` exists in current directory):

```
# Usage: ./deploy.sh <VM_IP>
VM_IP=$1

scp index.html azureuser@$VM_IP:/tmp/

ssh azureuser@$VM_IP '\
    sudo mv /tmp/index.html /var/www/html/ && \
    sudo chown www-data:www-data /var/www/html/index.html'

echo "Visit http://$VM_IP/ in your browser"
```

Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-
Server Foundation-
Network Foundation-
Deployment-- 1. Deploy HTML with SCP-- 2. Deploy .NET MVC App with Systemd-- 3. Use Github Actions to Deploy your App-- 4. Implementing Azure Key Vault with MongoDB on Azure VM-- 5. Monitoring Azure VMs with Azure Monitor-
Cloud Databases-
Webapp Development-
Code Collaboration-
DockerTutorials

# 2. Deploy .NET MVC App with Systemd

🟡


## Goal

Deploy a .NET 10 MVC web application to an Azure Ubuntu 24.04 VM using SCP and configure it as a systemd service for production-ready process management.

> **What you’ll learn:**
>
> * How to provision Azure VMs using the Azure CLI
> * When to use dedicated service users for application security
> * Best practices for deploying .NET applications with systemd

## Prerequisites

> **Before starting, ensure you have:**
>
> * ✓ Azure CLI installed and authenticated (`az login`)
> * ✓ SSH key pair configured (`~/.ssh/id_rsa` and `~/.ssh/id_rsa.pub`)
> * ✓ .NET 10 SDK installed locally
> * ✓ Terminal access on your local machine (macOS, Linux, or WSL)

## Exercise Steps

### Overview

1. **Provision Azure VM with Azure CLI**
2. **Create the .NET MVC Application**
3. **Install .NET Runtime on VM**
4. **Create Application Service User**
5. **Deploy Application with SCP**
6. **Configure systemd Service**
7. **Verify the Deployment**

### **Step 1:** Provision Azure VM with Azure CLI

Create an Ubuntu 24.04 VM on Azure using the Azure CLI. This approach is repeatable and scriptable, unlike manual portal deployments. Starting the VM first allows it to boot while you prepare your application locally.

1. **Open** your terminal
2. **Set** the variables for your deployment:

   ```
   resource_group="DotNetDeployGroup"
   vm_name="DotNetAppVM"
   location="northeurope"
   ```
3. **Create** a resource group to contain all related resources:

   ```
   az group create --name $resource_group --location $location
   ```
4. **Create** the Ubuntu 24.04 VM:

   ```
   az vm create \
       --resource-group $resource_group \
       --name $vm_name \
       --image Ubuntu2404 \
       --size Standard_B1s \
       --admin-username azureuser \
       --generate-ssh-keys
   ```
5. **Open** port 5000 for the Kestrel web server:

   ```
   az vm open-port --resource-group $resource_group --name $vm_name --port 5000
   ```
6. **Retrieve** the VM’s public IP address and store it for later use:

   ```
   vm_ip=$(az vm show -g $resource_group -n $vm_name --show-details --query publicIps -o tsv)
   echo "VM IP: $vm_ip"
   ```

> ℹ **Concept Deep Dive**
>
> The Azure CLI `az vm create` command provisions compute, storage, and networking resources together. The `--generate-ssh-keys` flag uses your existing SSH keys or creates new ones if needed. Using environment variables for resource names makes scripts reusable and reduces errors from typos.
>
> ⚠ **Common Mistakes**
>
> * Forgetting to open port 5000 blocks access to Kestrel
> * Using a region far from your location increases latency during development
> * Not saving the VM IP address means you’ll need to query it again later
>
> ✓ **Quick check:** Run `ssh azureuser@$vm_ip 'hostname'` to verify SSH connectivity

### **Step 2:** Create the .NET MVC Application

Build a simple .NET MVC application locally that will serve as your deployable artifact. Publishing in Release configuration creates optimized binaries suitable for production deployment.

1. **Create** a new MVC project:

   ```
   dotnet new mvc -n HelloDotnet
   ```
2. **Navigate** into the project directory:

   ```
   cd HelloDotnet
   ```
3. **Publish** the application in Release configuration:

   ```
   dotnet publish -c Release -o ./publish
   ```
4. **Verify** the published output:

   ```
   ls -la ./publish/
   ```

> ℹ **Concept Deep Dive**
>
> The `dotnet publish` command compiles your application and copies all dependencies to a single folder. The `-c Release` flag enables compiler optimizations and strips debug symbols, resulting in faster execution and smaller binaries. The output folder contains everything needed to run the application on any machine with the .NET runtime installed.
>
> ⚠ **Common Mistakes**
>
> * Publishing in Debug configuration includes debug symbols and disables optimizations
> * Forgetting to publish (just running `dotnet build`) doesn’t include all required files
> * Publishing to a nested path makes SCP commands more complex
>
> ✓ **Quick check:** The `publish` folder should contain `HelloDotnet.dll` and several other DLL files

### **Step 3:** Install .NET Runtime on VM

Install the ASP.NET Core runtime on your Azure VM. The runtime is sufficient for running published applications; the full SDK is only needed for development and building.

1. **Connect** to your VM via SSH:

   ```
   ssh azureuser@$vm_ip
   ```
2. **Download** the Microsoft package repository configuration:

   ```
   wget https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
   ```
3. **Install** the package repository and clean up:

   ```
   sudo dpkg -i packages-microsoft-prod.deb && rm packages-microsoft-prod.deb
   ```
4. **Update** package lists and install the ASP.NET Core runtime:

   ```
   sudo apt update && sudo apt install -y aspnetcore-runtime-10.0
   ```
5. **Verify** the installation:

   ```
   dotnet --list-runtimes
   ```

> ℹ **Concept Deep Dive**
>
> Microsoft provides official .NET packages through their APT repository. The `aspnetcore-runtime-10.0` package includes everything needed to run ASP.NET Core web applications. Installing only the runtime (not the SDK) follows the principle of minimal installation — servers should only have what they need to run applications, reducing attack surface and disk usage.
>
> ⚠ **Common Mistakes**
>
> * Installing `dotnet-sdk-10.0` instead of `aspnetcore-runtime-10.0` wastes disk space
> * Forgetting `sudo apt update` after adding the repository causes “package not found” errors
> * Missing the `aspnetcore` prefix installs only the base runtime without web server components
>
> ✓ **Quick check:** The `dotnet --list-runtimes` output should include `Microsoft.AspNetCore.App 10.0.x`

### **Step 4:** Create Application Service User

Create a dedicated system user to run your application. Running applications as non-privileged users limits the damage potential if the application is compromised — a fundamental security practice.

Continue in the same SSH session from Step 3:

1. **Create** a system user without login capabilities:

   ```
   sudo useradd --system --shell /usr/sbin/nologin --no-create-home dotnet-app
   ```
2. **Create** the application directory:

   ```
   sudo mkdir -p /opt/dotnet-app
   ```
3. **Set** ownership so azureuser can deploy and dotnet-app can read:

   ```
   sudo chown azureuser:dotnet-app /opt/dotnet-app && sudo chmod 750 /opt/dotnet-app
   ```
4. **Verify** the directory was created correctly:

   ```
   ls -la /opt/
   ```
5. **Exit** the SSH session to return to your local machine:

   ```
   exit
   ```

> ℹ **Concept Deep Dive**
>
> System users (`--system`) are designed for running services, not for human login. The `/usr/sbin/nologin` shell prevents interactive login even if someone tries. Using `/opt` for applications follows the Filesystem Hierarchy Standard — `/opt` is for optional software packages not managed by the system package manager. The `750` permission (rwxr-x—) allows the owner full access and group members to read and execute.
>
> ⚠ **Common Mistakes**
>
> * Creating a regular user instead of a system user adds unnecessary entries to `/etc/passwd`
> * Using `/home` for applications mixes user data with application files
> * Setting `777` permissions defeats the purpose of having a dedicated user
>
> ✓ **Quick check:** The `ls -la /opt/` output should show `dotnet-app` directory owned by `azureuser:dotnet-app`

### **Step 5:** Deploy Application with SCP

Transfer your published application to the VM. We use a two-step process: first copy to `/tmp` (universally writable), then move to `/opt` with correct permissions. This avoids permission issues during transfer.

1. **Create** a temporary directory on the VM:

   ```
   ssh azureuser@$vm_ip 'mkdir -p /tmp/dotnet-app'
   ```
2. **Transfer** the published files to the temporary directory:

   ```
   scp -r ./publish/* azureuser@$vm_ip:/tmp/dotnet-app/
   ```
3. **Move** the files to the application directory:

   ```
   ssh azureuser@$vm_ip 'mv /tmp/dotnet-app/* /opt/dotnet-app/'
   ```
4. **Set** correct ownership and permissions for the deployed files:

   ```
   ssh azureuser@$vm_ip 'sudo chown -R dotnet-app:dotnet-app /opt/dotnet-app && sudo chmod -R 750 /opt/dotnet-app'
   ```
5. **Clean up** the temporary directory:

   ```
   ssh azureuser@$vm_ip 'rmdir /tmp/dotnet-app'
   ```

> ℹ **Concept Deep Dive**
>
> The `-r` flag in SCP recursively copies directories and their contents. Transferring to `/tmp` first, then moving with proper permissions, is a common deployment pattern that avoids direct permission complexity. After deployment, the files are owned by the service user, meaning only that user can modify them — even if the web application is compromised, attackers can’t easily replace the binaries.
>
> ⚠ **Common Mistakes**
>
> * Forgetting `-r` when copying directories causes “not a regular file” errors
> * Leaving files in `/tmp` may result in automatic cleanup by the system
> * Not changing ownership means systemd can’t run the application as the service user
>
> ✓ **Quick check:** Run `ssh azureuser@$vm_ip 'ls -la /opt/dotnet-app/'` to verify files are present with correct ownership

### **Step 6:** Configure systemd Service

Create a systemd service unit file to manage your application as a system service. Systemd handles starting, stopping, restarting, and monitoring your application automatically.

1. **Connect** to your VM via SSH:

   ```
   ssh azureuser@$vm_ip
   ```
2. **Create** the service unit file using nano:

   ```
   sudo nano /etc/systemd/system/dotnet-app.service
   ```
3. **Add** the following content to the file:

   > `/etc/systemd/system/dotnet-app.service`

   ```
   [Unit]
   Description=.NET MVC Application
   After=network.target

   [Service]
   Type=simple
   User=dotnet-app
   Group=dotnet-app
   WorkingDirectory=/opt/dotnet-app
   ExecStart=/usr/bin/dotnet /opt/dotnet-app/HelloDotnet.dll
   Restart=always
   RestartSec=5
   Environment=ASPNETCORE_URLS=http://0.0.0.0:5000
   Environment=ASPNETCORE_ENVIRONMENT=Production

   [Install]
   WantedBy=multi-user.target
   ```
4. **Save and exit** the editor (in nano: `Ctrl+O`, `Enter`, `Ctrl+X`)
5. **Reload** systemd to recognize the new service:

   ```
   sudo systemctl daemon-reload
   ```
6. **Enable** the service to start on boot:

   ```
   sudo systemctl enable dotnet-app.service
   ```
7. **Start** the service:

   ```
   sudo systemctl start dotnet-app.service
   ```
8. **Check** the service status:

   ```
   sudo systemctl status dotnet-app.service
   ```
9. **Exit** the SSH session:

   ```
   exit
   ```

> ℹ **Concept Deep Dive**
>
> Systemd is the standard service manager on modern Linux distributions. The `[Unit]` section defines dependencies (`After=network.target` ensures network is available). The `[Service]` section specifies how to run the application. `Restart=always` with `RestartSec=5` means systemd will restart the application if it crashes, waiting 5 seconds between attempts. The `ASPNETCORE_URLS` environment variable tells Kestrel to listen on all network interfaces.
>
> ⚠ **Common Mistakes**
>
> * Adding inline comments in systemd unit files causes parsing errors
> * Forgetting `daemon-reload` after creating the file means systemd doesn’t see it
> * Using `localhost` instead of `0.0.0.0` makes the app inaccessible from outside the VM
> * Typos in the `ExecStart` path cause immediate service failure
>
> ✓ **Quick check:** The status output should show “active (running)” in green

### **Step 7:** Verify the Deployment

Test that your application is running correctly and survives restarts. A thorough verification ensures your deployment is production-ready.

1. **Open** a web browser on your local machine
2. **Navigate to:** `http://<YOUR_VM_IP>:5000/`
3. **Verify** you see the default ASP.NET MVC welcome page with the “Welcome” heading
4. **Check** application logs if the page doesn’t load:

   ```
   ssh azureuser@$vm_ip 'sudo journalctl -u dotnet-app.service -n 50'
   ```
5. **Test** automatic restart by restarting the service:

   ```
   ssh azureuser@$vm_ip 'sudo systemctl restart dotnet-app.service'
   ```
6. **Verify** the page still loads after restart
7. **Test** persistence across VM reboot (optional but recommended):

   ```
   ssh azureuser@$vm_ip 'sudo reboot'
   ```

   Wait a minute, then verify the page loads again.

> ✓ **Success indicators:**
>
> * ASP.NET MVC welcome page loads in browser
> * Service status shows “active (running)”
> * No errors in journalctl output
> * Application survives service restart
> * Application starts automatically after VM reboot
>
> ✓ **Final verification checklist:**
>
> * ☐ Azure VM provisioned with port 5000 open
> * ☐ .NET application published locally
> * ☐ ASP.NET Core runtime installed on VM
> * ☐ Service user created with appropriate permissions
> * ☐ Application files deployed to /opt/dotnet-app
> * ☐ Systemd service configured and enabled
> * ☐ Web page accessible via browser at VM’s public IP on port 5000

## Common Issues

> **If you encounter problems:**
>
> **“Connection refused” on port 5000:** Check that the Azure NSG allows inbound traffic on port 5000 and the service is running with `systemctl status dotnet-app.service`
>
> **Service fails immediately:** Check the ExecStart path matches your DLL name. View logs with `journalctl -u dotnet-app.service -n 100`
>
> **“Permission denied” errors:** Verify file ownership with `ls -la /opt/dotnet-app/` — files should be owned by `dotnet-app:dotnet-app`
>
> **Page loads but shows errors:** Check `ASPNETCORE_ENVIRONMENT` is set to `Production` and view application logs for details
>
> **Service doesn’t start after reboot:** Verify the service is enabled with `systemctl is-enabled dotnet-app.service`
>
> **Still stuck?** Review the complete logs with `journalctl -u dotnet-app.service --no-pager` and check for specific error messages

## Summary

You’ve successfully deployed a .NET MVC application to an Azure VM with systemd which:

* ✓ Establishes infrastructure-as-code patterns using Azure CLI
* ✓ Follows security best practices with dedicated service users
* ✓ Prepares your application for production with automatic restart and boot persistence

> **Key takeaway:** Systemd provides robust process management for .NET applications on Linux, handling crashes, restarts, and boot persistence automatically. While this manual deployment teaches fundamental concepts, production environments typically use CI/CD pipelines to automate these steps.

## Going Deeper (Optional)

> **Want to explore more?**
>
> * Add a reverse proxy (nginx) in front of Kestrel for SSL termination
> * Configure health checks in systemd using `Type=notify` with .NET’s systemd integration
> * Set up log rotation for application logs using journald configuration
> * Create a deployment script that automates all manual steps

## Done! 🎉

Great job! You’ve learned how to deploy a .NET application to Linux with proper service management. This foundation will help you understand container orchestration and cloud-native deployment patterns.

## Cleanup

When you’re finished with the exercise, remove the Azure resources to avoid charges:

```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
az group delete --name DotNetDeployGroup --yes --no-wait
```

## TL;DR

Complete deployment script (run from your local machine):

```
#!/bin/bash
set -e

resource_group="DotNetDeployGroup"
vm_name="DotNetAppVM"
location="northeurope"

az group create --name $resource_group --location $location

az vm create \
    --resource-group $resource_group \
    --name $vm_name \
    --image Ubuntu2404 \
    --size Standard_B1s \
    --admin-username azureuser \
    --generate-ssh-keys

az vm open-port --resource-group $resource_group --name $vm_name --port 5000

vm_ip=$(az vm show -g $resource_group -n $vm_name --show-details --query publicIps -o tsv)

dotnet new mvc -n HelloDotnet
cd HelloDotnet
dotnet publish -c Release -o ./publish

ssh azureuser@$vm_ip 'wget https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && sudo dpkg -i packages-microsoft-prod.deb && rm packages-microsoft-prod.deb && sudo apt update && sudo apt install -y aspnetcore-runtime-10.0'

ssh azureuser@$vm_ip 'sudo useradd --system --shell /usr/sbin/nologin --no-create-home dotnet-app; sudo mkdir -p /opt/dotnet-app; sudo chown azureuser:dotnet-app /opt/dotnet-app; sudo chmod 750 /opt/dotnet-app'

scp -r ./publish/* azureuser@$vm_ip:/opt/dotnet-app/

ssh azureuser@$vm_ip 'sudo chown -R dotnet-app:dotnet-app /opt/dotnet-app'

ssh azureuser@$vm_ip 'sudo tee /etc/systemd/system/dotnet-app.service > /dev/null << EOF
[Unit]
Description=.NET MVC Application
After=network.target

[Service]
Type=simple
User=dotnet-app
Group=dotnet-app
WorkingDirectory=/opt/dotnet-app
ExecStart=/usr/bin/dotnet /opt/dotnet-app/HelloDotnet.dll
Restart=always
RestartSec=5
Environment=ASPNETCORE_URLS=http://0.0.0.0:5000
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target
EOF'

ssh azureuser@$vm_ip 'sudo systemctl daemon-reload && sudo systemctl enable dotnet-app.service && sudo systemctl start dotnet-app.service'

echo "Deployment complete! Visit http://$vm_ip:5000/"
```

## Appendix: Update Script

After the initial deployment, use this script to deploy application updates. Save it as `update-app.sh` in your project directory:

```
#!/bin/bash
set -e

if [ -z "$1" ]; then
    echo "Usage: ./update-app.sh <VM_IP>"
    exit 1
fi

vm_ip=$1

echo "Building application..."
dotnet publish -c Release -o ./publish

echo "Uploading files to $vm_ip..."
ssh azureuser@$vm_ip 'mkdir -p /tmp/dotnet-app-update'
scp -r ./publish/* azureuser@$vm_ip:/tmp/dotnet-app-update/

echo "Deploying and restarting service..."
ssh azureuser@$vm_ip 'sudo systemctl stop dotnet-app.service && \
    sudo rm -rf /opt/dotnet-app && \
    sudo mv /tmp/dotnet-app-update /opt/dotnet-app && \
    sudo chown -R dotnet-app:dotnet-app /opt/dotnet-app && \
    sudo systemctl start dotnet-app.service'

echo "Update complete! Visit http://$vm_ip:5000/"
```

Make the script executable and run it:

```
chmod +x update-app.sh
./update-app.sh <YOUR_VM_IP>
```

The script stops the service before updating files to prevent file-in-use errors, then restarts it after deployment.

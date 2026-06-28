---

title: 3. Use Github Actions to Deploy your App
author: Marcus Ackre Medina
type: exercise
topic: deployment
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/exercises/3-deployment/8-use-github-actions-to-deploy-your-app.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-"
tags: ["actions", "app", "azure", "cloud", "deploy", "deployment", "devops", "docker", "dotnet", "exercise"]
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

# 3. Use Github Actions to Deploy your App

🟡


## Introduction

In this tutorial, we will automate the deployment process of an application, to an Azure Linux VM, using Github Actions.

The integration of Github Actions into the deployment workflow offers a streamlined and efficient approach to automate the build and deployment processes. This guide will introduce you to the foundational concepts and step-by-step instructions required to set up a Continuous Integration and Continuous Deployment (CI/CD) pipeline using Github Actions.

We’ll start by preparing a Github repository and adding a workflow, followed by implementing a sample ASP.NET web application. The tutorial will guide you through developing a CI/CD pipeline as a *Github workflow*, utilizing a *Github-hosted* runner for the CI (build) part, and a *self-hosted* Github runner for the CD (deploy) part. This process ensures that every push to the repository triggers the automated deployment of your application.

This tutorial is designed for developers with a basic understanding of web development, git, and cloud services.

## Method

* **Setup Github Repository:** Initialize a Github repository and configure a workflow for automation.
* **Create ASP.NET Web Application:** Develop a simple ASP.NET web application to serve as the deployment target.
* Build CI/CD Pipeline: Design a continuous integration and continuous deployment (CI/CD) pipeline using Github Actions.
  + **Continuous Integration:** Utilize Github-hosted runners to build and test your application upon each push or pull request.
  + **Continuous Deployment:** Set up a virtual machine (VM) with a self-hosted Github runner to automate the deployment process.
* **Automated Deployment Trigger:** Ensure the entire deployment process is automated and triggered by any push to the designated repository branch.

## Prerequisites

* An Azure account. If you don’t have one, sign up at [Azure’s official site](https://azure.microsoft.com/).
* Basic familiarity with Azure services and ASP.NET web development.
* A Github account
* Basic familiarity with git and Github
* VSCode connected to Github

## Step 1: Create a Github Repository with an ASP.NET Web Application

Creating a git repository marks both the end of a development phase and the beginning of the CI/CD pipeline. Let’s first setup a git repo and “develop” a webapp, which we check-in to the repo.

1. **Create Project Directory**: Create a new directory named `GithubActionsDemo` to house your project.
2. **Generate ASP.NET Web Application**: Within VSCode, navigate to your newly created directory and execute the following commands to create a new ASP.NET web application and a `.gitignore` file tailored for .NET projects:

   ```
   dotnet new webapp
   dotnet new gitignore
   ```
3. **Initialize Git Repository**: Enable version control by initializing a git repository, adding your project files to it, and committing them with an initial message:

   ```
   git init
   git add .
   git commit -m "Initial Commit"
   ```
4. **Push to Github**: Use VSCode’s integrated git functionality to connect your local repository to Github and push your initial commit. Follow the prompts in VSCode to authenticate and specify your repository details on Github.

This step will set up your project for development and prepare it for an automated CI/CD pipeline, making it ready for continuous integration and deployment with Github Actions.

## Step 2: Automate Continuous Integration with a Github-Hosted Runner

With our web application now stored in the repository, the next step is to automate the Continuous Integration (CI) part of our CI/CD pipeline. This involves building and publishing the web application to an *artifact repository*. For this tutorial, we’ll utilize Github’s own artifact storage.

### Setting Up the CI Workflow

1. **Access Your Github Repository**: Log in to Github, navigate to your `GithubActionsDemo` repository, and switch to the **Actions** tab.
2. **Initialize the Workflow**: Use the search bar within the Actions tab to find a predefined **.NET** workflow, then click **Configure** to use it as a starting point for your CI process.
3. **Configure the Workflow File**: You’ll be prompted to edit the workflow file. Here’s a template for a basic CI workflow designed for an ASP.NET application:

   > cicd.yaml

   ```
   # This workflow will build the GithubActionsDemo project

   name: GithubActionsDemo

   on:
     push:
       branches:
       - "main"
     workflow_dispatch:

   jobs:
     build:
       runs-on: ubuntu-latest
       steps:

       - name: Install .NET SDK
         uses: actions/setup-dotnet@v4
         with:
           dotnet-version: '10.0.x'

       - name: Check out this repo
         uses: actions/checkout@v4

       - name: Restore dependencies (install Nuget packages)
         run: dotnet restore

       - name: Build and publish the app
         run: |
           dotnet build --no-restore
           dotnet publish -c Release -o ./publish        

       - name: Upload app artifacts to Github
         uses: actions/upload-artifact@v4
         with:
           name: app-artifacts
           path: ./publish
   ```
4. Name the file `cicd.yaml` and commit the change into the repo by pressing the button **Commit changes…**. Click **Commit changes** again in the popup window. This will initiate the CI workflow.

### Verify the Workflow’s Success

* **Check Workflow Execution**: After committing your workflow file, go to the **Actions** tab again to observe the running workflow.
* **Verify Artifact Upload**: Navigate to the **Summary** page of your workflow run. Under the *Artifacts* section, you should see your application artifacts listed. Download and inspect them to ensure they are as expected.
* **Green Checkmark**: A successful build will be indicated by a green checkmark.

### Important Reminder

* **Sync Your Local Repository**: Ensure to pull the latest changes from your Github repository into your local VSCode workspace to avoid any synchronization issues as you continue to develop your application.

This setup creates the foundation for continuous integration, automatically handling the build and artifact storage steps every time you push changes to your main branch.

## Step 3: Provision a VM with a self-hosted runner

Transitioning your app to a live environment requires deploying it to an Azure Linux VM, installing necessary runtimes, and configuring the VM to handle deployments. This process includes setting up a self-hosted runner on the VM to interact with Github Actions, enabling automated deployment directly from your repository.

### Provision the Linux VM

To provision and configure your Azure Linux VM, follow these steps:

1. **Prepare Script Files**: Create two files: `provision_vm.sh` for provisioning the VM and `cloud-init_dotnet.yaml` for initializing it with necessary configurations.

   > provision\_vm.sh

   ```
   #!/bin/bash

   resource_group=GithubActionsDemoRG
   vm_name=GithubActionsDemoVM
   vm_port=5000

   az group create --location northeurope --name $resource_group

   az vm create --name $vm_name --resource-group $resource_group \
                --image Ubuntu2404 --size Standard_B1s \
                --generate-ssh-keys --admin-username azureuser \
                --custom-data @cloud-init_dotnet.yaml

   az vm open-port --port $vm_port --resource-group $resource_group --name $vm_name
   ```

   > cloud-init\_dotnet.yaml

   ```
   #cloud-config

   # Install .Net Runtime 10.0
   runcmd:
     # Register Microsoft repository (which includes .Net Runtime 10.0 package)
     - wget -q https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
     - dpkg -i packages-microsoft-prod.deb

     # Install .Net Runtime 10.0
     - apt-get update
     - apt-get install -y aspnetcore-runtime-10.0

     # Enable the application service
     - systemctl daemon-reload
     - systemctl enable GithubActionsDemo.service

   # Create a service for the application
   write_files:
     - path: /etc/systemd/system/GithubActionsDemo.service
       content: |
         [Unit]
         Description=ASP.NET Web App running on Ubuntu

         [Service]
         WorkingDirectory=/opt/GithubActionsDemo
         ExecStart=/usr/bin/dotnet /opt/GithubActionsDemo/GithubActionsDemo.dll
         Restart=always
         RestartSec=10
         KillSignal=SIGINT
         SyslogIdentifier=GithubActionsDemo
         User=www-data
         Environment=ASPNETCORE_ENVIRONMENT=Production
         Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false
         Environment="ASPNETCORE_URLS=http://*:5000"

         [Install]
         WantedBy=multi-user.target      
       owner: root:root
       permissions: '0644'
   ```
2. **Execute Provisioning Script**: Change the script’s permission with `chmod +x provision_vm.sh` and run it to set up your VM.

   ```
   chmod +x provision_vm.sh
   ./provision_vm.sh
   ```

#### Verify the provisioning

1. Go into the Azure portal and verify that a VM has been provisioned.
2. Login to the VM

   ```
   ssh azureuser@<public_ip>
   ```

   > Output

   ```
   azureuser@GithubActionsDemoVM:~$
   ```

### Configure the Deployment Workflow

1. **Update the Workflow**: Integrate a deployment job that downloads build artifacts and deploys them to the VM. Use the self-hosted runner for deployment tasks. Use the code snippet below.

   > cicd.yaml

   ```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
   ...

     deploy:
       runs-on: self-hosted
       needs: build

       steps:
       - name: Download the artifacts from Github (from the build job)
         uses: actions/download-artifact@v4
         with:
           name: app-artifacts

       - name: Stop the application service
         run: |
           sudo systemctl stop GithubActionsDemo.service        

       - name: Deploy the the application
         run: |
           sudo rm -Rf /opt/GithubActionsDemo || true
           sudo cp -r /home/azureuser/actions-runner/_work/GithubActionsDemo/GithubActionsDemo/ /opt/GithubActionsDemo        

       - name: Start the application service
         run: |
           sudo systemctl start GithubActionsDemo.service
   ```
2. Don’t push the new workflow just yet. First we should install the runner.

### Setting Up a Self-Hosted Runner on the VM

1. **Configure Runner on GitHub**: Navigate to your repository’s settings, find the Actions tab, and set up a new self-hosted runner following GitHub’s instructions:

   * select the **Settings** tab
   * Select **Actions -> Runners** in the side menu
   * Click **New self-hosted runner**
   * Select **Linux** (Architecture: x64)
2. **Install Runner on VM**: SSH into your VM, execute the provided script to install and start the GitHub runner.

   * Run the code from Github
     - Press `<Enter>`to accept all default values in the configuration wizard

   ```
   # The code from Github

   ...

   ./run.sh
   ```

#### Verify the runner installation process

If the installation process went well you should see the following output in the terminal

> Output

```
azureuser@GithubActionsDemoVM:~/actions-runner$ ./run.sh

√ Connected to GitHub

Current runner version: '2.314.1'
2024-03-11 13:39:55Z: Listening for Jobs
```

In the Github portal, you should also see the runner as *Idle*.

### Verify that the deployment went well

Now it’s time to push your new Github workflow (the `cicd.yaml` file) and see how the runner picks up the job and carries it out. Best PracticeAfter pushing the new code to Github you should see how the runner picks up the job.

```
azureuser@GithubActionsDemoVM:~/actions-runner$ ./run.sh

√ Connected to GitHub

Current runner version: '2.314.1'
2024-03-11 13:39:55Z: Listening for Jobs
2024-03-11 13:52:45Z: Running job: deploy
2024-03-11 13:52:57Z: Job deploy completed with result: Succeeded
```

Open up a browser and navigate to

```
http://<public_ip>:5000
```

You should now see your app running on the VM.

> ### Best Practice: Run the runner as a service instead
>
> In a real scenario you should run the runner as a service in the background. You can do this by running the following commands:
>
> ```
> # Run the Github runner as a service
>
> sudo ./svc.sh install azureuser
> sudo ./svc.sh start
> ```
>
> You can follow the logs with `journalctl` (use autocomplete with `<tab>` to find your service)
>
> ```
> sudo journalctl -u actions.runner.<user>-GithubActionsDemo.GithubActionsDemoVM.service -f
> ```

## Step 4: Use the new CI/CD Pipeline for Continuous Deployment

Putting your newly established CI/CD pipeline into action. Here’s how it works:

1. **Implement Changes**: Start by opening your project in Visual Studio Code (VSCode). Make any desired modifications to your codebase.
2. **Commit and Push**: Once you’re satisfied with the changes, commit them to your local repository and push the updates to your GitHub repository.
3. **Observe Deployment**: Navigate to your application’s URL in a web browser. The deployment might take a moment, so refresh the page after a minute or two to see your changes go live.

By following these steps, you go through the full cycle of your CI/CD pipeline, automating the build and deployment processes, and significantly streamlining your workflow.

## Conclusion

In this tutorial, we’ve set up and used a CI/CD pipeline with GitHub Actions for deploying an ASP.NET web application to an Azure VM. This pipeline enhances your productivity and ensures that your application is always up to date with the latest changes in your codebase.

This automated deployment strategy offers several benefits, including reduced manual errors, faster release cycles, and a more robust and reliable deployment process. It enables you to focus more on development and less on the deployment process.

## Don’t Forget

Azure services incur costs. Delete resources you no longer need.

# Happy Deploying! 🚀

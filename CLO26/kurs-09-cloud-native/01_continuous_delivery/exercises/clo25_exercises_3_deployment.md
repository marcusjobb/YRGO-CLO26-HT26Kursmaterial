Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-
Server Foundation-
Network Foundation-
Deployment-
Cloud Databases-
Webapp Development-
Code Collaboration-
DockerTutorials

# Deployment

🟡


Goal Deploy a simple HTML file to an nginx web server on an Azure Ubuntu VM using SCP (Secure Copy Protocol), establishing the foundation for manual web application deployment.
What you’ll learn:
How to create and transfer files to remote servers using SCP When to use manual deployment vs automated pipelines Best practices for deploying to nginx web servers Prerequisites Before starting, ensure you have:
✓ An Azure Ubuntu VM with a public IP address ✓ SSH key pair configured (~/. [»](/exercises/3-deployment/1-deploy-html-with-scp/)

Goal Deploy a .NET 10 MVC web application to an Azure Ubuntu 24.04 VM using SCP and configure it as a systemd service for production-ready process management.
What you’ll learn:
How to provision Azure VMs using the Azure CLI When to use dedicated service users for application security Best practices for deploying .NET applications with systemd Prerequisites Before starting, ensure you have:
✓ Azure CLI installed and authenticated (az login) ✓ SSH key pair configured (~/. [»](/exercises/3-deployment/2-deploy-dotnet-mvc-scp-systemd/)

Introduction In this tutorial, we will automate the deployment process of an application, to an Azure Linux VM, using Github Actions.
The integration of Github Actions into the deployment workflow offers a streamlined and efficient approach to automate the build and deployment processes. This guide will introduce you to the foundational concepts and step-by-step instructions required to set up a Continuous Integration and Continuous Deployment (CI/CD) pipeline using Github Actions. [»](/exercises/3-deployment/8-use-github-actions-to-deploy-your-app/)

Exercise 1: Implementing Azure Key Vault with MongoDB on Azure VM Goal Implement Azure Key Vault for secure secret management in your ASP.NET Core application, connecting to MongoDB/Cosmos DB and deploying to an Azure Ubuntu VM with managed identity for secure authentication.
Learning Objectives By the end of this exercise, you will:
Provision Azure Key Vault and Azure Cosmos DB for MongoDB API Configure secret management for sensitive connection strings Implement the Options Pattern for configuration Use feature flags to enable different services based on environment Setup managed identity for VM-to-Key Vault authentication Deploy an ASP. [»](/exercises/3-deployment/4-implementing-azure-key-vault-with-mongodb-on-azure-vm/)

Goal Configure monitoring for an Azure Virtual Machine (VM) using Azure Monitor to gain visibility into system performance and application logs.
What you’ll learn:
How to create a Log Analytics Workspace as a central data store How to enable Azure Monitor Insights for VM performance metrics (CPU, Memory, Disk) How to collect Linux Syslog via Data Collection Rules How to redirect Nginx access logs to Syslog for centralized analysis How to query and analyze logs using KQL (Kusto Query Language) Prerequisites Before starting, ensure you have: [»](/exercises/3-deployment/5-monitoring-vms-with-azure-monitor/)

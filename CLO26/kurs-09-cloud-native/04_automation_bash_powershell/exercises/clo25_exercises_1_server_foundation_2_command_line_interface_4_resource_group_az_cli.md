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

# 4. Creating a Resource Group Using AZ CLI

🟡


## Goal

Create and manage an Azure resource group using command-line tools instead of the portal interface.

> **What you’ll learn:**
>
> * How to access and use Azure Cloud Shell
> * How to create resource groups using Azure CLI commands
> * How to verify and list resources from the command line

## Prerequisites

> **Before starting, ensure you have:**
>
> * ✓ An active Azure subscription
> * ✓ Access to the Azure Portal

## Exercise Steps

### Overview

1. **Log in to Azure Cloud Shell**
2. **Create a Resource Group**
3. **Clean Up Resources (Optional)**

### **Step 1:** Log in to Azure Cloud Shell

The Azure Cloud Shell provides a browser-based terminal that comes pre-configured with the Azure CLI. Using Cloud Shell eliminates the need to install tools locally while giving you full access to Azure management capabilities.

1. **Open** the [Azure Portal](https://portal.azure.com/) and **click** the **Cloud Shell** icon (top-right corner).
2. **Select** **Bash** as your shell environment if prompted.

> ℹ **Concept Deep Dive**
>
> The Azure Cloud Shell is a Terminal in the Azure Portal that comes pre-configured with the Azure CLI. It enables you to run CLI commands without additional setup.

### **Step 2:** Create a Resource Group

Resource groups serve as containers for all resources within Azure. Creating one via CLI demonstrates how automation and scripting can replace manual portal operations, making deployments repeatable and faster.

1. **Run** the following command to create a resource group:

   ```
   az group create --name MyCLIDemoGroup --location northeurope
   ```

   * **`--name`**: Specifies the name of the resource group (e.g., `MyCLIDemoGroup`).
   * **`--location`**: Sets the region where the resources in the group will be located (e.g., `northeurope`).

> ℹ **Concept Deep Dive**
>
> A **Resource Group** acts as a container for all resources within Azure, making it easier to organize, manage, and delete resources as a single unit.
>
> ✓ **Quick check:** Review the output to confirm successful creation. You should see a JSON response similar to:
>
> ```
> {
>   "id": "/subscriptions/<subscription-id>/resourceGroups/MyCLIDemoGroup",
>   "location": "eastus",
>   "name": "MyCLIDemoGroup",
>   "properties": {
>     "provisioningState": "Succeeded"
>   }
> }
> ```
>
> In the Azure Portal under **Resource groups** you should also see it listed.
> You can also list all resource groups in your subscription by running the command:
>
> ```
> az group list --output table
> ```

### **Step 3:** Clean Up Resources (Optional)

Deleting unused resource groups prevents unnecessary charges and keeps your subscription organized. The CLI makes cleanup quick and can be incorporated into automated workflows.

1. **Delete** the resource group to avoid unnecessary costs:

   ```
   az group delete --name MyCLIDemoGroup --no-wait --yes
   ```

   * **`--no-wait`**: Ensures the command returns immediately without waiting for the operation to complete.
   * **`--yes`**: Confirms the deletion without additional prompts.

## Common Issues

> **If you encounter problems:**
>
> **Cloud Shell not loading:** Refresh the browser or try a different browser. Ensure pop-ups are allowed for portal.azure.com.
>
> **“Subscription not found” error:** Run `az account list` to verify your subscription is active and selected.

## Summary

You’ve successfully created and managed a resource group using Azure CLI which:

* ✓ Demonstrated command-line resource management
* ✓ Introduced you to Azure CLI syntax and commands
* ✓ Showed how to verify resources from the terminal

> **Key takeaway:** The Azure CLI enables scriptable, repeatable infrastructure management that forms the foundation for automation and Infrastructure as Code practices.

## Going Deeper (Optional)

> **Want to explore more?**
>
> * Create multiple resource groups in different regions using a single script
> * Explore `az group list` with different output formats (json, yaml, tsv)
> * Use `az configure` to set default values for location and resource group

## Done 🎉

You have successfully created and managed a resource group using Azure CLI, gaining hands-on experience with command-line operations in Azure.

---

title: 4. Implementing Azure Key Vault with MongoDB on Azure VM
author: Marcus Ackre Medina
type: exercise
topic: deployment
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/exercises/3-deployment/4-implementing-azure-key-vault-with-mongodb-on-azure-vm.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-"
tags: ["azure", "cloud", "deployment", "devops", "docker", "dotnet", "exercise", "git", "implementing", "key"]
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

# 4. Implementing Azure Key Vault with MongoDB on Azure VM

🟡


# Exercise 1: Implementing Azure Key Vault with MongoDB on Azure VM

## Goal

Implement Azure Key Vault for secure secret management in your ASP.NET Core application, connecting to MongoDB/Cosmos DB and deploying to an Azure Ubuntu VM with managed identity for secure authentication.

## Learning Objectives

By the end of this exercise, you will:

* Provision **Azure Key Vault** and **Azure Cosmos DB for MongoDB API**
* Configure **secret management** for sensitive connection strings
* Implement the **Options Pattern** for configuration
* Use **feature flags** to enable different services based on environment
* Setup **managed identity** for VM-to-Key Vault authentication
* Deploy an ASP.NET Core application to an **Azure Ubuntu VM**
* Understand **configuration provider precedence** in ASP.NET Core

## Prerequisites

* An Azure account with active subscription
* Azure CLI installed
* .NET 10.0 SDK installed
* Docker and Docker Compose installed
* Basic understanding of ASP.NET Core MVC applications
* Completed Exercise 5 or have a similar application structure

## Understanding Azure Key Vault in ASP.NET Core Applications

### Why Use Azure Key Vault?

1. **Secure Secret Management**: Store sensitive configuration data like connection strings and API keys securely
2. **Centralized Configuration**: Manage secrets across multiple applications and environments
3. **Access Control**: Fine-grained control over who or what can access specific secrets
4. **Audit Trail**: Track when and by whom secrets are accessed
5. **Rotation**: Easily rotate credentials without redeploying applications

### How Azure Key Vault Integrates with ASP.NET Core

ASP.NET Core uses a configuration system based on key-value pairs from various providers. The configuration providers are applied in a specific order, with later providers overriding values from earlier ones. Azure Key Vault is typically one of the last providers registered, allowing it to override settings from appsettings.json files.

### Configuration Provider Precedence

ASP.NET Core loads configuration from multiple providers. Each subsequent provider can override values from earlier ones. The default precedence order (lowest to highest priority) is:

1. `appsettings.json` (base settings, all environments)
2. `appsettings.{Environment}.json` (e.g., appsettings.Development.json)
3. **User secrets** (Development environment only)
4. **Environment variables** (using `__` as section separator, e.g. `MongoDb__ConnectionString`)
5. **Command-line arguments** (e.g. `--MongoDb:ConnectionString="..."`)
6. **Azure Key Vault** (when explicitly added as a configuration provider in code)

This means a value in Azure Key Vault will override the same key set anywhere else — in appsettings files, user secrets, or environment variables. In Phase II of this exercise, you will demonstrate each of these layers hands-on.

## Step-by-Step Instructions

### Phase I: Setting Up Azure Resources and Application Code

### Step 0: Choose Your Student Suffix

Before starting, pick a short, unique suffix (e.g., your initials + a digit: `jd1`, `ame`, `group3`). This suffix is appended to Azure resource names that must be globally unique (Cosmos DB and Key Vault). You will use it throughout the exercise.

### Step 1: Provision Azure Cosmos DB with MongoDB API

1. Log in to Azure and create a Cosmos DB account. Replace `<suffix>` with your chosen suffix:

   > `infrastructure/provision_cosmosdb.sh`

   ```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
   #!/bin/bash
   set -euo pipefail

   resource_group="AzureKeyVaultResourceGroup"
   db_name="cloudsoft-mongodb-<suffix>"

   # Create resource group
   az group create --location northeurope --name "$resource_group"

   # Create Cosmos DB account with MongoDB API
   az cosmosdb create \
       --name "$db_name" \
       --resource-group "$resource_group" \
       --kind MongoDB \
       --capabilities EnableServerless \
       --default-consistency-level Session \
       --server-version 7.0
   ```

   > 💡 **Information**
   >
   > * `set -euo pipefail` is a defensive scripting pattern that makes bash scripts fail fast:
   >   + `-e` — exit immediately if any command fails (non-zero exit code)
   >   + `-u` — treat unset variables as an error instead of silently expanding to empty
   >   + `-o pipefail` — a pipeline fails if *any* command in the pipe fails, not just the last one
   > * Without this, a failed `az` command would be silently ignored and the script would continue with missing or incorrect values
   > * The Cosmos DB account name must be globally unique across all of Azure — the suffix ensures yours doesn’t collide with other students'
2. After creation, retrieve the connection string (you’ll need it later):

   ```
   az cosmosdb keys list \
       --name "cloudsoft-mongodb-<suffix>" \
       --resource-group "AzureKeyVaultResourceGroup" \
       --type connection-strings \
       --query "connectionStrings[?description=='Primary MongoDB Connection String'].connectionString" \
       --output tsv
   ```

### Step 2: Provision Azure Key Vault

1. Create an Azure Key Vault. Use the same suffix:

   > `infrastructure/provision_keyvault.sh`

   ```
    #!/bin/bash
    set -euo pipefail

    resource_group="AzureKeyVaultResourceGroup"
    vault_name="cloudsoftkv<suffix>"

    # Create resource group
    az group create --location northeurope --name "$resource_group"

    # Create Key Vault
    az keyvault create \
        --name "$vault_name" \
        --resource-group "$resource_group" \
        --location northeurope \
        --enable-rbac-authorization true

    # Get your user principal ID
    USER_ID=$(az ad signed-in-user show --query id --output tsv)
    VAULT_ID=$(az keyvault show --name "$vault_name" --resource-group "$resource_group" --query id -o tsv)

    # Assign Key Vault Administrator role to yourself
    az role assignment create \
        --assignee "$USER_ID" \
        --role "Key Vault Administrator" \
        --scope "$VAULT_ID"

    # Wait for RBAC propagation
    echo "Waiting for RBAC propagation (30 seconds)..."
    sleep 30
   ```

   > 💡 **Information**
   >
   > * `--enable-rbac-authorization true` is required so that Azure RBAC role assignments control access to the vault. Without this flag, the vault defaults to access policy mode and the role assignments in this exercise (Key Vault Administrator for you, Key Vault Secrets User for the VM) will have no effect
   > * Key Vault names must be globally unique across all of Azure — the suffix ensures yours is unique
2. Note the Key Vault URI (you’ll need it later):

   ```
   az keyvault show \
     --name "cloudsoftkv<suffix>" \
     --resource-group "AzureKeyVaultResourceGroup" \
     --query properties.vaultUri \
     --output tsv
   ```

### Step 3: Create Azure Key Vault Options Class

1. Create a `Configurations` directory if it doesn’t exist yet:

   ```
   mkdir -p Configurations
   ```
2. Create an options class for Azure Key Vault configuration:

   > `src/Configurations/AzureKeyVaultOptions.cs`

   ```
   namespace CloudSoft.Configurations;

   public class AzureKeyVaultOptions
   {
       public const string SectionName = "AzureKeyVault";
       public string? KeyVaultUri { get; set; }
   }
   ```

   > 💡 **Information**
   >
   > * Using the Options Pattern allows for strongly-typed access to configuration sections
   > * The `SectionName` constant helps maintain consistency when referencing this configuration section

### Step 4: Add Required NuGet Packages

1. Add the necessary packages for Azure Key Vault integration:

   ```
   dotnet add package Azure.Extensions.AspNetCore.Configuration.Secrets
   dotnet add package Azure.Identity
   dotnet add package Azure.Security.KeyVault.Secrets
   ```

   > 💡 **Information**
   >
   > * `Azure.Extensions.AspNetCore.Configuration.Secrets` provides the configuration provider
   > * `Azure.Identity` provides authentication mechanisms including DefaultAzureCredential
   > * `Azure.Security.KeyVault.Secrets` is the client library for Key Vault secrets

### Step 5: Register Azure Key Vault Configuration Provider

1. Update `Program.cs` to register Azure Key Vault as a configuration provider:

   > `src/Program.cs`

   ```
   ...

     using Azure.Identity;

     ...

     var builder = WebApplication.CreateBuilder(args);

   ...

     // Check if Azure Key Vault should be used
     bool useAzureKeyVault = builder.Configuration.GetValue<bool>("FeatureFlags:UseAzureKeyVault");

     if (useAzureKeyVault)
     {
         // Configure Azure Key Vault options
         builder.Services.Configure<AzureKeyVaultOptions>(
             builder.Configuration.GetSection(AzureKeyVaultOptions.SectionName));

         // Get Key Vault URI from configuration
         var keyVaultOptions = builder.Configuration
             .GetSection(AzureKeyVaultOptions.SectionName)
             .Get<AzureKeyVaultOptions>();
         var keyVaultUri = keyVaultOptions?.KeyVaultUri;

         // Register Azure Key Vault as configuration provider
         if (string.IsNullOrEmpty(keyVaultUri))
         {
             throw new InvalidOperationException("Key Vault URI is not configured.");
         }

         builder.Configuration.AddAzureKeyVault(
             new Uri(keyVaultUri),
             new DefaultAzureCredential());

         Console.WriteLine("Using Azure Key Vault for configuration");
     }

     ...
   ```

   > 💡 **Information**
   >
   > * The `DefaultAzureCredential` will handle authentication to Azure Key Vault
   > * In development, it uses your Azure CLI credentials
   > * In production, it will use the VM’s managed identity
   > * The code reads `FeatureFlags:UseAzureKeyVault` and `AzureKeyVault:KeyVaultUri` from configuration — you will provide these values in the next step

### Step 6: Configure Application Settings and Local MongoDB

Now that the Key Vault integration code is in place, configure the settings files that provide the values it reads, and start the local MongoDB instance.

1. Update `appsettings.json` to include placeholders for Azure Key Vault:

   > `src/appsettings.json`

   ```
   {
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*",
     "FeatureFlags": {
       "UseMongoDb": false,
       "UseAzureStorage": false,
       "UseAzureKeyVault": false
     },
     "MongoDb": {
       "ConnectionString": "mongodb://{username}:{password}@{hostname}:{port}",
       "DatabaseName": "cloudsoft",
       "SubscribersCollectionName": "subscribers"
     },
     "AzureKeyVault": {
       "KeyVaultUri": "https://{keyvaultname}.vault.azure.net/"
     }
   }
   ```
2. Update `appsettings.Development.json` to enable MongoDB with the local Docker connection string:

   > `src/appsettings.Development.json`

   ```
   {
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "FeatureFlags": {
       "UseMongoDb": true,
       "UseAzureStorage": false,
       "UseAzureKeyVault": false
     },
     "MongoDb": {
       "ConnectionString": "mongodb://root:example@localhost:27017",
       "DatabaseName": "cloudsoft",
       "SubscribersCollectionName": "subscribers"
     },
     "AzureKeyVault": {
       "KeyVaultUri": "https://{keyvaultname}.vault.azure.net/"
     }
   }
   ```
3. Create `docker-compose.yml` in the `infrastructure/` directory for local MongoDB instance:

   > `infrastructure/docker-compose.yml`

   ```
   services:
     mongodb:
       image: mongo:latest
       container_name: mongodb
       restart: always
       ports:
         - "27017:27017"
       environment:
         MONGO_INITDB_ROOT_USERNAME: ${MONGO_USERNAME:-root}
         MONGO_INITDB_ROOT_PASSWORD: ${MONGO_PASSWORD:-example}
       volumes:
         - mongodb_data:/data/db
       networks:
         - mongo_network

     mongo-express:
       image: mongo-express:latest
       container_name: mongo-express
       restart: always
       ports:
         - "8081:8081"
       environment:
         ME_CONFIG_MONGODB_ADMINUSERNAME: ${MONGO_USERNAME:-root}
         ME_CONFIG_MONGODB_ADMINPASSWORD: ${MONGO_PASSWORD:-example}
         ME_CONFIG_MONGODB_SERVER: mongodb
       depends_on:
         - mongodb
       networks:
         - mongo_network

   volumes:
     mongodb_data:

   networks:
     mongo_network:
       driver: bridge
   ```
4. Start the local MongoDB instance:

   ```
   docker-compose -f infrastructure/docker-compose.yml up -d
   ```

### Step 7: Add MongoDB Connection String as Secret in Azure Key Vault

1. Retrieve the Cosmos DB connection string and store it as a secret in Key Vault. Use your suffix:

   > `infrastructure/store_keyvault_secret.sh`

   ```
   db_name="cloudsoft-mongodb-<suffix>"
   resource_group="AzureKeyVaultResourceGroup"
   vault_name="cloudsoftkv<suffix>"

   # Retrieve the connection string from Cosmos DB
   CONNECTION_STRING=$(az cosmosdb keys list \
       --name "$db_name" \
       --resource-group "$resource_group" \
       --type connection-strings \
       --query "connectionStrings[?description=='Primary MongoDB Connection String'].connectionString" \
       --output tsv)

   # Store it in Key Vault
   # Note: We use -- instead of : for secret names
   az keyvault secret set \
       --vault-name "$vault_name" \
       --name "MongoDb--ConnectionString" \
       --value "$CONNECTION_STRING"
   ```

   > 💡 **Information**
   >
   > * Azure Key Vault uses `--` instead of `:` in secret names to match ASP.NET Core’s configuration hierarchy
   > * The secret name `MongoDb--ConnectionString` maps to `MongoDb:ConnectionString` in your application

> **Rollback — Phase I**
>
> If something went wrong during Phase I:
>
> * **Cosmos DB name taken:** Change your suffix and re-run the provisioning command.
> * **Key Vault RBAC fails:** Delete the vault (`az keyvault delete --name cloudsoftkv<suffix> --resource-group AzureKeyVaultResourceGroup`), purge if soft-deleted (`az keyvault purge --name cloudsoftkv<suffix>`), then re-run.
> * **Secret not stored:** Re-run the Step 7 commands — they are idempotent.
> * **App won’t compile:** Run `dotnet restore`, verify `AzureKeyVaultOptions.cs` namespace, and check `Program.cs` for typos.
> * **Start completely over:** Run `bash 4-teardown.sh` to delete all Azure resources, then begin from Step 1.

> **Checkpoint — Phase I Complete**
>
> At this point you should have:
>
> * Cosmos DB account `cloudsoft-mongodb-<suffix>` provisioned
> * Key Vault `cloudsoftkv<suffix>` provisioned with RBAC enabled
> * Secret `MongoDb--ConnectionString` stored in Key Vault
> * `AzureKeyVaultOptions.cs` in `Configurations/`
> * Azure NuGet packages installed (check `.csproj`)
> * Key Vault config block in `Program.cs`
> * `appsettings.json` and `appsettings.Development.json` configured
> * Local MongoDB running (`docker-compose`)
> * `dotnet run` shows “Using MongoDB repository” and app works at `http://localhost:5292`
>
> You can pause here and resume later. Your Azure resources will persist.

### Phase II: Exploring Configuration Precedence

Now that you have both a local MongoDB (Docker) and a remote Cosmos DB with its connection string stored in Key Vault, you can demonstrate how each configuration layer overrides the previous one.

### Step 8: Configuration Baseline — appsettings Files

1. First, verify your `appsettings.Development.json` has `UseAzureKeyVault` set to `false` (Key Vault is not needed for local development):

   ```
   "FeatureFlags": {
     "UseMongoDb": true,
     "UseAzureStorage": false,
     "UseAzureKeyVault": false
   }
   ```
2. Run the app in **Production** mode to see the base `appsettings.json` take effect (all flags false):

   ```
   ASPNETCORE_ENVIRONMENT=Production dotnet run
   ```
3. Check the console output — you should see:

   * `Using in-memory repository`

   This confirms the base `appsettings.json` is loaded with `UseMongoDb: false`.
4. Stop the application (`Ctrl+C`) and run it normally (Development environment):

   ```
   dotnet run
   ```
5. Check the console output — you should now see:

   * `Using MongoDB repository`

   This confirms `appsettings.Development.json` has overridden the base config, setting `UseMongoDb: true` and providing the local Docker MongoDB connection string.
6. Open the application in your browser and add a test subscriber via the Subscribe page. Then navigate to the Subscribers page and verify the subscriber appears. This data is stored in your local Docker MongoDB.

> 💡 **Information**
>
> * You have just demonstrated layers 1 and 2 of the configuration precedence:
>   + **Layer 1**: `appsettings.json` — all flags `false`, placeholder connection string
>   + **Layer 2**: `appsettings.Development.json` — overrides `UseMongoDb` to `true` and provides the local connection string
> * The environment-specific file always wins over the base file for any keys they share

### Step 9: Overriding Configuration with User Secrets

User secrets let you override configuration values **without modifying any files in your project**. The secret values are stored outside the project directory, so they are never accidentally committed to source control.

1. Initialize user secrets for the project:

   ```
   dotnet user-secrets init
   ```

   This adds a `UserSecretsId` to your `.csproj` file and creates a `secrets.json` file in a platform-specific location outside your project tree.
2. Set a **wrong** MongoDB connection string via user secrets to prove they override `appsettings.Development.json`:

   ```
   dotnet user-secrets set "MongoDb:ConnectionString" "mongodb://root:wrongpassword@localhost:27017"
   ```
3. Run the application:

   ```
   dotnet run
   ```
4. The application starts but you should see a MongoDB authentication error in the console or when navigating to the Subscribers page. This proves the user secret has overridden the correct connection string from `appsettings.Development.json`.
5. Now override the user secret with an **environment variable**. Environment variables take precedence over user secrets:

   ```
   MongoDb__ConnectionString="mongodb://root:example@localhost:27017" dotnet run
   ```
6. The application should work again — the Subscribers page loads and your earlier test subscriber is still there. This proves the environment variable has overridden the broken user secret value.

   > 💡 **Information**
   >
   > * Environment variables use `__` (double underscore) as the section separator instead of `:`. So `MongoDb:ConnectionString` becomes `MongoDb__ConnectionString`
   > * This is because `:` is not valid in environment variable names on all platforms
7. Clean up the user secret so it doesn’t interfere with later steps:

   ```
   dotnet user-secrets remove "MongoDb:ConnectionString"
   ```
8. Verify it’s removed:

   ```
   dotnet user-secrets list
   ```

> 💡 **Information**
>
> * User secrets are the recommended way to manage sensitive values during **local development** — they keep secrets out of your source tree and away from version control
> * Azure Key Vault solves the same problem for **production** — centralised, secure, access-controlled secret storage
> * The precedence layers you have now demonstrated:
>   + **Layer 1**: `appsettings.json` (base)
>   + **Layer 2**: `appsettings.Development.json` (overrides base)
>   + **Layer 3**: User secrets (overrides appsettings files)
>   + **Layer 4**: Environment variables (overrides user secrets)

### Step 10: Azure Key Vault Overrides Everything

Azure Key Vault is the highest-precedence configuration provider in our application because it is registered last in `Program.cs`. This step demonstrates that Key Vault values override all local configuration.

1. Ensure your Azure CLI is logged in:

   ```
   az login
   ```
2. Temporarily enable Key Vault in `appsettings.Development.json` and set the correct Key Vault URI:

   ```
   "FeatureFlags": {
     "UseMongoDb": true,
     "UseAzureStorage": false,
     "UseAzureKeyVault": true
   },
   "AzureKeyVault": {
     "KeyVaultUri": "https://cloudsoftkv{SUFFIX}.vault.azure.net/"
   }
   ```

   Replace `{SUFFIX}` with the suffix you chose in Step 0 (e.g., `cloudsoftkvjd1`).
3. Run the application:

   ```
   dotnet run
   ```
4. Check the console output — you should see:

   * `Using Azure Key Vault for configuration`
   * `Using MongoDB repository`
5. Open the Subscribers page in your browser. **The subscriber list is empty** — even though you added a subscriber earlier. This is because the Key Vault secret `MongoDb--ConnectionString` contains the **Cosmos DB** connection string, which has overridden the local Docker MongoDB connection string from `appsettings.Development.json`. You are now reading from a different database.
6. Revert `appsettings.Development.json` back to disable Key Vault:

   ```
   "FeatureFlags": {
     "UseMongoDb": true,
     "UseAzureStorage": false,
     "UseAzureKeyVault": false
   }
   ```
7. Run the application again:

   ```
   dotnet run
   ```
8. Open the Subscribers page — your earlier test subscriber **reappears**. The app is back to using local Docker MongoDB.

> 💡 **Information**
>
> * This is the full precedence stack in action. The Key Vault secret for `MongoDb:ConnectionString` silently overrode the value from `appsettings.Development.json` — no code changes needed, just a configuration provider registered in the right order
> * In `Program.cs`, Key Vault is added via `builder.Configuration.AddAzureKeyVault(...)` **after** the default providers, which is why it has the highest precedence
> * In production on the Azure VM, Key Vault will always be enabled and the VM’s managed identity handles authentication — no credentials stored anywhere

> **Rollback — Phase II**
>
> Phase II only changes local configuration — no Azure resources are created or modified:
>
> * **Broken user secrets:** `dotnet user-secrets clear` removes all user secrets for the project.
> * **appsettings.Development.json in a bad state:** Set `UseAzureKeyVault` back to `false` and restore the connection string to `mongodb://root:example@localhost:27017`.
> * **Azure CLI not authenticated:** Run `az login` again.
> * **Key Vault returns 403:** Verify your RBAC assignment with `az role assignment list --scope $(az keyvault show --name cloudsoftkv<suffix> --resource-group AzureKeyVaultResourceGroup --query id -o tsv) --output table`.

> **Checkpoint — Phase II Complete**
>
> You have now demonstrated all configuration precedence layers:
>
> * `appsettings.json` base config (in-memory repository)
> * `appsettings.Development.json` override (local Docker MongoDB)
> * User secrets override (wrong password broke connection)
> * Environment variable override (correct password restored it)
> * Azure Key Vault override (switched to Cosmos DB, empty subscriber list)
> * After reverting, local Docker MongoDB data reappeared
> * User secrets cleaned up (`dotnet user-secrets list` shows nothing)
> * `UseAzureKeyVault` set back to `false` in Development config
>
> You can pause here. No Azure state was changed in Phase II.

### Phase III: Deploying to Azure VM with Managed Identity

### Step 11: Provision an Azure Virtual Machine

1. Create a cloud-init file for VM provisioning:

   > `infrastructure/cloud-init_dotnet.yaml`

   ```
   #cloud-config

   # Update the package list
   package_update: true

   runcmd:
     # Install .NET Runtime 10.0
     - add-apt-repository ppa:dotnet/backports
     - apt-get update
     - apt-get install -y aspnetcore-runtime-10.0

     # Create the /opt/CloudSoft directory
     - mkdir -p /opt/CloudSoft
     - chown azureuser:azureuser /opt/CloudSoft

     # Enable and start the service
     - systemctl daemon-reload
     - systemctl enable CloudSoft.service

   # Create systemd service and environment file
   write_files:
     - path: /etc/systemd/system/CloudSoft.service
       content: |
         [Unit]
         Description=ASP.NET Web App running on Ubuntu

         [Service]
         WorkingDirectory=/opt/CloudSoft
         ExecStart=/usr/bin/dotnet /opt/CloudSoft/CloudSoft.dll
         Restart=always
         RestartSec=10
         KillSignal=SIGINT
         SyslogIdentifier=CloudSoft
         User=www-data
         EnvironmentFile=/etc/CloudSoft/.env

         [Install]
         WantedBy=multi-user.target      
       owner: root:root
       permissions: '0644'

     - path: /etc/CloudSoft/.env
       content: |
         ASPNETCORE_ENVIRONMENT=Production
         ASPNETCORE_URLS=http://+:5000      
       owner: root:root
       permissions: '0600'
   ```
2. Create a VM deployment script:

   > `infrastructure/provision_vm.sh`

   ```
   #!/bin/bash
   set -euo pipefail

   SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

   resource_group="AzureKeyVaultResourceGroup"
   vm_name="CloudSoftVM"
   vm_port=5000

   echo "Creating VM '$vm_name' with cloud-init..."
   az vm create \
       --resource-group "$resource_group" \
       --name "$vm_name" \
       --image Ubuntu2404 \
       --size Standard_B1s \
       --generate-ssh-keys \
       --admin-username azureuser \
       --custom-data @"$SCRIPT_DIR/cloud-init_dotnet.yaml"

   echo "Opening port $vm_port..."
   az vm open-port \
       --port "$vm_port" \
       --resource-group "$resource_group" \
       --name "$vm_name"

   # Get public IP
   vm_pub_ip=$(az vm show \
       --resource-group "$resource_group" \
       --name "$vm_name" \
       --show-details \
       --query publicIps \
       --output tsv)

   echo ""
   echo "VM provisioned successfully!"
   echo "Public IP: $vm_pub_ip"
   echo "SSH:       ssh azureuser@$vm_pub_ip"
   echo "App URL:   http://$vm_pub_ip:$vm_port"
   ```
3. Make the script executable and run it:

   ```
   chmod +x infrastructure/provision_vm.sh
   bash infrastructure/provision_vm.sh
   ```

### Step 12: Create Production Settings

1. Create an `appsettings.Production.json` file:

   > `src/appsettings.Production.json`

   ```
   {
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "FeatureFlags": {
       "UseMongoDb": false,
       "UseAzureStorage": false,
       "UseAzureKeyVault": false
     },
     "MongoDb": {
       "ConnectionString": "Get from Key Vault",
       "DatabaseName": "cloudsoft",
       "SubscribersCollectionName": "subscribers"
     },
     "AzureKeyVault": {
       "KeyVaultUri": "https://cloudsoftkv{SUFFIX}.vault.azure.net/"
     }
   }
   ```

   Replace `{SUFFIX}` with the suffix you chose in Step 0 (e.g., `cloudsoftkvjd1`).

### Step 13: Create Deployment Script

1. Create a deployment script:

   > `3-deploy-app.sh`

   ```
   #!/bin/bash
   set -euo pipefail

   PROJECT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

   resource_group="AzureKeyVaultResourceGroup"
   vm_name="CloudSoftVM"
   vm_port=5000

   # Get public IP
   vm_pub_ip=$(az vm show \
     --resource-group "$resource_group" \
     --name "$vm_name" \
     --show-details \
     --query publicIps \
     --output tsv)

   # Publish the application
   echo "Publishing application..."
   dotnet publish "$PROJECT_DIR/src/CloudSoft.csproj" --configuration Release --output "$PROJECT_DIR/publish"

   # Stop the service before copying files
   echo "Stopping the service..."
   ssh azureuser@${vm_pub_ip} "sudo systemctl stop CloudSoft.service" || true

   # Copy files to VM
   echo "Copying files to VM..."
   scp -r "$PROJECT_DIR"/publish/* azureuser@${vm_pub_ip}:/opt/CloudSoft/

   # Start service
   echo "Starting service..."
   ssh azureuser@${vm_pub_ip} "sudo systemctl start CloudSoft.service"

   # Cleanup
   rm -rf "$PROJECT_DIR/publish"

   # Browser URL
   echo "Deployment complete! Application is running at:"
   echo "http://$vm_pub_ip:$vm_port"
   ```

   > 💡 **Information**
   >
   > * `|| true` after the `systemctl stop` command prevents the script from failing on the **first deployment**, when the service hasn’t been started yet and there is nothing to stop. The `||` operator means “if the previous command fails, run the next command instead” — in this case, `true` simply succeeds and the script continues.
   > * This is particularly important because we use `set -euo pipefail` at the top of the script, which causes bash to exit on any command failure. Without `|| true`, the first deployment would abort at the stop step.
2. Make the script executable and run it to deploy the application:

   ```
   chmod +x 3-deploy-app.sh
   ./3-deploy-app.sh
   ```
3. Verify that the application is running on the VM with the feature flags disabled:

   ```
   curl http://VM_IP_ADDRESS:5000
   ```

### Step 14: Configure Managed Identity for Key Vault Access

1. Enable system-assigned managed identity for the VM:

   ```
   az vm identity assign \
     --resource-group AzureKeyVaultResourceGroup \
     --name CloudSoftVM
   ```
2. Get the VM’s principal ID:

   ```
   VM_PRINCIPAL_ID=$(az vm identity show \
     --resource-group AzureKeyVaultResourceGroup \
     --name CloudSoftVM \
     --query principalId \
     --output tsv)
   ```
3. Assign the “Key Vault Secrets User” role to the VM:

   ```
   VAULT_ID=$(az keyvault show \
     --name "cloudsoftkv<suffix>" \
     --resource-group "AzureKeyVaultResourceGroup" \
     --query id -o tsv)

   az role assignment create \
     --assignee "$VM_PRINCIPAL_ID" \
     --role "Key Vault Secrets User" \
     --scope "$VAULT_ID"
   ```

   > 💡 **Information**
   >
   > * The “Key Vault Secrets User” role allows the VM to read secrets but not modify them
   > * This follows the principle of least privilege
   > * The scope limits access to just this specific Key Vault

### Step 15: Update Production Settings and Deploy

1. Update `appsettings.Production.json` to enable MongoDB and Azure Key Vault:

   > `src/appsettings.Production.json`

   ```
   {
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "FeatureFlags": {
       "UseMongoDb": true,
       "UseAzureStorage": false,
       "UseAzureKeyVault": true
     },
     "MongoDb": {
       "ConnectionString": "Get from Key Vault",
       "DatabaseName": "cloudsoft",
       "SubscribersCollectionName": "subscribers"
     },
     "AzureKeyVault": {
       "KeyVaultUri": "https://cloudsoftkv{SUFFIX}.vault.azure.net/"
     }
   }
   ```

   Replace `{SUFFIX}` with the suffix you chose in Step 0 (e.g., `cloudsoftkvjd1`).
2. Deploy the updated application:

   ```
   ./3-deploy-app.sh
   ```
3. Verify that the application is now successfully connecting to Cosmos DB using the connection string from Key Vault:

   ```
   ssh azureuser@VM_IP_ADDRESS "sudo journalctl -u CloudSoft.service -n 100"
   ```

> **Rollback — Phase III**
>
> * **VM won’t create:** Check your Azure subscription quota for the region. Try a different VM size if `Standard_B1s` is unavailable.
> * **SSH connection refused:** Wait 2-3 minutes after VM creation for cloud-init to complete. Check with: `az vm get-instance-view --resource-group AzureKeyVaultResourceGroup --name CloudSoftVM --query "instanceView.statuses[1]" -o table`.
> * **App deploys but won’t start:** SSH in and check logs: `sudo journalctl -u CloudSoft.service -n 100`. Common issues: .NET runtime not installed yet (cloud-init still running), wrong Key Vault URI, feature flags not enabled.
> * **Managed identity 403 on Key Vault:** RBAC assignments can take up to 5 minutes to propagate. Wait and restart: `sudo systemctl restart CloudSoft.service`.
> * **Start completely over:** Run `bash 4-teardown.sh`, wait for deletion, then re-run from Step 11.

> **Checkpoint — Phase III Complete**
>
> * VM `CloudSoftVM` is running with a public IP
> * VM has system-assigned managed identity
> * VM’s identity has “Key Vault Secrets User” role on your Key Vault
> * `appsettings.Production.json` has `UseMongoDb: true`, `UseAzureKeyVault: true`, and correct Key Vault URI
> * App deployed and running on VM port 5000
> * `curl http://<VM_IP>:5000` returns 200
> * Logs show “Using Azure Key Vault for configuration” and “Using MongoDB repository”
>
> Run `bash 5-verify.sh` for automated verification, or `bash 4-teardown.sh` to clean up.

## Configuration Precedence Quick Reference

As demonstrated in Phase II (Steps 8–10), this is the precedence order in our application (lowest to highest priority):

| Priority | Provider | Exercise step | Example |
| --- | --- | --- | --- |
| 1 | `appsettings.json` | Step 8 | Base config, all flags `false` |
| 2 | `appsettings.{Environment}.json` | Step 8 | `UseMongoDb: true`, local connection string |
| 3 | User secrets | Step 9 | `dotnet user-secrets set "MongoDb:ConnectionString" "..."` |
| 4 | Environment variables | Step 9 | `MongoDb__ConnectionString="..." dotnet run` |
| 5 | Command-line arguments | — | `dotnet run --MongoDb:ConnectionString="..."` |
| 6 | Azure Key Vault | Step 10 | Secret `MongoDb--ConnectionString` overrides all above |

Key Vault has the highest precedence because it is registered last in `Program.cs` via `builder.Configuration.AddAzureKeyVault(...)`, after all default providers have been added.

## Managed Identity Authentication Flow

When using managed identity on the VM, the authentication flow works as follows:

1. The application uses `DefaultAzureCredential()`, which attempts multiple authentication methods in a specific order.
2. On the VM with managed identity, it accesses an Azure Instance Metadata Service endpoint on the VM.
3. This service provides an access token for the VM’s identity to access Azure services.
4. Azure Key Vault accepts this token and grants access to secrets according to the assigned RBAC permissions.
5. The connection string is securely retrieved and used to connect to Cosmos DB.

This approach eliminates the need for storing credentials in the application or on the VM, significantly enhancing security.

## Final Tests

To verify everything is working correctly:

1. Check VM logs for successful startup:

   ```
   ssh azureuser@VM_IP_ADDRESS "sudo journalctl -u CloudSoft.service -n 100"
   ```
2. Look for these log lines:

   * “Using Azure Key Vault for configuration”
   * “Using MongoDB repository”
3. Test the application’s functionality that depends on the database:

   * Navigate to the subscriber form
   * Add a new subscriber
   * Verify the subscriber is saved to Cosmos DB
4. For full verification, you can check Cosmos DB in the Azure Portal to see that data is being stored correctly.
5. Run the automated verification script:

   ```
   bash 5-verify.sh
   ```

## Troubleshooting

### Common Issues and Solutions

1. **Connection String Format Issues**

   * Cosmos DB connection strings are formatted differently from standard MongoDB strings
   * Ensure SSL is enabled and retrywrites is set correctly
   * Verify that the appName parameter is correctly set
2. **Managed Identity Problems**

   * Check that the VM has a system-assigned identity
   * Verify RBAC role assignments with `az role assignment list`
   * Ensure the VM has been restarted after enabling managed identity
3. **Key Vault Access Issues**

   * Check Key Vault firewall settings
   * Verify the Key Vault URI is correct in the application settings
   * Ensure the application has appropriate permissions
4. **Default Credential Authentication Problems**

   * In development: Verify Azure CLI login status
   * In production: Check VM’s managed identity status
   * Review application logs for authentication errors
5. **Application Cannot Find Secrets**

   * Secret names in Key Vault use `--` instead of `:`
   * Verify secret names match configuration paths (e.g., `MongoDb--ConnectionString`)

### Diagnostic Commands

```
# Check VM managed identity
az vm identity show --resource-group AzureKeyVaultResourceGroup --name CloudSoftVM

# Check role assignments
az role assignment list --assignee VM_PRINCIPAL_ID

# Check Key Vault access policies
az keyvault show --name "cloudsoftkv<suffix>" --resource-group "AzureKeyVaultResourceGroup"

# Test Key Vault access from VM
ssh azureuser@VM_IP_ADDRESS "curl 'http://169.254.169.254/metadata/identity/oauth2/token?api-version=2018-02-01&resource=https%3A%2F%2Fvault.azure.net' -H 'Metadata: true'"
```

## Done! 🎉

Congratulations! You’ve successfully implemented Azure Key Vault integration with your ASP.NET Core application, deployed it to an Azure VM using managed identity, and connected securely to Cosmos DB. This secure architecture ensures your application can access sensitive configuration without exposing credentials in your code or configuration files.

### Key Concepts Learned

* **Configuration Provider System** in ASP.NET Core
* **Azure Key Vault** for secure secret management
* **Managed Identity** for passwordless authentication
* **Options Pattern** for strongly-typed configuration
* **Feature Flags** for environment-specific behavior

This pattern can be extended to secure any sensitive configuration data your application needs, such as API keys, connection strings, and other secrets. 🚀

---

# Appendix: TLDR — Getting Up and Running

If you already have the CloudSoft application code and want to get everything provisioned and deployed quickly, the scripts below cover the entire exercise. They are located in the `infrastructure/` directory.

> **Prerequisites:** Azure CLI logged in (`az login`), .NET 10.0 SDK, Docker running with local MongoDB (`docker-compose -f infrastructure/docker-compose.yml up -d`), SSH keys available.

Run them in order, or use `2-provision-configure-all.sh` (in the project root) to run steps 1–4 automatically.

## Script Overview

| Script | Exercise Steps | Purpose |
| --- | --- | --- |
| `config.sh.template` | Step 0 | Student suffix configuration (copy to `config.sh`) |
| `docker-compose.yml` | Step 6 | Local MongoDB + Mongo Express for development |
| `2-provision-configure-all.sh` | — | Master script (project root) that runs steps 1–4 in order |
| `provision_cosmosdb.sh` | Step 1 | Creates Cosmos DB with MongoDB API |
| `provision_keyvault.sh` | Steps 2, 7 | Creates Key Vault, assigns admin role, stores connection string |
| `provision_vm.sh` | Step 11 | Creates Ubuntu VM with cloud-init |
| `cloud-init_dotnet.yaml` | Step 11 | VM bootstrap: installs .NET 10, creates systemd service |
| `setup_managed_identity.sh` | Step 14 | Enables managed identity, assigns Key Vault Secrets User role |
| `3-deploy-app.sh` | Step 13 | Publishes app, copies to VM, starts service |
| `4-teardown.sh` | — | Deletes the entire resource group and all resources |
| `5-verify.sh` | — | Automated verification of local and Azure environment |

## Quick Start

```
# 0. Set your student suffix
cp infrastructure/config.sh.template infrastructure/config.sh
# Edit infrastructure/config.sh and set STUDENT_SUFFIX="<your-suffix>"

# 1. Start local MongoDB for development
docker-compose -f infrastructure/docker-compose.yml up -d

# 2. Provision all Azure resources (Cosmos DB, Key Vault, VM, managed identity)
bash 2-provision-configure-all.sh

# 3. Update appsettings.Production.json with the Key Vault URI printed by the script
#    Set UseMongoDb=true and UseAzureKeyVault=true

# 4. Deploy the application to the VM
bash 3-deploy-app.sh

# 5. Verify everything works
bash 5-verify.sh
```

## docker-compose.yml — Local MongoDB (Step 6)

Runs MongoDB and Mongo Express locally for development.

> `infrastructure/docker-compose.yml`

```
services:
  mongodb:
    image: mongo:latest
    container_name: mongodb
    restart: always
    ports:
      - "27017:27017"
    environment:
      MONGO_INITDB_ROOT_USERNAME: ${MONGO_USERNAME:-root}
      MONGO_INITDB_ROOT_PASSWORD: ${MONGO_PASSWORD:-example}
    volumes:
      - mongodb_data:/data/db
    networks:
      - mongo_network

  mongo-express:
    image: mongo-express:latest
    container_name: mongo-express
    restart: always
    ports:
      - "8081:8081"
    environment:
      ME_CONFIG_MONGODB_ADMINUSERNAME: ${MONGO_USERNAME:-root}
      ME_CONFIG_MONGODB_ADMINPASSWORD: ${MONGO_PASSWORD:-example}
      ME_CONFIG_MONGODB_SERVER: mongodb
    depends_on:
      - mongodb
    networks:
      - mongo_network

volumes:
  mongodb_data:

networks:
  mongo_network:
    driver: bridge
```

## 2-provision-configure-all.sh — Master Orchestration

Runs all provisioning scripts in the correct dependency order.

> `2-provision-configure-all.sh` (project root)

```
#!/bin/bash
set -euo pipefail

# =============================================================================
# CloudSoft — Master Provisioning Script
# Provisions all Azure resources in the correct order.
# Resource Group: "AzureKeyVaultResourceGroup"
# =============================================================================

PROJECT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
INFRA_DIR="$PROJECT_DIR/infrastructure"

# Validate student configuration
if [ ! -f "$INFRA_DIR/config.sh" ]; then
    echo "ERROR: infrastructure/config.sh not found."
    echo "Copy infrastructure/config.sh.template to infrastructure/config.sh and set your STUDENT_SUFFIX."
    exit 1
fi
source "$INFRA_DIR/config.sh"
if [ -z "$STUDENT_SUFFIX" ]; then
    echo "ERROR: STUDENT_SUFFIX is empty in infrastructure/config.sh."
    exit 1
fi

echo "========================================="
echo "CloudSoft — Azure Resource Provisioning"
echo "Student suffix: $STUDENT_SUFFIX"
echo "========================================="
echo ""

# Step 1: Cosmos DB
echo "[1/4] Provisioning Cosmos DB..."
bash "$INFRA_DIR/provision_cosmosdb.sh"
echo ""

# Step 2: Key Vault
echo "[2/4] Provisioning Key Vault..."
bash "$INFRA_DIR/provision_keyvault.sh"
echo ""

# Step 3: Virtual Machine
echo "[3/4] Provisioning Virtual Machine..."
bash "$INFRA_DIR/provision_vm.sh"
echo ""

# Step 4: Managed Identity
echo "[4/4] Setting up Managed Identity..."
bash "$INFRA_DIR/setup_managed_identity.sh"
echo ""

echo "========================================="
echo "All resources provisioned successfully!"
echo "========================================="
echo ""
echo "Next steps:"
echo "  1. Update appsettings.Production.json with the Key Vault URI above"
echo "  2. Enable feature flags (UseMongoDb=true, UseAzureKeyVault=true)"
echo "  3. Run: bash 3-deploy-app.sh"
```

## provision\_cosmosdb.sh — Cosmos DB (Step 1)

Creates the resource group and a Cosmos DB account with MongoDB API.

> `infrastructure/provision_cosmosdb.sh`

```
#!/bin/bash
set -euo pipefail

# =============================================================================
# CloudSoft — Provision Cosmos DB (MongoDB API)
# =============================================================================

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/config.sh"

if [ -z "$STUDENT_SUFFIX" ]; then
    echo "ERROR: STUDENT_SUFFIX is not set."
    echo "Copy infrastructure/config.sh.template to infrastructure/config.sh and set your suffix."
    exit 1
fi

resource_group="AzureKeyVaultResourceGroup"
db_name="cloudsoft-mongodb-${STUDENT_SUFFIX}"

echo "Creating resource group '$resource_group'..."
az group create --location northeurope --name "$resource_group"

echo "Creating Cosmos DB account '$db_name' (this may take ~5 minutes)..."
az cosmosdb create \
    --name "$db_name" \
    --resource-group "$resource_group" \
    --kind MongoDB \
    --capabilities EnableServerless \
    --default-consistency-level Session \
    --server-version 7.0

echo "Retrieving connection string..."
CONNECTION_STRING=$(az cosmosdb keys list \
    --name "$db_name" \
    --resource-group "$resource_group" \
    --type connection-strings \
    --query "connectionStrings[?description=='Primary MongoDB Connection String'].connectionString" \
    --output tsv)

echo ""
echo "Cosmos DB provisioned successfully!"
echo "Connection String: $CONNECTION_STRING"
echo ""
echo "This connection string will be stored in Key Vault by provision_keyvault.sh"
```

## provision\_keyvault.sh — Key Vault + Secret Storage (Steps 2, 7)

Creates Key Vault with RBAC authorization, assigns admin role, retrieves the Cosmos DB connection string and stores it as a secret.

> `infrastructure/provision_keyvault.sh`

```
#!/bin/bash
set -euo pipefail

# =============================================================================
# CloudSoft — Provision Key Vault + Store Cosmos DB Secret
# =============================================================================

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/config.sh"

if [ -z "$STUDENT_SUFFIX" ]; then
    echo "ERROR: STUDENT_SUFFIX is not set."
    echo "Copy infrastructure/config.sh.template to infrastructure/config.sh and set your suffix."
    exit 1
fi

resource_group="AzureKeyVaultResourceGroup"
vault_name="cloudsoftkv${STUDENT_SUFFIX}"
db_name="cloudsoft-mongodb-${STUDENT_SUFFIX}"

echo "Creating Key Vault '$vault_name'..."
az keyvault create \
    --name "$vault_name" \
    --resource-group "$resource_group" \
    --location northeurope \
    --enable-rbac-authorization true

# Assign Key Vault Administrator role to current user
echo "Assigning Key Vault Administrator role to current user..."
USER_ID=$(az ad signed-in-user show --query id --output tsv)
VAULT_ID=$(az keyvault show --name "$vault_name" --resource-group "$resource_group" --query id -o tsv)

az role assignment create \
    --assignee "$USER_ID" \
    --role "Key Vault Administrator" \
    --scope "$VAULT_ID"

# Wait for RBAC propagation
echo "Waiting for RBAC propagation (30 seconds)..."
sleep 30

# Get Cosmos DB connection string
echo "Retrieving Cosmos DB connection string..."
CONNECTION_STRING=$(az cosmosdb keys list \
    --name "$db_name" \
    --resource-group "$resource_group" \
    --type connection-strings \
    --query "connectionStrings[?description=='Primary MongoDB Connection String'].connectionString" \
    --output tsv)

# Store connection string in Key Vault
echo "Storing MongoDB connection string in Key Vault..."
az keyvault secret set \
    --vault-name "$vault_name" \
    --name "MongoDb--ConnectionString" \
    --value "$CONNECTION_STRING"

# Get Key Vault URI
VAULT_URI=$(az keyvault show \
    --name "$vault_name" \
    --resource-group "$resource_group" \
    --query properties.vaultUri \
    --output tsv)

echo ""
echo "Key Vault provisioned successfully!"
echo "Key Vault URI: $VAULT_URI"
echo "Vault Name:    $vault_name"
echo ""
echo "Update appsettings.Production.json with:"
echo "  \"KeyVaultUri\": \"$VAULT_URI\""
```

## provision\_vm.sh — Azure VM (Step 9)

Creates an Ubuntu 24.04 VM with cloud-init and opens port 5000.

> `infrastructure/provision_vm.sh`

```
#!/bin/bash
set -euo pipefail

# =============================================================================
# CloudSoft — Provision Azure VM
# =============================================================================

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

resource_group="AzureKeyVaultResourceGroup"
vm_name="CloudSoftVM"
vm_port=5000

echo "Creating VM '$vm_name' with cloud-init..."
az vm create \
    --resource-group "$resource_group" \
    --name "$vm_name" \
    --image Ubuntu2404 \
    --size Standard_B1s \
    --generate-ssh-keys \
    --admin-username azureuser \
    --custom-data @"$SCRIPT_DIR/cloud-init_dotnet.yaml"

echo "Opening port $vm_port..."
az vm open-port \
    --port "$vm_port" \
    --resource-group "$resource_group" \
    --name "$vm_name"

# Get public IP
vm_pub_ip=$(az vm show \
    --resource-group "$resource_group" \
    --name "$vm_name" \
    --show-details \
    --query publicIps \
    --output tsv)

echo ""
echo "VM provisioned successfully!"
echo "Public IP: $vm_pub_ip"
echo "SSH:       ssh azureuser@$vm_pub_ip"
echo "App URL:   http://$vm_pub_ip:$vm_port"
```

## cloud-init\_dotnet.yaml — VM Bootstrap (Step 9)

Installs .NET 10 runtime, creates the application directory, and sets up the systemd service.

> `infrastructure/cloud-init_dotnet.yaml`

```
#cloud-config

# Update the package list
package_update: true

runcmd:
  # Install .NET Runtime 10.0
  - add-apt-repository ppa:dotnet/backports
  - apt-get update
  - apt-get install -y aspnetcore-runtime-10.0

  # Create the /opt/CloudSoft directory
  - mkdir -p /opt/CloudSoft
  - chown azureuser:azureuser /opt/CloudSoft

  # Enable and start the service
  - systemctl daemon-reload
  - systemctl enable CloudSoft.service

# Create systemd service and environment file
write_files:
  - path: /etc/systemd/system/CloudSoft.service
    content: |
      [Unit]
      Description=ASP.NET Web App running on Ubuntu

      [Service]
      WorkingDirectory=/opt/CloudSoft
      ExecStart=/usr/bin/dotnet /opt/CloudSoft/CloudSoft.dll
      Restart=always
      RestartSec=10
      KillSignal=SIGINT
      SyslogIdentifier=CloudSoft
      User=www-data
      EnvironmentFile=/etc/CloudSoft/.env

      [Install]
      WantedBy=multi-user.target      
    owner: root:root
    permissions: '0644'

  - path: /etc/CloudSoft/.env
    content: |
      ASPNETCORE_ENVIRONMENT=Production
      ASPNETCORE_URLS=http://+:5000      
    owner: root:root
    permissions: '0600'
```

## setup\_managed\_identity.sh — Managed Identity + RBAC (Step 14)

Enables system-assigned managed identity on the VM and grants it the Key Vault Secrets User role.

> `infrastructure/setup_managed_identity.sh`

```
#!/bin/bash
set -euo pipefail

# =============================================================================
# CloudSoft — Setup Managed Identity + Key Vault RBAC
# =============================================================================

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/config.sh"

if [ -z "$STUDENT_SUFFIX" ]; then
    echo "ERROR: STUDENT_SUFFIX is not set."
    echo "Copy infrastructure/config.sh.template to infrastructure/config.sh and set your suffix."
    exit 1
fi

resource_group="AzureKeyVaultResourceGroup"
vm_name="CloudSoftVM"
vault_name="cloudsoftkv${STUDENT_SUFFIX}"

echo "Enabling system-assigned managed identity on VM '$vm_name'..."
az vm identity assign \
    --resource-group "$resource_group" \
    --name "$vm_name"

echo "Retrieving VM principal ID..."
VM_PRINCIPAL_ID=$(az vm identity show \
    --resource-group "$resource_group" \
    --name "$vm_name" \
    --query principalId \
    --output tsv)

echo "Assigning 'Key Vault Secrets User' role to VM..."
VAULT_ID=$(az keyvault show --name "$vault_name" --resource-group "$resource_group" --query id -o tsv)

az role assignment create \
    --assignee "$VM_PRINCIPAL_ID" \
    --role "Key Vault Secrets User" \
    --scope "$VAULT_ID"

echo ""
echo "Managed identity configured successfully!"
echo "VM Principal ID: $VM_PRINCIPAL_ID"
echo "The VM can now read secrets from Key Vault '$vault_name'"
```

## 3-deploy-app.sh — Deploy to VM (Step 13)

Publishes the application, copies it to the VM, and starts the systemd service.

> `3-deploy-app.sh` (project root)

```
#!/bin/bash
set -euo pipefail

# =============================================================================
# CloudSoft — Deploy Application to Azure VM
# =============================================================================

PROJECT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

resource_group="AzureKeyVaultResourceGroup"
vm_name="CloudSoftVM"
vm_port=5000

# Get public IP
vm_pub_ip=$(az vm show \
    --resource-group "$resource_group" \
    --name "$vm_name" \
    --show-details \
    --query publicIps \
    --output tsv)

# Publish the application
echo "Publishing application..."
dotnet publish "$PROJECT_DIR/src/CloudSoft.csproj" --configuration Release --output "$PROJECT_DIR/publish"

# Stop the service before copying files
echo "Stopping the service..."
ssh azureuser@"${vm_pub_ip}" "sudo systemctl stop CloudSoft.service" || true

# Copy files to VM
echo "Copying files to VM..."
scp -r "$PROJECT_DIR"/publish/* azureuser@"${vm_pub_ip}":/opt/CloudSoft/

# Start service
echo "Starting service..."
ssh azureuser@"${vm_pub_ip}" "sudo systemctl start CloudSoft.service"

# Cleanup
rm -rf "$PROJECT_DIR/publish"

echo ""
echo "Deployment complete!"
echo "Application: http://$vm_pub_ip:$vm_port"
echo ""
echo "Check logs with:"
echo "  ssh azureuser@$vm_pub_ip \"sudo journalctl -u CloudSoft.service -n 50\""
```

## 4-teardown.sh — Delete All Resources

Deletes the entire resource group and everything in it. Use when you’re done with the exercise.

> `4-teardown.sh` (project root)

```
#!/bin/bash
set -euo pipefail

# =============================================================================
# CloudSoft — Tear Down All Azure Resources
# =============================================================================

resource_group="AzureKeyVaultResourceGroup"

echo "WARNING: This will delete the resource group '$resource_group'"
echo "         and ALL resources within it (VM, Cosmos DB, Key Vault, etc.)"
echo ""
read -p "Are you sure? (yes/no): " confirmation

if [ "$confirmation" != "yes" ]; then
    echo "Teardown cancelled."
    exit 0
fi

echo "Deleting resource group '$resource_group'..."
az group delete --name "$resource_group" --yes --no-wait

echo ""
echo "Resource group deletion initiated (running in background)."
echo "Check status with:"
echo "  az group show --name \"$resource_group\" --query properties.provisioningState -o tsv"
```

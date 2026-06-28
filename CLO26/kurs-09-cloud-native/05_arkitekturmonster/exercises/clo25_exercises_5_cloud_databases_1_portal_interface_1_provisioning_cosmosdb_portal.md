---

title: 1. Provisioning CosmosDB via Portal
author: Marcus Ackre Medina
type: exercise
topic: cloud
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/exercises/5-cloud-databases/1-portal-interface/1-provisioning-cosmosdb-portal.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-"
tags: ["azure", "cloud", "cosmosdb", "docker", "dotnet", "exercise", "git", "iac", "networking", "portal"]
week_fit: []
---

Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-
Server Foundation-
Network Foundation-
Deployment-
Cloud Databases--
1. Portal Interface--- 1. Provisioning CosmosDB via Portal--
2. Command Line Interface--
3. ARM and Bicep--
4. Connecting from Code--
5. Azure Blob Storage-
Webapp Development-
Code Collaboration-
DockerTutorials

# 1. Provisioning CosmosDB via Portal

🟡


# Provisioning CosmosDB via Portal

## Goal

Create an Azure Cosmos DB account with the MongoDB API through the Azure Portal, set up a database and collection, explore the Data Explorer, and retrieve the connection string for application use.

> **What you’ll learn:**
>
> * How to create a Cosmos DB account with MongoDB API compatibility
> * How to choose serverless capacity mode to minimize costs
> * How to create databases and collections in Cosmos DB
> * How to navigate and use Data Explorer to manage data
> * How to locate and copy the connection string for application use
> * Key differences between Cosmos DB and native MongoDB

## Prerequisites

> **Before starting, ensure you have:**
>
> * ✓ Active Azure subscription with resource creation permissions
> * ✓ Familiarity with the Azure Portal (or completed Section 1: Server Foundation)
> * ✓ Basic understanding of databases and JSON document structure

## Exercise Steps

### Overview

1. **Create a Cosmos DB Account**
2. **Create a Database**
3. **Create a Collection**
4. **Insert a Test Document**
5. **Retrieve the Connection String**
6. **Explore Data Explorer Features**

### **Step 1:** Create a Cosmos DB Account

Set up an Azure Cosmos DB account configured with the MongoDB API. Cosmos DB is Microsoft’s globally distributed, multi-model database service. By selecting the MongoDB API, you get a cloud-hosted database that speaks the MongoDB wire protocol — meaning existing MongoDB drivers and tools work with minimal changes. Choosing serverless capacity mode ensures you only pay for what you use, which is ideal for learning and development.

1. **Navigate to** the Azure Portal at <https://portal.azure.com>
2. **Search for** “Azure Cosmos DB” using the search bar at the top
3. **Select** Azure Cosmos DB from the search results
4. **Click** the **+ Create** button
5. **Select** “Azure Cosmos DB for MongoDB” from the API options
6. **Choose** the “Request unit (RU) database account” option
7. **Configure** the Basics tab with the following settings:

   * **Subscription**: Select your subscription
   * **Resource Group**: Create new or use existing (e.g., `CloudDatabasesRG`)
   * **Account Name**: Enter a globally unique name (e.g., `cosmosdb-yourname-bcd`, lowercase letters, numbers, and hyphens only)
   * **Region**: Select `North Europe` (or a region close to you)
   * **Capacity mode**: Select `Serverless`
8. **Click** Review + Create, then **click** Create
9. **Wait** for the deployment to complete (this may take 2–5 minutes)

> ℹ **Concept Deep Dive**
>
> Cosmos DB is a fully managed NoSQL database service that supports multiple data models through different APIs: MongoDB, Cassandra, Gremlin (graph), Table, and the native NoSQL API. The MongoDB API compatibility layer allows applications to use standard MongoDB drivers and the MongoDB query language against Cosmos DB — meaning existing MongoDB application code works with minimal or no changes.
>
> Serverless capacity mode charges only for consumed Request Units (RU/s) and storage. There is no minimum charge when idle, unlike provisioned throughput which reserves and charges for RU/s continuously. This makes serverless ideal for development, learning, and workloads with intermittent traffic. A Request Unit (RU) is a normalized measure of database operation cost — a simple point-read of a 1 KB document costs approximately 1 RU.
>
> ⚠ **Common Mistakes**
>
> * The account name must be globally unique across all of Azure — if your name is taken, add a random suffix
> * Choosing “Provisioned throughput” instead of “Serverless” will incur continuous charges even when idle
> * Selecting a region far from your location increases latency for development and testing
> * Forgetting to note the resource group name makes it harder to manage or clean up resources later
>
> ✓ **Quick check:** Navigate to the Cosmos DB account overview page and verify it shows “API: MongoDB” and “Capacity mode: Serverless”

### **Step 2:** Create a Database

Create a database within your Cosmos DB account to serve as a logical container for your collections. A database in Cosmos DB is analogous to a database in MongoDB — it groups related collections together and provides a namespace for organizing your data.

1. **Navigate to** your newly created Cosmos DB account in the Azure Portal
2. **Click** “Data Explorer” in the left menu
3. **Click** the “New Database” button in the toolbar (or use the dropdown arrow next to “New Collection”)
4. **Enter** the Database id: `bookmarks_db`
5. **Click** OK to create the database

> ℹ **Concept Deep Dive**
>
> In Cosmos DB with the MongoDB API, the data hierarchy is: Account → Database → Collection → Document. This mirrors the MongoDB structure where a server hosts databases, databases contain collections, and collections hold JSON documents. The database level is primarily an organizational unit — provisioning and scaling happen at the collection or account level depending on your capacity mode.
>
> ✓ **Quick check:** The `bookmarks_db` database appears in the Data Explorer tree view on the left side

### **Step 3:** Create a Collection

Create a collection within your database to store documents. A collection is where your actual data lives — equivalent to a table in relational databases but schema-free. Each collection requires a shard key (partition key) that determines how Cosmos DB distributes data across physical partitions for scalability.

1. **Expand** the `bookmarks_db` database in Data Explorer
2. **Click** the “…” menu next to `bookmarks_db`
3. **Select** “New Collection”
4. **Configure** the collection settings:

   * **Collection id**: `bookmarks`
   * **Shard key**: `/category`
5. **Click** OK to create the collection

> ℹ **Concept Deep Dive**
>
> The shard key (partition key) is one of the most important design decisions when creating a collection. It determines how Cosmos DB distributes data across physical partitions. A good shard key has high cardinality (many distinct values) and distributes both storage and request volume evenly. For the bookmarks collection, `/category` provides reasonable distribution across values like “cloud”, “development”, “tools”, etc.
>
> In production, the partition key cannot be changed after creation — you would need to create a new collection and migrate your data. Choose carefully by considering your most frequent query patterns: queries that include the partition key in the filter are efficient single-partition queries, while queries without it require expensive cross-partition fan-out.
>
> ⚠ **Common Mistakes**
>
> * Choosing a low-cardinality shard key (like `/status` with only “active”/“inactive”) creates hot partitions
> * Using a field that doesn’t exist in most documents results in all those documents landing in the same partition
> * The shard key path is case-sensitive — `/Category` and `/category` are different
>
> ✓ **Quick check:** The `bookmarks` collection appears under `bookmarks_db` in the Data Explorer tree, showing the shard key as `/category`

### **Step 4:** Insert a Test Document

Insert a document into your collection to verify the setup and explore how Cosmos DB stores data. This step demonstrates the schema-free nature of document databases — you can insert any valid JSON without predefined schemas or migrations.

1. **Expand** `bookmarks_db` → `bookmarks` in Data Explorer
2. **Click** on “Documents” (or “Items”)
3. **Click** the “New Document” button (or “New Item”)
4. **Replace** the default JSON with the following document:

   ```
   {
       "title": "Azure Cosmos DB Documentation",
       "url": "https://learn.microsoft.com/en-us/azure/cosmos-db/",
       "category": "cloud",
       "tags": ["azure", "database", "nosql"],
       "createdAt": "2026-02-22T10:00:00Z"
   }
   ```
5. **Click** Save
6. **Observe** the additional properties that Cosmos DB automatically adds to the document: `_id`, `_rid`, `_self`, `_etag`, and `_ts`
7. **Insert** a second document with a different category to see multiple partitions in use:

   ```
   {
       "title": "MongoDB Driver for .NET",
       "url": "https://www.mongodb.com/docs/drivers/csharp/current/",
       "category": "development",
       "tags": ["mongodb", "dotnet", "driver"],
       "createdAt": "2026-02-22T10:05:00Z"
   }
   ```

> ℹ **Concept Deep Dive**
>
> Cosmos DB adds several system properties to each document. The `_id` field is the unique document identifier (auto-generated if not provided, just like MongoDB). The `_rid` is the resource identifier used internally by Cosmos DB. The `_etag` enables optimistic concurrency — if two users try to update the same document simultaneously, the etag helps detect conflicts. The `_ts` is a Unix timestamp recording the last modification time.
>
> Notice that you did not need to define a schema before inserting data. Each document in the collection can have a different structure. However, the shard key field (`category` in this case) should be present in every document for proper partition distribution.
>
> ⚠ **Common Mistakes**
>
> * Forgetting to include the shard key field (`category`) in your document — the document will still be stored but will land in a null-key partition
> * Manually setting `_id` to a value that already exists will cause a duplicate key error
> * Inserting very large documents (exceeding 2 MB) will be rejected by Cosmos DB
>
> ✓ **Quick check:** Both documents appear in the Documents view, each with system properties (`_id`, `_rid`, `_etag`, `_ts`) automatically added

### **Step 5:** Retrieve the Connection String

Locate and copy the connection string that applications will use to connect to your Cosmos DB account. This connection string uses the standard MongoDB connection format, which is what enables existing MongoDB drivers to connect to Cosmos DB transparently.

1. **Navigate to** the Cosmos DB account overview (click the account name in the breadcrumb)
2. **Click** “Connection strings” under the Settings section in the left menu
3. **Locate** the “PRIMARY CONNECTION STRING” field
4. **Click** the copy button next to the connection string to copy it to your clipboard
5. **Note** the connection string format:

   ```
   mongodb://<account>:<key>@<account>.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&...
   ```
6. **Save** the connection string in a safe, temporary location (you will need it in later exercises)

> ℹ **Concept Deep Dive**
>
> The connection string uses the standard MongoDB connection URI format (`mongodb://...`), which is what enables existing MongoDB drivers and tools to connect to Cosmos DB without code changes. Key differences from a typical MongoDB connection string:
>
> * **Port 10255**: Cosmos DB uses this port for the MongoDB API endpoint, rather than MongoDB’s default port 27017
> * **`ssl=true`**: All connections to Cosmos DB are encrypted with TLS/SSL — this is mandatory, not optional
> * **`retrywrites=false`**: Cosmos DB does not support MongoDB’s retryable writes feature in the same way native MongoDB does, so this must be explicitly disabled
>
> The connection string contains your account access key, which grants full read/write access to all databases in the account. Treat it like a password — never commit it to source code, share it in chat, or include it in screenshots. In production and in later exercises, you will use environment variables and Azure App Service configuration to manage this securely.
>
> ⚠ **Common Mistakes**
>
> * Copying the connection string with trailing whitespace can cause connection failures
> * Accidentally using the secondary connection string works fine but makes key rotation confusing later
> * Sharing the connection string publicly (e.g., in a Git commit) exposes your entire database — Azure will sometimes detect this and send security alerts
>
> ✓ **Quick check:** The connection string starts with `mongodb://` and contains `.mongo.cosmos.azure.com:10255`

### **Step 6:** Explore Data Explorer Features

Familiarize yourself with Data Explorer’s query and management capabilities. Data Explorer is your primary tool for interacting with Cosmos DB data directly from the Azure Portal — useful for debugging, ad hoc queries, and verifying that your application is storing data correctly.

1. **Navigate to** Data Explorer in the left menu
2. **Expand** `bookmarks_db` → `bookmarks` → `Documents`
3. **Try a filter query** by entering the following in the filter bar:

   ```
   { "category": "cloud" }
   ```
4. **Click** “Apply Filter” to see only documents matching the filter
5. **Try a more complex query** — filter by a nested array value:

   ```
   { "tags": "azure" }
   ```
6. **Insert** one or two more documents with different categories (e.g., `"tools"`, `"learning"`) to observe how documents distribute across partition key values
7. **Explore** the Mongo Shell (if available) at the bottom of Data Explorer — you can run MongoDB shell commands directly. First, select the database, then query the collection:

   ```
   use bookmarks_db
   db.bookmarks.find({ "category": "cloud" })
   ```

> ℹ **Concept Deep Dive**
>
> Data Explorer supports both the visual document browser and a MongoDB-compatible shell. The filter bar accepts standard MongoDB query syntax, making it familiar if you have MongoDB experience. Queries that filter on the shard key (`category`) are efficient single-partition queries. Queries without the shard key require scanning all partitions, which consumes more Request Units and is slower at scale.
>
> The Mongo Shell in Data Explorer supports a subset of MongoDB shell commands. This is useful for quick operations but for more complex scripting, you would use the `mongosh` CLI tool or connect from application code using the MongoDB driver.
>
> ✓ **Quick check:** Filter queries return the expected subset of documents, and you can see documents from different categories in the collection

## Common Issues

> **If you encounter problems:**
>
> **“Account name already exists”:** Cosmos DB account names must be globally unique. Add a random number or your initials to the name.
>
> **Deployment takes longer than 10 minutes:** This is unusual. Check the deployment status under “Deployments” in your resource group. If it fails, delete and try again with a different region.
>
> **Cannot see Data Explorer:** Make sure you are navigated to the Cosmos DB account resource, not the resource group. Data Explorer is in the left menu of the Cosmos DB account blade.
>
> **Filter query returns no results:** Ensure the field names and values match exactly (they are case-sensitive). Check that you are using double quotes for JSON strings.
>
> **Still stuck?** Verify you selected “MongoDB” as the API type when creating the account. If you chose a different API, you will need to create a new account — the API type cannot be changed after creation.

## Summary

You’ve successfully provisioned an Azure Cosmos DB account with the MongoDB API which:

* ✓ Provides a fully managed, globally distributable NoSQL database
* ✓ Uses serverless capacity to minimize costs for development
* ✓ Supports the MongoDB wire protocol for driver compatibility
* ✓ Stores schema-free JSON documents with automatic indexing

> **Key takeaway:** Cosmos DB with the MongoDB API gives you the managed infrastructure benefits of a cloud database service (automatic scaling, global distribution, built-in high availability) while maintaining compatibility with the MongoDB ecosystem. The connection string format is standard MongoDB, meaning existing MongoDB drivers and tools work without code changes — only the connection string needs to change.

## Going Deeper (Optional)

> **Want to explore more?**
>
> * Explore the “Metrics” blade to understand Request Unit consumption for different operations
> * Try the “Replicate data globally” option to see how Cosmos DB enables multi-region deployments
> * Read about Cosmos DB consistency levels and how they differ from MongoDB’s read/write concerns
> * Investigate the “Firewall and virtual networks” settings to understand network security options

## Done! 🎉

Great job! You’ve provisioned your first Azure Cosmos DB account with the MongoDB API, created a database and collection, inserted and queried documents, and retrieved the connection string. This foundation prepares you for automating the same provisioning with the Azure CLI and Bicep in the next exercises.

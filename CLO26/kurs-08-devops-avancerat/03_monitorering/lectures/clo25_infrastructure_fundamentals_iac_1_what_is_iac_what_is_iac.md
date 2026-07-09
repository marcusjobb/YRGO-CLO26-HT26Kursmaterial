Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure Fundamentals-
Compute-
Network-
Storage-
IaC-- What Is IaC-- ARM & BicepExercisesTutorials

# What Is IaC

🟡


# What Is Infrastructure as Code?

**Infrastructure as Code (IaC)** is the practice of managing and provisioning computing infrastructure through machine-readable definition files, rather than through manual processes or interactive configuration tools.

## The Problem IaC Solves

Traditional infrastructure management involves:

* **Manual configuration** through web consoles or GUIs
* **Undocumented changes** that accumulate over time
* **Configuration drift** where environments become inconsistent
* **Difficult reproduction** of environments for testing or disaster recovery
* **Error-prone processes** that don’t scale

## Core Principles

### Declarative vs Imperative

IaC tools typically use one of two approaches:

| Approach | Description | Example |
| --- | --- | --- |
| **Declarative** | Define the desired end state; the tool figures out how to achieve it | Bicep, ARM, Terraform |
| **Imperative** | Define the exact steps to execute in order | Shell scripts, Azure CLI |

Most modern IaC tools are **declarative**—you describe *what* you want, not *how* to build it.

### Idempotency

Running the same IaC template multiple times produces the same result. If a resource already exists in the desired state, no changes are made. This makes deployments safe and repeatable.

### Version Control

IaC files are plain text, enabling:

* **Git history** for all infrastructure changes
* **Code review** via pull requests
* **Branching** for testing changes safely
* **Rollback** to previous configurations

## Benefits of IaC

### Consistency

The same template deploys identical infrastructure every time, eliminating “works on my machine” problems for environments.

### Speed

Provisioning that took hours or days manually can happen in minutes through automation.

### Documentation

The code *is* the documentation. Reading a template shows exactly what infrastructure exists.

### Collaboration

Teams can work together on infrastructure the same way they collaborate on application code.

### Cost Control

Templates make it easy to spin up and tear down environments, avoiding forgotten resources that accumulate costs.

## IaC in the Azure Ecosystem

Azure provides several IaC options:

| Tool | Type | Description |
| --- | --- | --- |
| **ARM Templates** | Native JSON | Azure’s original IaC format |
| **Bicep** | DSL | Modern language that compiles to ARM |
| **Terraform** | Multi-cloud | HashiCorp’s platform-agnostic tool |
| **Pulumi** | General-purpose | Use familiar programming languages |

In this course, we focus on **Bicep** as the recommended approach for Azure-native infrastructure.

## A Simple Example

Here’s what IaC looks like in practice. This Bicep snippet creates a storage account:

```
param location string = resourceGroup().location
param storageAccountName string

resource storage 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: storageAccountName
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
}
```

Compare this to clicking through the Azure Portal:

1. Navigate to Storage accounts
2. Click Create
3. Fill in subscription, resource group, name
4. Select region, performance tier, redundancy
5. Click through networking, data protection, encryption tabs
6. Review and create

The Bicep file captures all decisions in ~15 lines of readable code.

## The IaC Workflow

A typical IaC workflow follows these steps:

1. **Write** - Author templates in your editor
2. **Validate** - Check syntax and preview changes (`what-if`)
3. **Review** - Get code review from teammates
4. **Deploy** - Apply changes to target environment
5. **Iterate** - Modify templates as requirements evolve

## Key Takeaways

* IaC treats infrastructure configuration as software artifacts
* Declarative templates describe desired state, not procedures
* Version control enables collaboration, review, and history
* Idempotent deployments are safe to run repeatedly
* Azure Bicep is the modern choice for Azure infrastructure

---

Next, we’ll explore [ARM & Bicep](/infrastructure-fundamentals/iac/2-arm-and-bicep/arm-and-bicep/) in detail, learning how to write and deploy templates for real Azure resources.

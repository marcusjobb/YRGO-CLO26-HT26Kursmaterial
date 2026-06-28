---

title: ARM & Bicep
author: Marcus Ackre Medina
type: lecture
topic: infrastructure
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/infrastructure-fundamentals/iac/2-arm-and-bicep/arm-and-bicep.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure Fundamentals-"
tags: ["arm", "azure", "bicep", "cloud", "git", "iac", "infrastructure", "linux", "networking"]
week_fit: []
---

Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure Fundamentals-
Compute-
Network-
Storage-
IaC-- What Is IaC-- ARM & BicepExercisesTutorials

# ARM & Bicep

🟡


[Watch the presentation](/infrastructure-fundamentals/iac/2-arm-and-bicep/arm-and-bicep-slides/)

[Se presentationen på svenska](/infrastructure-fundamentals/iac/2-arm-and-bicep/arm-and-bicep-slides-swe/)

---

Infrastructure as Code (**IaC**) lets us define Azure resources **declaratively** and deploy them **repeatably**. This week we move from **Portal & CLI** to **templates** that describe our Ubuntu VM and its network—clean, versioned, and testable.

## Why IaC (for our VM)

* **Consistent**: same template → same VM, NIC, NSG, Public IP every time
* **Auditable**: templates live in Git; PR review replaces “click ops”
* **Safe changes**: use `what-if` to see impact before applying

## ARM vs Bicep (quick compare)

* **ARM**: verbose JSON; native to Azure Resource Manager
* **Bicep**: concise DSL that compiles to ARM; better ergonomics, modules, linter
* **Engine**: both deploy via **Azure Resource Manager**

## Template anatomy (mini-map)

* **parameters**: inputs (location, namePrefix, adminUser, sshKey, myIp)
* **resources**: VNet/Subnet, NSG rules, Public IP, NIC, **Ubuntu VM**
* **outputs**: public IP address (so we can browse Nginx)

## Commands you’ll use

* `az deployment group what-if -f main.bicep -p dev.bicepparam`
* `az deployment group create -f main.bicep -p dev.bicepparam`
* `bicep decompile main.json` and `bicep build main.bicep`

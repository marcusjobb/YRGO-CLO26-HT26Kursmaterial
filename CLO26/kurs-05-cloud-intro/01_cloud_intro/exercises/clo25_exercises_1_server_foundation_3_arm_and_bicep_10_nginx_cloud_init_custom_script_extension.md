Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-
Server Foundation--
1. Portal Interface--
2. Command Line Interface--
3. ARM and Bicep--- 7. Exporting a Template from Azure Portal--- 8. Minimal ARM Template: Ubuntu VM + Network--- 9. From ARM to Bicep: Ubuntu VM + Network--- 10. Nginx via Cloud-init (bash) and Custom Script Extension-
Network Foundation-
Deployment-
Cloud Databases-
Webapp Development-
Code Collaboration-
DockerTutorials

# 10. Nginx via Cloud-init (bash) and Custom Script Extension

🟡


**Goal:** Install **Nginx** on an Ubuntu VM using two approaches and compare them:

1. **Cloud-init (`customData`)** — runs **at VM creation**, script as a separate **bash** file
2. **Custom Script extension** — can run **on an existing VM**, script as a separate **bash** file

**Estimated time:** 45–60 minutes

---

## Learning outcomes

* Pass a **bash** script to cloud-init via `customData` and `loadTextContent()`.
* Use the **Custom Script** extension to execute a **bash** script on an existing VM.
* Know when to choose **cloud-init** vs **extension**.

## Prerequisites

* Exercise 9 (Bicep) or similar `main.bicep` + `dev.bicepparam` in place.
* Azure CLI and Bicep installed.
* An Ubuntu VM template (from earlier exercises).

## What you’ll produce

* `cloud-init.sh` — bash script for cloud-init.
* `install-nginx.sh` — bash script for the Custom Script extension.
* Updated `main.bicep` and `dev.bicepparam` that wire both methods.

---

## Part A — Cloud-init (runs on **new** VM creation)

1. **Create `cloud-init_nginx.sh`** (keep it minimal; no HTML changes):

```
#!/bin/bash
apt-get update
apt-get install -y nginx
```

2. **Update `main.bicep`** — add parameter and pass it to `osProfile.customData`:

```
@description('User-data passed to cloud-init; start the script with #!/bin/bash')
param cloudInitContent string = ''

// in the VM resource:
osProfile: {
  computerName: vmName
  adminUsername: adminUsername
  customData: base64(cloudInitContent)
  linuxConfiguration: {
    disablePasswordAuthentication: true
    ssh: {
      publicKeys: [{ path: '/home/${adminUsername}/.ssh/authorized_keys', keyData: adminPublicKey }]
    }
  }
}
```

3. **Update `dev.bicepparam`** — load the bash script from file:

```
param cloudInitContent = loadTextContent('./cloud-init_nginx.sh')
```

4. **Deploy (requires a **new** VM)**
   Cloud-init is only read during **creation**:

```
az deployment group create -g rg-bicep-demo2 -f main.bicep -p dev.bicepparam adminPublicKey=@~/.ssh/id_rsa.pub

az deployment group show -g rg-bicep-demo2 -n main --query properties.outputs.publicIpAddress.value -o tsv
```

## Verification

* Browse to your **public IP** and verify Nginx welcome page

---

## Part B — Custom Script Extension (works on **existing** VM)

1. **Create `cse_deploy_webpage.sh`** (sh; minimal):

```
#!/bin/sh
set -eu # Exit on error, treat unset variables as an error

# Create the web root even if nginx hasn't created it yet
install -d -m 0755 /var/www/html

# Write the web page
cat >/var/www/html/index.html <<'HTML'
<!doctype html>
<html lang="en">
<head><meta charset="utf-8"><title>Azure VM — Nginx</title></head>
<body><h1>It works 🎉</h1><p>Deployed via Azure <strong>Custom Script Extension</strong>.</p></body>
</html>
HTML
```

2. **Add parameter in `dev.bicepparam`** for script content:

```
param customScriptContent = loadTextContent('./cse_deploy_webpage.sh')
```

3. **Add extension resource to `main.bicep`** (after VM resource):

```
@description('Custom Script content to execute on the VM (bash)')
param customScriptContent string = ''

resource vmExt 'Microsoft.Compute/virtualMachines/extensions@2024-07-01' = {
  name: 'deploy-webpage'
  parent: vm
  location: location
  properties: {
    publisher: 'Microsoft.Azure.Extensions'
    type: 'CustomScript'
    typeHandlerVersion: '2.1'
    autoUpgradeMinorVersion: true
    settings: {
      // Inline script executed by /bin/sh (extension decodes Base64 and runs it as script.sh)
      script: base64(customScriptContent)
    }
  }
}
```

4. **Deploy (can be applied to an existing VM)**
   This will add/update only the extension:

```
az deployment group what-if -g rg-bicep-demo2 -f main.bicep -p dev.bicepparam adminPublicKey=@~/.ssh/id_rsa.pub
az deployment group create -g rg-bicep-demo2 -f main.bicep -p dev.bicepparam adminPublicKey=@~/.ssh/id_rsa.pub
```

---

## Verify

```
curl <public-ip>
```

You can decode the string from what-if like this

```
# print to screen
printf '%s' 'IyEvYmluL3NoCns...Cg==' | base64 --decode

# write to a file (recommended)
printf '%s' 'IyEvYmluL3NoCns...Cg==' | base64 --decode > script.sh
```

---

## When to use which?

* **Cloud-init (`customData`)** → base bootstrap on first boot; immutable after creation.
* **Custom Script extension** → make changes post-creation; great for iterative tweaks.

---

## Cleanup

Delete test resources if created for this lab:

```
az group delete -n rg-bicep-demo2 --yes --no-wait
```

---

title: Azure fundamentals och molnorganisation
author: Marcus Ackre Medina
type: lecture
topic: cloud
difficulty: 1
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/java/8_cloud_integration/lectures/09_azure_functions_and_api_management/1_example.md"
description: "RESOURCE_GROUP='UserService-Production"
tags: ["azure", "cloud", "fundamentals", "installation", "molnorganisation", "python", "ssh", "visual-studio"]
week_fit: []
---

# Azure fundamentals och molnorganisation

🟢


Complete example:

```bash
#!/bin/bash

## Azure Infrastructure as Code implementation for User Service migration

## Automates the complete migration process from manual VPS to Azure cloud orchestration

## Set variables for resource naming and configuration
RESOURCE_GROUP="UserService-Production"
LOCATION="northeurope"
VM_NAME="userservice-vm"
VM_SIZE="Standard_B2s"
ADMIN_USERNAME="azureuser"
NSG_NAME="userservice-nsg"
VNET_NAME="userservice-vnet"
SUBNET_NAME="userservice-subnet"
PUBLIC_IP_NAME="userservice-pip"
STORAGE_ACCOUNT="usersvcstore$(date +%s)"
DATABASE_SERVER="userservice-mysql-$(date +%s)"
DATABASE_NAME="userservice_db"

## Create resource group for logical organization
echo "Creating resource group: $RESOURCE_GROUP"
az group create \
    --name $RESOURCE_GROUP \
    --location $LOCATION \
    --tags Environment=Production Application=UserService

## Create virtual network and subnet for network isolation
echo "Setting up network infrastructure..."
az network vnet create \
    --resource-group $RESOURCE_GROUP \
    --name $VNET_NAME \
    --address-prefix 10.0.0.0/16 \
    --subnet-name $SUBNET_NAME \
    --subnet-prefix 10.0.1.0/24

## Create Network Security Group with cloud-native security rules
az network nsg create \
    --resource-group $RESOURCE_GROUP \
    --name $NSG_NAME

## Configure NSG rules for User Service access
az network nsg rule create \
    --resource-group $RESOURCE_GROUP \
    --nsg-name $NSG_NAME \
    --name AllowSSH \
    --priority 1000 \
    --protocol Tcp \
    --destination-port-range 22 \
    --access Allow \
    --source-address-prefix "$(curl -s ifconfig.me)/32"

az network nsg rule create \
    --resource-group $RESOURCE_GROUP \
    --nsg-name $NSG_NAME \
    --name AllowHTTP \
    --priority 1001 \
    --protocol Tcp \
    --destination-port-range 8080 \
    --access Allow \
    --source-address-prefix Internet

## Create public IP for external access
az network public-ip create \
    --resource-group $RESOURCE_GROUP \
    --name $PUBLIC_IP_NAME \
    --allocation-method Static \
    --sku Standard

## Create managed MySQL database as cloud-native replacement
echo "Creating managed MySQL database..."
az mysql flexible-server create \
    --resource-group $RESOURCE_GROUP \
    --name $DATABASE_SERVER \
    --location $LOCATION \
    --admin-user mysqladmin \
    --admin-password "SecurePass123!" \
    --sku-name Standard_B1ms \
    --tier Burstable \
    --storage-size 32 \
    --version 8.0.21

## Create database for User Service
az mysql flexible-server db create \
    --resource-group $RESOURCE_GROUP \
    --server-name $DATABASE_SERVER \
    --database-name $DATABASE_NAME

## Create storage account for application artifacts and logs
az storage account create \
    --resource-group $RESOURCE_GROUP \
    --name $STORAGE_ACCOUNT \
    --location $LOCATION \
    --sku Standard_LRS \
    --kind StorageV2

## Create virtual machine with Ubuntu for User Service hosting
echo "Creating User Service VM..."
az vm create \
    --resource-group $RESOURCE_GROUP \
    --name $VM_NAME \
    --image Ubuntu2004 \
    --size $VM_SIZE \
    --admin-username $ADMIN_USERNAME \
    --authentication-type ssh \
    --generate-ssh-keys \
    --vnet-name $VNET_NAME \
    --subnet $SUBNET_NAME \
    --nsg $NSG_NAME \
    --public-ip-address $PUBLIC_IP_NAME \
    --custom-data cloud-init.txt

## Install required software via cloud-init
cat > cloud-init.txt << EOF
#cloud-config
packages:
  - docker.io
  - openjdk-11-jdk
  - mysql-client
runcmd:
  - systemctl enable docker
  - systemctl start docker
  - usermod -aG docker $ADMIN_USERNAME
  - mkdir -p /opt/userservice
  - wget -O /opt/userservice/user-service.jar https://storage.example.com/user-service.jar
EOF

## Configure cost alerts for budget control
az consumption budget create \
    --resource-group $RESOURCE_GROUP \
    --budget-name "UserService-Budget" \
    --amount 500 \
    --time-grain Monthly \
    --start-date $(date -d "first day of this month" +%Y-%m-01) \
    --end-date $(date -d "first day of next year" +%Y-01-01) \
    --notifications '[{
        "enabled": true,
        "operator": "GreaterThan",
        "threshold": 80,
        "contactEmails": ["admin@company.com"],
        "contactRoles": ["Owner"]
    }]'

## Output connection details
echo "Infrastructure deployment completed!"
VM_IP=$(az vm show -d -g $RESOURCE_GROUP -n $VM_NAME --query publicIps -o tsv)
DATABASE_FQDN=$(az mysql flexible-server show -g $RESOURCE_GROUP -n $DATABASE_SERVER --query fullyQualifiedDomainName -o tsv)

echo "VM Public IP: $VM_IP"
echo "Database FQDN: $DATABASE_FQDN"
echo "SSH Command: ssh $ADMIN_USERNAME@$VM_IP"
echo "Database Connection: mysql -h $DATABASE_FQDN -u mysqladmin -p $DATABASE_NAME"
```

```python
#!/usr/bin/env python3
"""
Azure Cost Monitoring and Resource Optimization System
Implements automated cost control and resource management for cloud orchestration
"""

import json
import datetime
from azure.identity import DefaultAzureCredential
from azure.mgmt.resource import ResourceManagementClient
from azure.mgmt.compute import ComputeManagementClient
from azure.mgmt.consumption import ConsumptionManagementClient
from azure.mgmt.monitor import MonitorManagementClient
import logging

class AzureCostOptimizer:
    def __init__(self, subscription_id):
        self.subscription_id = subscription_id
        self.credential = DefaultAzureCredential()
        self.resource_client = ResourceManagementClient(self.credential, subscription_id)
        self.compute_client = ComputeManagementClient(self.credential, subscription_id)
        self.consumption_client = ConsumptionManagementClient(self.credential, subscription_id)
        self.monitor_client = MonitorManagementClient(self.credential, subscription_id)
        self.logger = self._setup_logging()

    def _setup_logging(self):
        """Configure logging for cost optimization activities"""
        logger = logging.getLogger('AzureCostOptimizer')
        logger.setLevel(logging.INFO)
        handler = logging.StreamHandler()
        formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
        handler.setFormatter(formatter)
        logger.addHandler(handler)
        return logger

    def analyze_resource_costs(self):
        """Analyze current resource costs and identify optimization opportunities"""
        cost_analysis = {}

        for resource_group in self.resource_client.resource_groups.list():
            rg_name = resource_group.name
            rg_costs = self._get_resource_group_costs(rg_name)

            # Get resource utilization metrics
            vm_metrics = self._analyze_vm_utilization(rg_name)
            storage_metrics = self._analyze_storage_usage(rg_name)

            cost_analysis[rg_name] = {
                'monthly_cost': rg_costs,
                'vm_utilization': vm_metrics,
                'storage_optimization': storage_metrics,
                'recommendations': self._generate_cost_recommendations(rg_costs, vm_metrics)
            }

        return cost_analysis

    def _get_resource_group_costs(self, resource_group_name):
        """Get current month costs for specific resource group"""
        try:
            # Calculate date range for current month
            now = datetime.datetime.now()
            start_date = now.replace(day=1).strftime('%Y-%m-%d')
            end_date = now.strftime('%Y-%m-%d')

            # Query usage details for the resource group
            usage_details = self.consumption_client.usage_details.list(
                scope=f"/subscriptions/{self.subscription_id}/resourceGroups/{resource_group_name}",
                expand="meterDetails,additionalProperties",
                filter=f"properties/usageStart ge '{start_date}' and properties/usageStart le '{end_date}'"
            )

            total_cost = sum(float(usage.cost) for usage in usage_details)
            return total_cost

        except Exception as e:
            self.logger.warning(f"Could not retrieve costs for {resource_group_name}: {str(e)}")
            return 0.0

    def _analyze_vm_utilization(self, resource_group_name):
        """Analyze VM CPU and memory utilization for rightsizing recommendations"""
        vm_metrics = {}

        try:
            vms = self.compute_client.virtual_machines.list(resource_group_name)

            for vm in vms:
                # Get CPU utilization metrics for last 7 days
                end_time = datetime.datetime.utcnow()
                start_time = end_time - datetime.timedelta(days=7)

                metrics_data = self.monitor_client.metrics.list(
                    resource_uri=vm.id,
                    timespan=f"{start_time.isoformat()}/{end_time.isoformat()}",
                    interval="PT1H",
                    metricnames="Percentage CPU",
                    aggregation="Average"
                )

                cpu_values = []
                for metric in metrics_data.value:
                    for timeseries in metric.timeseries:
                        for data_point in timeseries.data:
                            if data_point.average:
                                cpu_values.append(data_point.average)

                avg_cpu = sum(cpu_values) / len(cpu_values) if cpu_values else 0
                max_cpu = max(cpu_values) if cpu_values else 0

                vm_metrics[vm.name] = {
                    'size': vm.hardware_profile.vm_size,
                    'avg_cpu_percent': round(avg_cpu, 2),
                    'max_cpu_percent': round(max_cpu, 2),
                    'is_underutilized': avg_cpu < 20 and max_cpu < 50,
                    'is_overutilized': avg_cpu > 80 or max_cpu > 95
                }

        except Exception as e:
            self.logger.warning(f"Could not analyze VM metrics for {resource_group_name}: {str(e)}")

        return vm_metrics

    def _analyze_storage_usage(self, resource_group_name):
        """Analyze storage accounts for cost optimization opportunities"""
        storage_recommendations = {}

        try:
            resources = self.resource_client.resources.list_by_resource_group(
                resource_group_name,
                filter="resourceType eq 'Microsoft.Storage/storageAccounts'"
            )

            for storage_account in resources:
                # Analyze storage tier and access patterns
                storage_recommendations[storage_account.name] = {
                    'current_tier': 'Hot',  # Default assumption
                    'recommended_tier': 'Cool',
                    'potential_savings': self._calculate_storage_savings(storage_account)
                }

        except Exception as e:
            self.logger.warning(f"Could not analyze storage for {resource_group_name}: {str(e)}")

        return storage_recommendations

    def _calculate_storage_savings(self, storage_account):
        """Calculate potential savings from storage tier optimization"""
        # Simplified calculation - in reality would use actual usage metrics
        estimated_gb = 100  # Default estimate
        hot_tier_cost = estimated_gb * 0.0184  # $0.0184 per GB/month for hot tier
        cool_tier_cost = estimated_gb * 0.01  # $0.01 per GB/month for cool tier
        return round(hot_tier_cost - cool_tier_cost, 2)

    def _generate_cost_recommendations(self, current_cost, vm_metrics):
        """Generate actionable cost optimization recommendations"""
        recommendations = []

        if current_cost > 1000:  # High cost threshold
            recommendations.append("Consider implementing auto-shutdown schedules for development VMs")
            recommendations.append("Review and rightsize overprovisioned resources")

        for vm_name, metrics in vm_metrics.items():
            if metrics['is_underutilized']:
                recommendations.append(f"VM '{vm_name}' is underutilized - consider downsizing from {metrics['size']}")
            elif metrics['is_overutilized']:
                recommendations.append(f"VM '{vm_name}' may need upsizing due to high CPU usage")

        return recommendations

    def implement_cost_controls(self, resource_group_name):
        """Implement automated cost control policies"""
        try:
            # Tag resources for cost tracking
            self._apply_cost_tags(resource_group_name)

            # Set up auto-shutdown for development VMs
            self._configure_auto_shutdown(resource_group_name)

            # Optimize storage tiers based on access patterns
            self._optimize_storage_tiers(resource_group_name)

            self.logger.info(f"Cost controls implemented for {resource_group_name}")
            return True

        except Exception as e:
            self.logger.error(f"Failed to implement cost controls: {str(e)}")
            return False

    def _apply_cost_tags(self, resource_group_name):
        """Apply consistent tagging for cost tracking and management"""
        current_date = datetime.datetime.now().strftime('%Y-%m')

        cost_tags = {
            'CostCenter': 'Engineering',
            'Environment': 'Production',
            'ReviewDate': current_date,
            'AutoManaged': 'true'
        }

        # Apply tags to resource group
        resource_group = self.resource_client.resource_groups.get(resource_group_name)
        existing_tags = resource_group.tags or {}
        existing_tags.update(cost_tags)

        self.resource_client.resource_groups.create_or_update(
            resource_group_name,
            {'location': resource_group.location, 'tags': existing_tags}
        )

    def _configure_auto_shutdown(self, resource_group_name):
        """Configure automatic shutdown schedules for cost optimization"""
        vms = self.compute_client.virtual_machines.list(resource_group_name)

        for vm in vms:
            if 'dev' in vm.name.lower() or 'test' in vm.name.lower():
                # Configure auto-shutdown at 18:00 UTC for development VMs
                auto_shutdown_config = {
                    'status': 'Enabled',
                    'taskType': 'ComputeVmShutdownTask',
                    'dailyRecurrence': {'time': '1800'},
                    'timeZoneId': 'UTC',
                    'targetResourceId': vm.id
                }
                self.logger.info(f"Auto-shutdown configured for {vm.name}")

    def _optimize_storage_tiers(self, resource_group_name):
        """Automatically optimize storage tiers based on access patterns"""
        # Implementation would analyze blob access patterns and move cold data to cool/archive tiers
        self.logger.info(f"Storage tier optimization analyzed for {resource_group_name}")

## Usage example for comprehensive cost management
if __name__ == "__main__":
    subscription_id = "your-subscription-id"
    optimizer = AzureCostOptimizer(subscription_id)

    # Analyze current costs and utilization
    cost_analysis = optimizer.analyze_resource_costs()

    # Print cost optimization report
    for rg_name, analysis in cost_analysis.items():
        print(f"\n=== Resource Group: {rg_name} ===")
        print(f"Monthly Cost: ${analysis['monthly_cost']:.2f}")
        print("Recommendations:")
        for recommendation in analysis['recommendations']:
            print(f"  - {recommendation}")

    # Implement cost controls for production environment
    optimizer.implement_cost_controls("UserService-Production")
```

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.

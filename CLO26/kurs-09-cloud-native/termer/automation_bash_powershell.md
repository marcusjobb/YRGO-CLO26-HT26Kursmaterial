# 04 Automation Bash Powershell — Programmeringstermer

## Bash
Unix/Linux-kommando-tolk och skriptspråk. Standard på de flesta servrar och i containrar.

## PowerShell
Microsofts automation- och konfigurationsramverk. Bygger på .NET. cmdlets, pipelining, objekt.

## Cmdlet
PowerShell-kommando (verb-substantiv): Get-Process, Set-Variable, Remove-Item.

## Pipeline (PowerShell)
Skicka objekt mellan cmdlets: `Get-Process | Where CPU -gt 10 | Stop-Process`.

## Shebang
Första raden i bash-skript: `#!/bin/bash`. Anger vilken tolk som ska köra skriptet.

## Cron
Schemaläggare i Unix/Linux. Kör skript vid specificerade tidpunkter: `0 2 * * * /script.sh`.

## Az CLI
Azure Command-Line Interface. Hantera Azure-resurser från terminalen.

## Azure PowerShell
PowerShell-modul för Azure-hantering. `New-AzResourceGroup -Name myGroup -Location westeurope`.

## Automation Account
Azure-tjänst för schemalagd automation. Kör PowerShell- eller Python-runbooks.

## Runbook
PowerShell- eller Python-skript som körs i Azure Automation. Kan triggas av schema eller händelse.


# YAML och Helm

## YAML för Kubernetes

ALL kommunikation med Kubernetes sker via YAML-manifest.

### Grundregler
- Använd ALLTID mellanslag (inte tab) för indentering
- 2 spaces per nivå
- Strängar behöver normalt inte citat

### Varje manifest har fyra delar

```yaml
apiVersion: apps/v1           # API-version
kind: Deployment              # Resurstyp
metadata:                     # Namn, labels, etc.
spec:                         # Specifikation
```

## Helm — Kubernetes Package Manager

Helm löser problemet med återanvändbara Kubernetes-konfigurationer.

| Utan Helm | Med Helm |
|-----------|----------|
| Manuella YAML-filer | Templates med variabler |
| Hårdkodade värden | values.yaml |
| Kopiera mellan projekt | Återanvändbara charts |
| Otymplig versionshantering | Helm releaser |

### Helm Chart-struktur

```
mychart/
├── Chart.yaml         # Metadata
├── values.yaml        # Standardvärden
├── templates/         # Go templates → YAML
│   ├── deployment.yaml
│   ├── service.yaml
│   └── _helpers.tpl   # Hjälpfunktioner
```

### Template Syntax

```yaml
replicas: {{ .Values.replicaCount }}
image: "{{ .Values.image.repository }}:{{ .Values.image.tag }}"
```

### Värdeprioritet (högst → lägst)

1. `--set image.tag=v2` (högst)
2. `--values prod-values.yaml`
3. `values.yaml` (i chartet)

### Helm-kommandon

```bash
helm create mychart                 # Skapa nytt chart
helm install my-release ./mychart   # Installera
helm upgrade my-release ./mychart   # Uppgradera
helm rollback my-release 1          # Rollback
helm list                           # Lista releases
helm uninstall my-release           # Ta bort
```

## Viktigaste lärdomarna

- K8s-manifest = apiVersion + kind + metadata + spec
- Helm = mallar + värden → återanvändbar K8s-konfiguration
- Separera per miljö: values.yaml (dev) + prod-values.yaml
- `helm upgrade` och `helm rollback` = versionshantering för K8s
- Go templates i Helm är kraftfulla men håll dem enkla

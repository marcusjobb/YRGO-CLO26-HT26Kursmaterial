---
marp: true
theme: default
class: invert
paginate: true
---

# YAML och Helm

**Kurs:** Kubernetes
**Modul:** 02 — YAML och Helm

---

## Vad ska vi lära oss idag?

- **YAML för Kubernetes** — skriva manifest
- **Helm** — Kubernetes package manager
- **Charts** — mallbaserade templates
- **Värden och variabler** — konfigurera dina deploymenter
- **Release management** — installera, uppgradera, rollback

---

## YAML-grunderna

YAML används för ALLA Kubernetes-manifest. Grundregler:

```yaml
# Kommentar
key: value                    # Nyckel-värde
name: "Marcus"               # Sträng (citat valfritt)
count: 3                     # Heltal
price: 99.50                 # Decimaltal
active: true                 # Boolean
tags:                        # Lista (array)
  - frontend
  - api
  - database
metadata:                    # Nästlat objekt
  name: my-app
  labels:
    app: my-app
```

**⚠️ VIKTIGT:** Använd ALLTID mellanslag (inte tab) för indentering. 2 spaces per nivå.

---

## Kubernetes Manifest

```yaml
apiVersion: apps/v1           # API-version (beror på resurs-typ)
kind: Deployment              # Resurs-typ
metadata:                     # Metadata om resursen
  name: my-app
  labels:
    app: my-app
spec:                         # Specifikation (beror på resurs-typ)
  replicas: 3
  selector:
    matchLabels:
      app: my-app
  template:
    metadata:
      labels:
        app: my-app
    spec:
      containers:
      - name: app
        image: myapp:latest
        ports:
        - containerPort: 80
```

---

## Helm — Vad och Varför?

Helm = Package manager för Kubernetes. Som apt/yum/NuGet men för K8s.

| Utan Helm | Med Helm |
|-----------|----------|
| Manuellt skapa YAML-filer | Templates med {{ variabler }} |
| Hårdkodade värden | values.yaml för konfiguration |
| Kopiera YAML mellan projekt | Återanvändbara charts |
| Svårt att versionshantera | Helm releaser med revisioner |

---

## Helm Chart — Struktur

```
mychart/
├── Chart.yaml          # Metadata: namn, version, beskrivning
├── values.yaml         # Standardvärden för templates
├── charts/             # Beroende (subcharts)
├── templates/          # Go templates som genererar YAML
│   ├── deployment.yaml
│   ├── service.yaml
│   ├── _helpers.tpl    # Hjälpfunktioner
│   └── NOTES.txt       # Instruktioner efter installation
└── .helmignore         # Filattribut att ignorera
```

---

## Helm Template — Exempel

**templates/deployment.yaml:**
```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: {{ .Values.appName }}
spec:
  replicas: {{ .Values.replicaCount }}
  selector:
    matchLabels:
      app: {{ .Values.appName }}
  template:
    spec:
      containers:
      - name: {{ .Values.appName }}
        image: "{{ .Values.image.repository }}:{{ .Values.image.tag }}"
        ports:
        - containerPort: {{ .Values.service.port }}
```

**values.yaml:**
```yaml
appName: my-app
replicaCount: 3
image:
  repository: myregistry/app
  tag: latest
service:
  port: 80
```

---

## Helm-kommandon

```bash
# Skapa nytt chart
helm create mychart

# Installera ett chart
helm install my-release ./mychart

# Uppgradera (ny version)
helm upgrade my-release ./mychart --set image.tag=v2

# Rollback till föregående revision
helm rollback my-release 1

# Lista releases
helm list

# Se revisioner
helm history my-release

# Avinstallera
helm uninstall my-release
```

---

## Värdeinmatning — Flera sätt

Prioritet (högst → lägst):

```bash
# 1. --set (högst prioritet)
helm install my-release ./chart --set image.tag=v2

# 2. --values (extern fil)
helm install my-release ./chart -f prod-values.yaml

# 3. values.yaml (i chartet — lägst prioritet)
```

**Användning:** ha en values.yaml med default-värden, och en prod-values.yaml med produktionsspecifik konfiguration.

---

## Sammanfattning

- ✅ Kubernetes manifest = YAML (apiVersion, kind, metadata, spec)
- ✅ Helm = package manager för Kubernetes
- ✅ Charts = mallar (templates) + värden (values.yaml)
- ✅ `helm install`, `helm upgrade`, `helm rollback`
- ✅ Separera konfiguration per miljö (dev vs prod)
- ➡️ Nästa: Ingress och tillgänglighet

---

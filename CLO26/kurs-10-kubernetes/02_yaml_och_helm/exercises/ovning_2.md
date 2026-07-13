# Övning 2 — Installera och uppgradera med Helm

🟡 Mellannivå

---

## Bakgrunden

Rena YAML-filer fungerar bra för enkla appar. Men så snart du vill driftsätta samma app i dev, stage och prod med lite olika inställningar — börjar det bli jobbigt att hålla koll på tre kopior av i stort sett identiska filer.

Helm löser det med ett Chart: en mall med platshållare, och en `values.yaml` som fyller i dem. Du ändrar en rad i values — Helm sköter resten.

---

## Vad gäller

- [ ] Som utbildare vill jag att du kan skapa ett Helm Chart och installera det
- [ ] Som utbildare vill jag att du kan uppgradera en release med ändrade värden
- [ ] Som utbildare vill jag att du förstår hur `values.yaml` och `--set` hänger ihop

---

## Uppgift

**Steg 1 — Skapa ett Chart**

```bash
helm create minsida
```

Granska strukturen:

```bash
ls minsida/
ls minsida/templates/
```

Vad finns det för filer? Vad tror du var och en gör?

**Steg 2 — Anpassa values.yaml**

Öppna `minsida/values.yaml` och ändra:

```yaml
replicaCount: 2

image:
  repository: nginx
  tag: alpine
  pullPolicy: IfNotPresent

service:
  type: ClusterIP
  port: 80
```

Ta bort allt som rör `ingress` och `autoscaling` om de finns — vi behöver dem inte nu.

**Steg 3 — Installera**

```bash
helm install min-release ./minsida
```

Verifiera:

```bash
kubectl get pods
kubectl get service
helm list
```

**Steg 4 — Uppgradera**

Ändra antalet replicas utan att ändra values.yaml:

```bash
helm upgrade min-release ./minsida --set replicaCount=4
```

Kontrollera:

```bash
kubectl get pods
helm history min-release
```

**Steg 5 — Rollback**

```bash
helm rollback min-release 1
kubectl get pods
```

Hur många pods körs nu?

**Steg 6 — Svara på frågorna**

1. Vad är skillnaden mellan en Helm **Chart** och en Helm **release**?
2. Varför sparar Helm historik? Hur hjälper det i produktion?
3. Ändrar `--set replicaCount=4` filen `values.yaml`? Testa — kolla filen efter uppgraderingen.

**Steg 7 — Städa upp**

```bash
helm uninstall min-release
```

## Förväntad output

```output
NAME         NAMESPACE   REVISION   STATUS     CHART          APP VERSION
min-release  default     1          deployed   minsida-0.1.0  1.16.0
```

```output
REVISION   UPDATED                  STATUS      CHART          DESCRIPTION
1          Wed Jul 09 ...           superseded  minsida-0.1.0  Install complete
2          Wed Jul 09 ...           deployed    minsida-0.1.0  Upgrade complete
```

## Tips

> `helm template ./minsida` renderar alla templates lokalt utan att installera något. Användbart för att se exakt vilken YAML Helm kommer att skapa.

> `--set` är bra för snabba tester. För miljöspecifika inställningar: skapa en separat fil, t.ex. `values-prod.yaml`, och kör `helm install ... -f values-prod.yaml`.

> ⏱️ 15-minutersregeln: om `helm install` klagar på syntaxfel — kör `helm lint ./minsida` för att se var felet är.

---

*Facit finns hos läraren.*

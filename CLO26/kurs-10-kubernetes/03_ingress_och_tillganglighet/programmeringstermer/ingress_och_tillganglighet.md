# 03 Ingress Och Tillganglighet — Programmeringstermer

## Ingress
Kubernetes-resurs som exponerar HTTP/HTTPS-rutter från utsidan till tjänster i klustret.

## Ingress Controller
Implementation av Ingress. Hanterar lastbalansering, SSL-terminering, routing. Exempel: Nginx, Traefik.

## Ingress Rule
Vägregel: domän + sökväg → vilken service. Exempel: `api.mitt.nu/api` → service `api:80`.

## TLS Certificate
SSL-certifikat för HTTPS. Kubernetes stödjer automatisk certifikathantering via cert-manager och Let's Encrypt.

## LoadBalancer Service
Kubernetes Service-typ som skapar en extern lastbalanserare (Azure LB, ELB).

## NodePort Service
Kubernetes Service-typ som exponerar en port på varje nods IP. Utveckling/labb.

## ClusterIP Service
Standard Service-typ. Bara nåbar inifrån klustret. DNS-namn: `service.namespace.svc.cluster.local`.

## ExternalDNS
Synkronisera Kubernetes Services med DNS-leverantör (Azure DNS, Route53). Automatiska DNS-poster.

## Network Policy
Kubernetes-brandvägg. Styr trafik mellan pods: `allow: namespace=prod, port=80`.

## Headless Service
Service utan ClusterIP. Används för stateful applikationer som behöver direkt pod-åtkomst.


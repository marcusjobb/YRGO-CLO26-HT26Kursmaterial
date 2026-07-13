# Övning — Kör din första container 🟢

Det snabbaste sättet att förstå Docker är att bara köra saker och se vad som händer. Den här övningen är upplagd så — du kör kommandon, tittar på outputen och kopplar ihop det med hur Docker faktiskt fungerar.

Inget att bygga. Inget att konfigurera. Bara leka.

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

---

## Del 1 — hello-world

Docker har en image som heter `hello-world`. Den finns till precis ett syfte: visa att allt fungerar.

```bash
docker run hello-world
```

Docker laddar ner imagen automatiskt om du inte redan har den lokalt. Det kallas att "pulla" en image.

### Förväntad output

```plaintext
Unable to find image 'hello-world:latest' locally
latest: Pulling from library/hello-world
...
Hello from Docker!
This message shows that your installation appears to be working correctly.
```

Första raden är inte ett fel — det är Docker som berättar att den inte hittade imagen lokalt och hämtar den från Docker Hub istället.

---

## Del 2 — nginx i bakgrunden

Nu kör du en riktig webbserver. Flaggan `-d` betyder "detached" — containern körs i bakgrunden och du får tillbaka terminalen direkt.

```bash
docker run -d -p 8080:80 --name min-nginx nginx
```

Flaggan `-p 8080:80` kopplar ihop port 8080 på din dator med port 80 inuti containern. Nginx lyssnar på port 80 — men du når den via 8080.

Öppna webbläsaren och gå till `http://localhost:8080`.

### Förväntad output (terminalen)

```plaintext
Unable to find image 'nginx:latest' locally
latest: Pulling from library/nginx
...
a3f7e7e44c2b1d3f9e5c6b7a8d2e4f1c
```

Du ser en lång hash — det är container-ID:t för din nya, körande container.

---

## Del 3 — vad kör just nu?

```bash
docker ps
```

Visar alla körande containers. Kolumnen `PORTS` visar port-mappningen du satte upp.

```bash
docker images
```

Visar alla images du har lokalt. Här ser du `hello-world` och `nginx` — de ligger kvar på din dator tills du tar bort dem.

### Förväntad output (docker ps)

```plaintext
CONTAINER ID   IMAGE   COMMAND                  CREATED        STATUS        PORTS                  NAMES
a3f7e7e44c2b   nginx   "/docker-entrypoint.…"   2 minutes ago  Up 2 minutes  0.0.0.0:8080->80/tcp   min-nginx
```

---

## Del 4 — se loggarna

Nginx loggar varje request. Du kan läsa loggarna utan att gå in i containern:

```bash
docker logs min-nginx
```

Ladda om `http://localhost:8080` i webbläsaren och kör sen `docker logs min-nginx` igen. Se vad som dyker upp.

### Förväntad output

```plaintext
/docker-entrypoint.sh: /docker-entrypoint.d/ is not empty, will attempt to perform configuration
...
172.17.0.1 - - [13/Jul/2026:10:42:13 +0000] "GET / HTTP/1.1" 200 615 "-" "Mozilla/5.0 ..."
```

Sista raden är din webbläsares request. Status `200` — allt gick bra.

---

## Del 5 — gå in i containern

Containern är en liten isolerad Linux-miljö. Du kan öppna ett skal inuti den:

```bash
docker exec -it min-nginx bash
```

Nu är du inne. Prova:

```bash
ls /usr/share/nginx/html
cat /usr/share/nginx/html/index.html
exit
```

`/usr/share/nginx/html` är mappen nginx serverar filer från. `index.html` är den sida du såg i webbläsaren.

### Förväntad output

```plaintext
root@a3f7e7e44c2b:/# ls /usr/share/nginx/html
50x.html  index.html
```

`exit` tar dig tillbaka till din vanliga terminal. Containern fortsätter köra.

---

## Del 6 — stopp och städning

```bash
docker stop min-nginx
docker rm min-nginx
```

`stop` skickar en signal till containern att stänga ner snyggt. `rm` tar bort containern helt — men imagen finns kvar lokalt.

Verifiera att containern är borta:

```bash
docker ps -a
```

Flaggan `-a` visar även stoppade containers. `min-nginx` ska inte synas längre.

---

## Del 7 — en .NET-app från Docker Hub

Microsoft publicerar en färdig ASP.NET Core-demoapp på Docker Hub. Kör den direkt:

```bash
docker run -d -p 8080:8080 --name dotnet-demo mcr.microsoft.com/dotnet/samples:aspnetapp
```

Öppna `http://localhost:8080`. Du ser en körande ASP.NET Core-applikation — utan att installera .NET, utan att kompilera något.

```bash
docker logs dotnet-demo
docker exec -it dotnet-demo bash
```

Utforska. Titta på filstrukturen inuti containern. Var finns applikationen?

```bash
exit
docker stop dotnet-demo
docker rm dotnet-demo
```

---

## Utmanande frågor

<details><summary>Tips 1 — vad händer om du kör docker ps utan -a?</summary>

```plaintext
docker ps visar bara körande containers.
docker ps -a visar alla — även stoppade.

Prova: stoppa en container med docker stop, kör sedan docker ps och docker ps -a.
Vad är skillnaden i outputen?
```

</details>

<details><summary>Tips 2 — vad händer om du kör docker run utan -d?</summary>

```plaintext
Utan -d körs containern i förgrunden — terminalen "låses" och du ser loggar direkt.
Ctrl+C stoppar containern.

Prova: docker run -p 8080:80 nginx (utan -d).
Ladda om localhost:8080 och titta vad som händer i terminalen.
```

</details>

<details><summary>Tips 3 — kan du köra två containers samtidigt?</summary>

```plaintext
Ja — men de måste ha olika host-portar.

Prova:
docker run -d -p 8080:80 --name webb1 nginx
docker run -d -p 8081:80 --name webb2 nginx

docker ps visar båda. Besök localhost:8080 och localhost:8081.
Städa efteråt: docker stop webb1 webb2 && docker rm webb1 webb2
```

</details>

<details><summary>Lösningsförslag — snabbreferens för alla kommandon</summary>

Här är en samlad referens över det du har använt:

```bash
# Ladda ner en image utan att starta den
docker pull nginx

# Kör en container i bakgrunden med port-mapping och namn
docker run -d -p 8080:80 --name min-nginx nginx

# Visa körande containers
docker ps

# Visa alla containers (även stoppade)
docker ps -a

# Visa lokala images
docker images

# Se loggar från en container
docker logs min-nginx

# Gå in i en körande container
docker exec -it min-nginx bash

# Stoppa en container (snyggt)
docker stop min-nginx

# Ta bort en container
docker rm min-nginx

# Stoppa och ta bort i ett steg
docker rm -f min-nginx
```

Mönstret är alltid detsamma: `docker pull` hämtar imagen, `docker run` startar containern, `docker ps` visar vad som kör, och `docker stop` + `docker rm` städar upp.

En image är mallen. En container är den körande instansen. Du kan ha många containers från samma image.

</details>

# Docker — installation och databaser

Docker låter dig köra program i isolerade behållare (containers) utan att installera dem direkt på datorn. Det betyder att du kan starta en MySQL-server, testa den, och sedan ta bort allt utan att det lämnar spår i systemet.

---

## Installera Docker

### Windows

Installera **Docker Desktop**:

1. Ladda ner från [docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop/)
2. Kör installationsfilen och följ guiden
3. Docker Desktop kräver **WSL 2** — installationsprogrammet hjälper dig om det saknas
4. Starta om datorn

Efter omstart hittar du Docker-ikonen i systemfältet. Öppna Docker Desktop och vänta tills statusen visar *"Docker Desktop is running"*.

---

### macOS

Installera **Docker Desktop**:

1. Ladda ner från [docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop/)
2. Välj rätt version — **Apple Silicon** (M1/M2/M3/M4) eller **Intel**
3. Dra appen till Applications
4. Starta Docker Desktop från Launchpad

På macOS integrerar Docker Desktop med operativsystemet via en menyikonen och filsystemets volymer — du behöver ingen separat VM.

> **Alternativ:** [OrbStack](https://orbstack.dev/) är ett lättare alternativ till Docker Desktop för macOS. Det installeras som en vanlig app och startar snabbare.

---

### Linux

På Linux kör Docker direkt mot kerneln utan något virtualiseringslager — inget desktop-program behövs.

**Ubuntu / Debian:**

```bash
sudo apt update
sudo apt install -y docker.io
sudo systemctl enable docker
sudo systemctl start docker
sudo usermod -aG docker $USER
```

Logga ut och in igen så att gruppändringen träder i kraft.

**Verifiera att det fungerar:**

```bash
docker run hello-world
```

Du ska se ett meddelande som börjar med *"Hello from Docker!"*.

---

## Verifiera installationen (Windows och macOS)

Öppna en terminal och kör:

```bash
docker --version
docker run hello-world
```

Om du ser *"Hello from Docker!"* är allt klart.

---

## Databasbilder

> **Vänta med dessa** tills kursen når det momentet. Installera inte en databasserver innan du behöver den — onödiga containers tar minne och portar.

De tre databaser vi använder på kursen:

| Databas | Används när |
|---------|------------|
| MySQL | Introduktion till relationsdatabaser |
| MS SQL Server | Entity Framework Core (EF) |
| PostgreSQL | Om du vill testa och jämföra |

---

## MySQL

### Installera (hämta imagen)

```bash
docker pull mysql:latest
```

### Starta

```bash
docker run --name mysql-dev \
  -e MYSQL_ROOT_PASSWORD=root \
  -p 3306:3306 \
  -d mysql:latest
```

- `--name mysql-dev` — ett namn du känner igen
- `-e MYSQL_ROOT_PASSWORD=root` — lösenord för root-användaren
- `-p 3306:3306` — kopplar port 3306 på din dator till containern
- `-d` — kör i bakgrunden

### Stoppa

```bash
docker stop mysql-dev
```

### Starta igen (utan att skapa ny container)

```bash
docker start mysql-dev
```

### Ta bort

```bash
docker stop mysql-dev
docker rm mysql-dev
```

---

## MS SQL Server

MS SQL Server används när vi arbetar med Entity Framework Core i .NET.

### Installera

```bash
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

> **Apple Silicon (M1/M2/M3/M4):** Lägg till `--platform linux/amd64` på alla kommandon nedan om imagen inte startar.

### Starta

```bash
docker run --name mssql-dev \
  -e ACCEPT_EULA=Y \
  -e SA_PASSWORD=Passw0rd! \
  -p 1433:1433 \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

- `-e ACCEPT_EULA=Y` — krävs av Microsoft, godkänner licensvillkoren
- `-e SA_PASSWORD=Passw0rd!` — lösenordet måste uppfylla SQL Servers krav: minst 8 tecken, stor bokstav, siffra och specialtecken

### Stoppa

```bash
docker stop mssql-dev
```

### Starta igen

```bash
docker start mssql-dev
```

### Ta bort

```bash
docker stop mssql-dev
docker rm mssql-dev
```

---

## PostgreSQL

### Installera

```bash
docker pull postgres:latest
```

### Starta

```bash
docker run --name postgres-dev \
  -e POSTGRES_PASSWORD=postgres \
  -p 5432:5432 \
  -d postgres:latest
```

### Stoppa

```bash
docker stop postgres-dev
```

### Starta igen

```bash
docker start postgres-dev
```

### Ta bort

```bash
docker stop postgres-dev
docker rm postgres-dev
```

---

## Snabböversikt

| Kommando | Vad det gör |
|----------|-------------|
| `docker pull <image>` | Hämtar en image från internet |
| `docker run ...` | Skapar och startar en ny container |
| `docker start <namn>` | Startar en befintlig (stoppad) container |
| `docker stop <namn>` | Stoppar en körande container |
| `docker rm <namn>` | Tar bort containern helt |
| `docker ps` | Visar alla körande containers |
| `docker ps -a` | Visar alla containers, även stoppade |

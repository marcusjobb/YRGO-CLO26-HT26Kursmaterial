# HTML och Docker

🟢


### Steg 1: Skapa projektstruktur

Skapa en mappstruktur för projektet.

```sh
mkdir hello-world-docker
cd hello-world-docker
mkdir src
```

### Steg 2: Skapa en enkel HTML-fil

Skapa en `index.html` fil inuti `src` mappen.

```html
<!-- src/index.html -->
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Hello World</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            text-align: center;
            margin-top: 50px;
        }
    </style>
</head>
<body>
<h1>Hello World!</h1>
</body>
</html>
```

### Steg 3: Skapa en Dockerfile

Skapa en `Dockerfile` i roten av projektet.

```dockerfile
# Dockerfile
FROM nginx:alpine
COPY src /usr/share/nginx/html
```

### Steg 4: Bygga och köra Docker image

_(Se till att Docker är startat)_

Bygg Docker-image och kör en container.

```sh
# Bygg Docker-image
docker build -t hello-world .

# Kör en container
docker run -d -p 8080:80 hello-world
```

### Komplett projektstruktur

```
hello-world-docker/
├── Dockerfile
└── src/
    └── index.html
```

### Exekveringskommandon

För att bygga och köra projektet:

1. Navigera till projektmappen:

   ```sh
   cd hello-world-docker
   ```

2. Bygg Docker-image:

   ```sh
   docker build -t hello-world .
   ```

3. Kör Docker-container:

   ```sh
   docker run -d -p 8080:80 hello-world
   ```

4. Öppna en webbläsare och navigera till `http://localhost:8080` för att se "Hello World!" sidan.

5. Visa vilka container som körs:

   ```sh
   docker ps
   CONTAINER ID   IMAGE          COMMAND                  CREATED          STATUS          PORTS                    NAMES
   54b6d302c50f   hello-world    "nginx -g 'daemon of…"   2 seconds ago    Up 1 second     0.0.0.0:8080->80/tcp   goofy_shockley
   # eller
   docker container ls
   CONTAINER ID   IMAGE          COMMAND                  CREATED          STATUS          PORTS                    NAMES
   54b6d302c50f   hello-world    "nginx -g 'daemon of…"   2 seconds ago    Up 1 second     0.0.0.0:8080->80/tcp   goofy_shockley
   ```

6. Stoppa containern:

   ```sh
   docker stop <container-id>
   # tex
   docker stop 54b6d302c50f21a2c0cb14aaec57ddabae37e103e524e654c079770651a5faf5
   # även de första unika tecknen i idt är tillräckliga.
   # tex
   docker stop 54b6d302c50f
   54b6d302c50f
   # tex
   docker stop 54b
   54b
   ```

7. Ta bort containern:

   ```sh
   docker rm <container-id>
   # tex
   docker stop 54b
   54b
   ```

Detta är den grundläggande inställningen för din första del av lektionen med HTML och Docker. Nästa steg skulle vara att
skapa ett Spring Boot API-projekt. Låt mig veta om du vill gå vidare med det!

# Docker_Live

🟢


**Skapad:** tisdag den 07:e januari, 2025 11:35

<details><summary>.dockerignore</summary>

```dockerignore

```
</details>

<details><summary>Dockerfile</summary>

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet build --no-restore -c Release
CMD ["dotnet", "test", "--logger:trx", "--results-directory:/testresults"]

```
</details>

<details><summary>.vscode/settings.json</summary>

```json
{
  "workbench.colorCustomizations": {}
}

```
</details>


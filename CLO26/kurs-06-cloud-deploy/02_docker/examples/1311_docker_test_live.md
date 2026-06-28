---

title: Docker_test_Live
author: Marcus Ackre Medina
type: example
topic: docker
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/livecode/2025/01-07/1311_docker_test_live.md"
description: "Skapad:** tisdag den 07:e januari, 2025 13:11"
tags: ["csharp", "docker", "docker_test_live", "git", "live", "test", "verktyg", "visual-studio"]
week_fit: []
---

# Docker_test_Live

🟢


**Skapad:** tisdag den 07:e januari, 2025 13:11

<details><summary>.dockerignore</summary>

```dockerignore
**/.classpath
**/.dockerignore
**/.env
**/.git
**/.gitignore
**/.project
**/.settings
**/.toolstarget
**/.vs
**/.vscode
**/*.*proj.user
**/*.dbmdl
**/*.jfm
**/azds.yaml
**/bin
**/charts
**/docker-compose*
**/Dockerfile*
**/node_modules
**/npm-debug.log
**/obj
**/secrets.dev.yaml
**/values.dev.yaml
LICENSE
README.md
!**/.gitignore
!.git/HEAD
!.git/config
!.git/packed-refs
!.git/refs/heads/**
```
</details>

<details><summary>Docker_test_Live.sln</summary>

```sln

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.12.35527.113 d17.12
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Docker_test_Live", "Docker_test_Live\Docker_test_Live.csproj", "{59540445-31DF-477E-A509-E43235E17563}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Docker_test_LiveTests", "Docker_test_LiveTests\Docker_test_LiveTests.csproj", "{5D898AD0-CDBF-4514-AF70-32AEDF69B5D2}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{59540445-31DF-477E-A509-E43235E17563}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{59540445-31DF-477E-A509-E43235E17563}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{59540445-31DF-477E-A509-E43235E17563}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{59540445-31DF-477E-A509-E43235E17563}.Release|Any CPU.Build.0 = Release|Any CPU
		{5D898AD0-CDBF-4514-AF70-32AEDF69B5D2}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{5D898AD0-CDBF-4514-AF70-32AEDF69B5D2}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{5D898AD0-CDBF-4514-AF70-32AEDF69B5D2}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{5D898AD0-CDBF-4514-AF70-32AEDF69B5D2}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal

```
</details>

<details><summary>Docker_test_Live/Calculator.cs</summary>

```cs
// MIT License
// Copyright (c) [YEAR] [NAME]
// Permission is granted under the MIT License to use, modify, and distribute 
// this software, provided credit is given to the original creator ([NAME]).
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND.

namespace Docker_test_Live;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Calculator
{
	public int Add(int a, int b)
	{
		return a + b;
	}

	public int Subtract(int a, int b)
	{
		return a - b;
	}
}

```
</details>

<details><summary>Docker_test_Live/Dockerfile</summary>

```dockerfile
# Base image for runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies

# Kopierar över projektet
COPY ["Docker_test_Live/Docker_test_Live.csproj", "Docker_test_Live/"]

# Kopierar över testerna
COPY ["Docker_test_LiveTests/Docker_test_LiveTests.csproj", "Docker_test_LiveTests/"]

# Laddar nuggets och annat
RUN dotnet restore "Docker_test_Live/Docker_test_Live.csproj"
# Laddar nuggets och annat till testerna
RUN dotnet restore "Docker_test_LiveTests/Docker_test_LiveTests.csproj"

# Copy all remaining files
COPY . .

# Build both projects
WORKDIR "/src/Docker_test_Live"
RUN dotnet build "Docker_test_Live.csproj" -c $BUILD_CONFIGURATION
RUN dotnet publish "Docker_test_Live.csproj" -c $BUILD_CONFIGURATION -o /app/publish

WORKDIR "/src/Docker_test_LiveTests"
RUN dotnet build "Docker_test_LiveTests.csproj" -c $BUILD_CONFIGURATION

#Runtime image
FROM base AS runtime
COPY --from=build /app/publish .
#ENTRYPOINT ["dotnet", "Docker_test_Live.dll"]

# Test image
FROM build AS test
WORKDIR "/src/Docker_test_LiveTests"
ENTRYPOINT ["dotnet", "test", "Docker_test_LiveTests.csproj","--logger:trx"]

```
</details>

<details><summary>Docker_test_Live/Docker_test_Live.csproj</summary>

```csproj
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <DockerDefaultTargetOS>Linux</DockerDefaultTargetOS>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.VisualStudio.Azure.Containers.Tools.Targets" Version="1.21.0" />
  </ItemGroup>

</Project>

```
</details>

<details><summary>Docker_test_Live/Docker_test_Live.csproj.user</summary>

```user
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <ActiveDebugProfile>Container (Dockerfile)</ActiveDebugProfile>
  </PropertyGroup>
</Project>
```
</details>

<details open><summary>Docker_test_Live/Program.cs</summary>

```cs
// MIT License
// Copyright (c) [YEAR] [NAME]
// Permission is granted under the MIT License to use, modify, and distribute 
// this software, provided credit is given to the original creator ([NAME]).
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND.

using Docker_test_Live;

Console.WriteLine("Hello, Docker!");

Calculator calculator = new Calculator();

Console.WriteLine("Enligt calc är 2+5="+calculator.Add(2,5));
Console.WriteLine("Enligt calc är 3-5=" + calculator.Subtract(3, 5));


```
</details>

<details><summary>Docker_test_Live/Properties/launchSettings.json</summary>

```json
{
  "profiles": {
    "Docker_test_Live": {
      "commandName": "Project"
    },
    "Container (Dockerfile)": {
      "commandName": "Docker"
    }
  }
}
```
</details>

<details><summary>Docker_test_LiveTests/CalculatorTests.cs</summary>

```cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Docker_test_Live;

namespace Docker_test_Live.Tests;

[TestClass()]
public class CalculatorTests
{
	[TestMethod()]
	public void AddTest()
	{
		int expected = 5;
		int a = 2;
		int b = 3;

		Calculator calculator = new Calculator();
		int actual = calculator.Add(a, b);
		Assert.AreEqual(expected, actual);
	}

	[TestMethod()]
	public void SubtractTest()
	{
		int expected = 2;
		int a = 5;
		int b = 3;

		Calculator calculator = new Calculator();
		int actual = calculator.Subtract(a, b);

		Assert.AreEqual(expected, actual);
	}
}

```
</details>

<details><summary>Docker_test_LiveTests/Docker_test_LiveTests.csproj</summary>

```csproj
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.1" />
    <PackageReference Include="MSTest" Version="3.6.1" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Docker_test_Live\Docker_test_Live.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Using Include="Microsoft.VisualStudio.TestTools.UnitTesting" />
  </ItemGroup>

</Project>

```
</details>


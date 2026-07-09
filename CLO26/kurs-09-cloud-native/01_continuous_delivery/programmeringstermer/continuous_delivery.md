# 01 Continuous Delivery — Programmeringstermer

## Continuous Delivery
Automatisera hela releaseskedjan så att varje ändring potentiellt kan lanseras till produktion.

## GitFlow
Branchstrategi med main, develop, feature, release och hotfix-branches. Strukturerad men komplex.

## Trunk-Based Development
Kortlivade feature-branches som mergas till main flera gånger dagligen. Mindre komplexitet.

## Release Train
Schemalagda releaser (t.ex. varje fredag). Funktioner som inte hinner med får vänta till nästa tåg.

## Semantic Versioning
Versionsnummer: MAJOR.MINOR.PATCH. MAJOR = bakåtinkompatibel, MINOR = ny funktion, PATCH = bugfix.

## Rollback
Återgå till föregående version vid problem. Kräver kompatibla databas-migrationer.

## Artifact Repository
Central lagring av byggresultat. Azure Artifacts, GitHub Packages, JFrog Artifactory.

## Dependency Management
Hantering av externa bibliotek och versioner. NuGet för .NET, npm för Node.js.

## SBOM
Software Bill of Materials. Lista över alla beroenden och deras versioner i en applikation.

## Supply Chain Security
Säkerhet i beroendekedjan. Verifiera att paket inte manipulerats. Signering, hash-kontroll.


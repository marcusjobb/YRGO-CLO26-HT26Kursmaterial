@echo off
cd /d "%~dp0Grafik"
where dotnet >nul 2>&1
if errorlevel 1 (
  echo Spelet behover .NET 10 SDK. Installera det och prova igen.
  pause
  exit /b 1
)
echo Startar ett nytt spel. Webblasaren oppnas nar servern ar redo.
echo Hall detta fonster oppet medan du spelar.
dotnet run --project Grafik.csproj -- --OpenBrowser true
pause

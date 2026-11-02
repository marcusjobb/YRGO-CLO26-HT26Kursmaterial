# 07 Blazor — Programmeringstermer

## Blazor
Microsofts ramverk för att bygga interaktiva webapplikationer med C# istället för JavaScript.

## Razor Component
Återanvändbar UI-komponent i Blazor. Skriven i .razor-filer med HTML och C#.

## Blazor Server
Hosting-modell där komponenter körs på servern och UI-uppdateringar skickas via SignalR.

## Blazor WebAssembly
Hosting-modell där komponenter körs i webbläsaren via WebAssembly. Ingen server krävs efter nedladdning.

## Component Parameter
Parameter som skickas till en komponent från dess förälder. Dekoreras med [Parameter].

## Event Callback
Mekanism för att skicka händelser från en barnkomponent till en förälder.

## @code
Blazor-syntax för att lägga till C#-kod (metoder, fält, egenskaper) i en .razor-fil.

## @bind
Tvåvägsdatabindning i Blazor. Synkar en C#-egenskap med ett UI-element.

## @onclick
Händelsehanterare i Blazor. Andra händelser: @onchange, @onsubmit, @onkeypress.

## Layout
Mall för Blazor-sidor. Definierar gemensam struktur (header, nav, footer) som återanvänds.

## Dependency Injection (Blazor)
Blazor har inbyggd DI. Tjänster injectas via @inject eller [Inject].


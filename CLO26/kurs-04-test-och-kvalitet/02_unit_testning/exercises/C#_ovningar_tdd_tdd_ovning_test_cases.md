---

title: Tdd Övning, Test Cases
author: Marcus Ackre Medina
type: exercise
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/TDD/TDD övning, Test Cases.docx"
description: "Ni ska planera testandet av webbsidan för ett gym – ni ska alltså inte programmera något, utan bara läsa"
tags: ["cases.docx", "csharp", "exercise", "git", "tdd", "test", "testing", "övning"]
week_fit: []
---
TDD övning
Ni ska planera testandet av webbsidan för ett gym – ni ska alltså inte programmera något, utan bara läsa
på scenariot och se vad som behöver testas.
Detta är inget vanligt gym, det är ett gym som inriktar sig på pensionärer. Då de är speciellt utsatta när
det gäller Covid-19 är restriktionerna hårdare för gymmet.
Gymmet stänger vid 19:00.
De ska kunna boka tid för träning, lektioner, massage och PT.
På grund av rådande pandemi får man inte ha mer än 10 personer i gym-rummet samtidigt. Därför har
dop-in tider till gymmet tagits bort och man måste boka in sig.
Bokning av massage påverkas inte av antalet personer då det alltid är max två personer i samma rum. 5
massörer finns att välja på.
Bokning av klasser (yoga exempelvis) påverkas antalet personer då det får vara max 10 personer i
samma rum.
PT tid är under dagtid, vilket gör att PT och gym-tid går in i varandra när det gäller bokning, alltså en
bokning för PT innebär att det blir 2 personer per bokad timme i gym-rummet.
Bokning för vanlig gymträning sker numera på samma sätt som de andra.
Lektioner är på 40 minuter.
Massage är på 40 minuter (inklusive avslappning efteråt).
PT pass är på 40 minuter.
Träning i gym-rummet är 1 timmes pass.
Orsaken till att passen är olika långa är för att minska risken för att många samlas i duschen.
Gruppens uppgift är att först och främst komma på minst fem Test scenarios.
I mån av tid ska dessa scenarios delas upp i test cases.
Sedan ska vi diskutera det i Meet igen.

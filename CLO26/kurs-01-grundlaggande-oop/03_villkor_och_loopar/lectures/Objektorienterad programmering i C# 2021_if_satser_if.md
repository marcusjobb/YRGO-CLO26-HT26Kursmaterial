---

title: If
author: Marcus Ackre Medina
type: lecture
topic: conditions
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/If-satser/If.pdf"
description: "• If använder vi för att ge programmet olika vägar att gå"
tags: ["conditions", "csharp", "if.pdf"]
week_fit: []
---
.net21
If

Utbildningsledare
Annika Lund
annika.lund@molndal.se

Utbildare
Marcus Medina
marcus.medina@codic.se

Om …. Isåfall… gör si eller så

Mjölkdilemmat i pseudokod
• Definiera
• Att handla: Mjölk

• Gå till butiken
• Handla ett paket mjölk
• Om de har ägg
• Köp sex stycken

• Ägg är odefinierade

If
• If använder vi för att ge programmet olika vägar att gå
• Efter if skriver vi måsvingar { }, allt inom måsvingar hamnar i ett litet
rum för sig. Man har tillgång till alla variabler som deklarerats utanför
måsvingarna och innanför måsvingarna. När man når sista måsvingen
så förstörs alla variabler som deklarerats inom måsvingarna. På så sätt
är en if-sats ett eget litet namespace.

If

If

Vad är if
• If är precis som namnet antyder : Frågan om
• Om (specifik villkor uppfylls)
• Gör detta

• Annars Om (specifik villkor uppfylls)
• Gör detta

• Annars // i värsta fall
• Gör detta om inget annat matchar villkoren

Inline If eller Ternary If
• Fungerar precis som en if men bara på en rad även vid tilldelning av en
variabel, eller fristående.
• Om (villkor)
• Skriv ut ”OK”

• Annars
• Skriv ”Not OK”

• Kan skrivas såhär
• Skriv ( villkor ? ”OK” : ”Not OK”);
• x>10 ? y++ : z--;

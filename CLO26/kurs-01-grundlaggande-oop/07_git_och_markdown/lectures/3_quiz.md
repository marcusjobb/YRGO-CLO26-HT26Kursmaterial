---

title: 3. UML-diagram för Databasapplikationer
author: Marcus Ackre Medina
type: lecture
topic: git
difficulty: 1
language: python
status: adapted
marcus_voice: true
source: "Old_courses/2025/java/3_oop_advanced/lecture_2/lecture/3_quiz.md"
description: "1. Vad är ett huvudsyfte med att använda UML för databaser?"
tags: ["databasapplikationer", "git", "quiz", "uml-diagram", "verktyg"]
week_fit: []
---

# 3. UML-diagram för Databasapplikationer

🟢


Quiz:

1. Vad är ett huvudsyfte med att använda UML för databaser?

A) Att göra koden mer effektiv
B) Att visualisera systemets struktur innan kodning påbörjas
C) Att förbättra databasens prestanda
D) Att automatiskt generera databastabeller

<details>
<summary>Svar:</summary>
Rätt svar: B

A) Fel - UML påverkar inte direkt kodens effektivitet
B) Rätt - Detta nämns explicit i materialet under 3.1
C) Fel - UML påverkar inte databasens prestanda
D) Fel - UML är ett modelleringsverktyg, inte ett kodgenereringsverktyg
</details>

---

2. Vilket påstående är korrekt gällande relationen mellan Customer och Order i klassdiagrammet?

A) En kund kan ha exakt en order
B) En order kan ha flera kunder
C) En kund kan ha flera ordrar
D) En order måste ha minst två kunder

<details>
<summary>Svar:</summary>
Rätt svar: C

A) Fel - Diagrammet visar "1" till "\*" relation
B) Fel - En order kan endast ha en kund enligt diagrammet
C) Rätt - Detta visas i diagrammet med "1" till "\*" notationen
D) Fel - En order kan endast ha en kund enligt diagrammet
</details>

---

3. Hur representeras en one-to-many relation i Java-koden mellan Customer och Order?

A) @OneToOne
B) @ManyToMany
C) @OneToMany och @ManyToOne
D) @ManyToOne och @OneToOne

<details>
<summary>Svar:</summary>
Rätt svar: C

A) Fel - Detta skulle indikera en en-till-en relation
B) Fel - Detta skulle indikera en många-till-många relation
C) Rätt - I koden används @OneToMany i Customer och @ManyToOne i Order
D) Fel - Denna kombination skulle vara inkonsistent
</details>

---

4. Vilken typ av diagram används för att visa hur objekt interagerar över tid?

A) Klassdiagram
B) ER-diagram
C) Sekvensdiagram
D) Flödesdiagram

<details>
<summary>Svar:</summary>
Rätt svar: C

A) Fel - Klassdiagram visar klassernas struktur och relationer
B) Fel - ER-diagram representerar databasstrukturen
C) Rätt - Detta nämns explicit i materialet under 3.3
D) Fel - Detta är en annan typ av diagram som inte nämns i materialet
</details>

---

5. Vad är syftet med EntityManager i OrderRepository?

A) Att hantera databasens säkerhet
B) Att optimera SQL-queries
C) Att hantera persistens och databastransaktioner
D) Att generera UML-diagram

<details>
<summary>Svar:</summary>
Rätt svar: C

A) Fel - EntityManager hanterar inte primärt säkerhet
B) Fel - Detta är inte huvudsyftet med EntityManager
C) Rätt - EntityManager används för persist() och merge() operationer i koden
D) Fel - EntityManager har ingen koppling till UML-diagramgenerering
</details>

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.

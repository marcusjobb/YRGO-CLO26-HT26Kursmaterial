---
marp: true
theme: nion-dark
paginate: true
---

# Backspegel: quiz om mönstren

Strategy, Factory, Observer och Builder

---

## Din tur först (3 min)

Skriv ner, för dig själv:

1. **Ett mönster** du skulle kunna förklara för en kompis.
2. **En sak** som fortfarande känns oklar.

Den oklara saken tar du med till fredagens handledning.

---

## Fråga 1

Mira byter från svärd till pilbåge med en enda rad, och `Hero` ändras inte.

Vilket mönster är det?

---

## Fråga 2

En klass väljer själv vilken typ av objekt som ska skapas, och anroparen vet inte vilken klass som används.

Vilket mönster är det?

---

## Fråga 3

När Miras hälsa ändras ska livmätaren och ljudet uppdateras, utan att `Hero` känner till dem.

Vilket mönster är det?

---

## Fråga 4

En hjälte har titel, sköld, följeslagare och startguld. Du vill bygga den steg för steg, och varje steg ska kunna utelämnas.

Vilket mönster är det?

---

## UML 1

I klassdiagrammet står `Hero "1" -- "*" Quest : har`.

Vad betyder `*` vid `Quest`?

---

## UML 2

Mellan `SwordAttack` och `IAttackStrategy` går en streckad linje med en tom triangel mot interfacet.

Vad betyder den linjen?

---

## Svar

1. **Strategy:** byt beteende genom att byta objekt.
2. **Factory:** skapandet samlat på ett ställe.
3. **Observer:** någon ropar, andra lyssnar.
4. **Builder:** bygg objektet steg för steg.

**UML 1:** en hjälte har många uppdrag. `*` betyder "många".

**UML 2:** `SwordAttack` lovar att implementera `IAttackStrategy`. Streckad linje med tom triangel = implementerar interfacet.

---

## Det här gjorde vi i dag

- ✅ Strategy, Factory, Observer och Builder: vad de löser
- ✅ Vilket mönster som passar vilket problem

---

## Härnäst

- **Torsdag–fredag:** Läxa D, Mira och verktygslådan.
- **Fredag:** handledning. Ta med din oklara sak.

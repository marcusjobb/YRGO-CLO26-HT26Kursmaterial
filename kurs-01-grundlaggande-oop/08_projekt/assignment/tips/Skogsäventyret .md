# Skogsäventyret — hur planerar man ett sånt spel?

Innan du skriver kod: rita klasserna på papper eller whiteboard. Det här är en checklista för vad du behöver ha koll på.

---

## 1. Stridssystemet — en gemensam bas

Spelare och monster gör samma sak i strid: attackerar, tar skada, kollar om de lever. Lägg det i en **basklass** så slipper du skriva det två gånger.

```
                     ┌───────────────────────┐
                     │        Warrior          │
                     │ (fightSystem, HP, Attack)│
                     └───────────▲───▲─────────┘
                                 │   │
                       ┌─────────┘   └─────────┐
              ┌────────┴────────┐    ┌─────────┴────────┐
              │     Spelare       │    │      Monster       │
              └───────────────────┘    └─────────▲───▲──────┘
                                                  │   │
                                       ┌──────────┘   └──────────┐
                                ┌──────┴──────┐          ┌───────┴──────┐
                                │   Monster1    │          │   Monster2    │
                                └───────────────┘          └───────────────┘
```

```csharp
class Warrior
{
    public int Hp { get; set; }
    public bool Alive => Hp > 0;
    public virtual void Attack(Warrior mål) { }
}

class Spelare : Warrior { }

class Monster : Warrior { }

class Monster1 : Monster
{
    public override void Attack(Warrior mål) { /* eget beteende */ }
}

class Monster2 : Monster
{
    public override void Attack(Warrior mål) { /* eget beteende */ }
}
```

**Varför gör man så här?** `Spelare` och alla monster ärver `Hp`, `Alive` och grundstrukturen från `Warrior`. Varje monstertyp skriver bara sin egen `Attack()` — precis som i polymorfism-lektionen (`List<Monster>`, `List<Item>`).

---

## 2. Övriga klasser att planera

| Klass | Ansvar |
|-------|--------|
| `Town` | Vad spelaren kan göra i byn (vila, prata, gå vidare) |
| `Store` | Köpa/sälja utrustning |
| `Weapon` | Vapen spelaren och monster kan använda |

---

## 3. Spelloopen

Spelet fortsätter så länge spelaren lever:

```csharp
while (spelare.Alive)
{
    // strid, handel, förflyttning — det som händer i en spelomgång
}
```

---

## 4. Vem styr vad? — Spelmotorn

Låt **en** klass hålla ihop flödet. Den anropar de andra, men innehåller inte själva spelreglerna:

```csharp
class Spel
{
    void FightRound(Spelare spelare, Monster monster) { }
    void BesökTown(Spelare spelare) { }
    void BesökStore(Spelare spelare) { }
}
```

---

## Checklista innan du börjar koda

- [ ] Rita klassdiagrammet (vilka ärver från vad?)
- [ ] Bestäm vad `Warrior` ska innehålla, vad som är unikt för `Spelare` respektive `Monster`
- [ ] Bestäm hur många monstertyper du vill ha — kom ihåg att `List<Monster>` funkar oavsett antal
- [ ] Skissa spelloopen: vad händer i en runda?
- [ ] Skriv `Spel`-klassen sist — den är bara "dirigenten", inte reglerna

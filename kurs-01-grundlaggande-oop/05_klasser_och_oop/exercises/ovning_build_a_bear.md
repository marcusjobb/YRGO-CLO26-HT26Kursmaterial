# Övning — Build-A-Bear 🐻

🟡 Mellannivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Du ska bygga ett program som låter användaren bygga sin egen björn — del för del.
En björn har ett huvud, en kropp, armar, ben, ögon och en mun. Varje kroppsdel har en textur och en färg.

Programmet ska kunna sätta ihop björnen och sedan presentera hur den ser ut.

---

## Del 1 — En kroppsdel som datatyp

Innan vi kan bygga en björn behöver vi kunna representera en **kroppsdel**.

En kroppsdel har:
- Ett **namn** — t.ex. `"Head"`, `"Left Arm"`, `"Mouth"`
- En **textur** — t.ex. `"Hairy"`, `"Sharp-toothed"`
- En **färg** — t.ex. `"Brown"`, `"White"`

> 💡 En klass är en datatyp du definierar själv. Precis som `int` lagrar ett heltal kan en `BodyPart`-klass lagra allt som hör ihop med en kroppsdel.

**Hur skulle du designa den klassen?** Tänk igenom det innan du läser tipsen.

<details>
<summary>Tips 1 — Vad behöver lagras?</summary>

Tre värden: namn, textur och färg. Alla är strängar. Tre privata fält räcker.

</details>

<details>
<summary>Tips 2 — Vem får ändra värdena?</summary>

En kroppsdel förändras inte efter att den skapats — en brun, hårig arm förblir en brun, hårig arm. Det räcker med `get; private set;` eller till och med bara `get;` (init-only).

</details>

<details>
<summary>Tips 3 — Hur skapas den?</summary>

Konstruktorn tar in namn, textur och färg och tilldelar dem. Inget mer behövs.

</details>

<details>
<summary>Förslag</summary>

```csharp
class BodyPart
{
    private string _name;
    private string _texture;
    private string _colour;

    public string Name    { get { return _name; }    private set { _name = value; } }
    public string Texture { get { return _texture; } private set { _texture = value; } }
    public string Colour  { get { return _colour; }  private set { _colour = value; } }

    public BodyPart(string name, string texture, string colour)
    {
        Name    = name;
        Texture = texture;
        Colour  = colour;
    }
}
```

</details>

---

## Del 2 — Björnen som samlar ihop delarna

Nu när vi kan representera en kroppsdel behöver vi en klass som håller koll på **hela björnen**.

En `BuildABear`-klass ska:
- Ha en property för varje kroppsdel: huvud, kropp, vänster arm, höger arm, vänster ben, höger ben, vänster öga, höger öga, mun
- Ha en metod `AddBodyPart(string name, string texture, string colour)` som skapar en `BodyPart` och tilldelar den rätt property beroende på namnet
- Ha en metod `PrintBear()` som skriver ut hur björnen ser ut

**Hur skulle du designa den klassen?** Tänk igenom det innan du läser tipsen.

<details>
<summary>Tips 1 — Vilken typ har varje property?</summary>

Varje kroppsdel är av typen `BodyPart` — den klass du just skapade. En björn som ännu inte fått ett huvud har `null` som värde — deklarera därför som `BodyPart?` (nullable).

</details>

<details>
<summary>Tips 2 — Hur vet AddBodyPart vilken property som ska sättas?</summary>

En `switch` på `name.ToLower()` funkar bra. `"head"` → `Head = part`, `"torso"` → `Torso = part`, osv.

</details>

<details>
<summary>Tips 3 — Hur skriver PrintBear ut en kroppsdel?</summary>

Gör en privat hjälpmetod `PrintPart(string label, BodyPart? part)` som skriver ut kroppen med naturlig text — och inget alls om `part` är `null`.

</details>

<details>
<summary>Förslag</summary>

```csharp
class BuildABear
{
    public BodyPart? Head     { get; set; }
    public BodyPart? Torso    { get; set; }
    public BodyPart? LeftArm  { get; set; }
    public BodyPart? RightArm { get; set; }
    public BodyPart? LeftLeg  { get; set; }
    public BodyPart? RightLeg { get; set; }
    public BodyPart? LeftEye  { get; set; }
    public BodyPart? RightEye { get; set; }
    public BodyPart? Mouth    { get; set; }

    public void AddBodyPart(string name, string texture, string colour)
    {
        BodyPart part = new BodyPart(name, texture, colour);
        switch (name.ToLower())
        {
            case "head":      Head     = part; break;
            case "torso":     Torso    = part; break;
            case "left_arm":  LeftArm  = part; break;
            case "right_arm": RightArm = part; break;
            case "left_leg":  LeftLeg  = part; break;
            case "right_leg": RightLeg = part; break;
            case "left_eye":  LeftEye  = part; break;
            case "right_eye": RightEye = part; break;
            case "mouth":     Mouth    = part; break;
            default:
                Console.WriteLine($"Unknown body part: {name}");
                break;
        }
    }

    public void PrintBear()
    {
        Console.WriteLine("You have built a bear:");
        PrintPart("Head",      Head);
        PrintPart("Torso",     Torso);
        PrintPart("Left Arm",  LeftArm);
        PrintPart("Right Arm", RightArm);
        PrintPart("Left Leg",  LeftLeg);
        PrintPart("Right Leg", RightLeg);
        PrintPart("Left Eye",  LeftEye);
        PrintPart("Right Eye", RightEye);
        PrintPart("Mouth",     Mouth);
    }

    private void PrintPart(string label, BodyPart? part)
    {
        if (part != null)
            Console.WriteLine($"  With a {part.Colour.ToLower()} {part.Texture.ToLower()} {label.ToLower()}");
    }
}
```

</details>

---

## Del 3 — Sätt ihop allt i Main

Nu har du verktygen. Bygg din björn i `Main()`.

```csharp
BuildABear bear = new BuildABear();
bear.AddBodyPart("Head",      "Hairy",        "Brown");
bear.AddBodyPart("Torso",     "Hairy",        "Brown");
bear.AddBodyPart("Left_Arm",  "Hairy",        "Brown");
bear.AddBodyPart("Right_Arm", "Hairy",        "Brown");
bear.AddBodyPart("Left_Leg",  "Hairy",        "Brown");
bear.AddBodyPart("Right_Leg", "Hairy",        "Brown");
bear.AddBodyPart("Left_Eye",  "Hairy",        "Brown");
bear.AddBodyPart("Right_Eye", "Hairy",        "Brown");
bear.AddBodyPart("Mouth",     "Sharp-toothed","White");

bear.PrintBear();
```

### Förväntad output

```
You have built a bear:
  With a brown hairy head
  With a brown hairy torso
  With a brown hairy left arm
  With a brown hairy right arm
  With a brown hairy left leg
  With a brown hairy right leg
  With a brown hairy left eye
  With a brown hairy right eye
  With a white sharp-toothed mouth
```

---

## Utmaning

- Vad händer om du anropar `bear.PrintBear()` innan du lagt till alla delar? Saknade delar ska inte synas i utskriften.
- Lägg till en `LeftEar` och `RightEar` — vad behöver du ändra i `BuildABear`?
- Bygg en andra varelse — ett monster, en robot, en alien — med andra kroppsdelar och färger.

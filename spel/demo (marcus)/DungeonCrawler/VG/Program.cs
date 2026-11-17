using DungeonCrawlerVG;

// Rum-nätverket: foaje → bibliotek + rustningskammare → torn → skattkammaren (låst med nyckel)
var rum = new Dictionary<string, Rum>
{
    ["foaje"] = new("Foajén",
        "En dammig hall med spruckna marmorgolv. En gammal vakt slumrar mot väggen.",
        new() { ["norr"] = "bibliotek", ["öster"] = "rustningskammare" },
        ["fackla"],
        låstaDörrar: null),

    ["bibliotek"] = new("Biblioteket",
        "Höga bokhyllor täcker väggarna. En mystisk figur i hörnet verkar ha svar.",
        new() { ["syd"] = "foaje", ["öster"] = "torn" },
        ["bok"]),

    ["rustningskammare"] = new("Rustningskammaren",
        "Tomma rustningsstativ och en gammal smed som polerar en skyddsamulett.",
        new() { ["väster"] = "foaje", ["norr"] = "torn" },
        ["nyckel"]),

    ["torn"] = new("Tornrummet",
        "En spiraltrappa slingrar sig uppåt. Skattkammardörren är låst.",
        new() { ["syd"] = "bibliotek", ["väster"] = "rustningskammare", ["norr"] = "skattkammaren" },
        [],
        låstaDörrar: new() { ["norr"] = "nyckel" }),

    ["skattkammaren"] = new("Skattkammaren",
        "Guld och juveler glittrar i alla riktningar. Du har nått målet!",
        new() { ["syd"] = "torn" },
        ["krona"]),
};

var npcs = new Dictionary<string, Npc>
{
    ["foaje"] = new("Gammal vakt", [
        "Det finns en nyckel någonstans i den östra kammaren...",
        "Passa dig för skattkammardörren — den är låst.",
        "Hitta nyckeln. Öppna dörren. Ta guldet. Enkelt.",
    ]),
    ["bibliotek"] = new("Mystisk figur", [
        "Böcker kan leda dig till sanningen, men nyckeln öppnar dörrarna.",
        "Skattkammarens hemlighet är äldre än detta dungeon.",
        "Jag har läst varje bok här. Ingen säger hur man tar sig ut.",
    ]),
    ["rustningskammare"] = new("Gammal smed", [
        "Jag smider inte längre. Men jag gömde nyckeln till skattkammaren här en gång.",
        "Ta nyckeln. Det är allt jag kan ge dig.",
        "Gå norrut mot tornet och sedan norrut igen — om du har nyckeln.",
    ]),
};

var nuvarandeRumId  = "foaje";
var inventory       = new List<string>();
var låstaDörrar_upp = new HashSet<string>();  // upplåsta riktningar i form av "rum:riktning"
var meddelande      = "";

void VisaRum()
{
    var r = rum[nuvarandeRumId];
    Console.WriteLine("=== DUNGEON CRAWLER [VG] ===\n");
    Console.WriteLine($"📍 {r.Namn}");
    Console.WriteLine($"   {r.Beskrivning}");

    var utgångarVisa = r.Utgångar.Keys
        .Select(rikt =>
        {
            bool låst = r.LåstaDörrar.ContainsKey(rikt)
                        && !låstaDörrar_upp.Contains($"{nuvarandeRumId}:{rikt}");
            return låst ? $"{rikt} (🔒)" : rikt;
        });
    Console.WriteLine($"   Utgångar: {string.Join(", ", utgångarVisa)}");

    if (r.Föremål.Count > 0)
        Console.WriteLine($"   Föremål: {string.Join(", ", r.Föremål)}");
    if (npcs.TryGetValue(nuvarandeRumId, out var npc))
        Console.WriteLine($"   NPC: {npc.Namn}");
    Console.WriteLine($"   Inventory: {(inventory.Count == 0 ? "(tom)" : string.Join(", ", inventory))}");
    if (meddelande != "")
    {
        Console.WriteLine($"\n> {meddelande}");
        meddelande = "";
    }
    Console.WriteLine();
}

while (true)
{
    Console.Clear();
    VisaRum();

    if (nuvarandeRumId == "skattkammaren")
    {
        Console.WriteLine("🏆 DU VANN! Du hittade skattkammaren och klarade Dungeon Crawler VG.");
        Console.WriteLine("\nTryck Enter för att avsluta.");
        Console.ReadLine();
        break;
    }

    Console.Write("Kommando: ");
    var input = Console.ReadLine()?.Trim().ToLower() ?? "";
    var delar = input.Split(' ', 2);
    var verb  = delar[0];
    var obj   = delar.Length > 1 ? delar[1] : "";

    var r = rum[nuvarandeRumId];

    if (verb == "gå" && obj != "")
    {
        if (!r.Utgångar.ContainsKey(obj))
        {
            meddelande = $"Du kan inte gå {obj} härifrån.";
        }
        else if (r.LåstaDörrar.ContainsKey(obj)
                 && !låstaDörrar_upp.Contains($"{nuvarandeRumId}:{obj}"))
        {
            var krävdNyckel = r.LåstaDörrar[obj];
            if (inventory.Contains(krävdNyckel))
            {
                låstaDörrar_upp.Add($"{nuvarandeRumId}:{obj}");
                nuvarandeRumId = r.Utgångar[obj];
                meddelande     = $"Du låser upp dörren med {krävdNyckel} och kliver in i {rum[nuvarandeRumId].Namn}.";
            }
            else meddelande = $"Dörren är låst. Du behöver en {krävdNyckel}.";
        }
        else
        {
            nuvarandeRumId = r.Utgångar[obj];
            meddelande     = $"Du går {obj} och kliver in i {rum[nuvarandeRumId].Namn}.";
        }
    }
    else if (verb == "ta" && obj != "")
    {
        if (r.Föremål.Remove(obj))
        {
            inventory.Add(obj);
            meddelande = $"Du tar {obj}.";
        }
        else meddelande = $"Det finns ingen {obj} här.";
    }
    else if (verb == "prata")
    {
        if (npcs.TryGetValue(nuvarandeRumId, out var npc))
            meddelande = $"{npc.Namn}: \"{npc.NästaReplik()}\"";
        else
            meddelande = "Det finns ingen att prata med här.";
    }
    else if (verb == "titta")    meddelande = r.Beskrivning;
    else if (verb == "inventory") meddelande = inventory.Count == 0 ? "Du bär ingenting." : "Du bär: " + string.Join(", ", inventory) + ".";
    else if (verb == "hjälp")    meddelande = "Kommandon: gå [riktning] · ta [föremål] · prata · titta · inventory · hjälp · avsluta";
    else if (verb == "avsluta")  break;
    else meddelande = $"Okänt kommando: {input}. Skriv 'hjälp' för kommandon.";
}

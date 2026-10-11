using DungeonCrawlerG;

var rum = new Dictionary<string, Rum>
{
    ["foaje"] = new("Foajén",
        "En dammig hall med spruckna marmorgolv. Ljuset flimrar i en trasig lykta.",
        new() { ["norr"] = "bibliotek", ["öster"] = "rustningskammare" },
        ["fackla"]),

    ["bibliotek"] = new("Biblioteket",
        "Höga bokhyllor täcker väggarna. Böcker är utspridda på golvet. Doften av gammalt papper.",
        new() { ["syd"] = "foaje", ["öster"] = "torn" },
        ["bok"]),

    ["rustningskammare"] = new("Rustningskammaren",
        "Tomma rustningsstativ stirrar på dig med tomma ögonhålor. En nyckel glittrar i hörnet.",
        new() { ["väster"] = "foaje", ["norr"] = "torn" },
        ["nyckel"]),

    ["torn"] = new("Tornrummet",
        "En spiraltrappa slingrar sig uppåt mot mörkret. Alla vägar leder hit.",
        new() { ["syd"] = "bibliotek", ["väster"] = "rustningskammare", ["norr"] = "skattkammaren" },
        []),

    ["skattkammaren"] = new("Skattkammaren",
        "Guld och juveler glittrar i alla riktningar. Du har nått målet!",
        new() { ["syd"] = "torn" },
        ["krona"]),
};

var nuvarandeRumId = "foaje";
var inventory      = new List<string>();
var meddelande     = "";

void VisaRum()
{
    var r = rum[nuvarandeRumId];
    Console.WriteLine($"=== DUNGEON CRAWLER [G] ===\n");
    Console.WriteLine($"📍 {r.Namn}");
    Console.WriteLine($"   {r.Beskrivning}");
    Console.WriteLine($"   Utgångar: {string.Join(", ", r.Utgångar.Keys)}");
    if (r.Föremål.Count > 0)
        Console.WriteLine($"   Föremål: {string.Join(", ", r.Föremål)}");
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
        Console.WriteLine("🏆 DU VANN! Du hittade skattkammaren och klarade Dungeon Crawler.");
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
        if (r.Utgångar.TryGetValue(obj, out var nästaRum))
        {
            nuvarandeRumId = nästaRum;
            meddelande     = $"Du går {obj} och kliver in i {rum[nästaRum].Namn}.";
        }
        else meddelande = $"Du kan inte gå {obj} härifrån.";
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
    else if (verb == "titta")  meddelande = r.Beskrivning;
    else if (verb == "inventory") meddelande = inventory.Count == 0 ? "Du bär ingenting." : "Du bär: " + string.Join(", ", inventory) + ".";
    else if (verb == "hjälp")  meddelande = "Kommandon: gå [riktning] · ta [föremål] · titta · inventory · hjälp · avsluta";
    else if (verb == "avsluta") break;
    else meddelande = $"Okänt kommando: {input}. Skriv 'hjälp' för kommandon.";
}

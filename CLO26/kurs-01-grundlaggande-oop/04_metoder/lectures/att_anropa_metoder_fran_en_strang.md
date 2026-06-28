---

title: Att anropa metoder från en sträng
author: Marcus Ackre Medina
type: lecture
topic: methods
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "exercises_to_spread_out/Att anropa metoder från en sträng.md"
description: "Hur anropar man en metod när man bara har en sträng som talar om vad man ska anropa?"
tags: ["anropa", "csharp", "delegates", "från", "linq", "methods", "metoder", "reflection", "sträng", "switch-expression"]
week_fit: []
---

# Att anropa metoder från en sträng

🔴



# Intro

Hur anropar man en metod när man bara har en sträng som talar om vad man ska anropa?

Det finns olika sätt, från enkel till vansinnigt men kul. Vi ska kolla på olika alternativ. Vi kan alltså anropa metoder på detta sätt och använda det vi får tillbaka som vanligt.

```

var call = "SendEmail";
var response = Demo.CallMethod(call,"marcus.medina@codic.se");
```

```


if (response != null)
{
foreach (var row in response)
{
Console.WriteLine(row);
}
}
```


Eller i en kortare form.


```

var call = "SendEmail";
Demo.CallMethod(call)?.ForEach(x => Console.WriteLine(x.Info));
```


# If

If är det enklaste och vanligaste lösningen.

## Fördelar

- Det är enkelt

- Det är lätthanterligt

- Ifall fler metoder läggs in kommer det att behövas att man skapar en ny ifsats

## Nackdelar

- Koden blir lång och klumpig, speciellt om man har måsvingar vid varje if

- Koden kan bli oläsbar om det blir för många

## Kodexempel

```


internal List<WhatEver> CallMethod(string callThis, params string[] data)
{
var method = callThis.ToLower();
if (method == "call") return MyCoolClass.Call(data);
else if (method == "sendemail") return MyCoolClass.SendEmail(data);
else if (method == "havedinner") return MyCoolClass.HaveDinner(data);
else if (method == "singasong") return MyCoolClass.SingASong(data);
else if (method == "dosometapdancing") return MyCoolClass.DoSomeTapDancing(data);
return null;
}
```


# Switch

Switch är snyggare än If en tar gärna en hel del mer plats, vilket inte är så bra ibland. Men annars är den mer lätthanterlig för den är mer överskådlig.

## Fördelar

- Det är enkelt

- Det är lätthanterligt

- Ifall fler metoder läggs in kommer det att behövas att man skapar en ny case; kod; sats

## Nackdelar

- Koden blir lång

- Koden kan bli oläsbar om det blir för många

- Har man måsvingar på sina Cases blir den extremt lång

## Kodexempel

```


internal List<WhatEver> CallMethod(string callThis, params string[] data)
{
var method = callThis.ToLower();
switch (method)
{
case "call":
return MyCoolClass.Call(data);
case "sendemail":
return MyCoolClass.SendEmail(data);
case "havedinner":
return MyCoolClass.HaveDinner(data);
case "singasong":
return MyCoolClass.SingASong(data);
case "dosometapdancing":
return MyCoolClass.DoSomeTapDancing(data);
default:
return null;
}
}
```


# Switch Expression

Smartare form av Switch som presenterades i C#8 och används alltför sällan

## Fördelar

- Det är enkelt

- Det är lätthanterligt

- Ska fler metoder läggas in så lägger man bara till en rad

## Nackdelar

- Koden kan bli oläsbar om det man switchar på har olika längd  (se call och dosometapdancing), många sådana rader gör koden jobbig att läsa

## Kodexempel

```


internal List<WhatEver> CallMethod(string callThis, params string[] data)
{
var method = callThis.ToLower();
return method switch
{
"call" => MyCoolClass.Call(data),
"dosometapdancing" => MyCoolClass.DoSomeTapDancing(data),
"sendemail" => MyCoolClass.SendEmail(data),
"havedinner" => MyCoolClass.HaveDinner(data),
"singasong" => MyCoolClass.SingASong(data),
_ => null,
};
}
```


# Delegate

Delegates har vi inte pratat mycket om, men det är absolut något man inte bör missa. Delegates är typer som kan användas som mall för en metod, och med hjälp av dem kan vi lagra metoden i en variabel för att sedan anropa det. Vi kan alltså lägga en massa metoder i en lista och söka listan i med LinQ för att sedan anropa den valda metoden.

## Fördelar

- Ganska cool funktion

- Ska fler metoder läggas in så lägger man bara till en rad i listan

## Nackdelar

- Det är krångligt om man inte förstår sig på delegates och Linq,

- Svårare att felsöka då allting sker dynamiskt.

## Kodexempel

```


internal delegate List<WhatEver> Caller(params string[] data);
internal static List<WhatEver> CallMethod(string callThis, params string[] data)
{
var method = callThis.ToLower();
var list = new Dictionary<string, Caller>
{
{"sendemail",MyCoolClass.SendEmail},
{"havedinner",MyCoolClass.HaveDinner},
{"singasong",MyCoolClass.SingASong},
{"dosometapdancing",MyCoolClass.DoSomeTapDancing},
};
```

```


var call = list.FirstOrDefault(m => m.Key == method).Value;
return call != null ? call(data) : null; 
    // Samma sak som (call!=null) return call(data) else return null;
}
```


# Reflection i enkel form

Nu går vi över till månens mörka sida i .Net världen. Vi ska använda något som heter reflektion. Det är när man kommunicerar med .Net för att få reda på strukturen i klasser, egenskaper och metoder.

Hur fungerar Reflection. Man ber .Net förse en med en instans av en klass, exempelvis MittProjekt.MinCoolaKlass. Man anger inga parametrar och inga parenteser när man skickar efter sin instans. Får man null så har man angett fel namn eller så finns inte klassen.

```
var classType = Type.GetType(className); // typeof(MyCoolClass); funkar bra med
```

Nästa steg är att beställa en instans av sin klass, vi skickar in null för att vi antar att den har en kostruktor med inga parametrar. Man kan annars skicka en object[] array med parametrar. Dessa objekt kan vara Strings, int, klasser mm. Om klassen är statisk kommer detta att krascha, så en Try Catch skadar inte.

```
var classInstance = Activator.CreateInstance(classType, null);
```


Ska vi skicka in värden till en property får vi hämta instans av propertyn och sätta värdet med en objekt, eller en objektarray. Om propertyn är null betyder det att det inte är en property eller att namnet på propertyn inte finns.

```

var prop = classType.GetProperty(methodName);
prop.SetValue(classInstance, objParams);
```

För att anropa en metod gör vi på samma sätt som med en property men anropar istället GetMethod(). Får vi null, så finns inte metoden eller så är den inte publik. Sedan anropar vi den via Invoke() där vi skickar in en array med objekt[]. Dessa objekt kan vara Strings, int, klasser mm.

```

var theMethod = classType.GetMethod(methodName);
theMethod.Invoke(classInstance, new object[] { objParams });
```


## Fördelar

- Ganska cool funktion

- Ska fler metoder läggas in så lägger man bara till en rad i listan

## Nackdelar

- Man måste ha full koll på vad den gör

- Svår att debugga


## Kodexempel

```


internal static List<WhatEver> CallMethod (string callThis, params string[] data)
{
var classType = typeof(MyCoolClass);
if (classType != null) return null;
object classInstance = null;
var constructorInfo = classType.GetConstructor(Type.EmptyTypes);
if (constructorInfo?.IsPublic == true && !classType.IsAbstract)
classInstance = Activator.CreateInstance(classType, null);//ej statisk/abstrakt
```

```


var method = classType.GetMethod(callThis);
var property = classType.GetProperty(callThis);
property?.SetValue(classInstance, data[0]);
```

```


return method!=null?(List<WhatEver>)method.Invoke(classInstance,new object[]{data}):null;
}
```


# Reflection Hardcore

Här är en mer generisk version av reflektion som inte är beroende av klasstyp (får den via en sträng) eller av metodnamn.

Det är mycket att tweaka innan den koden blir helt generisk så den kan användas på vad som helst, men för enkla anrop så räcker exemplet gott och väl.

## Fördelar

- Det är ascoolt

- Lägger man till nya metoder behöver man inte ändra i klassen

## Nackdelar

- Komplicerat och omständigt

- Svårt att debugga

- Lätt att göra fel och svårt att komma på var felet är

- Den kommunicerar enbart med Objects, vilket gör att allt ska omvandlas till Object och sedan tillbaka till sina typer

## Kodexempel

```


internal static List<WhatEver> CallMethod (string callThis, params string[] data)
{
return (List<WhatEver>)CallMethod("ConsoleProject1.MyAwesomeClass", callThis, data);
}
```

```


/// <summary>
/// Kallar en metod genom att använda Reflection
/// </summary>
/// <param name="className">Namnet på klassen (inklusive namespace ex. ConsoleProject1.MyAwesomeClass)</param>
/// <param name="methodName">Namnet på metoden</param>
/// <param name="objParams">Parametrar som ska skickas in till metoden</param>
/// <returns>Required object</returns>
private static object CallMethod(string className, string methodName, params object[] objParams)
{
// Hämta klassens ID
var classType = Type.GetType(className);
// Om klassen inte finns, returnera null
if (classType == null) return null;
```

```


//Hämta en instans av klassen
var classInstance = new object();
var constructorInfo = classType.GetConstructor(Type.EmptyTypes);
if (constructorInfo?.IsPublic == true && !classType.IsAbstract)
classInstance = Activator.CreateInstance(classType, null);//använd tom constructor
```

```


// Kolla om det är en property
var prop = classType.GetProperty(methodName);
if (prop != null)
{
// Sätt property värdet
prop.SetValue(classInstance, objParams[0]);
// Returnera värdet för skojs skull
Debug.WriteLine("Value is now " + prop.GetValue(classInstance).ToString());
return null;
}
```

```


// Om det inte var en property så är det en metod
var theMethod = classType.GetMethod(methodName);
// Om metoden inte finns eller inte är publikt, returnera null
if (theMethod == null) return null;
```

```


//Anropa metoden och returnera vad den returnerar
return theMethod.Invoke(classInstance, new object[] { objParams });
}
```


# Reflektion om Reflection

Nu undrar du, vad mer kan man göra med reflection?

Man kan få en lista på alla metoder och properties i en klass, läsa värden från properties och man kan tilldela properties värden, självklart kan man anropa metoder.

### När har man nytta av reflection?

Om du har en odokumenterad DLL fil som du vill veta mer om, eller om du vill anropa metoder i en DLL fil som du läst in dynamiskt till minnet (det är en annan historia).

Vad jag har sett så har detta använts i samband med plugins till sina program.

Det är kul att leka med det, men det kan lätt bli instabilt om man inte har full koll på vad man gör, använd detta med försiktighet.

Om du undrar hur man läser in DLL filer och använder det som plugins, här har du ett bra exempel

Dynamic Load .NET Assembly - CodeProject


# Vilket sätt ska man använda?

Använd det sätt som känns trevligast och enklast att använda.

Det är den regel man alltid ska köra när man programmerar, gör inte saker krångligare än vad de behöver vara.

## Vilket sätt skulle jag (Marcus) använda?

Personligen skulle jag nog använda Switch Expression för att den är mest överskådlig, men den som är bäst för framtida anpassningar är Delegates.

Man kan exempelvis ha en klass med en publik Dictionary av metoder, alla andra klasser som vill samarbeta med den registrera sina metoder till listan. Och sedan när någon vill använda något av de registrerade metoderna så frågar man Dictionaryn om vilken metod man ska köra.

Sen är frågan om jag verkligen skulle använda strängar, personligen föredrar jag Enums, det är mycket lättare att arbeta med siffror än med text.


Hälsningar

Marcus

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.

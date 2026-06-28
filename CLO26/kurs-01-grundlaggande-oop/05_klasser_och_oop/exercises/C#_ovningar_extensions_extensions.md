---

title: Extensions
author: Marcus Ackre Medina
type: exercise
topic: oop
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Extensions/Extensions.docx"
description: "Extensions i C# är något helt annorlunda, på sätt och vis. Det vi gör med extensions är att vi kort och gott"
tags: ["csharp", "exercise", "extensions.docx", "oop", "sql", "test"]
week_fit: []
---
Extensions
Extensions i C# är något helt annorlunda, på sätt och vis. Det vi gör med extensions är att vi kort och gott
lägger till funktioner i redan existerande klasser. För… att i C# är allting klasser!
En extension följer vissa regler.
1. Den måste finnas i en statisk klass
2. Det måste vara en statisk metod
3. Första parametern är this, vilket talar om för metoden vilken klass den ska koppla sig till
(this string  extension till string, this-DataRow  extension till DataRow, this int extension
till int osv)
Dessa extensions fungerar enbart i projektet du har dem, så om du vill använda dem i andra projekt får
du importera klassen, skapa en referens till ditt projekt eller skapa en nuget som du laddar ner varje
gång du vill ha den funktionaliteten. En annan nackdel är att om du använder en massa speciallösningar
så blir din kod snyggare, men om någon vill kopiera din kod måste du komma ihåg att de kommer att
behöva dina extensions med.
Extensions är lite roliga på så sätt att de anropas som vanliga metoder eller från själva typen du skapat.
Nu blev det säker rörigt, så vi kollar på ett exempel.
var message = "This is Sparta!";
Console.WriteLine(message);

Alltid tråkigt att behöva skriva Console.WriteLine() bara för att testa om en variabel fått rätt värde. Vi gör
det enklare för oss.
var message = "This is Sparta!";
message.Print();

eller rentav…
"This is Sparta!".Print();

☹

Nu tänker du… vänta lite nu! string.Print finns inte! Nä det är sant
men det skulle vara najs om det
fanns. Vad gör man som programmerare när funktioner man vill ha inte finns? Jo, man skapar dem!
public static class StringExtensions
{
public static void Print(this string text)
{
Console.WriteLine(text);
}

}
Kan anropas som ”hello”.Print() eller myString.print() eller StringExtensions.Print(MyString)

Vi skapar alltså en statisk klass med en statisk metod (fyll gärna på med fler) som har this string till sin
första parameter. Detta innebär att den blir ett tillägg till string. Intellisense fattar direkt vad vi sysslar
med och kommer att hjälpa till med att använda dessa metoder med.

Nu blev det kul, vad mer kan vi göra?
Vad mer kan vi göra?
Om du har en DataTable och vill ha värdet från en specifik rad (vanligen första raden) så måste du först
kolla om det finns Rows, sen kolla om den raden du vill ha finns, sedan läsa av värdet på din kolumn och
urvärdera om den är DBNull eller NULL och slutligen omvandla det till ett nummer eller en sträng. Det är
många rader kod som får upprepas.
int rowToCheck = 3;
string column = "name";
if (db.Rows.Count>rowToCheck)
{
DataRow row = dt.Rows[rowToCheck];
if (row[column] != DBNull.Value)
{
return row[column] as string;
}
}

Kan vi göra detta enklare för oss?
Vad sägs om detta. Vi kopierar in koden till två extension metoder och ändrar en till att returnera int och
en till att returnera en string.
public static class DBExtensions
{
public static string GetString(this DataTable dt, string column, int rowToCheck = 0)
{
if (dt != null && dt.Rows.Count > rowToCheck)
{
DataRow row = dt.Rows[rowToCheck];
if (row[column] != DBNull.Value)
{
return row[column] as string;
}
}
return "";
}
public static int GetInt(this DataTable dt, string column, int rowToCheck = 0)
{
if (dt != null && dt.Rows.Count > rowToCheck)
{
DataRow row = dt.Rows[rowToCheck];
if (row[column] != DBNull.Value)
{
return (int)row[column];
}
}
return 0;
}

}

Nu kan du använda
DataTable dt = db.GetDataTable(”Select * FROM People”);
Console.WriteLine(dt.GetString(”Namn”, 3)); // Namnet i 4e raden i listan

Sådär, vad har vi lärt oss? Alla specialmetoder vi skapar kan vi lätt göra om till att bli extensions, vi kan
alltid anropa extensions från typen vi kopplat dem till eller genom klassnamn.metodnamn som man gör
med statiska metoder.
Gott råd är att döpa dina extensions till namn som förklarar vad de gör och döp dina klasser till namnet
på typen du vill hantera, för att lättare kunna hitta dem senare.
Andra roliga extensions jag kan bjuda på…
// Skriver ut tostring från alla objekt
public static void Debug(this object obj)
{
System.Diagnostics.Debug.WriteLine(obj.ToString());
}

Och lite Visual Basic metoder för stränghantering som C# saknar
// returnerar x antal tecken ur en sträng
public static string Left(this string text, int pos)
{
return text.Substring(0, pos);
}
// returnerar x antal tecken från slutet ur en sträng
public static string Right(this string text, int pos)
{
return text.Substring(text.Length - pos);
}
// returnerar en sträng från given position
public static string Mid(this string text, int pos)
{
return text.Substring(pos);
}
// returnerar x antal tecken från en viss position
public static string Mid(this string text, int pos, int length)
{
return text.Substring(pos, length);
}

Så nu är det din tur att skapa Extensions och dela dina coolaste i Discorden så kan alla bygga upp en
egen extensionsamling som i längden blir en ”egen C#”.
Good luck
/Marcus

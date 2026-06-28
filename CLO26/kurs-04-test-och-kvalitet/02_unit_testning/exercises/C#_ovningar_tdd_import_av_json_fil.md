---

title: Import Av Json Fil
author: Marcus Ackre Medina
type: exercise
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/TDD/Import av JSON fil.pdf"
description: "Import av JSON fil – Vad kan gå fel?"
tags: ["csharp", "exercise", "fil.pdf", "import", "json", "oop", "test", "testing"]
week_fit: []
---
Import av JSON fil – Vad kan gå fel?
[{"guid":"f8bcfa4a-bd9f-4f92-bd3 c08 b7e3543c74", "age":39,"name ":"Howard Moore","gender":"male","company":"LETPRO","e
mail":"howardmoore@letpro.com","phone":"+1 (995) 4432936","address":"742 Colonial Road, Chapin, West Virginia, 1726"},{"guid":"f1b7cb47a02b-4477-ab242e6a6040d473","age":26,"name":"Frazier Riddle","gender":"male","company":"INFOTRIPS",
"email":"frazierriddle@infotrips.com","phone":"+1 (995) 4232502","address":"729 Bayview Place, Jamestown, Massachusetts,8838"},{"guid":"456ba5f8
-ab4f-4d94-b114f0578a2f299 c","age":39,"name":"Graves Boone","gender":"male","company":"ANIVET","ema
il":"gravesboone@anivet.com","phone":"+1 (828) 5882527","address":"250 Pierrepont Place, Caroline, Ohio, 9563"},{"guid":"506cbb0e-6bb74348-bd915fadfd6e46ae","age":20,"name":"Iva Morris","gender":"female","company":"ENORMO","emai
l":"ivamorris@enormo.com","phone":"+1 (862) 4872268","address":"342 Banker Street, Blanco, Missouri, 2806"},{"guid":"92fbe272-75384481-83c6fd8dc41af61d","age":25,"name ":"Kirby Christian","gender":"male","company":"IMANT","e
mail":"kirbychristian@imant.com","phone":"+1 (906) 4202185","address":"420 Congress Street, Saranap, Puerto Rico, 7239"},{"guid":"8ff5885dcbfa-4490-ab08b68784a15d31","age":37,"name":"Atkins Hicks","gender":"male","company":"XIIX","email"
:"atkinshicks@xiix.com","phone":"+1 (998) 5122552","address":"857 Oriental Court, Leming, Oklahoma, 5234"},{"guid":"0b01d825-81ca4256-ad25d8a6367f7482","age":21,"name":"Middleton Cooley","gender":"male","company":"FLEETMIX"
,"email":"middletoncooley@fleetmix.com","phone":"+1 (970) 4273597","address":"172 Madison Street, Bellfountain, Indiana, 6674"}]

Antagande: Alla personer i JSON filen har guid, detta gör att oavsett i hur många olika maskiner de
importeras kommer de alltid att ha samma unika id.

Slutsats: Inget problem med dubbletter så länge man kollar om personens guid finns i databasen

if (json == null) return new List<Person>();
var import = JsonConvert.DeserializeObject<Person[]>(json);
List<Person> People = new List<Person>();
try
{
foreach (var person in import)
{
var existing = People.FirstOrDefault(i => i.guid == person.guid);
if (existing == null && person!=null)
{
if (person.guid == null) person.guid = Guid.NewGuid().ToString();
People.Add(person);
}
}
}
catch (Exception ex)
{
Debug.WriteLine(ex.Message);
}
Return People;

Antagande: guid är null i ett objekt
Slutsats: Ooops… det där får inte hända! Men OK det kan hända. Vi skapar den innan vi sparar i listan.
Det är relativt ofarligt för att GUIDs upprepas aldrig.
if (existing == null && person!=null)
{
if (person.guid == null) person.guid = Guid.NewGuid().ToString();
People.Add(person);
}

Antagande: Personen är null
Slutsats: Agh! Sånt får inte hända! Men men… det händer… Here we go…
if (existing == null && person!=null)
{
//Massor med kod
}

Antagande: JSON filen är skadad
Slutsats: Dekodning av filen kommer att stanna av innan importen startar, så den kommer inte att
orsaka korrupt information, men data kan komma att försvinna. En try catch fixar så att programmet
inte kraschar.
try
{
//Massor med kod
}
catch (Exception ex)
{
Debug.WriteLine(ex.Message);
}

Antagande: JSON filen är tom
Slutsats: Ingenting händer, ingenting importeras
var import = JsonConvert.DeserializeObject<Person[]>(json);
List<Person> People = new List<Person>();
foreach (var person in import)
{
//Massor med kod
}

Antagande: JSON filen är null
Slutsats: Kontrollera om filen är null annars kraschar programmet
if (json == null) return new List<Person>();
var import = JsonConvert.DeserializeObject<Person[]>(json);

Antagande: Nerladdning av JSON fil stoppas
Slutsats: Ingenting kommer att importeras då filen är skadad, se ”JSON filen
är skadad”
Sammanfattning.
Vi har inte testat koden, men vi har skyddat upp ganska bra för vad som kan
tänkas komma in i listan. Man skulle kunna säga att vi tänker TDD men
hoppade över testerna, det ska man inte göra, så nu, om du vill verifiera…
skapa tester och skicka in JSON-kod som är korrekt eller skadad, och kolla
sedan i listan som returneras. Det är inte så man jobbar TDD men men…
alltid bra att planera i förväg innan man hamnar i en kod-labyrint som inte
ens Jareth - The goblin king skulle kunna ta sig ut ur.

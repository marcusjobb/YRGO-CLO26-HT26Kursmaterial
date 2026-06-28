---

title: Tdd Övningar, Crush Kill Destroy!
author: Marcus Ackre Medina
type: exercise
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/TDD/TDD övningar, Crush Kill Destroy!.docx"
description: "TDD övningar, Crush Kill Destroy!"
tags: ["crush", "csharp", "destroy!.docx", "exercise", "kill", "oop", "tdd", "test", "testing", "övningar"]
week_fit: []
---
TDD övningar, Crush Kill Destroy!
Skapa metoder och testa sönder dem
Metod
public int StringLength(params string[] array)

Beskrivning
Den här metoden ska räkna ut den sammanlagda
längden av alla strängar i listan.

public int GetSum(int[] numbers)

Tester
 ”Hello”,”World”
 Tom lista
 Null
 String[]{”Hello”,null,”World”}
Summera ihop alla talen i arrayen

public int SumValues(int[] numbers, int
indexStart, int indexEnd)

Tester
 Arrayen kan vara NULL
 Arrayen kan vara tom
 new int[0];
 Array.Empty<int>();
Summera alla talen, med start från given index
fram till slutIndex

public int SumValues(int[] numbers, int
indexStart, int amount)

Tester
 IndexStart kan vara mindre än 0
 IndexStart eller IndexEnd kan vara större
än numbers.Length
 Arrayen kan vara NULL
 Arrayen kan vara tom
 new int[0];
 Array.Empty<int>();
Summera alla talen, med start från given index
och följande antal givna i amount
Tester
 IndexStart kan vara mindre än 0
 IndexStart plus amount kan vara större
än numbers.Length
 Arrayen kan vara NULL
 Arrayen kan vara tom
 new int[0];
 Array.Empty<int>();

public string GetToken(string text, int
wordIndex)

Hämta ordet i texten, i index som angetts

Tester
 Id kan vara mindre än 0
 Id kan vara större än vad Person listan
kan ta emot, exempelvis int.MaxValue
 Stringen vi får tillbaka kan vara null
 Text kan vara null
 Text kan var string.Empty
Skapa nu följande klasser
public class Person
{
public int Id { get; set; }
public string Name { get; set; }
public EyeColors EyeColors { get; set; }
}
public class EyeColors
{
public string Left { get; set; }
public string Right { get; set; }
}

Skapa nu en klass kallad PeopleHandler och ge den följande metoder
public List<Person> SeedPeople()
{
return new List<Person>
{
new Person(0,"Pekka","Green","Green"),
new Person(1,"James","Brown","Yellow"),
new Person(2,"Peter","Brown",null),
};
}

Seedar personer till en lista

public Person GetPerson(int id)

Hämta en person ur listan
Test






public string GetEyeColors(int id)

Id = 1
Id = 999
Id = -1
Id = 0
Kan ge null som returvärde?

Hämtar en person ur listan och skapar en sträng

public int GetAge(DateTime birthDate)

med ögonfärg kommaseparerat
Test
 Id = 2
 Id = 999
 Id = -1
 Id = 0
Räknar någons ålder

public int GetAge(string birthDate)

Test
 2010-11-11
 1970-06-20
 2021-12-21
 Null
 Datetime.MinValue
 Datetime.MaxValue
 DateTime.Now.AddMonths(1)
Räknar någons ålder

public int GetConceptionDate(string birthDate)

Test
 2010-11-11
 1970-06-20
 2021-12-21
 Null
 03/11/2021
 03/11/2021
Räknar ut ungefärlig befruktningsdatum (minus 9
månader från födelsedagen)
Test









2010-11-11
1970-06-20
2021-12-21
Null
03/11/2021
03/11/2021
String kan vara null
DateTime.MinValue

Nu skapar vi den klassiska Calculator klassen
public int AddValues(int x, int y) => x + y;
public int SubtractValues(int x, int y) => x - y;
public double DivideValues(int x, int y) => x / y;
public int MultiplyValues(int x, int y) => x * y;

Vad händer om x = int.MaxValue eller x=int.MinValue ?
Vad händer om x eller y är noll?
Vad händer om x eller y är mindre än 0?
Och en null-vänlig version då
public int AddValues(int? x, int? y) => (int)x + (int)y;
public int SubtractValues(int? x, int? y) => (int)x - (int)y;
public double DivideValues(int? x, int? y) => (double)x / (double)y;
public int MultiplyValues(int? x, int? y) => (int)x * (int)y;

Kör samma tester mot dem

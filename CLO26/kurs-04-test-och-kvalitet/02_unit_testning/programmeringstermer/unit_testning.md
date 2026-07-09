# 02 Unit Testning — Programmeringstermer

## Unit Test
Test som verifierar en enskild enhet av kod (oftast en metod) i isolation från beroenden.

## Test Case
Ett specifikt test som verifierar ett scenario. Innehåller indata, förväntat resultat och assertion.

## Assertion
Kontroll i ett test som verifierar att faktiskt resultat matchar förväntat. Exempel: Assert.Equal(5, result);

## AAA-mönster
Arrange (förbered), Act (utför), Assert (kontrollera). Standardstruktur för enhetstester.

## Test Fixture
Förberedd miljö eller data som återanvänds över flera testmetoder.

## Mock
Falskt objekt som simulerar ett verkligt beroende. Används för att isolera testet.

## Stub
Enkel implementation som returnerar förutbestämda värden. Används när du bara behöver indata till testet.

## Code Coverage
Mått på hur stor del av koden som täcks av tester. Hög täckning minskar risk för dolda buggar.

## xUnit
Vanligaste testramverket för .NET. Alternativ: NUnit, MSTest.

## Fluent Assertions
Bibliotek som ger mer läsbara assertions: `result.Should().Be(5);`.


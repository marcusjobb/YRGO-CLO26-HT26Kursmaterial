---

title: Csrepgrund
author: Marcus Ackre Medina
type: exercise
topic: syntax
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/CSharpUtmaningar/Studentversion/Repetition 1/CSharpRepetition/CSRepGrund.cs"
description: "﻿#pragma warning disable RCS1163 // Unused parameter."
tags: ["csharp", "csrepgrund.cs", "exercise", "oop", "syntax", "test"]
week_fit: []
---
```csharp
﻿#pragma warning disable RCS1163 // Unused parameter.
#pragma warning disable IDE0060 // Remove unused parameter
#pragma warning disable RCS1118 // Mark local variable as const.
namespace CSharpRepetition.Net20.MarcusMedina
{
    using CSharpRepetition.MarcusKod;

    /// <summary>
    /// Testa dina grundkunskaper i C#.
    /// </summary>
    public class CSRepGrund
    {
        /// <summary>
        /// Du kommer att få ett obestämt antal tal som funktionen ska
        /// returnera medelvärde på
        /// (Linq Average räknas som fusk).
        /// </summary>
        /// <param name="tal">Alla tal som ska behandlas.</param>
        /// <returns>Medelvärdet av talen.</returns>
        public int Medelvärde(params int[] tal)
        {
            int medelvärde = 0;
            // ---------------------------------------------------------------------------------
            // Skriv din kod nedan
            // ---------------------------------------------------------------------------------

            // ---------------------------------------------------------------------------------
            NiceDebug.DebugThis("Medelvärdet är : " + medelvärde);
            return medelvärde;
        }

        /// <summary>
        /// Du kommer att få ett obestämt antal tal som funktionen ska
        /// returnera lägsta och högsta värdet av de talen
        /// (Linq Max/Min räknas som fusk).
        /// </summary>
        /// <param name="tal">Talen som ska kontrolleras.</param>
        /// <returns>Max och Minimivärden.</returns>
        public (int, int) MinimiOchMaximiVärde(params int[] tal)
        {
            int max = int.MinValue;
            int min = int.MaxValue;
            // ---------------------------------------------------------------------------------
            // Skriv din kod nedan
            // ---------------------------------------------------------------------------------

            // ---------------------------------------------------------------------------------
            NiceDebug.DebugThis("Min och Max    : " + (min, max));
            return (min, max);
        }

        /// <summary>
        /// Skapa en loop som går igenom alla siffror i en sträng
        /// och adderar dem, när den är klar ska den returnera
        /// summan, exempelvis "552" ska ge svaret "12" alla andra
        /// tecken ska ignoreras "54.3" ska bli 12.
        /// </summary>
        /// <param name="nummer">Sträng med nummeriska värden<see cref="string"/>.</param>
        /// <returns>Summan av alla tal i strängen.</returns>
        public int SummeraText(string nummer)
        {
            int summa = 0;

            // ---------------------------------------------------------------------------------
            // Skriv din kod nedan
            // ---------------------------------------------------------------------------------

            // ---------------------------------------------------------------------------------
            NiceDebug.DebugThis("Summa          : " + summa);
            return summa;
        }
    }
}
#pragma warning restore RCS1118 // Mark local variable as const.
#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore RCS1163 // Unused parameter.

```

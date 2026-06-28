---

title: Csrepgrund
author: Marcus Ackre Medina
type: exercise
topic: syntax
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/CSharpUtmaningar/Facit/Repetition 1/CSharpRepetition/CSRepGrund.cs"
description: "﻿namespace CSharpRepetition.Net20.MarcusMedina"
tags: ["csharp", "csrepgrund.cs", "exercise", "oop", "syntax", "test"]
week_fit: []
---
```csharp
﻿namespace CSharpRepetition.Net20.MarcusMedina
{
    using System;

    using CSharpRepetition.MarcusKod;

    /// <summary>
    /// Testa dina grundkunskaper i C#
    /// </summary>
    public class CSRepGrund 
    {
        /// <summary>
        /// Du kommer att få ett obestämt antal tal som funktionen ska
        /// returnera medelvärde på
        /// (Linq Average räknas som fusk).
        /// </summary>
        /// <param name="tal">Alla tal som ska behandlas</param>
        /// <returns>Medelvärdet av talen</returns>
        public int Medelvärde(params int[] tal)
        {
            int summa = 0;
            int res = 0;
            // ---------------------------------------------------------------------------------
            // Skriv din kod nedan
            // ---------------------------------------------------------------------------------
            for (int i = 0; i < tal.Length; i++)
            {
                summa += tal[i];
            }
            res = summa / tal.Length;
            // ---------------------------------------------------------------------------------
            NiceDebug.DebugThis("Medelvärdet är : " + res);
            return res;
        }

        /// <summary>
        /// Du kommer att få ett obestämt antal tal som funktionen ska
        /// returnera lägsta och högsta värdet av de talen
        /// (Linq Max/Min räknas som fusk).
        /// </summary>
        /// <param name="tal">Talen som ska kontrolleras</param>
        /// <returns>Max och Minimivärden.</returns>
        public (int, int) MinimiOchMaximiVärde(params int[] tal)
        {
            int max = int.MinValue;
            int min = int.MaxValue;

            // ---------------------------------------------------------------------------------
            // Skriv din kod nedan
            // ---------------------------------------------------------------------------------
            for (int i = 0; i < tal.Length; i++)
            {
                if (max < tal[i])
                {
                    max = tal[i];
                }

                if (min > tal[i])
                {
                    min = tal[i];
                }
            }

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
            for (int i = 0; i < nummer.Length; i++)
            {
                string ch = nummer.Substring(i, 1);
                if (int.TryParse(ch, out int n))
                {
                    summa += n;
                }
            }
            // ---------------------------------------------------------------------------------

            NiceDebug.DebugThis("Summa          : " + summa);
            return summa;
        }
    }
}

```

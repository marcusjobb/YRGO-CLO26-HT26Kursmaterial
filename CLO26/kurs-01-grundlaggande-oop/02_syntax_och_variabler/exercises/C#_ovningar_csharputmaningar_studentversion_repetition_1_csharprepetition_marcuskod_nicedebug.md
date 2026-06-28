---

title: Nicedebug
author: Marcus Ackre Medina
type: exercise
topic: syntax
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/CSharpUtmaningar/Studentversion/Repetition 1/CSharpRepetition/MarcusKod/NiceDebug.cs"
description: "﻿namespace CSharpRepetition.MarcusKod"
tags: ["csharp", "exercise", "nicedebug.cs", "oop", "syntax"]
week_fit: []
---
```csharp
﻿namespace CSharpRepetition.MarcusKod
{
    /// <summary>
    /// Definition av <see cref="NiceDebug" />.
    /// </summary>
    public static class NiceDebug
    {
        /// <summary>
        /// Startposition Y
        /// </summary>
        public static int StartY { get; set; } = 10;

        /// <summary>
        /// Startposition X
        /// </summary>
        public static int StartX { get; set; } = 3;

        /// <summary>
        /// Maximal längd för text i en kolumn
        /// </summary>
        public static int MaxLength { get; set; } = 32;

        /// <summary>
        /// Nollställ positionerna
        /// </summary>
        /// <param name="x">X<see cref="int"/> position.</param>
        public static void Reset(int x)
        {
            StartY = 9;
            StartX = x;
        }

        /// <summary>
        /// Skriv debugrad
        /// </summary>
        /// <param name="text">Texten<see cref="string"/>.</param>
        public static void DebugThis(string text)
        {
            ConsoleGUI gui = new ConsoleGUI();
            if (text.Length > MaxLength)
            {
                text = text.Substring(0, MaxLength);
            }

            gui.PrintAt(StartX, StartY++, text);
        }
    }
}
```

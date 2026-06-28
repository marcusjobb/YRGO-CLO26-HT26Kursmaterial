---

title: Nicedebug
author: Marcus Ackre Medina
type: exercise
topic: syntax
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/CSharpUtmaningar/Facit/Repetition 1/CSharpRepetition/MarcusKod/NiceDebug.cs"
description: "﻿namespace CSharpRepetition.MarcusKod"
tags: ["csharp", "exercise", "nicedebug.cs", "oop", "syntax"]
week_fit: []
---
```csharp
﻿namespace CSharpRepetition.MarcusKod
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CSharpRepetition.MarcusKod;

    public static class NiceDebug
    {
        public static int StartY { get; set; } = 10;
        public static int StartX { get; set; } = 3;
        public static int MaxLength { get; set; } = 34;

        public static void Reset(int x)
        {
            StartY = 9;
            StartX = x;
        }
        public static void DebugThis(string text)
        {
            var gui = new ConsoleGUI();
            if (text.Length > MaxLength) text = text.Substring(0, MaxLength);
            gui.PrintAt(StartX, StartY++, text);
        }

    }
}

```

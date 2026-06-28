---

title: Consolegui
author: Marcus Ackre Medina
type: exercise
topic: syntax
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/CSharpUtmaningar/Facit/Repetition 1/CSharpRepetition/MarcusKod/ConsoleGUI.cs"
description: "using System.Collections.Generic;"
tags: ["consolegui.cs", "csharp", "exercise", "oop", "syntax"]
week_fit: []
---
```csharp
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRepetition.MarcusKod
{
    public class ConsoleGUI
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int MaxWidth { get; set; } = 120;

        public void CenterTextAt(int y, string text, bool clearLine = false)
        {
            int x = MaxWidth / 2 - text.Length / 2;
            if (clearLine)
            {
                CleanLineAt(x, y, text);
            }
            else
            {
                PrintAt(x, y, text);
            }
        }

        public void CleanLineAt(int y, string text)
        {
            var line = " ║" + new string(' ', MaxWidth - 4) + "║ ";
            var pos = 3;
            var output = line.Substring(0, pos) + text + line.Substring(pos + text.Length);
            PrintAt(y, output);
        }

        public void CleanLineAt(int x, int y, string text)
        {
            var line = " ║" + new string(' ', MaxWidth - 4) + "║ ";
            var pos = x + 3;
            var output = line.Substring(0, pos) + text + line.Substring(pos + text.Length);
            PrintAt(y, output);
        }

        public void DrawPillar(int x, int y, int height)
        {
            PrintAt(x, y, "╦");
            for (int i = 1; i < height; i++)
            {
                PrintAt(x, i+y, "║");
            }
            PrintAt(x, y+height, "╩");
        }

        public void ClearLine(int Y) => PrintAt(Y, new string(' ', 100));

        public void ClearScreen() => Console.Clear();

        public void DrawBox(bool clearScreen = true)
        {
            Console.WindowHeight = 30;
            Console.WindowWidth = 120;
            if (clearScreen) ClearScreen();
            Console.WriteLine("");
            var ceiling = " ╔" + new string('═', MaxWidth - 4) + "╗ ";
            var sides = " ║" + new string(' ', MaxWidth - 4) + "║ ";
            var sideLine = " ╠" + new string('═', MaxWidth - 4) + "╣ ";
            var floor = " ╚" + new string('═', MaxWidth - 4) + "╝ ";
            SetPos(0, 0);
            Console.WriteLine(ceiling);
            for (int i = 0; i < 25; i++)
            {
                if (i == 3 || i == 6 || i== 22) { Console.WriteLine(sideLine); }
                Console.WriteLine(sides);
            }
            Console.Write(floor);
        }

        public void PrintAt(int y, string v)
        {
            Console.SetCursorPosition(0, y);
            Console.Write(v);
        }

        public void PrintAt(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        public void SetPos(int x = -1, int y = -1)
        {
            if (x < 0) x = X;
            if (y < 0) y = Y;
            Console.SetCursorPosition(x, y);
            X = x;
            Y = Y;
        }
    }
}

```

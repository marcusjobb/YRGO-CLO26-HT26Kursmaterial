---

title: Consolegui
author: Marcus Ackre Medina
type: exercise
topic: syntax
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/CSharpUtmaningar/Studentversion/Repetition 1/CSharpRepetition/MarcusKod/ConsoleGUI.cs"
description: "﻿namespace CSharpRepetition.MarcusKod"
tags: ["consolegui.cs", "csharp", "exercise", "oop", "syntax"]
week_fit: []
---
```csharp
﻿namespace CSharpRepetition.MarcusKod
{
    using System;

    /// <summary>
    /// Definiton av <see cref="ConsoleGUI" />.
    /// </summary>
    public class ConsoleGUI
    {
        /// <summary>
        /// X position
        /// </summary>
        public int X { get; set; } = 0;

        /// <summary>
        /// Y position
        /// </summary>
        public int Y { get; set; } = 0;

        /// <summary>
        /// Maximal bredd för text
        /// </summary>
        public int MaxWidth { get; set; } = 120;

        /// <summary>
        /// Centrera texten
        /// </summary>
        /// <param name="y">y<see cref="int"/>-position.</param>
        /// <param name="text">Texten<see cref="string"/>.</param>
        /// <param name="clearLine">Om raden ska tömmas först <see cref="bool"/>.</param>
        public void CenterTextAt(int y, string text, bool clearLine = false)
        {
            int x = (MaxWidth / 2) - (text.Length / 2);
            if (clearLine)
            {
                CleanLineAt(x, y, text);
            }
            else
            {
                PrintAt(x, y, text);
            }
        }

        /// <summary>
        /// Töm en rad och skriv text på given Y-position
        /// </summary>
        /// <param name="y">Y<see cref="int"/>-position.</param>
        /// <param name="text">Texten<see cref="string"/>.</param>
        public void CleanLineAt(int y, string text)
        {
            string line = " ║" + new string(' ', MaxWidth - 4) + "║ ";
            string output = line.Substring(0, 3) + text + line[(3 + text.Length)..];
            PrintAt(y, output);
        }

        /// <summary>
        /// Töm raden och skriv in text
        /// </summary>
        /// <param name="x">X<see cref="int"/> position.</param>
        /// <param name="y">Y<see cref="int"/> position.</param>
        /// <param name="text">The text<see cref="string"/>.</param>
        public void CleanLineAt(int x, int y, string text)
        {
            string line = " ║" + new string(' ', MaxWidth - 4) + "║ ";
            int pos = x + 3;
            string output = line.Substring(0, pos) + text + line[(pos + text.Length)..];
            PrintAt(y, output);
        }

        /// <summary>
        /// Rita ett steck på höjden
        /// </summary>
        /// <param name="x">X<see cref="int"/>-position.</param>
        /// <param name="y">Y<see cref="int"/>-position.</param>
        /// <param name="height">The height<see cref="int"/>.</param>
        public void DrawPillar(int x, int y, int height)
        {
            PrintAt(x, y, "╦");
            for (int i = 1; i < height; i++)
            {
                PrintAt(x, i + y, "║");
            }
            PrintAt(x, y + height, "╩");
        }

        /// <summary>
        /// Rensa en rad
        /// </summary>
        /// <param name="Y">The Y<see cref="int"/>.</param>
        public void ClearLine(int Y) => PrintAt(Y, new string(' ', 100));

        /// <summary>
        /// Töm skärmen utan att lägga in en box
        /// </summary>
        public void ClearScreen() => Console.Clear();

        /// <summary>
        /// Rita box
        /// </summary>
        /// <param name="clearScreen">Om skrämen ska rensas först<see cref="bool"/>.</param>
        public void DrawBox(bool clearScreen = true)
        {
            Console.WindowHeight = 30;
            Console.WindowWidth = 120;
            if (clearScreen)
            {
                ClearScreen();
            }

            Console.WriteLine("");
            string ceiling = " ╔" + new string('═', MaxWidth - 4) + "╗ ";
            string sides = " ║" + new string(' ', MaxWidth - 4) + "║ ";
            string sideLine = " ╠" + new string('═', MaxWidth - 4) + "╣ ";
            string floor = " ╚" + new string('═', MaxWidth - 4) + "╝ ";
            SetPos(0, 0);
            Console.WriteLine(ceiling);
            for (int i = 0; i < 25; i++)
            {
                if (i == 3 || i == 6 || i == 22) { Console.WriteLine(sideLine); }
                Console.WriteLine(sides);
            }
            Console.Write(floor);
        }

        /// <summary>
        /// Skriver på given position
        /// </summary>
        /// <param name="y">Y<see cref="int"/> position.</param>
        /// <param name="text">Texten<see cref="string"/>.</param>
        public void PrintAt(int y, string text)
        {
            Console.SetCursorPosition(0, y);
            Console.Write(text);
        }

        /// <summary>
        /// Skriv text på position
        /// </summary>
        /// <param name="x">X<see cref="int"/> position.</param>
        /// <param name="y">Y<see cref="int"/>position.</param>
        /// <param name="text">Texten<see cref="string"/>.</param>
        public void PrintAt(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        /// <summary>
        /// Set cursorposition
        /// </summary>
        /// <param name="x">X<see cref="int"/> position.</param>
        /// <param name="y">Y<see cref="int"/> position.</param>
        public void SetPos(int x = -1, int y = -1)
        {
            if (x < 0)
            {
                x = X;
            }

            if (y < 0)
            {
                y = Y;
            }

            Console.SetCursorPosition(x, y);
            X = x;
            Y = Y;
        }
    }
}
```

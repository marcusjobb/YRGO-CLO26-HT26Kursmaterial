"""
Skapar joins_demo.db och kör demofrågorna live.
Kör: python skapa_joins_demo.py
"""

import sqlite3
import pathlib

DB_PATH = pathlib.Path(__file__).parent / "joins_demo.db"
SQL_PATH = pathlib.Path(__file__).parent / "joins_demo.sql"


def skapa_databas(con: sqlite3.Connection) -> None:
    con.executescript("""
        CREATE TABLE Students (
            StudentId INTEGER PRIMARY KEY,
            FirstName TEXT NOT NULL,
            LastName  TEXT NOT NULL
        );
        CREATE TABLE Teachers (
            TeacherId   INTEGER PRIMARY KEY,
            TeacherName TEXT NOT NULL
        );
        CREATE TABLE Courses (
            CourseCode TEXT PRIMARY KEY,
            CourseName TEXT NOT NULL,
            TeacherId  INTEGER,
            FOREIGN KEY (TeacherId) REFERENCES Teachers(TeacherId)
        );
        CREATE TABLE Enrollments (
            EnrollmentId INTEGER PRIMARY KEY,
            StudentId    INTEGER,
            CourseCode   TEXT,
            Grade        INTEGER,
            FOREIGN KEY (StudentId)  REFERENCES Students(StudentId),
            FOREIGN KEY (CourseCode) REFERENCES Courses(CourseCode)
        );
        INSERT INTO Students VALUES
            (1,'Anna','Svensson'),(2,'Erik','Larsson'),
            (3,'Maja','Nilsson'),(4,'Oscar','Lindgren'),
            (5,'Lina','Berg'),(6,'Johan','Karlsson');
        INSERT INTO Teachers VALUES
            (1,'Marcus Medina'),(2,'Sara Holm'),(3,'Ali Hassan');
        INSERT INTO Courses VALUES
            ('CS101','Programmering grunder',1),
            ('CS102','Objektorienterad design',1),
            ('CS201','Databaser',2),
            ('CS202','Webbutveckling',3),
            ('CS301','Cloud och DevOps',2);
        INSERT INTO Enrollments VALUES
            (1,1,'CS101',5),(2,1,'CS102',4),(3,1,'CS201',5),
            (4,3,'CS101',3),(5,3,'CS102',4),(6,3,'CS201',5),
            (7,4,'CS101',4),(8,4,'CS202',3),
            (9,5,'CS101',2),(10,5,'CS102',3),(11,5,'CS201',2),
            (12,5,'CS202',4),(13,5,'CS301',3);
    """)


def kör_fråga(con: sqlite3.Connection, rubrik: str, sql: str) -> None:
    print(f"\n{'─' * 50}")
    print(f"  {rubrik}")
    print(f"{'─' * 50}")
    cursor = con.execute(sql)
    kolumner = [col[0] for col in cursor.description]
    rader = cursor.fetchall()
    bredd = [max(len(str(v)) for v in [k] + [r[i] for r in rader]) for i, k in enumerate(kolumner)]
    header = "  ".join(k.ljust(b) for k, b in zip(kolumner, bredd))
    print(header)
    print("  ".join("─" * b for b in bredd))
    for rad in rader:
        print("  ".join(str(v).ljust(b) if v is not None else "NULL".ljust(b) for v, b in zip(rad, bredd)))


def main() -> None:
    if DB_PATH.exists():
        DB_PATH.unlink()

    con = sqlite3.connect(DB_PATH)
    skapa_databas(con)
    print(f"✅ Databas skapad: {DB_PATH}")

    kör_fråga(con, "INNER JOIN — bara studenter med kurser",
        """
        SELECT s.FirstName, s.LastName, e.CourseCode
        FROM Students AS s
        INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId
        """)

    kör_fråga(con, "LEFT JOIN — alla studenter (Erik och Johan → NULL)",
        """
        SELECT s.FirstName, s.LastName, e.CourseCode
        FROM Students AS s
        LEFT JOIN Enrollments AS e ON s.StudentId = e.StudentId
        """)

    kör_fråga(con, "Hitta studenter utan kurser",
        """
        SELECT s.FirstName, s.LastName
        FROM Students AS s
        LEFT JOIN Enrollments AS e ON s.StudentId = e.StudentId
        WHERE e.EnrollmentId IS NULL
        """)

    kör_fråga(con, "JOIN + GROUP BY — antal kurser per student",
        """
        SELECT s.FirstName, s.LastName,
               COUNT(e.CourseCode) AS AntalKurser
        FROM Students AS s
        LEFT JOIN Enrollments AS e ON s.StudentId = e.StudentId
        GROUP BY s.StudentId, s.FirstName, s.LastName
        ORDER BY AntalKurser DESC
        """)

    kör_fråga(con, "Tre tabeller — student, kurs och lärare",
        """
        SELECT s.FirstName, c.CourseName, t.TeacherName
        FROM Students AS s
        INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId
        INNER JOIN Courses AS c     ON e.CourseCode = c.CourseCode
        INNER JOIN Teachers AS t    ON c.TeacherId = t.TeacherId
        """)

    con.close()
    print(f"\n✅ Klar! Öppna {DB_PATH.name} i DB Browser for SQLite för att utforska vidare.")


if __name__ == "__main__":
    main()

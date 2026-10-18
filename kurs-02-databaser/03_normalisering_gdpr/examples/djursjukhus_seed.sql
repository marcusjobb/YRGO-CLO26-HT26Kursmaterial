-- Klösarvet Djursjukhus AB — SQLite-databas
-- Används till lektionen om normalisering och JOINs (v.43)

PRAGMA foreign_keys = ON;

-- ─────────────────────────────────────────
-- Tabeller
-- ─────────────────────────────────────────

CREATE TABLE IF NOT EXISTS Agare (
    AgarId  INTEGER PRIMARY KEY AUTOINCREMENT,
    Namn    TEXT    NOT NULL,
    Alder   INTEGER NOT NULL,
    Telefon TEXT    NOT NULL
);

CREATE TABLE IF NOT EXISTS Djur (
    DjurId  INTEGER PRIMARY KEY AUTOINCREMENT,
    Namn    TEXT    NOT NULL,
    Art     TEXT    NOT NULL,
    AgarId  INTEGER NOT NULL,
    FOREIGN KEY (AgarId) REFERENCES Agare(AgarId)
);

CREATE TABLE IF NOT EXISTS Besok (
    BesokId INTEGER PRIMARY KEY AUTOINCREMENT,
    Datum   TEXT    NOT NULL,
    Orsak   TEXT    NOT NULL,
    DjurId  INTEGER NOT NULL,
    FOREIGN KEY (DjurId) REFERENCES Djur(DjurId)
);

-- ─────────────────────────────────────────
-- Ägare
-- ─────────────────────────────────────────

INSERT INTO Agare (Namn, Alder, Telefon) VALUES
    ('Sara',  34, '073-111 2233'),
    ('Johan', 52, '070-555 9988'),
    ('Lena',  61, '076-222 4455');

-- ─────────────────────────────────────────
-- Djur
-- ─────────────────────────────────────────

INSERT INTO Djur (Namn, Art, AgarId) VALUES
    ('Pluto', 'Hund', 1),  -- Saras hund
    ('Misse', 'Katt', 2),  -- Johans katt
    ('Ronja', 'Katt', 1),  -- Saras katt
    ('Fido',  'Hund', 3);  -- Lenas hund

-- ─────────────────────────────────────────
-- Besök
-- ─────────────────────────────────────────

INSERT INTO Besok (Datum, Orsak, DjurId) VALUES
    ('2026-09-01', 'Klöv',        1),  -- Pluto
    ('2026-09-03', 'Klöv',        2),  -- Misse
    ('2026-09-08', 'Vaccination', 1),  -- Pluto
    ('2026-09-12', 'Vaccination', 3),  -- Ronja
    ('2026-09-15', 'Kontroll',    4),  -- Fido
    ('2026-10-01', 'Kontroll',    2);  -- Misse

-- ─────────────────────────────────────────
-- Snabbkoll — kör dessa för att verifiera
-- ─────────────────────────────────────────

-- SELECT * FROM Agare;
-- SELECT * FROM Djur;
-- SELECT * FROM Besok;

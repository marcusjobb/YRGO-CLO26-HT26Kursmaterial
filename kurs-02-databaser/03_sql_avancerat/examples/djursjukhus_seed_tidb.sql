-- Klösarvet Djursjukhus AB — TiDB / MySQL-databas
-- Används till lektionen om GROUP BY och aggregat (v.43)
-- TiDB är MySQL-kompatibelt: kör i TiDB Cloud SQL Editor

-- ─────────────────────────────────────────
-- Tabeller
-- ─────────────────────────────────────────

CREATE TABLE IF NOT EXISTS Agare (
    AgarId  INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Namn    VARCHAR(100) NOT NULL,
    Alder   INT          NOT NULL,
    Telefon VARCHAR(20)  NOT NULL
);

CREATE TABLE IF NOT EXISTS Djur (
    DjurId  INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Namn    VARCHAR(100) NOT NULL,
    Art     VARCHAR(50)  NOT NULL,
    AgarId  INT          NOT NULL,
    FOREIGN KEY (AgarId) REFERENCES Agare(AgarId)
);

CREATE TABLE IF NOT EXISTS Besok (
    BesokId INT         NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Datum   DATE        NOT NULL,
    Orsak   VARCHAR(50) NOT NULL,
    DjurId  INT         NOT NULL,
    FOREIGN KEY (DjurId) REFERENCES Djur(DjurId)
);

-- ─────────────────────────────────────────
-- Data
-- ─────────────────────────────────────────

INSERT INTO Agare (Namn, Alder, Telefon) VALUES
    ('Sara',  34, '073-111 2233'),
    ('Johan', 52, '070-555 9988'),
    ('Lena',  61, '076-222 4455');

INSERT INTO Djur (Namn, Art, AgarId) VALUES
    ('Pluto', 'Hund', 1),
    ('Misse', 'Katt', 2),
    ('Ronja', 'Katt', 1),
    ('Fido',  'Hund', 3);

INSERT INTO Besok (Datum, Orsak, DjurId) VALUES
    ('2026-09-01', 'Klöv',        1),
    ('2026-09-03', 'Klöv',        2),
    ('2026-09-08', 'Vaccination', 1),
    ('2026-09-12', 'Vaccination', 3),
    ('2026-09-15', 'Kontroll',    4),
    ('2026-10-01', 'Kontroll',    2);

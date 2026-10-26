-- seed.sql — Säkerhet & Backup, kurs-02 v.44
-- Kör mot MySQL: docker exec -i mysql-shop mysql -uroot -pSuperSecret123 < seed.sql

CREATE DATABASE IF NOT EXISTS shopdb;
USE shopdb;

-- Tabell för SQL injection-demo
CREATE TABLE IF NOT EXISTS users (
    id   INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL,
    city  VARCHAR(100)
);

INSERT INTO users (name, email, city) VALUES
    ('Emilia Svensson', 'emilia@example.com', 'Göteborg'),
    ('Carlos Ramirez', 'carlos@example.com', 'Malmö'),
    ('Patrik Berg',    'patrik@example.com', 'Stockholm'),
    ('Anna Lindqvist', 'anna@example.com',   'Göteborg'),
    ('Bella Eriksson', 'bella@example.com',  'Uppsala');

-- Tabell för transaktions-demo
CREATE TABLE IF NOT EXISTS accounts (
    id      INT AUTO_INCREMENT PRIMARY KEY,
    owner   VARCHAR(100) NOT NULL,
    balance DECIMAL(10,2) NOT NULL DEFAULT 0.00
);

INSERT INTO accounts (owner, balance) VALUES
    ('Emilia', 5000.00),
    ('Carlos', 3000.00),
    ('Patrik', 8500.00);

-- Tabell för backup-demo
CREATE TABLE IF NOT EXISTS products (
    id    INT AUTO_INCREMENT PRIMARY KEY,
    name  VARCHAR(200) NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL DEFAULT 0
);

INSERT INTO products (name, price, stock) VALUES
    ('Laptop',       12990.00, 15),
    ('Mobiltelefon',  8490.00, 42),
    ('Hörlurar',      1290.00,  8),
    ('Skärm',         4990.00, 20),
    ('Tangentbord',    890.00, 55);

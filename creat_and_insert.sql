CREATE TABLE pizze (
    id SERIAL PRIMARY KEY,
    nazwa VARCHAR(100) NOT NULL,
    skladniki TEXT,
    cena NUMERIC(10,2) NOT NULL
);

-- Tabela Zamowienia
CREATE TABLE zamowienia (
    id SERIAL PRIMARY KEY,
    imieklienta VARCHAR(100) NOT NULL,
    numertelefonu VARCHAR(20),
    pizzaid INTEGER NOT NULL,
    datazamowienia TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50) NOT NULL DEFAULT 'nowe',
    FOREIGN KEY (pizzaid) REFERENCES pizze(id)
);

-- Wstawianie przykładowych danych do tabeli Pizze
INSERT INTO pizze (nazwa, skladniki, cena)
VALUES
('Margherita', 'sos pomidorowy, ser mozzarella, bazylia', 25.00),
('Pepperoni', 'sos pomidorowy, ser mozzarella, pepperoni', 30.00),
('Hawajska', 'sos pomidorowy, ser mozzarella, szynka, ananas', 32.00),
('Quattro Formaggi', 'sos śmietanowy, mozzarella, gorgonzola, parmezan, ricotta', 35.00),
('Capricciosa', 'sos pomidorowy, ser mozzarella, szynka, pieczarki, oliwki', 33.00);

-- Wstawianie przykładowych zamówień
INSERT INTO zamowienia (imieklienta, numertelefonu, pizzaid, datazamowienia, status)
VALUES
('Jan Kowalski', '123456789', 1, '2025-06-01 14:30:00', 'w realizacji'),
('Anna Nowak', '987654321', 2, '2025-06-01 15:15:00', 'gotowe'),
('Piotr Wiśniewski', '555666777', 3, '2025-06-01 16:00:00', 'nowe'),
('Maria Dąbrowska', '111222333', 1, '2025-06-01 16:45:00', 'dostarczone'),
('Tomasz Lewandowski', '444555666', 4, '2025-06-01 17:20:00', 'w realizacji');CREATE TABLE pizze (
    id SERIAL PRIMARY KEY,
    nazwa VARCHAR(100) NOT NULL,
    skladniki TEXT,
    cena NUMERIC(10,2) NOT NULL
);

-- Tabela Zamowienia
CREATE TABLE zamowienia (
    id SERIAL PRIMARY KEY,
    imieklienta VARCHAR(100) NOT NULL,
    numertelefonu VARCHAR(20),
    pizzaid INTEGER NOT NULL,
    datazamowienia TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(50) NOT NULL DEFAULT 'nowe',
    FOREIGN KEY (pizzaid) REFERENCES pizze(id)
);

-- Wstawianie przykładowych danych do tabeli Pizze
INSERT INTO pizze (nazwa, skladniki, cena)
VALUES
('Margherita', 'sos pomidorowy, ser mozzarella, bazylia', 25.00),
('Pepperoni', 'sos pomidorowy, ser mozzarella, pepperoni', 30.00),
('Hawajska', 'sos pomidorowy, ser mozzarella, szynka, ananas', 32.00),
('Quattro Formaggi', 'sos śmietanowy, mozzarella, gorgonzola, parmezan, ricotta', 35.00),
('Capricciosa', 'sos pomidorowy, ser mozzarella, szynka, pieczarki, oliwki', 33.00);

-- Wstawianie przykładowych zamówień
INSERT INTO zamowienia (imieklienta, numertelefonu, pizzaid, datazamowienia, status)
VALUES
('Jan Kowalski', '123456789', 1, '2025-06-01 14:30:00', 'w realizacji'),
('Anna Nowak', '987654321', 2, '2025-06-01 15:15:00', 'gotowe'),
('Piotr Wiśniewski', '555666777', 3, '2025-06-01 16:00:00', 'nowe'),
('Maria Dąbrowska', '111222333', 1, '2025-06-01 16:45:00', 'dostarczone'),
('Tomasz Lewandowski', '444555666', 4, '2025-06-01 17:20:00', 'w realizacji');
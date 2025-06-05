# 🍕 System Zarządzania Pizzerią 🍕

## Opis Aplikacji

System Zarządzania Pizzerią to aplikacja desktopowa napisana w C# z wykorzystaniem Windows Forms i Entity Framework Core. Aplikacja umożliwia kompleksowe zarządzanie pizzerią, w tym obsługę menu pizz oraz zamówień klientów.

### Główne funkcjonalności:

- **Zarządzanie pizzami**: dodawanie, edycja, usuwanie pizz z menu
- **Zarządzanie zamówieniami**: przyjmowanie, edycja i śledzenie statusu zamówień
- **Walidacja danych**: kompleksowa walidacja wprowadzanych danych
- **Intuicyjny interfejs**: przyjazny interfejs użytkownika z DataGridView
- **Relacyjna baza danych**: wykorzystanie PostgreSQL z relacjami między tabelami

### Technologie użyte:

- **C# .NET** - język programowania
- **Windows Forms** - interfejs użytkownika
- **Entity Framework Core** - ORM do obsługi bazy danych
- **PostgreSQL** - system zarządzania bazą danych
- **Npgsql** - dostawca PostgreSQL dla .NET

## Opis Bazy Danych

Aplikacja wykorzystuje relacyjną bazę danych PostgreSQL składającą się z dwóch głównych tabel:

### Tabela `pizze`

CREATE TABLE pizze (
id SERIAL PRIMARY KEY,
nazwa VARCHAR(100) NOT NULL,
skladniki TEXT,
cena NUMERIC(10,2) NOT NULL
);

text

**Pola:**
- `id` - unikalny identyfikator pizzy (klucz główny)
- `nazwa` - nazwa pizzy (wymagane, max 100 znaków)
- `skladniki` - lista składników oddzielonych przecinkami
- `cena` - cena pizzy (format: 10,2)

### Tabela `zamowienia`

CREATE TABLE zamowienia (
id SERIAL PRIMARY KEY,
imieklienta VARCHAR(100) NOT NULL,
numertelefonu VARCHAR(20),
pizzaid INTEGER NOT NULL,
datazamowienia TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
status VARCHAR(50) NOT NULL DEFAULT 'nowe',
FOREIGN KEY (pizzaid) REFERENCES pizze(id)
);

text

**Pola:**
- `id` - unikalny identyfikator zamówienia (klucz główny)
- `imieklienta` - imię i nazwisko klienta (wymagane)
- `numertelefonu` - numer telefonu klienta
- `pizzaid` - identyfikator zamówionej pizzy (klucz obcy)
- `datazamowienia` - data i czas złożenia zamówienia
- `status` - status zamówienia (nowe, w realizacji, gotowe, dostarczone)

### Relacje:
- **1:N** między `pizze` a `zamowienia` (jedna pizza może być w wielu zamówieniach)

## Konfiguracja i Uruchomienie Aplikacji

### Wymagania systemowe:

- **System operacyjny**: Windows 10/11
- **.NET Framework**: .NET 6.0 lub nowszy
- **PostgreSQL**: wersja 14 lub nowsza
- **Visual Studio**: 2022 lub nowszy (dla developmentu)

### Instalacja PostgreSQL:

1. Pobierz PostgreSQL z [oficjalnej strony](https://www.postgresql.org/download/)
2. Zainstaluj z domyślnymi ustawieniami
3. Zapamiętaj hasło dla użytkownika `postgres`
4. Domyślny port: `5432`

### Konfiguracja bazy danych:

1. **Utwórz bazę danych:**

CREATE DATABASE pizzeriadb;

text

2. **Wykonaj skrypt tworzący tabele:**
- Otwórz plik `pizzeria_baza.sql`
- Wykonaj skrypt w pgAdmin 

text

### Konfiguracja aplikacji:

1. **Sklonuj/pobierz kod źródłowy**

2. **Zainstaluj pakiety NuGet:**

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

text

3. **Skonfiguruj connection string:**

W pliku `AppDbContext.cs` zaktualizuj connection string:

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=pizzeriadb;Username=postgres;Password=TWOJE_HASŁO");
}

text

### Uruchomienie aplikacji:

1. **Przez Visual Studio:**
- Otwórz plik `.sln`
- Naciśnij `F5` lub `Ctrl+F5`

2. **Przez wiersz poleceń:**

dotnet build
dotnet run

text

### Struktura projektu:

WinFormsApp1/
├── Models/
│ ├── Pizza.cs # Model pizzy
│ ├── Zamowienie.cs # Model zamówienia
│ └── AppDbContext.cs # Kontekst Entity Framework
├── Forms/
│ ├── Form1.cs # Zarządzanie pizzami
│ └── Form3.cs # Zarządzanie zamówieniami
├── SQL/
│ └── pizzeria_baza.sql # Skrypt tworzący bazę danych
└── Program.cs # Punkt wejścia aplikacji

text

## Instrukcja użytkowania

### Zarządzanie pizzami (Form1):

1. **Dodawanie pizzy:**
   - Wprowadź nazwę (min. 3 znaki, bez cyfr)
   - Dodaj składniki (oddzielone przecinkami, min. 2 składniki)
   - Ustaw cenę (1-999 zł)

2. **Edycja pizzy:**
   - Kliknij na komórkę i wprowadź nowe dane
   - Walidacja zostanie wykonana automatycznie

3. **Usuwanie pizzy:**
   - Kliknij prawym przyciskiem na wiersz
   - Wybierz "Usuń" z menu kontekstowego
   - Potwierdź usunięcie

### Zarządzanie zamówieniami (Form3):

1. **Dodawanie zamówienia:**
   - Wprowadź imię klienta (min. 2 znaki, bez cyfr)
   - Podaj numer telefonu (dokładnie 9 cyfr)
   - Wybierz pizzę z listy rozwijanej
   - Ustaw datę (kliknij na komórkę daty dla kalendarza)
   - Wybierz status z dostępnych opcji

2. **Edycja zamówienia:**
   - Kliknij na komórkę i wprowadź zmiany
   - Status można zmienić z listy rozwijanej

3. **Usuwanie zamówienia:**
   - Kliknij prawym przyciskiem na wiersz
   - Wybierz "Usuń" i potwierdź

### Walidacja danych:

Aplikacja automatycznie waliduje:
- **Imiona**: tylko litery, 2-50 znaków
- **Numery telefonów**: tylko cyfry, dokładnie 9 znaków
- **Nazwy pizz**: bez cyfr, 3-50 znaków
- **Składniki**: min. 2 składniki oddzielone przecinkami
- **Ceny**: liczby dodatnie 1-999 zł
- **Daty**: poprawny format DateTime
- **Statusy**: tylko dozwolone wartości
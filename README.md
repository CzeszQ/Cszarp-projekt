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
- **PostgreSQL**: wersja 12 lub nowsza
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
- Wykonaj skrypt w pgAdmin lub przez psql:

psql -U postgres -d pizzeriadb -f pizzeria_baza.sql

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
PROJEKT BEFIT - CYFROWY DZIENNIK TRENINGOWY
O APLIKACJI
BeFit to aplikacja internetowa służąca do rejestrowania postępów treningowych. Zastępuje tradycyjny zeszyt – pozwala tworzyć sesje treningowe, dodawać do nich konkretne ćwiczenia oraz śledzić wyniki (ciężar, powtórzenia, a także parametry cardio jak tętno i tempo).

System automatycznie rozpoznaje użytkownika, dzięki czemu każdy widzi wyłącznie własne treningi.

GŁÓWNE FUNKCJONALNOŚCI

System Kont: Rejestracja i logowanie. Podział na Administratora (zarządza bazą ćwiczeń) i Użytkownika (prowadzi dziennik).

Baza Ćwiczeń: Gotowa lista 30 ćwiczeń (siłowych i cardio) wgrana automatycznie przy starcie.

Dziennik Sesji:

Tworzenie nowej sesji (data + notatka).

Automatyczne przejście do widoku szczegółów po utworzeniu.

Dodawanie ćwiczeń bezpośrednio do konkretnej sesji.

Obsługa parametrów: Liczba powtórzeń, Obciążenie [kg], Tempo, Tętno.

Statystyki: Podgląd wyników z ostatnich 4 tygodni.

TECHNOLOGIA

Framework: ASP.NET Core MVC (.NET 8)

Baza danych: SQLite (plik BeFit.db tworzony automatycznie)



INSTRUKCJA URUCHOMIENIA

Upewnij się, że masz zainstalowane środowisko .NET 8 SDK.

Otwórz folder projektu w terminalu.

Wpisz komendę:
dotnet run

Aplikacja sama utworzy bazę danych i wgra przykładowe ćwiczenia.

Otwórz w przeglądarce adres wyświetlony w terminalu (zazwyczaj http:

KONTO ADMINISTRATORA (Domyślne)
Aplikacja przy pierwszym uruchomieniu tworzy konto administratora do testów:
Login: admin@befit.pl
Hasło: BeFit123!
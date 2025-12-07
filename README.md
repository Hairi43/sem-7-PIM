# Aplikacja PIM

## Przegląd projektu
PimApp to interaktywna aplikacja mobilna stworzona w środowisku Unity, która łączy rywalizację w mini-grach z kołem fortuny. Głównym celem projektu jest stworzenie aplikacji mobilnej połączonej z bazą danych. Aplikacja oferuje zestaw 2 minigier zręcznościowo-logicznych, dobieranych za pomocą mechaniki "Koła fortuny". Całość wspierana jest przez integrację z chmurą Google Firebase, co zapewnia synchronizację osiągnięć w grach, a także bezpieczną autoryzację użytkowników.

---

## Architektura Systemu

System został zaprojektowany w oparciu o architekturę modułową, dzielącą aplikację na trzy główne warstwy logiczne. **Frontend**, realizowany w silniku Unity, odpowiada za zarządzanie interfejsem użytkownika oraz logikę gier. Warstwa **Danych** obejmuje komponenty takie jak `SaveManager`, `GameStateService` oraz `CollectiblesController`, które zarządzają bieżącym stanem aplikacji. **Backend Services** wykorzystują Firebase Auth do procesu uwierzytelniania oraz Firestore Database do trwałego przechowywania danych w chmurze.

Przepływ danych rozpoczyna się od weryfikacji tokenu autoryzacji Google przy uruchomieniu aplikacji. System następnie pobiera obiekt `GameData` z Firestore, który zawiera historię postępów i rekordy użytkownika. W trakcie rozgrywki stan `GameState` jest aktualizowany lokalnie, a po zakończeniu aktywności następuje synchronizacja z bazą danych.

---

## Moduły Aplikacji

Projekt składa się z niezależnych modułów funkcjonalnych zlokalizowanych w katalogu `Modules/`.
Moduł Start & Login inicjalizuje aplikację i obsługuje integrację z Google Sign-In za pośrednictwem klas `LoginManager` i `UserSession`. 
Centralnym punktem aplikacji jest moduł Achievements, który umożliwia dostęp do wszystkich funkcjonalności i podgląd odblokowanych gier i wyników. Wybór minigry realizowany jest Koło fortuny, wykorzystujący fizykę rotacji do losowania rozgrywki.Za serializację danych odpowiada moduł SaveData, obsługujący stany gier i elementy kolekcjonerskie.

---

## Minigry

Aplikacja oferuje zróżnicowane tryby rozgrywki. **Bob Dodger** jest grą zręcznościową, w której gracz steruje wózkiem, unikając przeszkód, co wymagało implementacji systemu spawnowania bomb w losowych koordynatach. **block builder** opiera się na fizyce wahadła, wymagając od gracza precyzyjnego upuszczania kołyszących się bloków w celu zbudowania wieżowca.

---

## Zarządzanie Danymi i Integracje

Dane użytkownika są przechowywane w kolekcji `users` w bazie Firestore i synchronizowane za pomocą singletona `FirestoreManager`. Model danych obejmuje listy stanów gier (`gameStates`) oraz odblokowanych elementów kolekcjonerskich (`facts`, `creatures`).

W strukturze kodu zastosowano standardowe wzorce projektowe. **Singleton** został użyty dla kluczowych menedżerów, takich jak `FirestoreManager` i `UserSession`, zapewniając globalny dostęp do instancji. Wzorzec **Observer** obsługuje zdarzenia globalne, przykładowo w klasie `EndOfGameManager`, natomiast **Service Locator** ułatwia dostęp do usług stanu gry poprzez `GameStateService`.

---

## Diagram przejść między scenami aplikacji

<img width="381" height="731" alt="diagram2" src="https://github.com/user-attachments/assets/b38b63bc-0169-478f-8223-5697cad15ea1" />

---

## Instalacja i Konfiguracja

Środowisko deweloperskie wymaga silnika Unity w wersji 2022.3 LTS lub nowszej oraz Android SDK dla API Level 24 (Android 7.0+). Projekt wykorzystuje zewnętrzne zależności, w tym Firebase SDK, ARFoundation, Google Sign-In Plugin oraz ZXing.Net.

Proces uruchomienia projektu przebiega w następujących krokach:
1.  Sklonowanie repozytorium z systemu kontroli wersji.
2.  Konfiguracja Firebase poprzez utworzenie projektu w konsoli, dodanie aplikacji Android (zgodnej z nazwą pakietu Unity) oraz umieszczenie pliku `google-services.json` w folderze `Assets/`. Należy również aktywować usługi Authentication i Firestore Database.
3.  Konfiguracja Google Sign-In wymagająca wygenerowania OAuth 2.0 Client ID w Google Cloud Console, wprowadzenia Web Client ID w skryptach oraz dodania odcisku SHA-1 w konsoli Firebase.
4.  Skompilowanie projektu dla platformy Android z ustawieniami architektury ARM64 i backendu IL2CPP.

---

## Autorzy i Licencja

Joanna Kuc, Michał Pawlica, Wincenty Wensker, Dawid Łapiński, Hubert Albanowski

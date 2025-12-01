Program stworzony w Unity. 
Program pozwala użytkownikowi na granie w minigry. 
Pozwala się zalogować za pomocą konta poczty gmail wykorzystując udostępniony przez Google sposób autoryzacji. 
Składa się z czterech głównych scen:
  -Startowej sceny, odpalanej na starcie aplikacji, ta scena pozwala na zalogowanie się za pomocą konta gmail
  -Scena „Koło fortuny”, scena zawiera koło fortuny pozwalające użytkownikowi na losowanie minigry do zagrania. 
  -Scena „Kolekcja”, zawiera panel skrótów do odblokowanych minigier pozwalający na granie bez ich losowania oraz zapisy rekordów
  -Scena „Autorzy”, zawiera nazwiska studentów odpowiedzialnych za to arcydzieło gałęzi gamingowej nauki
Oprócz tego każda minigra jest osobną sceną z własnymi zasadami, w wersji 0.2.8 early accesu dostępne są dwie minigry, ale autorzy nie zaprzeczają, że wraz z dalszym rozwojem wprowadzą nowe minigry, świetnie się nadające do zabicia czasu kiedy czeka się na przyjazd kuriera, obecne minigry to:
  -bomb dodger: piękna zręcznościowa minigra polegająca na unikaniu spadających bomb, za każde ominięcie gracz otrzymuję punkt
  -block builder : Gra polegająca na budowaniu coraz większej wieży za pomocą trafiania blokami w dotychczasową strukturę
Program wykorzystuje Googlowski FirestoreDB do zapisu i przechowywania rekordów, osiągnięć i odblokowanych przez użytkownika minigier (autorzy nie przyznają się do dodatkowego zbierania innych informacji na temat użytkownika :-) )

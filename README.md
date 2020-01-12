# Snake-In-WPF/ Pink Snake
Repozytorium projektu na przedmiot Programowanie Obiektowe w języku C#.

Autorzy: Piotr Pochopień, Kamil Jamruz

# Opis
Projekt jest tworzony w języku C# używając Windows Presentation Foundation w programie Visual Studio.<br/>
Projekt będzie grą opartą na sterowaniu strzałkami, zjadaniu "jedzenia" wydłużającego gracza i śmierci przy zetknięciu z własnym ciałem lub z kolcami.<br/>
Wąż zrobiony jest z kwadratów, a w kodzie będzie reprezentowany jako tablica której wartości będą updatowane co każdy ruch gracza.<br/>
Gra posiada wzrastający poziom trudności gdyż z każdym zjedzonym jabłkiem pojawiają się dodatkowe kolce co utrudnia rozgrywkę i zwiększa satysfakcję z grania. <br/>
Instyktowne menu pozwalające na wyjśćie z aplikacji, zaczęcie gry, zmiany języka lub zmiane koloru węża którym będzie się grało.

Gra została podzielona na 3 projekty:
-class Library w którym zawarta jest cała logika gry rozdzielona na odpowiednie klasy. <br/>
Jest tam zarówno obsługa przemieszczania się węża, zjadanie jabłek jak i tworzenie planszy oraz zmiana kolorów.

-TestProject w którym znajdują się wszystkie testy jednostkowe dotyczące logiki gry. <br/>
Opisują one każdy element zawarty w projekcie class Library.

-Snake WPF w którym znajduje się interfejs gry czyli menu wraz z przyciskami oraz obrazkami i ich ułożenie.

# Procentowy udział i realizacja zadań
Lider:Piotr Pochopień 50%<br/>
Kamil Jamruz 50%

Realizacja zadań:

Kamil Jamruz:<br/>
-Testy jednostkowe<br/>
-Logika gry (zarówno węża jak i powstawanie planszy oraz wszelkie zasady związane z naszą grą)<br/>
-kolorowanie węża i innych przedmiotów<br/>
-debugowanie<br/>

Piotr Pochopień:<br/>
-Interfejs gry (Menu wraz z interrakcją buttonów i UI gry)<br/>
-dokumentacja xml i github<br/>
-przygotowanie instalatora<br/>
-testy jednostkowe<br/>


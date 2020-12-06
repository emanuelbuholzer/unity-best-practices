# BP0008: Use small classes

## Cause

Eine Klasse übersteigt 100 Zeilen Code.

## Rule description

Der Einsatz resp. die Implementation von Klassen welche die Anzahl von mehr als 100 Zeilen Code übersteigen sprechen meist für ein Refactoring der Klasse.
In Anlehnung an das Single Responsibilty Princip (SRP)[[2]](*2) und der Aussage von Robert Martin (Uncle Bob): "Es sollte nie mehr als einen Grund geben eine Klasse zu ändern" [[1]](*1), kann davon ausgegangen werden, dass die Klasse Optimierungspotential aufweist.

## How to fix violations

Einsatz von kleinen, dafür vielen Klassen die folgende Grundsätze einhalten [[3]](*3):

* Jedes Modul oder jede Klasse sollte die Verantwortung eines einzelnen Teils einer Software tragen
* Die Verantwortung der Klasse sollte vollständig innerhalb der Klasse gekapselt werden
* Die Verantwortung aller Dienste sollten eng darauf ausgerichtet sein

Die daraus resultierenden Vorteile sind insbesondere[[3]](#3):

* Lesbarkeit
* Erweiterbarkeit
* Wiederverwendbarkeit / Erhöht Modularität

## When to suppress warnings

Nie

## Example of how to fix

### Description

Eine Design-Überarbeitung der Klasse auf Basis des SRP-Pattern vornehmen. 

## Related rules

Keine

## References

<a id="1">[1]</a>
Wikipedia, 21. September 2020, Prinzipien objektorientierten Designs.<br />
aufgerufen 23. September 2020 von https://de.wikipedia.org/wiki/Prinzipien_objektorientierten_Designs

<a id="2">[2]</a>
Unity Connect, 16. September 2020, SOLID Principles in Unity.<br /> 
Aufgerufen 23. September 2020 von https://connect.unity.com/p/solid-principles-in-unity-part-2-single-responsibility-principle

<a id="3">[3]</a>
HSLU GAMEDEV, Herbstsemester 2019, Game Architecture S.19<br />
https://drive.google.com/file/d/14pu-zxf7NAG8-nKgfBrdSwbPRTU-MPrx/view?usp=sharing
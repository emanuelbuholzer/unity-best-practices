# Effizientes Vergleichen von Strings

## Problem

Bestimmte C# 'String'-APIs gelten mit Ausnahme auf ordinale Vergleiche als äusserts uneffizient[[1]](#1).
Dies betrifft auch die folgende Methoden für Vergleiche von Strings, welche sehr oft genutzt wird:

**Beispiel**:
```csharp

String.Compare(String, String) //returns a boolean

String.Equals(String, String) //returns a integer

```
Die 'String.Equals)'-Methode sollte nicht ohne Methodenüberladung Vergleich von zwei Strings eingesetzt werden.[[3]](#3).

## Lösung

Damit beim Vergleich von Strings eine bessere Leistung wie auch ein sicherer Standart für den kulturunabhängigen Vergleich gewährleistet ist,
sollte die Methode mittels einem Parameter vom Typ StringComparison überladenen aufgerufen werden.

Eine andere, elegante Möglichkeit bietet der Einsatz der 'CompareOrdinal' Methode[[2]](#2). 


**Beispiel**:
```csharp

string a = "Hallo";
string b = "H@llo";

//mittels Überladenem Sting.Equal Aufruf
bool c = a.Equals(b, StringComparison.Ordinal);

//mittels String.CompareOrdinal

int d = CompareOrdinal(a, b);

```
Weiterführende Best Practices für Vergleiche und Handhabung von Strings sind unter [[3]](#3) auffindbar. 


## Referenzen
<a id="1">[1]</a>
Unity Technologies, 27. Oktober 2020, Strings and text. <br /> 
Aufgerufen 08. November 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html

<a id="2">[2]</a>
Microsoft Documentation, 11. November 2020,String.CompareOrdinal Method. <br />
Aufgerufen 11. November 2020 von https://docs.microsoft.com/en-us/dotnet/api/system.string.compareordinal?view=netcore-3.1

<a id="3">[3]</a>
Unity Technologies, 11. November 2020. September 2020, Best practices for comparing strings in .NET. <br />
Aufgerufen 11. November 2020 von https://docs.microsoft.com/en-us/dotnet/standard/base-types/best-practices-strings?redirectedfrom=MSDN
# Richtiger Umgang mit Strings

## Problem

Der Umngang mit Strings ist eine der häufigsten Performanceprobleatiken in Unity. In C# sind alle Strings immutable (unveränderlich), daher führt jede Änderung implizit zu einer Zuweisung eines neuen String.
Genau diese Verkettungen von Zeichenfolgen können zu Performanceproblematiken führen wenn es sich um grosse Strings, grosse Datenmengen oder engen Schleifenkosntrukte handelt.[[1]](#1) 

Daher sollten folgende Methoden wann immer möglich selbst implementiert oder zumindest mittels Ordinals ausgeführt werden:

```csharp
String.StartsWith(String)

String.EndsWith(String)
````
## Lösung


```csharp

```
## Referenzen

<a id="1">[1]</a>
Unity Dokumentation Version 5.6, 03. März 2016, Strings und Text<br/>
Aufgerufen 29. Oktober 2020 https://docs.unity3d.com/560/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity5.html

<a id="2">[2]</a>
Unity Dokumentation, 29. September 2020, Singletons in Unity<br/>
Aufgerufen 03. Okotber 2020 https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html
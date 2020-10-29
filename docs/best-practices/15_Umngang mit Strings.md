sich # Richtiger Umgang mit Strings

## Problem

Der Umngang mit Strings ist eine der häufigsten Performanceprobleatiken in Unity. In C# sind alle Strings immutable (unveränderlich), daher führt jede Änderung implizit zu einer Zuweisung eines neuen String.
Genau diese Verkettungen von Zeichenfolgen können zu Performanceproblematiken führen wenn es sich um grosse Strings, grosse Datenmengen oder engen Schleifenkosntrukte handelt. 

Daher sollten folgende Methoden immer selbst implementiert oder zumindest mittels Ordinals ausgeführt werden:

```csharp

String.StartsWith(String)

String.EndsWith(String)
````
## Lösung


```csharp

```
## Referenzen

<a id="1">[1]</a>
stackoverflow, 03. März 2016, GameObject.FindObjectOfType<>() vs GetComponent<>()<br/>
Aufgerufen 03. Oktober 2020 https://stackoverflow.com/questions/30310847/gameobject-findobjectoftype-vs-getcomponent

[2]
Unity Dokumentation, 29. September 2020, Singletons in Unity<br/>
Aufgerufen 03. Okotber 2020 https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html
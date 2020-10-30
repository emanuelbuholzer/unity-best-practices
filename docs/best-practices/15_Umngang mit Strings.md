# Richtiger Umgang mit Strings

## Problem

Der Umngang mit Strings ist eine der häufigsten Performanceproblematiken in Unity. In C# sind alle Strings immutable (unveränderlich), dass heisst jede Änderung führt implizit zu einer Zuweisung eines neuen String.
Diese Verkettungen von Zeichenfolgen führen zu Performanceproblemen wenn es sich um grosse Strings, grosse Datenmengen oder engen Schleifenkonstrukte handelt.[[1]](#1) 

Bei folgenden Methoden ist bei diesem Thema besondere Beachtung zu schenken:

```csharp
String.StartsWith()

String.EndsWith()

String.Format()
````
## Lösung
Aus oben beschriebener Problematik sollten die Methoden wann immer möglich selbst implementiert oder zumindest mittels Ordinals ausgeführt werden:

```csharp

// Beispiel eigener Implementierung für .StartWirhStrings und EndWithStrings

    public static bool CustomEndsWith(string a, string b) {
        int ap = a.Length - 1;
        int bp = b.Length - 1;

        while (ap >= 0 && bp >= 0 && a [ap] == b [bp]) {
            ap--;
            bp--;
        }
        return (bp < 0 && a.Length >= b.Length) || 

                (ap < 0 && b.Length >= a.Length);
        }

    public static bool CustomStartsWith(string a, string b) {
        int aLen = a.Length;
        int bLen = b.Length;
        int ap = 0; int bp = 0;

        while (ap < aLen && bp < bLen && a [ap] == b [bp]) {
        ap++;
        bp++;
        }

        return (bp == bLen && aLen >= bLen) || 

                (ap == aLen && bLen >= aLen);
    }
```

## Referenzen

<a id="1">[1]</a>
Unity Dokumentation, 27.10.2020 Strings und Text<br/>
Aufgerufen 29. Oktober https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
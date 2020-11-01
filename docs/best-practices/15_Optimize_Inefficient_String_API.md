# Optimierung von integrierten, ineffizienten String API's 

## Problem
Mit Ausnahme der Umstellung auf ordinale Vergleiche sind sind bestimmte C# String-API's äusserst ineffizient und können zu Performanceproblemen führen. Darunter befinden sich folgende Methoden:

```csharp
String.StartsWith()

String.EndsWith()

String.Format()
````

## Lösung
Gemäss beschriebener Problematik sollten die Methoden wann immer möglich selbst implementiert oder zumindest mittels Ordinals ausgeführt werden.
Dabei können die Methoden String.StartsWith und String.EndsWith relativ einfach ersetzt und optimiert werden. Die Methode String.Format ist dabei schwer zu ersetzen.
```csharp

// Beispiel einer Implementierung für .StartWirhStrings und EndWithStrings 

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
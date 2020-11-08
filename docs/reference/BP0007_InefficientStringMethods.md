# BP0007: Inefficient String Methods

## Cause

Sie haben eine der folgennden ineffizienten String Methoden aufgerufen:
  - `String.StartsWith`
  - `String.EndsWith`

## Rule description

Mit Ausnahme der Umstellung auf ordinale Vergleiche sind sind bestimmte C# String-API's äusserst ineffizient und können zu Performanceproblemen führen [[1]]. Darunter befinden sich folgende Methoden:
  - `String.StartsWith`
  - `String.EndsWith`

## How to fix violations

Gemäss beschriebener Problematik sollten die Methoden wann immer möglich selbst implementiert oder zumindest mittels Ordinals ausgeführt werden.
Dabei können die Methoden 'String.StartsWith' und 'String.EndsWith' relativ einfach ersetzt und optimiert werden.

## When to suppress warnings

Nie

## Example of a violation

### Description

Die ineffizienten String Methode `String.StartsWith` wird aufgerufen.

### Code

```csharp
"RespawnEnemy".StartsWith("Respawn");
```

## Example of how to fix

### Description

Eigene Implementierung liefern oder ordinale Methode verwenden [[1]]. 

### Code

```
// Beispiel einer Implementierung für .StartsWith und .EndsWith 

public static bool CustomEndsWith(string a, string b) {
    int ap = a.Length - 1;
    int bp = b.Length - 1;

    while (ap >= 0 && bp >= 0 && a [ap] == b [bp]) {
        ap--;
        bp--;
    }
    return (bp < 0 && a.Length >= b.Length) || (ap < 0 && b.Length >= a.Length);
    }

public static bool CustomStartsWith(string a, string b) {
    int aLen = a.Length;
    int bLen = b.Length;
    int ap = 0; int bp = 0;

    while (ap < aLen && bp < bLen && a [ap] == b [bp]) {
        ap++;
        bp++;
    }

    return (bp == bLen && aLen >= bLen) || (ap == aLen && bLen >= aLen);
}

//Beispiel mit Verwendung eines Ordinal
public static bool StartsWithOrdinal(string b, StringComparsion){
    a.Startswith(b, StringComparsion.Ordinal);  
}
```

## Related rules

[BP0006: Ordinal String Comparison](https://github.com/emanuelbuholer/unity-best-practices/blob/master/docs/reference/BP0006_OrdinalStringComparison.md)

## References
<a id="1">[1]</a>
Unity, 27. Oktober 2020 Strings und Text<br/>
Aufgerufen 29. Oktober https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html

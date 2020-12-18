# BP0013: Avoid Method References

## Cause

Es wurde eine Methodenreferenz verwendet.

## Rule description

Alle Methodenreferenzen in C# sind Verweistypen und werden demnach auf dem Heap alloziert.
Bei der Übergabe einer Methode (Referenz) oder einer anonymen Methode an eine andere Methoden, werden daher temmporäre Allokationen auf dem Heap vorgenommen [1].

Dabei wird nicht zwischen anonymen und normalen Methodenreferenzen unterschieden.
Dies kann zu ungewollten Speicherverbauch führen und zu Perfomanzeinbussen führen, speziell wenn Code pro Frame in der Update()-Methode aufgerufen wird.

## How to fix violations

Methodenreferenzen oder anonyme Methoden sollten wenn möglich vermieden werden, besonders in der `Update()`-Methode.

## When to suppress warnings

Die Unterdrückung macht grundsätzlich sinn, sofern sie nicht in einer `Update`-Methode geschieht.

## Example of a violation

### Description

Durch die Verwendung der `Select`-Methode muss eine anonyme Methode und daher eine Methodenreferenz verrwendet werden.

### Code

```csharp
int[] lengthsInMeters = getLengths();

int[] lengthsInMilimeters = lengthsInMeters.Select(l => l * 1000).ToArray();
```

## Example of how to fix

### Description

Anstatt die `Select`-Methode zu verwenden, wurde dessen Semantik mit einem `for`-Loop ausprogrammiert.

### Code

```csharp
int[] lengthsInMeters = getLengths();

int[] lengthsInMilimeters = new int[](lengthsInMeters.Length);
for (int i = 0; i < lengthsInMeters.Length; i++) 
{
    lengthsInMiliteres[i] = lengthsInMeters[i] * 1000;
}
```


## Related rules

[BP0012: Avoid Closures](https://github.com/emanuelbuholer/unity-best-practices/blob/master/docs/reference/BP0012_AvoidClosures.md)

## References

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, Understanding the managed heap. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html



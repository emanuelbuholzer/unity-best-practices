# BP0014: Avoid Boxing

## Cause

Es wurde ein primitiver Werttyp als Verweistyp verwendet.

## Rule description

Wenn primitive Werttypen (z.B. `int` oder `float`) an Objekt-typsierte Methoden übergeben werden, wird der primitve Werttyp als Verweistyp verwendet und [Boxing](https://docs.microsoft.com/de-ch/dotnet/csharp/programming-guide/types/boxing-and-unboxing) wird durchgeführt.

In diesem Fall werden die Wertetypen auf dem Heap alloziert anstatt auf dem Stack, was weniger effizient ist.
Da Unity kein Generational Garbage Collection verwendet, können die kleinen, häufigen temporären Boxing Allokationen nicht effizient aufgeräumt werden [[1]](#1).
Beide Faktoren können zu negativen Auswirkungen auf die Perfomance des Games wirken.


## How to fix violations

Boxing sollte nach Möglichkeit vermieden werden, wenn C#-Code für Unity-Laufzeiten geschrieben wird.

## When to suppress warnings

Grundsätzlich kann diese Diagnostic unterdrückt werden, jedoch kann es bei vielen Verstössen negative Auswirkungen auf die Perfomance des Games haben.

## Example of a violation

### Description

Die primiten Wertetypen `j1` und `j2` werden der Collection übergeben, indem die Methode `Add` aufgerufe wird. Diese ist durch die Verwendung von Generics Verweistypsiert und es geschieht Boxing.

### Code

```csharp
int j1 = 1;
int j2 = 32;

// Irgendwelcher Code

List<int> jointAngles = new List<int>();
jointAngles.Add(j1);
jointAngles.Add(j2);

// Irgendwelcher Code
foreach (int jointAngle in joingAngles) 
{
    // Irgendwas damit machen
}
```

## Example of how to fix

### Description

Durch die Verwendung eines Arrays kann dies umgangen werden.

### Code

```csharp
int j1 = 1;
int j2 = 32;

// Irgendwelcher Code

int[] jointAngles = new int[2]();
jointAngles[0] = j1;
jointAngles[1] = j2;

// Irgendwelcher Code
for(int i = 0; i < joingAngles.length; i+=1)
{
    // Irgendwas damit machen
}
```

## Related rules

[BP0015: BP0015: IEquality Comparer for Enum Dicts](https://github.com/emanuelbuholer/unity-best-practices/blob/master/docs/reference/BP0015_IEqualityComparerForEnumDicts.md)

## References

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, Understanding the managed heap. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html



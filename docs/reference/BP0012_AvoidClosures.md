# BP0012: Avoid Closures

## Cause

Es wurde eine Closurer verwendet.

## Rule description

C# generiert anonyme Klassen für Closures, damit die Variablen im externen Scope der Closure verfügbar sind. Für die generierten Klassen werden bei einem Aufruf einer Closure jeweils eine kopiert instanziert und dann die Kopie mit den Variablen im externen Scope instanziert.
Dabei werden übermässig und unnötig viel Ressourcen auf dem Heap alloziert.
Closures sollten daher vermieden werden [1]. 

Speziell in Code, welcher pro Frame in der `Update()`-Methode aufgerufen wird.

## How to fix violations

Closures sollten vermieden werden und nur in den für die Performanz unkritischen Stellen durch Methodenreferenzen oder anonyme Methoden ersetzt werden.

Als Alternative können Methoden, welche Closures verwenden, oft mit einfachen Primitiven, wie z.B. einem `for`-Loop umgeschrieben werden.

## When to suppress warnings

Die Unterdrückung macht grundsätzlich sinn, sofern sie nicht in einer `Update`-Methode geschieht.

## Example of a violation

### Description

Der Methode `Select` wird die anonyme Methode `l => l * scaler` übergeben, wobei diese die Variable `scaler` aus dem externen Scope verwendet. Es wird eine anonyme Klasse generiert.

### Code

```csharp
int[] lengthsInMeters = getLengths();

// Die Variable `scaler` ist nicht im Scope der anonymen Methode `v => v * scaler`,
// dahergehend entsteht daraus eine Closure.
int scaler = getScaler();
int[] lengthsInMilimeters = lengthsInMeters.Select(l => l * scaler).ToArray();
```

## Example of how to fix

### Description

Die `Select`-Methode kann mit einem einfachen `for`-Loop umgeschrieben werden. Es werden keine anonyme Klassen generiert.

### Code

```csharp
int[] lengthsInMeters = getLengths();

int scaler = getScaler();

int[] lengthsInMilimeters = new int[](lengthsInMeters.Length);
for (int i = 0; i < lengthsInMeters.Length; i++) 
{
    lengthsInMiliteres[i] = lengthsInMeters[i] * scaler;
}
```


## Related rules

[BP0013: Avoid Method References](https://github.com/emanuelbuholer/unity-best-practices/blob/master/docs/reference/BP0013_AvoidMethodReferences.md)

## References

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, Understanding the managed heap. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html



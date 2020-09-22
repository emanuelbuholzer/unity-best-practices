# Vermeiden von Closures

## Problem

C# generiert anonyme Klassen für Closures, damit die Variablen im externen Scope der Closure verfügbar sind.
Für die generierten Klassen werden bei einem Aufruf einer Closure jeweils eine kopiert instanziert und dann die Kopie mit den Variablen im externen Scope instanziert.
Dabei werden übermässig und unnötig viel Ressourcen auf dem Heap alloziert.

Closures sollten daher vermieden werden [[1]](#1). Speziell in Code, welcher pro Frame in der `Update()`-Methode aufgerufen wird.

**Beispiel**:
```csharp
int[] lengthsInMeters = getLengths();

// Die Variable `scaler` ist nicht im Scope der anonymen Methode `v => v * scaler`,
// dahergehend entsteht daraus eine Closure.
int scaler = getScaler();
int[] lengthsInMilimeters = lengthsInMeters.Select(l => l * scaler).ToArray();
```

## Lösung

Closures sollten vermieden werden und nur in den für die Performanz unkritischen Stellen durch Methodenreferenzen oder anonyme Methoden ersetzt werden.

**Beispiel**:
```csharp
int[] lengthsInMeters = getLengths();

int scaler = getScaler();

int[] lengthsInMilimeters = new int[](lengthsInMeters.Length);
for (int i = 0; i < lengthsInMeters.Length; i++) 
{
    lengthsInMiliteres[i] = lengthsInMeters[i] * scaler;
}
```

## Referenzen

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, Understanding the managed heap. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html

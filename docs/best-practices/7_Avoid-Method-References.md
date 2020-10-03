# Vermeiden von Methodenreferenzen und anonymen Methoden

## Problem

Alle Methodenreferenzen in C# sind Verweistypen und werden demnach auf dem Heap alloziert. 
Bei der Übergabe einer Methode (Referenz) oder einer anonymen Methode an eine andere Methoden, werden daher temmporäre Allokationen auf dem Heap vorgenommen [[1]](#1).
Dabei wird nicht zwischen anonymen und normalen Methodenreferenzen unterschieden.

Dies kann zu ungewollten Speicherverbauch führen und zu Perfomanzeinbussen führen, speziell wenn Code pro Frame in der `Update()`-Methode aufgerufen wird.

**Beispiel**:
```csharp
int[] lengthsInMeters = getLengths();

int[] lengthsInMilimeters = lengthsInMeters.Select(l => l * 1000).ToArray();
```

## Lösung

Methodenreferenzen oder anonyme Methoden sollten wenn möglich vermieden werden, besonders in der `Update()`-Methode.

**Beispiel**:
```csharp
int[] lengthsInMeters = getLengths();

int[] lengthsInMilimeters = new int[](lengthsInMeters.Length);
for (int i = 0; i < lengthsInMeters.Length; i++) 
{
    lengthsInMiliteres[i] = lengthsInMeters[i] * 1000;
}
```

## Referenzen

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, Understanding the managed heap. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html
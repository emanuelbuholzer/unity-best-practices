# Leere Collections in Rückgabewerten wiederverwenden

## Problem

Gewisse Entwickler präferieren den Einsatz von leeren Arrays oder Collections anstatt `null` als Rückgabewerte, wenn eine Methode eine leeres Array oder eine leere Collection zurückgeben muss [[1]](#1).
Dieses Pattern ist in vielen verwalteten Sprachen, wie z.B. C# oder Java üblich.

Dies kann ineffizient sein, wenn beim Erzeugen des leeren Arrays bzw. Collection jedesmal ein entsprechend neues Objekt erzeugt wird.

**Beispiel**:
```csharp
public static float[] solveJointAngles(float[] coords) 
{
    // Inverse Kinematische Berechnungen...
    if (!solver.HasSolution) return new float[0]; // Alternativ: new float[] { };
}
```

## Lösung

Allgemein ist es in solchen Fällen wesentlich effizienter, eine vorab allozierte Singleton-Instanz eines leeeren Arrays oder einer leeren Collection zurückzugeben.

Mit der `Enumerable.Empty<TResult>()` Methode wird ein solches Verhalten erreicht [[2]](#2). 
Die Methode erstelllt und cached ein leerres Array bzw. Collection from entsprechenden Typ.

**Beispiel**:
```csharp
public static float[] solveJointAngles(float[] coords) 
{
    // Inverse Kinematische Berechnungen...
    if (!solver.HasSolution) return new Enumerable.Empty<float>();
}
```

## Referenzen
<a id="1">[1]</a>
Unity Technologies, 16. September 2020, Understanding the managed heap. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html

<a id="2">[2]</a>
Microsoft, Enumerable.Empty<TResult> Methode.
Aufgerufen 20. September 2020 von https://docs.microsoft.com/de-ch/dotnet/api/system.linq.enumerable.empty?redirectedfrom=MSDN&view=netcore-3.1

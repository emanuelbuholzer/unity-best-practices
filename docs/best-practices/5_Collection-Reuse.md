# Wiederverwenden von Collections und Arrays

## Problem

Werden Collections oder Arrays z.B. bei jeder `Update()`-Methode neu initialisiert, werden unnötig viele Ressourcen auf dem Heap alloziert.
Bei der Verwendung von Collections und Arrays, sollten diese wenn immer möglich wiederverwendet werden [[1]](#1).

In diesem Fall wird eine Liste pro Frame neu intialisiert.

**Beispiel**:
```csharp
void Update() 
{
    List<Neighbor> nearestNeighbors = new List<Neighbor>();
    
    // Nearest neighbors finden
    findDistancesToNearestNeighbors(nearestNeighbors);
    nearestNeighbors.Sort();
    
    // Irgendwelcher Code...
}
```

## Lösung

Um dieses Problem zu lösen, können Collections oder Arrays z.B. als Felder deklariert werden und vor der Verwendung jeweils geleert werden.
Somit wird der Speicher beibehalten und über mehrere Frames hinweg verwendet.
Neuer Speicher wird nur alloziert, wenn die Liste vergrössert werden muss.

**Beispiel**:
```csharp
List<Neighbor> nearestNeighbors = new List<Neighbor>();

void Update() 
{
    nearestNeightbors.Clear();
    
    // Nearest neighbors finden
    findDistancesToNearestNeighbors(nearestNeighbors);
    nearestNeighbors.Sort();
    
    // Irgendwelcher Code...
}
```

## Referenzen

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, Understanding the managed heap. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html

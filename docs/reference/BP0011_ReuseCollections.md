# BP0011: Collection and array reuse

## Cause

Collection-Klassen und Array sollten nach der Initialisierung wann immer möglich wiederverwendet werden.

## Rule description

Wenn in C# Collection-Klassen oder Arrays verwendet werden, wann immer möglich sollte die Collection oder das Array nach Möglichkeit wiederverwendet werden.
Die Collection-Klassen haben Methode zum löschen des Inhalts `Collection<>.clear`. 
Dieser Aufruf löscht aber nur den Inhalt der Collection, gibt aber den durch die Initialisierung der Collection allozierten Speicher nicht frei.

## How to fix violations

Nach Möglichkeit bereits initialisierte Collection-Klassen und Arrays wiederverwenden.

## When to suppress warnings

Nie

## Example of a violation

### Description

Im nachfolgenden Beispiel In diesem Beispiel wird die nearestNeighborsListe einmal pro Frame neu erstellt, um einen Satz von Datenpunkten darin abzuspeichern.
Da bei jedem Aufruf von `Update()` wieder eine neue Liste mit zusätzlichem Speicher erstellt wird, ist die Implementation aus Performancegründen zu vermeiden[[1]](*1). 

### Code

```csharp

class ReuseCollectionAndArray
{
    void Update()
    {
        List<float> nearestNeighbors = new List<float>();
    
        findDistancesToNearestNeighbors(nearestNeighbors);
    
        nearestNeighbors.Sort();
    
        // … use the sorted list somehow …
    }
}
```

## Example of how to fix

### Description

Die Initialisierung der Liste sollte ausdiesem Grund ausserhalb des Methodenaufrufes einmalig erfolgen und innerhalb der MEthode wiederverwendet werden. 
Dadurch wird vermieden, dass in jedem Frame eine neue Liste erstellt wird[[1]](*1).

### Code

```csharp

class ReuseCollectionAndArray
{

    List<float> m_NearestNeighbors = new List<float>();

        void Update()
        {
            m_NearestNeighbors.Clear();
        
            findDistancesToNearestNeighbors(NearestNeighbors);
        
            m_NearestNeighbors.Sort();
        
            // … use the sorted list somehow …
        }
}
```

## Related rules

Keine

## References

<a id="1">[1]</a>
Unity, 27. Oktober 2020, Understanding the managed heap<br/>
Aufgerufen 12. Dezember 2020 https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html


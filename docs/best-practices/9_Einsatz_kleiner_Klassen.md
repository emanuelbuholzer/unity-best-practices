# Einsatz von kleinen Klassen 

## Problem

Der Einsatz resp. die Implementation von Klassen die eine Anzahl von mehr als 100 Zeilen Code übersteigen sprechen meist für ein Refactoring.
In Anlehnung an das Single Responsibilty Princip und der Aussage von Robert Martin (Uncle Bob): "eine Klasse sollte nur einen Grund haben, sich zu ändern", kann davon ausgegangen werden, dass die Klasse mehrere Softwareteile.


D [[1]](#1).

**Beispiel**:
```csharp
enum Key { a, b, c };

Dictionary<Key, string> keyStringValueDict = new Dictionary<Key, string>();
keyStringDictValue.add(Key.a, "Eine Value");
```

## Lösung

Mit einer Klasse, welche das `IEqualityComparer<T>` Interface implementiert, kann man dieses Verhalten umgehen.
Die Klasse wird aufgerufen, um den Aufruf von `GetHashCode` zu bewerkstelligen.
Dabei kann der Vergleich basierend auf einem Wertetyp geschehen und es wird kein Speicher auf dem Heap alloziert.

Die Klasse ist zustandslos und kann daher mit verschiedenen Instanzen eines solchen Dictionaries verwendet werden. 

**Beispiel**:
```csharp
public class KeyComparer : IEqualityComparer<Key> 
{
    public bool Equals(Key x, Key y) 
    {
        return x == y;
    }
    
    public int GetHashCode(Key x)
    {
        return (int) x;
    }
}

KeyComparer keyComparer = new KeyComparer();
Dictionary<Key, string> keyStringValueDict = new Dictionary<Key, string>(keyComparer);
keyStringDictValue.add(Key.a, "Eine Value");
```

## Referenzen

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, Understanding the managed heap. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html

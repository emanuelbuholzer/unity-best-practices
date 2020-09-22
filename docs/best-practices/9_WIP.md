# Verwenden eines IEqualityComperer bei der Verwendung von Enums in Dictionaries

## Problem

Dictionaries verwenden die Methode `Object.getHashCode(Object)` um den entsprechenden Hash Code für den Key des Dictionaries herauszufinden.
Dieser Aufruf wird bei den gängigen Methoden wie z.B. `Dictionary.add(key, value)`, `Dictionary.tryGetValue(key)`, `Dictionary.remove(key)`, etc. betätigt.

Die Methode `Object.getHashCode(Object)` verwendet hat einen Verweistyp als Parameter.
Wird als Argument ein `enum` übergeben, welches intern als `int` und daher als Werttyp gehandhabt wird, entsteht Boxing und es wird pro Aufruf unnötigt Speicher auf dem Heap alloziert [[1]](#1).

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

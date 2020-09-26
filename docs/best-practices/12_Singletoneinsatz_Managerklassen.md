# Verwendung von Managerklassen als Singleton 

## Problem

Managerklassen sollten naturgemäss nur einmal instanziert werden und auch szenenübergreifend erhalten bleiben. Es soll gewähleistet wreden, dass jeweils immer nur eien Instanz der Klasse existiert.


**Schlechtes Beispiel einer Implementation** :
```csharp

using UnityEngine;

public class PersistentManagerScript : MonoBehaviour

    public static PersistentManagerScript Instance {get; private set}
    
    //irgendwelche Variablen die Werte der Managerklasse halten
    //Bsp.
    //public int Value;
    
    void Awake()
    {
        if (Instance == null)
            
    }


```



## Lösung

Die getComponent-Methode sollte in der Methode Start() aufgerufen werden.
Die Methode Start() wird für den Frame aufgerufen, wenn ein Script aktivivert wird, bevor die Methode Update() erstmalig aufgerufen wird.

**Beispiel**
```csharp

using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        HingeJoint[] hinges = GetComponents<HingeJoint>();
        for (int i = 0; i < hinges.Length; i++)
        {
            hinges[i].useSpring = false;
        }
    }
}

```


## Referenzen

<a id="1">[1]</a>


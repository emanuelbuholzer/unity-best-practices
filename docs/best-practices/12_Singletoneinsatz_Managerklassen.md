# Verwendung von Managerklassen als Singleton 

## Problem

Managerklassen sollten naturgemäss nur einmal instanziert werden und auch szenenübergreifend erhalten bleiben. Dafür eignet sich der Einsatz des Singleton-Pattern.
Gemäss Singleton-Pattern soll gewähleistet werden, dass jeweils immer nur eine Instanz existiert.
Wichtig bei der Verwendung der Singleton ist, dass die Instanzierung bereits in der Awake-Methode erfolgt.


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
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
            
    }

```

Aufgrund der fehlenden Prüfung ob bereits eine Instanz existiert wird in jedem GameObject, dass das PersistentManagerScript aufruft eine zusätzliche Instanz erzeugt. 


## Lösung

Die Instanzierung in der Awake_Methode muss eien Prüfung beinhalten welche gewährleistet, dass weitere Instanzen sofort wieder zerstört werden. Damit wird gewähleistet, dass jeweils immer nur eine Instanz besteht. 

**Gutes Beispiel**
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
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
            
    }

```


## Referenzen

<a id="1">[1]</a>


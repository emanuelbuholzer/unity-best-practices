# Verwendung des Singleton-Pattern beim Einsatz von von Managerklassen 

## Problem

Managerklassen sollten naturgemäss nur einmal instanziert werden und auch szenenübergreifend erhalten bleiben. Dafür eignet sich der Einsatz des Singleton-Pattern besonders gut.
Gemäss dem Desingprinzip des Singleton-Pattern soll gewähleistet werden, dass jeweils immer nur eine Instanz existiert.
Wichtig beim der Verwendung von Singletons sind folgende Aspekte:

* Deklaration als static vom Typ Instance
* die Instanzierung erfolgt in der Awake-Methode
* Prüfung ob bereits eine Instanz besteht und falls eine besteht, werden keine weiteren davon instanziert
* SingletonScript immer an Root-GameObject anhängen


**Schlechtes Beispiel einer Implementation** :
```csharp

using UnityEngine;

public class PersistentManagerScript : MonoBehaviour

    public static PersistentManagerScript Instance {get; private set} //Typ Instance und Zugrifffestlegung 
    
    //irgendwelche Variablen die Werte der Managerklasse halten
    //Bsp.
    //public int Value;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Gewährleistet, dass die Instanz beim Szenenwechsel bestehen bleibt
        }
            
    }

```

Aufgrund der fehlenden Prüfung ob bereits eine Instanz existiert wird in jedem GameObject, dass das PersistentManagerScript aufruft eine zusätzliche Instanz erzeugt. 


## Lösung

Die Instanzierung in der Awake_Methode muss eine Prüfung beinhalten welche gewährleistet, dass weitere Instanzen sofort wieder zerstört werden. Damit wird gewähleistet, dass jeweils immer nur eine Instanz besteht. 

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
https://www.youtube.com/watch?v=CPKAgyp8cno



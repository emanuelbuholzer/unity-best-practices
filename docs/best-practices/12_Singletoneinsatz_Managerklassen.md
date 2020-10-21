# Verwendung des Singleton-Pattern bei der Implementierung von Managerklassen 

## Problem

Managerklassen sollten naturgemäss nur einmal instanziert werden und wenn erwünscht auch szenenübergreifend erhalten bleiben. Dafür eignet sich der Einsatz des Singleton-Pattern besonders gut.
Gemäss dem Desingprinzip des Singleton-Pattern soll gewähleistet werden, dass jeweils immer nur eine Instanz existiert.
Wichtig beim der Verwendung von Singletons sind folgende Aspekte:

* Deklaration als static vom Typ Instance
* die Instanzierung erfolgt in der Awake-Methode
* Prüfung ob bereits eine Instanz besteht und falls eine besteht, werden keine weiteren davon instanziert
* SingletonScript immer an Root-GameObject anhängen


**Schlechtes Beispiel** :
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
            DontDestroyOnLoad(gameObject); //Gewährleistet, dass die Instanz beim Szenenwechsel / Neuladen der Szene bestehen bleibt (Wenn erwünscht!)
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

[1]
Youtube, 26. September 2020, Singletons in Unity<br/>
Aufgerufen 26. September 2020 https://www.youtube.com/watch?v=CPKAgyp8cno

[2]
GAMEDEV-Vorlesung, Herbstsemester 2019, Game Architecture<br/>
Aufgerufen 20. September 2020 von http://devmag.org.za/2012/07/12/50-tips-for-working-with-unity-best-practices/



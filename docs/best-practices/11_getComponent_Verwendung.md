# Vermeiden von getComponent-Aufrufen in der Update-Methode

## Problem

Da Aufrufe in der update-Methode sehr rechenintensiv sind sollte der Zugriff auf andere GameObjects niemals über über getComponent() in der Methode update() erfolgen.
Ansonsten wird die Referenzierung bei jedem Frame erneut refernziert was unötige Ressourcen aufbraucht. 

**Beispiel**:
```csharp

using UnityEngine;

public class Example : MonoBehaviour
{
    void Update()
    {
        HingeJoint[] hinges = GetComponents<HingeJoint>();
        for (int i = 0; i < hinges.Length; i++)
        {
            hinges[i].useSpring = false;
        }
    }
}


```



## Lösung

Die getComponent-Methode sollte in der Methode Start() aufgerufen werden. Damit wird eine einmalige Referenzierung vor Ausführung der Update-Methode ausgeführt.  

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


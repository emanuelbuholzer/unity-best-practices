# Vermeiden von getComponent-Aufrufen in der Update-Methode

## Problem

Da Aufrufe in der update-Methode sehr rechenintensiv sind sollte der Zugriff auf andere GameObjects niemals über über getComponent() in der Methode update() erfolgen.
Ansonsten wird die Referenzierung bei jedem Frame erneut aufgerufen, was unötig Ressourcen aufbraucht. 

**Beispiel** generische Version der Methode:
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


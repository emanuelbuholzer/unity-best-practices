# Vermeiden von getComponent-Aufrufen in der Update-Methode

## Problem

Da Aufrufe in der update-Methode sehr rechenintensiv sind sollte der Zugriff auf andere GameObjects niemals über über getComponent() in der Methode update() erfolgen.

**Beispiel**:
```csharp



```



## Lösung


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


# Vermeiden von getComponent-Aufrufen in der Update-Methode

## Problem

Da Aufrufe in der Update-Methode sehr rechenintensiv sind, sollte der Zugriff auf andere GameObjects niemals über über `GetComponent()` / `GetComponents()` in der Methode `Update()` erfolgen.
Ansonsten wird die Referenzierung bei jedem Frame erneut aufgerufen, was unötig Ressourcen aufbraucht. Der Nachteil der Methode `GetComponent()` ist, dass dies ein innerer Aufruf ist und entsprechend nur GameObjects in der vorhandene Szene resp. Inspector zurückgibt.

**Beispiel** generische Version der Methode[[1]](#1):
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

Die `GetComponent`-Methode sollte in der Methode `Start` aufgerufen werden.
Die Methode `Start` wird für den Frame aufgerufen, wenn ein Script aktiviert wird, bevor die Methode `Update` erstmalig aufgerufen wird.

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
Unity Dokumentation,22. September 2020, GameObject.GetComponent<br/>
aufgerufen am 26. September 2020 von https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html


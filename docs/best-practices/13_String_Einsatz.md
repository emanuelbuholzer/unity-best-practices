# gameObject.Find(String name)

## Problem

Der Einsatz von Strings ist zwar in Unity sehr verbreitet, trotzdem sollte er im Allgemeinen immer sehr überlegt erfolgen.
Insbesondere wenn die Methode gameObject.Find(String name) eingesetzt wird, um Objekte zu referenzieren:

```csharp
gamemObject.Find(String name)
```

**Beispiel** :
```csharp

using UnityEngine;


public class ExampleClass : MonoBehaviour
{
    public GameObject hand;

    void Example()
    {
        hand = GameObject.Find("Hand");
    }

}
```

Im genannten Beispiel werden nur aktive GameObjects zurückgegeben. Wenn kein GameObject mit name gefunden werden kann, wird null zurückgegeben.
Dies funktioniert ausserdem nur im selben Gameobject bzw. derselben Hierarchie. Der Methodenaufruf ist sehr langsam und daher niemals in der Update-Methode zu verwenden.
Weiter wird jeweils nur das erste gefundene GameObject ausgegeben, weitere mit dem selben Namen werden vernachlässigt.

Dem Aspekt des Refactoring ist bei dieser Anwendung  besondere Beachtung zu schenken da der Aufwand sehr schnell anwächst.  


## Lösung

Generell sollte folgender Grundsatz eingehalten werden:

* Verwenden Sie Strings womöglich nur für Textausgaben


**Gutes Beispiel**
```csharp



```


## Referenzen

[1]
, 26. September 2020, Singletons in Unity
Aufgerufen 26. September 2020 https://docs.unity3d.com/ScriptReference/GameObject.Find.html

[2]
GAMEDEV-Vorlesung, Herbstsemester 2019, Game Architecture
Aufgerufen 20. September 2020 von http://devmag.org.za/2012/07/12/50-tips-for-working-with-unity-best-practices/



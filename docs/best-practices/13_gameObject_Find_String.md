# String Handhabung

## Problem

Der Einsatz von Strings ist zwar in Unity sehr verbreitet, trotzdem sollte er im Allgemeinen immer sehr überlegt erfolgen.
Insbesondere wenn die Methode `GameObject.Find` eingesetzt wird, um Objekte zu referenzieren:

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

Im genannten Beispiel werden nur aktive `GameObjects` zurückgegeben. Wenn kein `GameObject` mit name gefunden werden kann, wird `null` zurückgegeben.
Dies funktioniert ausserdem nur im selben `Gameobject` bzw. derselben Hierarchie. Wenn der name ein '/' Zeichen enthält, durchläuft es die Hierarchie wie ein Pfadname.
Der Methodenaufruf ist sehr langsam und daher niemals in der `Update`-Methode zu verwenden.
Weiter wird jeweils nur das erste gefundene `GameObject` ausgegeben. Wenn eine Szene mehrere `GameObjects` mit demselben Namen enthält gibt es keine Garantie dafür, dass ein bestimmtes `GameObject` zurückgegeben wird.

Der Aufruf von `GameObject.Find.Find` führt keinen rekursiven Abstieg in einer Hierarchie durch. Allgemein tut dies keine Methode welche die Referenzierung mittels `.Find` durchführt.

Dem Aspekt des Refactoring ist bei dieser Anwendung besondere Beachtung zu schenken da der Aufwand dafür sehr schnell wachsen kann.  


## Lösung

Folgender Grundsatz sollte im Umgang mit Strings wenn immer möglich eingehalten werden:

* Verwenden Sie Strings wann immer möglich ausschliesslich für Textausgaben

Unity bietet Tags an welche insbesondere beim Einsatz von `Collider` zu bevorzugen sind. Diese Tags können auch in der Unity Layer Collision Matrix einfach bearbeitet werden.
Ausserdem wird beim Einsatz der Methode `GameObject.FindWithTag` eine `UnityException` ausgelöst wenn der Name nicht vorhanden ist. 

**Beispiel**
```csharp
using UnityEngine;

public Klasse ExampleClass: MonoBehaviour
{
    public GameObject hand;
    
    void Example ()
    {
        hand = GameObject.FindWithTag ("Hand"); 
    }
}
```


## Referenzen

[1]Unity Technologies, 16. September 2020, Unity Engine Classes, gameObject.Find(String name)<br/>
Aufgerufen 26. September 2020 von https://docs.unity3d.com/ScriptReference/GameObject.Find.html

[2]GAMEDEV-Vorlesung, Herbstsemester 2019, Game Architecture<br/>
Aufgerufen 20. September 2020 von http://devmag.org.za/2012/07/12/50-tips-for-working-with-unity-best-practices/



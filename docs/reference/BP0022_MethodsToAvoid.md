# BP0022: Methods to avoid generaly

## Cause

Es wurde eine der folgenden Methoden aufgerufen:
  - `UnityEngine.GameObject.Find`
  - `UnityEngine.Object.FindObjectOfType`

## Rule description

Auf die Aufrufe der Methoden `Find` und `FindObjectsOfType` ist im Code generell zu verzichten. Da diese APIs über alle in Unity verwendeten GameObjects iterieren, sind mit zunehmenden Umfang der Projekte auch Performanceprobleme einhergehend [[1]](#1).
Eine Ausnahme der obigen Regeln kann bei der Referenzierung von Managerklassen (Singleton) mittels der 'FindObjectOfTyype' - API gemacht werden.

## How to fix violations

Generell auf den Einsatz der genannten Methoden verzichten. Ausser beim Refernzieren von Managerklassen in 'Awake' oder 'Start'. Nie in 'Update' verwenden!

## When to suppress warnings

Nie

## Example of a violation

### Description

Die Methode `GameObject.Find` wird in einer Methode aufgerufen, um ein Object namens Hand zu finden.
Im genannten Beispiel werden nur aktive `GameObjects` zurückgegeben. Wenn kein `GameObject` mit name gefunden werden kann, wird `null` zurückgegeben.
Dies funktioniert ausserdem nur im selben `Gameobject` bzw. derselben Hierarchie. Wenn der name ein '/' Zeichen enthält, durchläuft es die Hierarchie wie ein Pfadname.
Der Methodenaufruf ist sehr langsam und daher niemals in der `Update`-Methode zu verwenden.
Weiter wird jeweils nur das erste gefundene `GameObject` ausgegeben. Wenn eine Szene mehrere `GameObjects` mit demselben Namen enthält gibt es keine Garantie dafür, dass ein bestimmtes `GameObject` zurückgegeben wird.

Der Aufruf von `GameObject.Find.Find` führt keinen rekursiven Abstieg in einer Hierarchie durch. Allgemein tut dies keine Methode welche die Referenzierung mittels `.Find` durchführt.

### Code

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

## Example of how to fix

### Description
Falls immer möglich ist auf den Aufruf der Methoden
  - `UnityEngine.GameObject.Find`
  - `UnityEngine.Object.FindObjectOfType`

zu verzichten. 

## Related rules

[BP0001: Methods to avoid in Update](https://github.com/emanuelbuholer/unity-best-practices/blob/master/docs/reference/BP0001_MethodsToAvoidInUpdate.md)

## References

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, General Optimizations. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/2020.2/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity7.html

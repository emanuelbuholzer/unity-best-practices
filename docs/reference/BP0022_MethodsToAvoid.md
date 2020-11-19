# BP0022: Methods to avoid generaly

## Cause

Es wurde eine der folgenden Methoden aufgerufen:
  - `UnityEngine.GameObject.Find`
  - `UnityEngine.Object.FindObjectOfType`

## Rule description

Auf die Aufrufe der Methoden `Find` und `FindObjectsOfType` ist im Code generell zu verzichten. Da diese APIs über alle in Unity verwendeten GameObjects iterieren, sind mit zunehmenden Umfang der Projekte auch Performanceprobleme einhergehend [[1]].
Eine Ausnahme der obigen Regeln kann bei der Referenzierung von Managerklassen (Singleton) mittels der 'FindObjectOfTyype' - API gemacht werden.

## How to fix violations

Generell auf den Einsatz der genannten Methoden verzichten. Ausser beim Refernzieren von Managerklassen in 'Awake' oder 'Start'

## When to suppress warnings

Nie

## Example of a violation

### Description

Die Methode `GameObject.Find` wird in einer Methode aufgerufen, um ein Object namens Hand zu finden.

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

Die Methode `GameObject.FindWithTag` wird in einer Methode aufgerufen, um ein Object mit dem Tag Hand zu finden.

### Code

```csharp
using UnityEngine;

public Klasse ExampleClass: MonoBehaviour
{
    public GameObject hand;
    
    void Example ()
    {
        hand = GameObject.FindWithTag("Hand"); 
    }
}
```

## Related rules

[BP0001: Methods to avoid in Update](https://github.com/emanuelbuholer/unity-best-practices/blob/master/docs/reference/BP0001_MethodsToAvoidInUpdate.md)

## References

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, General Optimizations. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/2020.2/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity7.html



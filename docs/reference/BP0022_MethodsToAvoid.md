# BP0022: Methods to avoid generaly

## Cause

Es wurde eine der folgenden Methoden aufgerufen:
  - `UnityEngine.GameObject.Find`
  - `UnityEngine.Object.FindObjectOfType`

## Rule description

Auf die Aufrufe der Methoden `Find` und `FindObjectsOfType` ist im Code generell zu verzichten. Da diese APIs über alle in Unity verwendeten GameObjects iterieren, sind mit zunehmenden Umfang der Projekte auch Performanceprobleme einhergehend.
Eine Ausnahme der obigen Regeln kann bei der Referenzierung von Managerklassen (Singleton) mittels der 'FindObjectOfTyype' - API gemacht werden.

## How to fix violations

Generell auf den Einsatz der genannten Methoden verzichten. Ausser beim Refernzieren von Managerklassen in 'Awake' oder 'Start'

## When to suppress warnings

Nie

## Example of a violation

### Description

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

### Code

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

## Related rules

[BP0001: Methods to avoid in Update](https://github.com/emanuelbuholer/unity-best-practices/blob/master/docs/reference/BP0001_MethodsToAvoidInUpdate.md)

## References


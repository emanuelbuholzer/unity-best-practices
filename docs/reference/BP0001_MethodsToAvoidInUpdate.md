# BP0001: Methods to avoid in Update

## Cause

Es wurde eine der folgenden Methoden in der `Update` Methode aufgerufen:
  - `GetComponent`
  - `GetComponents`
  - `FindObjectOfType`

## Rule description

Da Aufrufe der Methoden `GetComponent`, `GetComponents`, `FindObjectsOfType` und `FindObjectOfType` sehr rechenintensiv sind, sollte der Zugriff auf andere GameObjects niemals über diese Methoden in der `Update` Methode erfolgen.
Da die Referenzierung bei jedem Frame erneut aufgerufen wird, was unötig Ressourcen aufbraucht.

## How to fix violations

Anstatt ein solche Aufrufe in der `Update` zu bewerkstelligen, sollten solche Aufrufe in der Methode `Start` oder `OnEnable` bewerkstelligt werdenn.
Von dort aus kann die Referenz gecached werden und in der `Update` Methode darauf zugegriffen werden.

Als Alternative kann eine Manager Klasse verwendet werden, welche die Referenz anbietet oder injectet.

## When to suppress warnings

Nie

## Example of a violation

### Description

Die Methode `GetComponent` wird in der `Update` Methode aufgerufen um einen `HingeJoint` zu erhalten.

### Code

```csharp
class Something : MonoBehaviour
{
    HingeJoint _HingeJoint;

    void Update()
    {
        HingeJoint hinge = gameObject.GetComponent(typeof(HingeJoint)) as HingeJoint;

        // Do something
    }
} 
```

## Example of how to fix

### Description

Die Methode `GetComponent` wird in der `Start`-Methode aufgerufen und der Rückgabe wird gecached, für die Verwendung in der `Update`-Methode. 

### Code

```csharp
class Something : MonoBehaviour
{
    HingeJoint _HingeJoint;

    void Start()
    {
        _HingeJoint = gameObject.GetComponent(typeof(HingeJoint)) as HingeJoint;

        // Do something
    }

    void Update()
    {
        // Do somethig with _HineJoint
    }
} 
```

## Related rules

[BP0023: Main Camera In Update](https://github.com/emanuelbuholer/unity-best-practices/blob/master/docs/reference/BP0023_MainCameraInUpdate.md)

## References
<a id="1">[1]</a>
Richard Wetzel, Herbstsemester 2019, Game Architecture<br/>
aufgerufen am 22. September 2020 von https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html

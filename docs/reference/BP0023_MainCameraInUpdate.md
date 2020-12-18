# BP0023: Main Camera in Update

## Cause

Das auf der Klasse `Camera` zu findende Property `main` wurde in einer `Update`-Methode aufgerufen.

## Rule description

Das auf der Klasse `Camera` zu findende Property `main` verwendet intern `Object.FindObjectWithTag`. 
Daher ist ein Aufruf auf diesem Property nicht effizienter als ein Aufruf auf `Object.FindObjectWithTag`.
Solche Aufrufe sollten nie in der `Update` Methode aufgerufen werden, da sie rechenintensiv sind.

## How to fix violations

Anstatt ein solcher Aufruf in der `Update` zu bewerkstelligen, sollte der Aufruf in der Methode `Start` oder `OnEnable` bewerkstelligt werdenn.
Von dort aus kann die Referenz gecached werden und in der `Update` Methode darauf zugegriffen werden.

Als Alternative kann eine `Camera`-Manager Klasse verwendet werden, welche die Referenz anbietet oder injectet.

## When to suppress warnings

Nie

## Example of a violation

### Description

Das auf der Klasse `Camera` zu findende Property `main` wird in der `Update`-Methode aufgerufen. 

### Code

```csharp
class Something : MonoBehaviour
{
    Camera _MainCamera;

    void Update()
    {
        _MainCamera = Camera.main;

        // Do something
    }
}
```

## Example of how to fix

### Description

Das auf der Klasse `Camera` zu findende Property `main` wird in der `Start`-Methode gecached, für die Verwendung in der `Update`-Methode. 

### Code

```csharp
class Something : MonoBehaviour
{
    Camera _MainCamera;

    void Start()
    {
        _MainCamera = Camera.main;

        // Do something
    }

    void Update() 
    {
        // Do something with _MainCamera
    }
}
```

## Related rules

[BP0001: Methods to Avoid in Update](https://github.com/emanuelbuholer/unity-best-practices/blob/master/docs/reference/BP0001_MethodsToAvoidInUpdate.md)

## References
<a id="1">[1]</a>
Unity Technologies, 06. Mai 2020, General Optimizations. <br /> 
Aufgerufen 3. November 2020 von https://docs.unity3d.com/2019.3/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity7.html

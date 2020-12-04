# BP0003: Strings References

## Cause

Es sind Methoden aufgerufen worden die Strings als Refernzierungsparameter verwenden.

## Rule description

Generell sollten Strings ausschliesslich für angezeigten Text verwendet werden[[2]](*2). Verzichten Sie insbesondere auf die Refenzierung mit Strings von Methodenaufrufen, Gameobjects, Prefabs usw. Eine Ausnahme bilden Animationen und Managerklassen. 
Ausserdem sollten diese Methoden, wenn schon verwendet, nicht in Aktualisierungsmethoden verwendet werden Bsp. 'Update' da die Performanceeinbusse sich dadurch steigern kann[[1]](*1).

## How to fix violations

Ein gezieltes Design der Klassen unter Einhaltung der Design Patterns wie auch direkte Methodenaufrufe helfen die "lazy" Art der Refenzierung per Strings zu vermeiden.   

## When to suppress warnings

Nie

## Example of a violation

### Description

Die Refernzierung mit String ist aus Gründen des Refactoring (hardcodiert) sowie der Verwendung von ineffzienten API's (Bsp. `UnityEngine.GameObject.Find`) welche zu Performanceproblemen führen kann zu vermeiden

### Code

```csharp

//Avoid StartCoroutine with method name
    this.StartCoroutine("SampleCoroutine");
    
```

## Example of how to fix

### Description

Verwende Methodenaufrufe direkt und 

### Code

```csharp

//Instead use the method directly
    this.StartCoroutine(this.SampleCoroutine());

```

## Related rules

[BP0001: Methods to avoid in Update](https://github.com/emanuelbuholzer/unity-best-practices/blob/master/docs/reference/BP0001_MethodsToAvoidInUpdate.md) <br/>
[BP0022: Methods to avoid generaly](https://github.com/emanuelbuholzer/unity-best-practices/blob/master/docs/reference/BP0022_MethodsToAvoid.md)

## References

<a id="1">[1]</a>
Rip Tutorial, 16. September 2020, Unity3d <br /> 
Aufgerufen 20. September 2020 von https://riptutorial.com/de/unity3d/example/25290/vermeiden-sie-den-aufruf-von-methoden--die-strings-verwenden

<a id="2">[2]</a>
Dev.Mag 50 Tips for working with Unity, 16. September 2020, Tip. 34 <br /> 
Aufgerufen 20. September 2020 von http://devmag.org.za/2012/07/12/50-tips-for-working-with-unity-best-practices/


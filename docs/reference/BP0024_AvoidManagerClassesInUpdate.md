# BP0024: Avoid reference manager-classes in update

## Cause

Eine mögliche Referenzierung einer Managerklasse mithilfe von `GameObject.FindObjectOfType<>` erfolgte in der Methode `Update()`.

## Rule description

Managerklassen sind aufgrund Ihres Einsatzes nur einmalig zu referenzieren und über die Szene oder auch Szenenübergreifend zu erhalten[[1]](*1).
Die korrekte Referenzierung sollte dabei folgende Aspekte zwingend einhalten.

* Verwende immer `GameObject.FindObjectOfType<>`, niemals `GetComponent<>` für die Referenzierung von Managerklassen
* Verwende die Referenzierung niemals in Update(), da sehr langsam und rechenintensiv

## How to fix violations

## When to suppress warnings

Nie

## Example of a violation

### Description

Managerklassen sollten nie in der `Update`-Methode referenziert werden.

### Code

```csharp

Update()
{
   soundManager =  GetComponent<SoundManager>();
}

```

## Example of how to fix

### Description

Managerklassen sollten immer in der `Start()`oder `Awake()` erfolgen.


### Code

```csharp

Start()
{
  soundManager =  GameObject.FindObjectOfType<SoundManager>();
}
```

## Related rules

[BP0001: Methods to avoid in update](https://github.com/emanuelbuholzer/unity-best-practices/blob/master/docs/reference/BP0001_MethodsToAvoidInUpdate.md)

## References

<a id="1">[1]</a>
stackoverflow, 03. März 2016, GameObject.FindObjectOfType<>() vs GetComponent<>()<br/>
Aufgerufen 03. Oktober 2020 https://stackoverflow.com/questions/30310847/gameobject-findobjectoftype-vs-getcomponent

<a id="2">[2]</a>
Richard Wetzel, Herbstsemester 2019, Game Architecture<br/>
aufgerufen am 22. September 2020 von https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html


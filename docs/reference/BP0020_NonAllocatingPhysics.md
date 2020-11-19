# BP0020: Non Allocating Physics

## Cause

Es wurde eine der folgenden Methode aus den Physics Modules aufgerufen.

[Physics](https://docs.unity3d.com/2019.4/Documentation/ScriptReference/Physics.html):
  - `BoxCast`
  - `CapsuleCast`
  - `OverlapBox`
  - `OverlapCapsule`
  - `OverlapSphere`
  - `Raycast`
  - `SphereCast`

[Physics2D](https://docs.unity3d.com/ScriptReference/Physics2D.html):
  - `BoxCast`
  - `CapsuleCast`
  - `CircleCast`
  - `GetRayIntersection`
  - `Linecast`
  - `OverlapArea`
  - `OverlapBox`
  - `OverlapCapsule`
  - `OverlapCircle`
  - `OverlapPoint`
  - `Raycast`
  
## Rule description

Mit der Einführung von Unity 5.3 sollten diese Methoden nicht mehr verwendet werden, da sie Speicher allozieren.
Dies sollte vom Benutzer selbst gemacht werden.

## How to fix violations

Die verwendeten Methoden sollten mit ihrer `NonAlloc`-Alternative verwendet werden. [[1]](#1).

[Physics](https://docs.unity3d.com/2019.4/Documentation/ScriptReference/Physics.html):
  - `BoxCastNonAlloc`
  - `CapsuleCastNonAlloc`
  - `OverlapBoxNonAlloc`
  - `OverlapCapsuleNonAlloc`
  - `OverlapSphereNonAlloc`
  - `RaycastNonAlloc`
  - `SphereCastNonAlloc`

[Physics2D](https://docs.unity3d.com/ScriptReference/Physics2D.html):
  - `BoxCastNonAlloc`
  - `CapsuleCastNonAlloc`
  - `CircleCastNonAlloc`
  - `GetRayIntersectionNonAlloc`
  - `LinecastNonAlloc`
  - `OverlapAreaNonAlloc`
  - `OverlapBoxNonAlloc`
  - `OverlapCapsuleNonAlloc`
  - `OverlapCircleNonAlloc`
  - `OverlapPointNonAlloc`
  - `RaycastNonAlloc`

## When to suppress warnings

Nie

## Example of a violation

### Description

Es wurde eine der obenstehenden Methoden aufgerufen

### Code

```csharp
BoxCast(...);
```

## Example of how to fix

### Description

Der Aufruf wurde mit der `NonAlloc`-Alternative ersetzt

### Code

```csharp
BoxCastNonAlloc(...);
```

## Related rules

Keine

## References

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, General Optimizations. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/2020.2/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity7.html



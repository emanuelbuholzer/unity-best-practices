# Nicht allozierende Physik APIs verwenden

## Problem

Mit der Einführung von Unity 5.3 werrden folgende Methoden der jeweiligen Physic APIs nicht mehr für die Verwendung empfohlen, da sie unnötig Speicher allozieren [[1]](#1).

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

## Lösung

Stattdessen sollten untenstehende nicht allozierende Methoden als Alternative der jeweiligen Physic APIs verwendet werden.

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

## Referenzen

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, General Optimizations. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/2020.2/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity7.html

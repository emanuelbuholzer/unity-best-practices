# BP0021: Optimize Mathematical Operations

## Cause

Es wurde eine mathematische Operation hinterlegt, welche sich optimieren lässt.

## Rule description

Für Vektor- und Quaternionen-Mathematik, sollte brücksichtigt werden, dass Integer-Mathematik schneller ist als Float-Mathematik und Float-Mathematik schneller ist als Vektor-, Matrix- oder Quaternionen-Mathematik [[1]].

## How to fix violations

Wann immer es die kommutative oder assoziative Arithmetik erlaubt, die Kosten der einzelnen mathematischen Operationen zu minimieren sollte dies gemacht werden.

## When to suppress warnings

Wenn diese Operationen nicht in engen Loops ausgeführt werden, können diese Diagnostic grundsätzlich ohne weiteres unterdrückt werdden.

## Example of a violation

### Description

Es werden zwei Vektor-Multiplikationen durchgeführt, obwohl nur eine notwendig wäre.

### Code

```csharp
Vector3 x;

int a, b;

Vector3 slow = a * x * b;
```

## Example of how to fix

### Description

Es wird eine Integer-Multiplikation und Vektor-Multiplikation durchgeführt, dies ist effizienter.

### Code

```csharp
Vector3 x;

int a, b;

Vector3 fast = a * b * x;
```

## Related rules

Keine

## References

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, General Optimizations. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/2020.2/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity7.html


# BP0019: Integer Get/Set Methods

## Cause

Es wurde eine `Get`- oder `Set`-Methode auf einem `Animator`, `Material` oder `Shader` aufgerufen und ein String-Parameter verwendet.

## Rule description

Unity verwendet intern keine Strings zur Adressierung von `Animator`, `Material` oder `Shader`. Die String-Methoden führen einfach ein String-Hashing durch und leiten dann die gehashte ID an die Integer-Methoden weiter [[1]].

## How to fix violations

Um eine Set- oder Get-Methode für einen `Animator`, `Material` oder `Shader` zu verwenden, sollte die Integer Methode anstelle der string-Methoden verwendet werden. Der einfachste Weg, sie zu verwenden, besteht darin, eine statische schreibgeschützte Integer-Variable für jeden Namen eines Properties zu deklarieren und die Integer-Variable anstelle des Strings zu verwenden. Diese werden beim Starten automatisch initialisiert, ohne dass weiterer Initialisierungscode erforderlich ist.

Die APIs um Property Namen als Integer auszulesen sind:
  - Animator: [Animator.StringToHash](https://docs.unity3d.com/ScriptReference/Animator.StringToHash.html)
  - Material und Shader: [Shader.PropertyToID](https://docs.unity3d.com/ScriptReference/Shader.PropertyToID.html)

## When to suppress warnings

Nie

## Example of a violation

### Description

Es wurde eine `Get`- oder `Set`-Methode auf einem `Animator`, `Material` oder `Shader` aufgerufen und ein String-Parameter verwendet.

### Code

```csharp
animator.SetBool("Jump", true );
```

## Example of how to fix

### Description

Es wurde eine `Get`- oder `Set`-Methode auf einem `Animator`, `Material` oder `Shader` aufgerufen und ein Integer-Parameter verwendet.

### Code

```csharp
static readonly int AnimatorBoolJumpProperty = nimator.StringToHash("Jump");

animator.SetBool(AnimatorBoolJumpProperty, true );
```

## Related rules

Keine

## References

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, General Optimizations. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/2020.2/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity7.html

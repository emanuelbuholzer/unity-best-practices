# BP0009: Use precompiled Regex 

## Cause

Es ist eine statische Verwendung von `Regex.Match`oder ein methodische Aufruf von `Regex.Replace`verwendet worden.

## Rule description

Die Verwendung von Regulären Ausdrücken ist sehr leistungsstark zum Bearbeiten und Abgleichen von Strings. Die Stärke geht aber mit der benötigten LEistung einher. Daher wird empfohlen keien statsichen 
Aufrufe von `Regex.Match`oder methodische Aufrufe von `Regex.Replace` zuverwenden. Diese Methoden kompilieren im laufenden Betrieb und erzeugen viel Datenmüll und kosten entsprechend Leistung. 

## How to fix violations

Es sollten immer vorkompilierte Ausdrücke verwendet. 

## When to suppress warnings

Nie

## Example of a violation

### Description

Der nachfolgende statsische Aufruf `Regex.Match` wird zur Laufzeit kompiliert und erzeugt bei jeder Verwendung rund 5kB Datenmüll[[1]](*1). 

### Code

```csharp
Regex.Match(myString, "foo");
```

## Example of how to fix

### Description

Druch ein einfaches Refactoring, der Verwendung von vorkompilierten Ausdrücken, kann eine eindeutig Verbesserung erreicht werden. Der Ausdruck erzeugt nur noch rund 320 Bytes Datenmüll. 
Wenn es sich bei den regulären Ausdrücken um invariante Zeichenfolgenliterale handelt, ist es daher wesentlich effizienter, sie vorab zu kompilieren,
und direkt darauf aufzurufen. Diese vorkompilierten Regexes sollten dann, wann immer möglich, wiederverwendet werden.[[1]](*1).

### Code

```csharp
var myRegExp = new Regex("foo");

myRegExp.Match(myString);
```

## Related rules

Keine

## References

<a id="1">[1]</a>
Unity, 27. Oktober 2020 Strings und Text<br/>
Aufgerufen 29. Oktober https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html

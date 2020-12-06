# BP0006: Ordinal String Comparison

## Cause

Es wurde die Methode `String.Compare` oder `String.Equals` aufgerufen, ohne Angabe eines ordinalen Vergleichs.

## Rule description

Bestimmte String-APIs gelten mit Ausnahme auf ordinale Vergleiche als äusserst uneffizient[[1]](#1).
Die `String.Equals`-Methode sollte nicht ohne Methodenüberladung (unter der Angabe eines ordinalen Vergleichs) eingesetzt werden[[3]](#3).

## How to fix violations

Damit beim Vergleich von Strings eine bessere Leistung gewährleistet ist, sollte die Methode mittels einem Parameter vom Typ StringComparison (beginnnend mit Ordinal) überladenen aufgerufen werden.

Eine andere, elegante Möglichkeit bietet der Einsatz der 'String.CompareOrdinal' Methode[[2]](#2). 

## When to suppress warnings

Wenn umbedingt auf ein ordinaler Vergleich verzichtet werden muss.

## Example of a violation

### Description

Regulärer Vergleich von Strings.

### Code

```csharp
String.Compare("Hallo", "H@llo");

String.Equals("Velo", "Velo");
```

## Example of how to fix

### Description

Verwendung eines Vergleichs mit ordinalem Vergleich.

### Code

```csharp
string a = "Hallo";
string b = "H@llo";

String.Compare(a, b, StringComparison.Ordinal);

String.Equals(a, b, StringComparison.Ordinal);
```

## Related rules


[BP0007: Inefficient String Methods](https://github.com/emanuelbuholzer/unity-best-practices/blob/master/docs/reference/BP0007_InefficientStringMethods.md)

## References
<a id="1">[1]</a>
Unity Technologies, 27. Oktober 2020, Strings and text. <br /> 
Aufgerufen 08. November 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html

<a id="2">[2]</a>
Microsoft Documentation, 11. November 2020,String.CompareOrdinal Method. <br />
Aufgerufen 11. November 2020 von https://docs.microsoft.com/en-us/dotnet/api/system.string.compareordinal?view=netcore-3.1

<a id="3">[3]</a>
Unity Technologies, 11. November 2020. September 2020, Best practices for comparing strings in .NET. <br />
Aufgerufen 11. November 2020 von https://docs.microsoft.com/en-us/dotnet/standard/base-types/best-practices-strings?redirectedfrom=MSDN

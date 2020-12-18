# BP0016: Avoid ForEach Loops

## Cause

Es wurde ein `foreach` Loop verwendet.

## Rule description

Im Allgemeinen sollten foreach-Loops vermieden werden. Sie boxen nicht nur, sondern die Kosten für Methodenaufrufe bei der Iteration über Collections sind viel langsamer als die manuelle Iteration über einen for- oder while-Loop.

Die Boxing-Operationen wurde aus dem foreach-Loop mit Unity 5.5 eliminiert. Dadurch wird der mit foreach-Schleifen verbundene Speicher-Overhead eliminiert. Der CPU-Leistungsunterschied im Vergleich zu äquivalentem Array-basiertem Code bleibt jedoch aufgrund des Methodenaufruf-Overheads bestehen [[1]]. 

## How to fix violations

Durch das Verwenden eines for- oder while-Loops kann dieses Problem umgangen werden.

## When to suppress warnings

Sofern der CPU Verbrauch nicht all zu hoch ist.

## Example of a violation

### Description

Es wurde ein foreach-Loop verwendet, um eine Zahl zu akkumulieren.

### Code

```csharp
int accum = 0;

foreach(int x in myList) {
    accum += x;
}
```

## Example of how to fix

### Description

Das Problem, eine Zahl zu akkumulieren, wurde mit einem for-Loop gelöst.

### Code

```csharrp
int accum = 0;

for(int i = 0; i <= myList.Count; i++) {
    accum += i;
}
```

## Related rules

[BP0014: Avoid Boxing](https://github.com/emanuelbuholer/unity-best-practices/blob/master/docs/reference/BP0014_AvoidBoxing.md)

## References

<a id="1">[1]</a>
Unity Technologies, 16. September 2020, Understanding the managed heap. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html


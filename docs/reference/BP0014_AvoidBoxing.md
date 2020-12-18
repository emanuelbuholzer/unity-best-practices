# BP0014: Avoid Boxing

## Cause

Es wurde eine primitive Variable in einen Verweistyp umgewandelt.

## Rule description

Boxing ist eine der häufigsten Quellen für unbeabsichtigte, temporäre Speicherzuweisungen in Unity-Projekten).
Es tritt immer dann auf, wenn ein werttypisierter Wert als Referenztyp verwendet wird. Dies tritt am häufigsten auf, wenn primitive werttypisierte Variablen (wie int und float) an objekttypisierte Methoden übergeben werden[[1]](#1).

C# -IDEs und Compiler geben im Allgemeinen keine Warnungen zum Boxen aus, obwohl dies zu unbeabsichtigten Speicherzuweisungen führt. Dies liegt daran, dass die C#-Sprache unter der Annahme entwickelt worden ist, dass kleine temporäre Zuweisungen von Garbage Collectors der Generation und Speicherpools mit Zuordnungsgrößensensitivität effizient verarbeitet werden.
Während der Allity-Allokator unterschiedliche Speicherpools für kleine und große Allokationen verwendet, ist der Garbage Collector von Unity notgenerationsübergreifend und kann daher die kleinen, häufigen temporären Allokationen, die durch das Boxen generiert werden, nicht effizient löschen.
Boxen sollte daher nach Möglichkeit vermieden werden, wenn C#-Code für Unity-Laufzeiten geschrieben wird[[1]](#1).

## How to fix violations

Vermeide das Verwenden von werttypisierten Werten als Übergabeparameter an objekttypisierte Methoden.

## When to suppress warnings

## Example of a violation

In nachfolgendem sehr einfach gehaltenem Beispiel wird die der int-Wert x geboxt und dann als Argument der objekttypisierten Methode `object.equals` zum Vergleich übergeben.

### Description

### Code

```csharp

int x = 1;

object y = new object();

y.Equals(x);

```

## Example of how to fix

### Description

Vergleich auf primitven Typen hingegen benötigt kein Boxing.

### Code

```csharp

int x = 1;
int y = 2;

x == y;

```


## Related rules

Keine

## References


<a id="1">[1]</a>
Unity Dokumentation, 16. September 2020, Understanding Optimization in Unity <br /> 
Aufgerufen 13. Dezember 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html


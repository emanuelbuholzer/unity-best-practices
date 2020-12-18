# BP0005: String concatenation

## Cause

Es ist eine String Konkatenation in einer Schleife oder in einer sich in jedem Frame wiederholenden Methode ohne Stringbuilder aufgerufen worden.

## Rule description

String Objekte sind immutable. Jedes Mal, wenn Sie eine der Methoden aus der System.String- Klasse verwenden, erstellen Sie ein neues Stringobjekt im Speicher, für das eine, zusàtzliche neue Speicherplatzzuweisung erforderlich ist.
Werden String Konkatenationen daher wiederholend in Schleifen oder in jedem Frame aufgerufen, kann durch den Aufwand eines immer wieder neu anzulegenden Stringobjekts zu Performanceproblemen führen[[1]](*1).

## How to fix violations

Wenn Sie eine Zeichenfolge ändern möchten, ohne dabei ein neues Objekt zu erstellen, verwenden Sie dazu die Klasse `System.Text.StringBuilder`.

## When to suppress warnings

Nie

## Example of a violation

### Description

Eine Konkatenation mit 'string` + `string` erzeugt ein neues Stringobjekt und belastet zusätzlich den Speicher.

### Code

```csharp

string a = "hallo";
string b = "World!":

string c = a + b;


```

## Example of how to fix

### Description

Die Verwendung der Klasse `System.Text.StringBuilder` erfolgt wie ein Stringobjekt. Nach initialisierung des Stringbuilderobjekts kann dieses ähnlich einem Array verwendet werden.
Dem Stringbuilderobjekt können Stringobjekte hinzugefügt, einzelne Zeichen entfernt werden ohne Performanceeinbussen durch neuerstellen des Stringbuilderobjekts[[3]](*3).

Eine komplette Übersicht der Klasse `System.Text.StringBuilder` und deren Methoden ist inder offiziellen .Net Dokumentation von Michrosoft zu finden[[3]](*3)

### Code

```csharp
// initialisieren eines StringBuilder-Objekts

StringBuilder myStringBuilder = new StringBuilder("Hello World!");

```

## Related rules

[RULEID: Friendly related rule name](https://github.com/emanuelbuholer/unity-best-practices/blob/master/docs/reference/RULEID_FriendlyRelatedRuleName.md)

## References

<a id="1">[1]</a>
Unity Technologies, 15. Dezember 2020, Understanding optimization in Unity<br/>
aufgerufen am 18.Dezember 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html

<a id="2">[2]</a>
Microsoft, 2020, Best practices for comparing strings in .NETy<br/>
aufgerufen am 18.Dezember 2020 von https://docs.microsoft.com/en-us/dotnet/standard/base-types/best-practices-strings?redirectedfrom=MSDN

<a id="3">[3]</a>
Microsoft, 2020, Using the StringBuilder Class in .NET<br/>
aufgerufen am 18.Dezember 2020 von https://docs.microsoft.com/en-us/dotnet/standard/base-types/stringbuilder



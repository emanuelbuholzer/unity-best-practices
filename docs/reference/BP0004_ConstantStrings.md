# BP0004: Constant Strings

## Cause

Sie haben eine Methode aufgerufe, welche einen oder mehrere Tags als Paramter benötigt. Dabei wurden die Tags nicht als Konstanten übergeben.

## Rule description

Wenn Tags nicht als Konstanten übergeben werden, besteht öfter die Möglichkeit sich zu vertippen, es gibt kein Auto Completion. Des weiteren ist es schwieriger diese wieder zu ändern [[1]]. Dies kann zu unerwünschten `NullPointerExceptions` zur Laufzeit führen.

## How to fix violations

Durch das verwenden von Constants, mit dem `const` Keyword, können Strings nicht mehr verändert werden und z.B. zentral abgelegt werden. Dies mitigert das Problem in einem gewissen Mass, jedoch nicht vollständig.

## When to suppress warnings

Wenn die Tags variabel übergeben werden und sichgergestellt werden kann, dass die Tags auch definitiv existieren.

## Example of a violation

### Description

In diesem Beispiel wurde der Tag `Respawn` in einer nicht konstanten Variable gespeichert und für den Aufrruf von `FindWithTag` verwendet.

### Code

```csharp
public class Something : MonoBehaviour
{
    void Start()
    {
        var tag = "Respawn";
        var g = gameObject.FindWithTag(tag)
    }
}
```

## Example of how to fix

### Description

In der statischen Klasse `Tags` werden die Tags als Konstanten abgelegt. Sie können hierbei zentral verwaltet werden.

### Code

```csharp
public static class Tags
{
    public const string Respawn = "Respawn";
}

public class Something : MonoBehaviour
{
    void Start()
    {
        var g = gameObject.FindWithTag(Tags.Respawn)
    }
}
```

## Related rules

Keine

## References
Devin Reimer, 11. März 2020, Tags, Layers and Scene Constants Generator in Unity. <br />
Aufgerufen 10. Oktober 2020 von http://blog.almostlogical.com/2014/03/11/tags-layers-and-scene-constants-generator-in-unity/

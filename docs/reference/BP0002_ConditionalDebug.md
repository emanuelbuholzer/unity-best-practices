# BP0002: Conditional Debug

## Cause

Eine Methode zum Ausgeben von Logs der Klasse `UnityEngine.Debug` wurde aufgerufen.

## Rule description

Die `UnityEngine.Debug` [Debugging API](https://docs.unity3d.com/ScriptReference/Debug.html) Aufrufe werden nicht von Releasebuilds entfernt und schreiben in Logdateien [[1]](#1).
Debug-Informationen werden normalerweise nicht in Releasebuilds ausgegeben.

## How to fix violations

Damit die Debugging API Aufrufe in Releasebuilds nicht ausgegeben werden, können diese mit einem [Conditional-Attribut](https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.conditionalattribute?redirectedfrom=MSDN&view=netcore-3.1) versehen werden.

Dafür muss eine Wrapper-Methode in einer Klassendeklaration oder einer Strukturdeklaration erstellt werden, mit dem Rückgabewert `void` [[2]](#2). 

Mit dem Conditional-Attribut wird die Methode, sofern der entsprechende Vorverarbeitungsbezeichner nicht gesetzt ist, im Releasebuild ignoriert bzw. ausgelassen.

Um den Vorverarbeitungsbezeichner in Debugbuilds zu setzen kann dieser global unter `<Projekt Pfad>/Assets/mcs.rsp` mit `-define:<Vorverarbeitungsbezeichner>` gesetzt werden.
Es kann auch einer der bereits existierenden Vorverarbeitungsbezeichner verwendet werden, wie z.B. `UNITY_EDITOR` oder `DEVELOPMENT_BUILD` [[3]](#3).
Des weiteren können Vorverarbeitungsbezeichner auch direkt im Code gesetzt werden.

**Beispiel**:
```csharp
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
#define ENABLE_LOGS
#endif 
```

Eine Einführung zu Vorverarbeitungskommandos und Vorverarbeitungsbezeichnern ist auf [Unity Learn](https://learn.unity.com/tutorial/introduction-to-preprocessing-commands#) zu finden.

## When to suppress warnings

Dies sollte nur unterdrückt werden, wenn Debugging Logs auch in Release Builds ausgegeben werden sollen.

## Example of a violation

### Description

In der `Start`-Methode wird in jedem Falle folgendes geloggt:
> Gerade gestartet

### Code

```csharp
public class Something : MonoBehaviour
{
    void Start() 
    {
        UnityEngine.Debug.Log("Gerade gestartet");
    }
}
```

## Example of how to fix

### Description

Zur Lösung kann eine Utility Klasse verwendet werden, wobei die Methoden derer mit einem `Conditional`-Attribut versehen sind.

### Code
```csharp
public static class DebugLogger 
{
    
    [Conditional("ENABLE_LOGS")]
    public static void Log(string message) 
    {
        UnityEngine.Debug.Log(message); 
    }
}
```

## Related rules

None

## References
<a id="1">[1]</a>
Unity Technologies, 16. September 2020, General Optimizations. <br /> 
Aufgerufen 20. September 2020 von https://docs.unity3d.com/2020.2/Documentation/Manual/BestPracticeUnderstandingPerformanceInUnity7.html

<a id="2">[2]</a>
Microsoft, 3. September 2020, Conditional-Attribut (C#-Programmierhandbuch). <br />
Aufgerufen 20. September 2020 von https://docs.microsoft.com/de-ch/previous-versions/visualstudio/visual-studio-2008/4xssyw96(v=vs.90)?redirectedfrom=MSDN#hinweise

<a id="3">[3]</a>
Unity Technologies, 17. September 2020, Platform dependent compilation. <br />
Aufgerufen 20. September 2020 von https://docs.unity3d.com/2020.2/Documentation/Manual/PlatformDependentCompilation.html?_ga=2.192344162.995033292.1600604413-1679067612.1600330815

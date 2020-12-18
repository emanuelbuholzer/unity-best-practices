# BP0010: Asset Postprocessor

## Cause

Es wurde keine Klasse gefunden, welche von der Klasse `AssetPostprrocessor` ableitet.

## Rule description

Werden Assets nicht komprimiert, kann dies auf Low-End-Mobilgeräten gefährlich sein. Wenn z. B. eine 4K-Textur nicht komprimiert wird kann dies schnnell runnd 180-200 Megabyte Speicher verbrauchen und kann schnell zu Out-of-Memory-Fehlern führen [[1]].

## How to fix violations

Durch die Verwendunng einer Klasse `AssetPostprocessor` im Projekt könnenn Checks auf Assets beim importieren gemacht werden.Somit können z. B. unkomprimierte 4K-Texturen erkannt werden.

Oft verwendete Checkss können in diesem Guide gefunden werden: https://docs.unity3d.com/ScriptReference/AssetPostprocessor.html

## When to suppress warnings

Sofern man sich sicher ist, dass man Assets immer komprimiert importiert oder der Verbrauch von Arbeitsspeicher nenbensächlich ist.

## Example of a violation

### Description

Es wurde keine Klasse gefunden, welche von der Klasse `AssetPostprrocessor` ableitet.

### Code

```csharp
// Hier ist kein AssetPostprrocessor
```

## Example of how to fix

### Description

Es wurde eine Klasse `AssetPostprrocessor` implementiert, mit einem einfachen Check. 

### Code

```csharp
public class ReadOnlyModelPostprocessor : AssetPostprocessor {

   public void OnPreprocessModel() {

        ModelImporter modelImporter =

 (ModelImporter)assetImporter;

        if(modelImporter.isReadable) {

            modelImporter.isReadable = false;

            modelImporter.SaveAndReimport();

        }

    }

}
```

## Related rules

Keine

## References

<a id="1">[1]</a>
Unity, 27. Oktober 2020 Asset Auditing<br/>
Aufgerufen 30. Oktober https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4.html

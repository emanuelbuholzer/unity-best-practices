# BP0017: Try to call array-valued Unity API not more often than necessary

## Cause

Es ist auf eine Unity API mit Array-Wert zugriffen worden.

## Rule description

Eine weniger erkennbare Ursache für eine falsche Array-Zuweisung ist der wiederholte Zugriff auf Unity-APIs, die Arrays zurückgeben.
Alle Unity-APIs, die Arrays zurückgeben, erstellen bei jedem Zugriff eine neue Kopie des Arrays.
Es ist äußerst ineffzient, häufiger als nötig auf eine Unity-API mit Array-Wert zuzugreifen[[1]](*1).
Während die CPU-Kosten für den einmaligen Zugriff auf eine Eigenschaft nicht sehr hoch sind, führen wiederholte Zugriffe in engen Schleifen zu Hotspots für die CPU-Leistung.
Darüber hinaus erweitern und belasten, wiederholte Zugriffe den verwalteten Heap nur unnötig.

## How to fix violations

Verwenden Sie die Unity-API's welche auf Array-Werte sowenig wie möglich udn beachten Sie bei einer Verwendung der API auf den richitigen Einsatz. 
## When to suppress warnings

Nie

## Example of a violation

### Description

In nachfolgendem Beispiel werden fälschlicherweise vier Kopien des `vertices`Arrays pro Schleifeniteration erstellt.
Die Zuweisungen erfolgen jedes Mal, wenn auf die `.vertices` Eigenschaft zugegriffen wird[[1]](*1).

### Code

```csharp

for(int i = 0; i < mesh.vertices.Length; i++)

{

    float x, y, z;

    x = mesh.vertices[i].x;

    y = mesh.vertices[i].y;

    z = mesh.vertices[i].z;

    // ...

    DoSomething(x, y, z);   

}
```

## Example of how to fix

### Description

Unabhängig von der Anzahl der Schleifeniterationen kann hier trivial in eine einzelne Array-Zuordnung umgestaltet werden und das `vertices`-Array vor Eintritt in die Schleife erfasst wird.[[1]](*1)

### Code

```csharp

var vertices = mesh.vertices;

for(int i = 0; i < vertices.Length; i++)

{

    float x, y, z;

    x = vertices[i].x;

    y = vertices[i].y;

    z = vertices[i].z;

    // ...

    DoSomething(x, y, z);   

}
```

## Related rules

Keine

## References

<a id="1">[1]</a>
Unity Technologies, 15. Dezember 2020, Understanding the managed heap. <br /> 
Aufgerufen 18. Dezember 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html



# Collider

## Problem

Collider sind ein massgebliches Element von Unity welches dazu dient GameObjects mit anderen zu interagieren. Dazu müssen sich die Collider der beiden Gameobjects berühren.

Folgende Methoden werden dazu seitens Unity.Engine zur Verfügung gestellt:

* OnCollisionEnter(Collision collision) //Wird aufgerufen, wenn dieser Collider / Rigidbody einen anderen Rigidbody / Collider zu berührt
* On CollisionStay(Collision collision) //Wird einmal pro Frame für jeden Collider / Rigidbody aufgerufen, der Starrkörper / Kollider berührt
* OnCollisionExit(Collision collision) //Wird aufgerufen, wenn dieser Collider / Rigidbody den anderen Rigidbody / Collider mehr berührt

Um einen Collider der Methode als Parameter zu übergeben soll eine Prüfung durchgeführt werden um welchen Collider es sich handelt. Im Spiel sollen ja nur bestimmte GameObject mit anderen interagieren (Bsp. eine Kugel mit einem Spieler).

**Schlechtes Beispiel einer Colliderprüfung**:
```csharp
using Unity.engine

public Class Player
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "Gun Bullet”)
        {
            DoSomething();
        }
    }
}
```
Die Prüfung wie im obigen Beispiel durchzuführen erscheint einfach aber mit zunehmender Komplexität und Erweiterung des Spiels birgt dies Risiken und bei Änderungen einen zusätzlichen Refactoraufwand. 


## Lösung

Unity bietet Tags an welche durch die Methode gameObject.compareTag(String name) referenziert werden können. Dies bietet eine elegante Lösung in Abstimmung mit der von Unity angebotenen Hilsmittel an.
Desweiteren können Tags einfach in Unity erstellt und per Layer Collison Matrix umgestellt werden[[1]].


**Beispiel**:
```csharp
int j1 = 1;
int j2 = 32;

// Irgendwelcher Code

int[] jointAngles = new int[2]();
jointAngles[0] = j1;
jointAngles[1] = j2;

// Irgendwelcher Code
for(int i = 0; i < joingAngles.length; i+=1)
{
    // Irgendwas damit machen
}
```

## Referenzen

<a id="1">[1]</a>
Unity Documentation, 22. September 2020, Layer-based collision detection.<br /> 
Aufgerufen 27. September 2020 von https://docs.unity3d.com/Manual/LayerBasedCollision.html

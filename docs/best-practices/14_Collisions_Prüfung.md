# Richtiger Einsatz beim Prüfen von Collision

## Problem

Collider sind ein massgebliches Element von Unity welches dazu dient, GameObjects mit anderen mittels Collisions[[1]](#1) interagieren zu lassen. Um die Interaktion auszulösen müssen sich die Collider der beiden Gameobjects berühren und entsprechend eine Methode implementiert haben.

Folgende Methoden werden dazu seitens Unity zur Verfügung gestellt:

* OnCollisionEnter(Collision collision) //Wird aufgerufen, wenn dieser Collider / Rigidbody einen anderen Rigidbody / Collider zu berührt
* OnCollisionStay(Collision collision) //Wird einmal pro Frame für jeden Collider / Rigidbody aufgerufen, der Starrkörper / Kollider berührt
* OnCollisionExit(Collision collision) //Wird aufgerufen, wenn dieser Collider / Rigidbody den anderen Rigidbody / Collider mehr berührt

Um einen Collider der Methode als Parameter zu übergeben soll eine Prüfung durchgeführt werden um welchen Collider es sich handelt. Im Spiel sollen ja nur bestimmte GameObject mit anderen interagieren (Bsp. eine Kugel mit einem Spieler).

**Schlechtes Beispiel einer Colliderprüfung**:
```csharp
public Class Player
{
    // Irgendwelcher Code
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "Bullet”)
        {
            DoSomething();
        }
    }
    
    // Irgendwelcher Code
}
```
Die Prüfung wie im obigen Beispiel durchzuführen erscheint einfach aber mit zunehmender Komplexität und Erweiterung des Spiels birgt dies Risiken und bei Änderungen einen zusätzlichen Refactoraufwand. 


## Lösung

Unity bietet Tags an welche durch die Methode gameObject.compareTag(String name)[[2]](#2) referenziert werden können. Dies bietet eine elegante Lösung in Abstimmung mit der von Unity angebotenen Hilsmittel an.
Desweiteren können Tags einfach in Unity erstellt und per Layer Collison Matrix umgestellt werden[[3]](#3).

Es empfiehlt sich die Refernzierung wie folgt zu implementieren

**Beispiel**:
```csharp
public Class Player
{
    // Irgendwelcher Code
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet”))
        {
            DoSomething();
        }
    }
    
    // Irgendwelcher Code
}
```

## Referenzen

<a id="1">[1]</a>
Unity Documentation, 22. September 2020, Collisions<br /> 
Aufgerufen 27. September 2020 von https://docs.unity3d.com/ScriptReference/Collision.html

<a id="2">[2]</a>
Unity Documentation, 22. September 2020, GameObject.FindWithTag<br /> 
Aufgerufen 27. September 2020 von https://docs.unity3d.com/ScriptReference/GameObject.FindWithTag.html

<a id="3">[3]</a>
Unity Documentation, 22. September 2020, Layer-based collision detection<br /> 
Aufgerufen 27. September 2020 von https://docs.unity3d.com/Manual/LayerBasedCollision.html


# Richtige Refernzierung von Managerklassen

## Problem

Managerklassen sind aufgrund Ihres Einsatzes nur einmalig zu referenzieren und über die Szene (manchmal auch szenenübergreifend vgl. BP) zu erhalten.
Dazu gibt es bessere und schlechtere Möglichkeiten eine Managerklasse zu referenzieren. Weiters spielt dabei die verwendete Methode zur Referenzierung eine wesentliche Rolle. 

* Verwende immer GameObject.FindObjectOfType<>, niemals GetComponent<>
* Verwende die Referenzierung niemals in Update(), da sehr langsam und rechenintensiv


## Optimmale Lösung

Die Refrenzierung erfolgt über den äusseren Aufruf GameObject.FindObjectOfType<>. Damit wird gewährleistet, dass im Gegensatz zur GetComponent<>-Methode auch ausserhalb der
angehängten Gameobjects nach dem Objekt gesucht wird.

``` csharpe
using UnityEngine;
using System.Collections;

// Search for any object of Type Camera,
// if found print its name, else print a message
// that says that it was not found.
public class ExampleClass : MonoBehaviour
{
    void Start()
    {
        Camera cam = (Camera)FindObjectOfType(typeof(Camera));
        if (cam)
            Debug.Log("Camera object found: " + cam.name);
        else
            Debug.Log("No Camera object could be found");
    }
}

```
## Referenzen

<a id="1">[1]</a>



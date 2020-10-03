# Richtige Refernzierung von Managerklassen

## Problem

Managerklassen sind aufgrund Ihres Einsatzes nur einmalig zu referenzieren und über die Szene (manchmal auch szenenübergreifend vgl. BP) zu erhalten.
Dazu gibt es bessere und schlechtere Möglichkeiten eine Managerklasse zu referenzieren. Weiters spielt dabei die verwendete Methode zur Referenzierung eine wesentliche Rolle. 

* Verwende immer GameObject.FindObjectOfType<>, niemals GetComponent<>
* Verwende die Referenzierung niemals in Update(), da sehr langsam und rechenintensiv


## Optimmale Lösung

Die Referenzierung erfolgt über den äusseren Aufruf GameObject.FindObjectOfType<>. Damit wird gewährleistet, dass im Gegensatz zur GetComponent<>-Methode auch ausserhalb der
aktuellen Szene nach dem GameObject gesucht wird[[1]](#1).

```csharp
using UnityEngine;
using System.Collections;

// suche nach einem Object vom Typ Camera
// wenn gefunden, gib den Namen aus
// oder gib "No Camera object could be found" zurück, wenn kein Object gefunden
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
stackoverflow, 03. März 2016, GameObject.FindObjectOfType<>() vs GetComponent<>()<br/>
Aufgerufen 03. Oktober 2020 https://stackoverflow.com/questions/30310847/gameobject-findobjectoftype-vs-getcomponent

[2]
Unity Dokumentation, 29. September 2020, Singletons in Unity<br/>
Aufgerufen 03. Okotber 2020 https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html
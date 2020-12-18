# BP0018: Cached Empty Arrays

## Cause

Ein Rückgabewert vom Typ Array wird zurückgegeben.

## Rule description

Ein bekanntes Pattern ist die Rückgabe eines vorab als Singleton Instanz erstelltes, leeres Arrays anstatt dem Wert `null` wenn eine Methode mit Array-Wert einen leeren Satz zurückgeben muss[[1]](*1).

## How to fix violations

Erstellen eines leeren Arrays als Singelton und Verwendung der Instanz wenn eine Methode eine Array-Wert `null`zurückgibt. 

## When to suppress warnings

Nie

## Example of a violation

### Description

Beim jedem Aufruf der Methode welche ein leerer Satz an Array zurückgeben kann wird ein neues Array erstellt.

### Code

```csharp

Array a = new Array[0];

public Array doSomething(a)
{
    Console.WriteLine(a);
}

doSomething(a);
doSomething(a);

```

## Example of how to fix

### Description

Verwende ein Singleton Instanz wenn ein lerres Array zurückgegeben wird. Da bei jedem Aufruf der Methode ein neues Array erstellt wird.

### Code

```csharp

//Instanziere eine leeres Array als Singleton in einer separaten Klasse

//Verwende die Singleton Instanz als Rückgabewert anstatt null wenn ein leerer Satz von Array zurückgegeben wird

public Array doSomething(a)
{
    if (a.lenght = 0)
    {
        Console.WriteLine(instance) //Singleton instance     
    }
    
    Console.WriteLine(a);
}

doSomething(a);
doSomething(a);

```

## Related rules

Keine

## References

<a id="1">[1]</a>
Unity Technologies, 15. Dezember 2020, Empty Array reuse <br /> 
Aufgerufen 18. Dezember 2020 von https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html


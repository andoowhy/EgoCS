# EgoCS
An **E**ntity (**G**ame**O**bject) **C**omponent **S**ystem framework for Unity3D, in C#.

[![Join the chat at https://gitter.im/andoowhy/EgoCS](https://badges.gitter.im/andoowhy/EgoCS.svg)](https://gitter.im/andoowhy/EgoCS?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

For more detailed info, please see the EgoCS [Wiki](https://github.com/andoowhy/EgoCS/wiki).

EgoCS aims to improve upon Unity3D's [GameObject / Component relationship](http://docs.unity3d.com/Manual/TheGameObject-ComponentRelationship.html) by completely decoupling Data and Behaviour, typical in Unity3D Components.

While there isn't a standard [Entity Component System (ECS)](https://en.wikipedia.org/wiki/Entity_component_system) pattern or reference implementation, EgoCS follows the most popular conventions:

* **Entities** (AKA **GameObjects**) are merely containers for Components (like in Unity3D).
* **Components** store data. And unlike Unity3D, Components ONLY store public data:

```C#
// Movement.cs
using UnityEngine;

[DisallowMultipleComponent]
public class Movement : MonoBehaviour
{
    public Vector3 velocity = new Vector3(1.0f, 2.0f, 3.0f);
}
```

* **Systems** run logic & perform updates on GameObjects with the desired attached components:

```C#
// MovementSystem.cs
using UnityEngine;

// MovementSystem updates any GameObject with a Transform & Movement Component
public class MovementSystem : EgoSystem<Transform, Movement>
{
    public override void Start()
    {
        // Create a Cube GameObject
        var cubeEgoComponent = Ego.AddGameObject(GameObject.CreatePrimitive(PrimitiveType.Cube));
        cubeEgoComponent.gameObject.name = "Cube";
        cubeEgoComponent.transform.position = Vector3.zero;

        // Add a Movement Component to the Cube
        Ego.AddComponent<Movement>( cubeEgoComponent.gameObject );
    }

    public override void Update( EgoComponent ego, Transform transform, Movement movement )
    {
	// ...move it by the velocity in its Movement Component
	transform.Translate( movement.velocity * Time.deltaTime );
    }
}
```

Following this convention literally, Systems are completely isolated from one another. To allow inter-system communication, EgoCS uses **Events** and a global **Event Queue**:

* Systems can register **Event Handlers** (methods) for specified Events. Multiple Systems can handle the same Event:

```C#
using UnityEngine;
using System.Collections;

public class ExampleSystem : EgoSystem<Rigidbody>
{
    public override void Start()
    {
        // Create a falling cube
        var cubeEgoComponent = Ego.AddGameObject( GameObject.CreatePrimitive( PrimitiveType.Cube ) );
        cubeEgoComponent.gameObject.name = "Cube";
        Ego.AddComponent<Rigidbody>( cubeEgoComponent.gameObject );
        cubeEgoComponent.transform.position = new Vector3( 0f, 10f, 0f );
        Ego.AddComponent<OnCollisionEnterComponent>( cubeEgoComponent.gameObject );

        // Create a stationary floor
        var floorEgoComponent = Ego.AddGameObject( GameObject.CreatePrimitive( PrimitiveType.Cube ) );
        floorEgoComponent.gameObject.name = "Floor";
        floorEgoComponent.transform.localScale = new Vector3( 10f, 1f, 10f );
        Ego.AddComponent<Rigidbody>( floorEgoComponent.gameObject ).isKinematic = true;
        Ego.AddComponent<OnCollisionEnterComponent>( floorEgoComponent.gameObject );

        // Register Event Handlers
        EgoEvents<CollisionEnterEvent>.AddHandler( Handle );
    }

    void Handle( CollisionEnterEvent e )
    {
        var name1 = e.egoComponent1.gameObject.name;
        var name2 = e.egoComponent2.gameObject.name;
        Debug.Log( name1 + " collided with " + name2 );
    }
}
```
    
* EgoCS provides built-in Events for most MonoBehavior Messages (OnCollisionEnter, OnTriggerExit, etc.), and you can easily create your own custom events:

```C#
// ExampleEvent.cs
public class ExampleEvent : EgoEvent
{
    public float num;

    public ExampleEvent( float num )
    {
        this.num = num;
    }
}

// ExampleSystem.cs
using UnityEngine;
using System.Collections;

public class ExampleSystem : EgoSystem<Rigidbody>
{
    public override void Start()
    {
        // Register Event Handlers
        EgoEvents<ExampleEvent>.AddHandler( Handle );

        // Create an Event
        var e = new ExampleEvent( 42f );
        EgoEvents<ExampleEvent>.AddEvent( e );
    }

    void Handle( ExampleEvent e )
    {
        Debug.Log( e.num ); //42
    }
}
```

* Event objects can be created while a System is starting or updating (Ex: Collision, Win, etc). These Events are automatically sent to the back of the Ego Event Queue.
* Events are handled **after** all systems have updated.

**TL;DR:** Changes in Data (Components) will not break logic, and changes in logic (Systems) will not break Data. Maximum decoupling is achieved, and you will never have to write `[RequireComponent(...)]` \**shudder*\* again.

# Installation

Place the "EgoCS" folder anywhere in your project's Assets folder:

```
cd [project_dir]/Assets/

git clone https://github.com/andoowhy/EgoCS.git EgoCS
```
	
Create an empty GameObject in the scene, and give it an appropriate name (Ex: `Game Manager` or `EgoCS`).

Attach an `EgoInterface` Component to this GameObject. This Component is the bridge between Unity3D and EgoCS.

Add your Systems to EgoCS in your `EgoInterface`'s static contructor:

```C#
public class EgoInterface : MonoBehaviour
{
    static EgoInterface()
    {
        // Add Systems Here:
        EgoSystems.systems.Add( new ExampleSystem() );
        EgoSystems.systems.Add( new MovementSystem() );
    }

    void Start()
    {
        EgoSystems.Start(); 
    }

    void Update()
    {
        EgoSystems.Update();
    }

    void FixedUpdate()
    {
        EgoSystems.FixedUpdate();
    }
}
```

# Debugging

Like with GameObjects and MonoBehaviours, you can easily enable & disable Systems on-the-fly before and during runtime:

![Easily Enable / Disable Systems](https://raw.githubusercontent.com/wiki/andoowhy/EgoCS/img/SystemTogglesExample.gif)

# Limitations
- Only OnTrigger\* and OnCollision\* MonoBehaviour Messages are converted into EgoEvents. More to be added soon.
- Unity3D v5.3+ Multi Scene Editing not supported (yet)

# TODO
- Example projects
- Documentation

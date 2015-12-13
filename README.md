# EgoCS
An **E**ntity (**G**ame**O**bject) **C**omponent **S**ystem framework for Unity3D, in C#.

EgoCS aims to improve upon Unity3D's [GameObject / Component relationship](http://docs.unity3d.com/Manual/TheGameObject-ComponentRelationship.html) by completely decoupling Data and Behaviour, typical in Unity3D Components.

While there isn't a standard [Entity Component System (ECS)](https://en.wikipedia.org/wiki/Entity_component_system) pattern or reference implementation, EgoCS follows the most popular conventions:

* **Entities** (AKA **GameObjects**) are merely containers for Components (like in Unity3D).
* **Components** store data. And unlike Unity3D, Components ONLY store public data:

    ```C#
    // Movement.cs
    using UnityEngine;
    
    public class Movement : MonoBehaviour
    {
        public Vector3 velocity = new Vector3(1.0f, 2.0f, 3.0f);
    }
    ```

* **Systems** run logic & perform updates on GameObjects with the desired attached components:

    ```C#
    // MovementSystem.cs
    using UnityEngine;

    //MovementSystem updates any GameObject with a Movement Component
    public class MovementSystem : EgoSystem<Movement>
    {
        public override void Start()
        {
            base.Start(); // Must be called when overriding Start()

            var mover = Ego.AddGameObject(GameObject.CreatePrimitive(PrimitiveType.Cube));
            mover.transform.position = Vector3.zero;
            
            Ego.AddComponent<Movement>(mover);
        }
                
        public override void Update()
        {
            // For any GameObject with a Cube Component Attached...
            foreach (var bundle in bundles)
            {
                var transform = bundle.transform;
                var movement = bundle.component1;
    
                // ...move it by the velocity in its Movment Component
                var velocity = movement.velocity;
                transform.Translate(velocity * Time.deltaTime);
            }
        }
    }
    ```

Following this convention literally, Systems are completely isolated from one another. To allow inter-system communication, EgoCS uses **Events** and a global **Event Queue**:

* Systems can register **Event Handlers** (methods) for specified Events. Multiple Systems can handle the same Event.

    ```C#
    using UnityEngine;
    using System.Collections;

    public class ExampleSystem : EgoSystem<Rigidbody>
    {
        public override void Start()
        {
            base.Start();
    
            // Create a falling cube
            var cube = Ego.AddGameObject(GameObject.CreatePrimitive(PrimitiveType.Cube));
            cube.name = "Cube";
            Ego.AddComponent<Rigidbody>(cube);
            cube.transform.position = new Vector3(0f, 10f, 0f);
            Ego.AddComponent<OnCollisionEnterComponent>(cube);
    
            // Create a stationary floor
            var floor = Ego.AddGameObject(GameObject.CreatePrimitive(PrimitiveType.Cube));
            floor.name = "Floor";
            floor.transform.localScale = new Vector3(10f, 1f, 10f);
            Ego.AddComponent<Rigidbody>(floor).isKinematic = true;
            Ego.AddComponent<OnCollisionEnterComponent>(floor);
    
            // Register Event Handlers
            EgoEvents<CollisionEnter>.AddHandler(Handle);
        }
    
        void Handle( CollisionEnter e )
        {
            var name1 = e.egoComponent1.gameObject.name;
            var name2 = e.egoComponent2.gameObject.name;
            Debug.Log(name1 + " collided with " + name2);
        }
    }
    ```
    
* EgoCS provides built-in Events for most MonoBehavior Messages (OnCollisionEnter, OnTriggerExit, etc.) You can easily create your own custom events

    ```C#
    // ExampleEvent.cs
    public class ExampleEvent : EgoEvent
    {
        public float num;
    
        public ExampleEvent(float num)
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
            base.Start();
    
            // Register Event Handlers
            EgoEvents<ExampleEvent>.AddHandler(Handle);
    
            // Create an Event
            var e = new ExampleEvent(42f);
            EgoEvents<ExampleEvent>.AddEvent(e);
        }
    
        void Handle(ExampleEvent e)
        {
            Debug.Log(e.num); //42
        }
    }
    ```

* Event objects can be created while a System is starting or updating (Ex: Collision, Win, etc). These Events are automatically sent to the back of the Ego Event Queue.
* Events are handled **after** all systems have updated

**TL;DR:** Changes in Data (Components) will not break logic, and changes in logic (Systems) will not break Data. Maximum decoupling is achieved, and you will never have to write `[RequireComponent(...)]` \**shudder*\* again .

# Installation

Place the "EgoCS" folder anywhere in your project's Assets folder:

    cd [project_dir]/Assets/[some_dir]
	git clone https://github.com/andoowhy/EgoCS.git

	
Create an empty GameObject in the scene, and give it an appropriate name (Ex: `Game Manager` or `EgoCS`).

Attach an `EgoInterface` Component to this GameObject. This Component is the bridge between Unity3D and EgoCS.

Add your Systems to EgoCS in `EgoInterface.Start()`:

```C#
void Start()
{
    // Add Systems Here
    Ego.AddSystem(new ExampleSystem());
    Ego.AddSystem(new MovementSystem()); 
}
```

# Limitations
- Only OnTrigger\* and OnCollision\* MonoBehaviour messages / callbacks are turned into EgoEvents. More to be added soon.
- Unity3D v5.3+ Multi Scene Editing not supported (yet)

# TODO
- Startup project, docs & walkthrough
- General Docs / Wiki

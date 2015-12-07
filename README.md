# EgoCS
An **E**ntity (**G**ame**O**bject) **C**omponent **S**ystem framework for Unity3D, in C#.

EgoCS aims to improve upon Unity3D's [GameObject / Component relationship](http://docs.unity3d.com/Manual/TheGameObject-ComponentRelationship.html) by completely decoupling Data and Behaviour, typical in Unity3D Components.

While there isn't a standard [Entity Component System (ECS)](https://en.wikipedia.org/wiki/Entity_component_system) pattern or reference implementation, EgoCS follows the most popular conventions:

* **Entities** (AKA **GameObjects**) are merely containers for Components (like in Unity3D).
* **Components** store data. And unlike Unity3D, Components ONLY store data:

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
* Event objects can be created while a System is starting or updating (Ex: Collision, Win, etc). These Events are automatically sent to the back of the global Event Queue.
* After every System has updated, every Event in the Event Queue is Handled, and the Queue is cleared.

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

# TODO
- Startup project, docs & walkthrough
- General Docs / Wiki

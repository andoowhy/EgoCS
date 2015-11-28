# EgoCS
An **E**ntity (**G**ame**O**bject) **C**omponent **S**ystem framework for Unity3D, in C#.

EgoCS aims to improve upon Unity3D's [GameOjbect / Component relationship](http://docs.unity3d.com/Manual/TheGameObject-ComponentRelationship.html) by completely decoupling Data and Behaviour, typical in Unity3D Components.

## Entity Component System (ECS)

While there isn't a standard [ECS pattern](https://en.wikipedia.org/wiki/Entity_component_system) or reference implementation, EgoCS follows the most popular conventions:

- **GameObjects** (AKA **Entities**) are merely containers for Components (like in Unity3D).
- **Components** store data (and unlike Unity3D, Components ONLY store data).
- **Systems** run logic & perform updates on GameObjects with the desired attached components.

Following this convention literally, Systems are completely isolated from one another. To allow inter-system communication, EgoCS uses **Events** and a global **Event Queue**:

- Systems can register **Event Handlers** (methods) for specified Events. Multiple Systems can handle the same Event.
- Event objects can be created while a System is starting or updating (Ex: Collision, Win, etc). These Events are automatically sent to the back of the global Event Queue.
- After every System has updated, every Event in the Event Queue is Handled, and the Queue is cleared.

**TL;DR:** Changes in Data (Components) will not break your logic, and changes in logic (Systems) will not break your Data. Maximum decoupling is achieved, and you will never have to write `[RequireComponent(...)]` \**shudder\** again .

# Installation

Place the "EgoCS" folder anywhere in your project's Assets folder:

    cd [project_dir]/Assets/
	git clone https://github.com/andoowhy/EgoCS.git
	
Create an empty GameObject in the scene, and give it an appropriate name (Ex: `Game Manager` or `EgoCS`).

Attach an `EgoInterface` Component to this GameObject. This Component is the bridge between Unity3D and EgoCS.

Add your Systems to EgoCS in `EgoInterface.Start()`.

# Limitations
- Only OnTrigger\* and OnCollision\* MonoBehaviour messages / callbacks are turned into EgoEvents. More to be added soon.

# TODO
- Startup project, docs & walkthrough
- General Docs / Wiki

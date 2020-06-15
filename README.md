# Solo Project: QuasarStorm (WebGL)

#### Hypercasual Asteroids clone with different ships and weapons that can be swapped at any time

## Featured Coding Skills

> ScriptableObject-Architecture
- Dynamic elements are implemented using ScriptableObjects, making it possible to easily change their properties during runtime (through both inspector and player input)
	
> Object Pooling
- nothing is destroyed, everything is reused as needed (eg. Projectiles get deactivated instead of destroyed, then redecorated and reactivated)
	
> Event-Driven
- Only UI elements and player input use Update() / FixedUpdate(), everything else is controlled through events
	
> Custom Update Coroutines 
- regular checks like simulation distance use separate coroutines with custom intervals instead of running the code every frame


## Feature Backlog

> Core
- Main Menu (Play, Settings)
- Settings (Master Volume, Quality Presets)

> Gameplay
- Customize bullets while playing

> Polish
- VFX
- Audio
- Projectile distance should be relative to player velocity
- Fix physics stutter

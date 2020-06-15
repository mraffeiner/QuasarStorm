# Solo Project: QuasarStorm (WebGL) (About 30 hours of work)

#### Hypercasual Asteroids clone with different ships and weapons that can be swapped at any time
Playable at https://tastiham.itch.io/quasarstorm?secret=yzMrgMqEazAioQYhdCn68fuPY

### Credits

Background Music: Kalte Ohren (septahelix remix) by septahelix (c) copyright 2019 Licensed under a Creative Commons Attribution (3.0) license. http://dig.ccmixter.org/files/septahelix/59527 Ft: starfrosch

Background Texture - n4 @ https://opengameart.org/node/25677

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

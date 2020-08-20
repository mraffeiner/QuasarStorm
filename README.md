# Solo Project: QuasarStorm (WebGL)

#### Hypercasual Asteroids clone with realistic physics, different ships and weapons that can be swapped at any time and unrestricted movement
Playable at https://tastiham.itch.io/quasarstorm?secret=yzMrgMqEazAioQYhdCn68fuPY

### Credits

Background Music: Kalte Ohren (septahelix remix) by septahelix (c) copyright 2019 Licensed under a Creative Commons Attribution (3.0) license. http://dig.ccmixter.org/files/septahelix/59527 Ft: starfrosch

Background Texture - n4 @ https://opengameart.org/node/25677

## Featured Skills

> ScriptableObject-Architecture
- Dynamic elements are implemented using ScriptableObjects, enabling the manipulation of eg. ship or weapon properties at runtime (through inspector / input)
	
> Object Pooling
- None of the instantiated objects are destroyed, but instead reused as needed through deactivation, redecoration and reactivation.
	
> Event-Driven
- Flow of game logic is controlled primarily through events instead of update loops
	
> Custom Update Coroutines 
- Taxing calculations like simulation distance checks use separate coroutines with custom intervals to prevent running code every frame

> Visual Polish
- Graphics use Unity URP, Post Processing and Custom Shaders created in ShaderGraph


## Feature Backlog

> Core
- Settings (Master Volume, Quality Presets)

> Gameplay
- Customize bullets while playing

> Polish
- General polish (UX / VFX / SFX)
- Projectile max distance should be relative to player velocity
- Physics should not stutter when applying linear / angular drag

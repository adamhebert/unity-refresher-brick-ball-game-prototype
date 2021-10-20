# unity-refresher-breakout-prototype

### Brick Game
- A long time classic and favorite. Easy to understand, and relatively (to a lot of other games) easy to build quickly.
- Current Features:  
--> Simple Menus  
--> Paddle  
--> Ball  
--> Bricks (Reset after all cleared)  
--> Score  
--> GameOver  

### Purpose
- First meaningful Unity development since 2015. This is not meant to be final implementation choices or the most highly optimized solutions early on, but more to refamiliarize myself with Unity and to learn what has changed. Will add to it going forward for fun/learning. Have left remarks of certain places that I'd like to go back and revisit, re-think through.

### Unity Version:  
*2020.3.20f1*

### OS Tested:  
*Windows 10*

### Resolutions Supported:  
*1024x768*
- I had originally attempted to support more resolutions and have things auto-scale, but I got lost in the weeds too early into the project that was meant to only be a few days of work. Will circle back to this, but it feels good at the one resolution. Used the PixelPerfect camera functionality to allow for transform scaling to match desired pixel size.

### Features/Implementations:  
- Menus with scene unloading/loading  
--> Had some fun with color lerping on the main menu
- Physics2D  
--> Paddle (PolygonCollider2D, Kinematic RigidBody2D), moves via key input.  
--> Ball (CircleCollider2D, Dynamic RigidBody2D), moves via forces applied...gets faster the more you hit it.  
--> Brick (BoxCollider2D, Static RigidBody2D)  
--> Wall (BoxCollider2D)  
- Score Tracking  
--> Bricks have different point values associated with them  
--> Every n score awards a free life
- Ball Death  
--> Losing all lives results in GameOver
- Played with EventHandlers
- Some simple Vector angle math
- Infinite level resetting upon completion
- DontDestroyOnLoad GameObject

### Some Possible Feature Upgrades (in no particular order)
- Examine Unity event system possibilites for better communication and collision resolutions between objects
- Create some kind of resolution management that allows for support of more standard resolutions
- Study the Unity physics system more in-depth to help drive better (more fun) gameplay mechanics
- Audio
- Animations
- Sprite Atlas to limit draw calls
- Shaders
- Input System research / key mapping functionality
- Data Drive the levels  
--> Good chance for some FSharp practice with an excel based type provider for level design
- Powerups
- Bricks with multiple hit points to break
- Etc!

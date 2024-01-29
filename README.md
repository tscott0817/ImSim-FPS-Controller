# ImSim-FPS-Controller
My absolute favorite type of video game is the Immersive Sim. Games like Deus Ex, System Shock, Dishonored, and Prey, among others.
This is my attempt at recreating some of the common controls and gameplay elements often found in these types of games. They emphasize player-environment interactions
and puzzle solving to progress through levels. My code here is what I use in combination with many assets from the Unity Asset Store. I can only share my code, but it
is used alongside many other things within Unity and from third party developers. 

![FPS Controller](https://i.imgur.com/Hzhnjid.png)

## Features
### Movement
- Walk, Run, Crouch, Jump
- Auto Step -> Naturally climb stairs. No need for slopes!
- Free Look -> Move player head independently of body to look while running. (Like ARMA, PUBG)
- Peek -> Look around corners with just head of player object.

### Actions
- Item Identification -> Item names are displayed on screen when looking at them.
- Fire Weapons -> Raycast and hitscan based weapon firing.
- Physics Based Grab -> Items tagged as 'physics objects' can be dynamically moved and placed.
- Throw -> Throw physics object if holding one.
- Proximity Based Highlighting -> Moveable items highlight when a certain distance from them (like Deus Ex)


### Interactions
- Climb Ladder
- Open Doors
- Turn on/off lights
- Rope Swing -> Attach player body to rope, then use movement keys to pump in direction, use jump to dismount. Used to cross large gaps.
- Surfaces -> Fire, Electricity, Poison Gas (depletes player health)

### Effects
- Lose Health, Die


## TODO
- Inventory Items -> As oppsed to Physics Items, Inventory Items will be added to the players inventory.
- Enemy Interactions -> Full combat loop still needs devised
- Lockpicking, Hacking
- Damage Types (Fire, Poison, etc...)

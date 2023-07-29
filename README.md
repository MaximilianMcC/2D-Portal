![Logo](https://i.imgur.com/qWTtHqe.png)
# 2D-Portal
A simple version of the game Portal, remade in 2D using SFML

# TODO:
Todo list of features that I might add.
- [x] Basic SFML setup
- [x] Velocity/Physics based player movement
- [ ] Player gravity
- [ ] Player animation
- [x] Somewhat tile based map
- [ ] Player collisions with map
- [ ] Fancy lighting
- [x] Portals that work X - X
- [ ] Portals that work Y - Y
- [x] Portals that work X - Y or Y- X
- [ ] Smooth portal "animations"
- [ ] Portal gun to create portals
- [ ] Button/Pressure plate
- [ ] Companion Cube and Weighted Storage Cube that has gravity
- [ ] Player can pick up cubes
- [ ] Cube dispenser thing
- [ ] Exit door that opens
- [x] Discord rich presence
- [ ] Ambient sound effects
- [ ] Responsive SFML window
- [ ] Settings editor
- [ ] Colorblind modes
- [ ] Turrets
- [ ] Player can die and respawn
- [ ] Moving platforms
- [ ] Toxic water that can kill the player
- [ ] Main menu
- [ ] AFK detection
- [ ] Save and load game
- [ ] Achievements
- [ ] Particles/screen shake
- [ ] Players friction is different depending on material
- [ ] Setting to change max fps or enable vsync
- [ ] Make a tilemap generator/editor GUI in java or something

# How it all works
Mostly notes and stuff so I don't forget what I'm doing

## Measurements
All measurements are in the table.
| **Measurement** | **Unit**  |
|-----------------|-----------|
| Time            | Seconds   |
| Weight/mass     | Kilograms |

## `GameObject` class
`GameObject` is a class that is supposed to be extended. All game objects are updated and rendered once every frame. They also have a start method that is called a single time when the game starts. Every frame their bounds gets set if the base Update is called.
## Tilemap syntax
- Names must be in all caps, end with `: `, and be in *kebab-case*.
- Array items start with a `:`, and are only strings (currently)
```
<PROPERTY-NAME>: <value>
<ARRAY-NAME>:
:<ITEM>
:<ITEM>
:<ITEM>
```
## Tilemap collisions/rendering
The tilemap is a collection of tiles. All of the tiles are converted to a single texture when they load, then a bunch of invisible rectangles are overlaid to create the collision. So the player is basically just floating on a texture.

This is good because then only 3 draw calls *(fill, map, and lighting)* are made per frame to create the map *(not including player, objects, portals, etc)*
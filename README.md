![Logo](https://i.imgur.com/qWTtHqe.png)
# 2D-Portal
A simple version of the game Portal, remade in 2D using SFML

# TODO:
Todo list of features that I might add.
- [x] Basic SFML setup
- [x] Velocity/Physics based player movement
- [ ] Player gravity
- [ ] Player animation
- [ ] Somewhat tile based map
- [ ] Player collisions with map
- [ ] Fancy lighting
- [x] Portals that work on X
- [ ] Portals that work on Y
- [ ] Portal gun to create portals
- [ ] Button/Pressure plate
- [ ] Companion Cube and Weighted Storage Cube that has gravity
- [ ] Player can pick up cubes
- [ ] Cube dispenser thing
- [ ] Exit door that opens
- [ ] Ambient sound effects
- [ ] Responsive SFML window
- [ ] Settings editor
- [ ] Colorblind modes
- [ ] Turrets
- [ ] Player can die and respawn
- [ ] Moving platforms
- [ ] Toxic water that can kill the player
- [ ] Main menu
- [ ] Save and load game
- [ ] Achievements
- [ ] Particles/screen shake
- [ ] Players friction is different depending on material
- [ ] Setting to change max fps or enable vsync

# How it all works
Mostly notes and stuff so I don't forget what I'm doing

## Measurements
All measurements are in the table.
| **Measurement** | **Unit**  |
|-----------------|-----------|
| Time            | Seconds   |
| Weight/mass     | Kilograms |

## `GameObject` class
`GameObject` is a class that is supposed to be extended. All game objects are updated and rendered once every frame. They also have a start method that is called a single time when the game starts.
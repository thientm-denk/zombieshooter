# ZomEndDay #

MINI SHOOTER GAME WITH UNITY

:wave: My Project at OneChain Game :wave:

## Table of Contents
- [Description](#description)
- [Preview Screenshot](#preview-screenshot)
- [Technology](#technology)
- [Functional requirements](#functional-requirements)
- [Contributors](#contributors)
- [License & Copyright](#license--copyright)

## Description
- This is a mini shooting game
- This works well on desktop, and WebGL
- Use WASD to move, Click to shoot
- Number 1 for primary weapon
- Number 2 for pistol
- You have 100 health, lose if become 0
- Try to kill zombie as most at posible (Score base on amount zombie killed)
- Available demo at itch.io: https://thientmdenk.itch.io/zomendday (Old version may contain some bug)
- Enjoy :heart:


## Preview Screenshot

<div align="center">

  <img src="./Picture/MenuMain.png" alt="MenuIngame" width="100%"></img> &nbsp;&nbsp; 
  <img src="./Picture/Menu.png" alt="Menu" width="100%"></img> &nbsp;&nbsp; 
  <img src="./Picture/Ingame.png" alt="Ingame" width="100%"></img> &nbsp;&nbsp;
  <img src="./Picture/Dead.png" alt="Dead" width="100%"></img> &nbsp;&nbsp;
  
</div>
  
## Technology

- RayCast to Manage Shooting
- Config
- UI, Game, Audio Manager
- Singleton pattern
- State Machine pattern to control Zombie
- NavMesh
- Linerenderer 
- Highscore storage with PlayerPref

## Functional requirements
**1. Player:**
- [X] Menu Interaction
- [X] Moving
- [x] Shooting
- [x] HighScore
- [x] Weapon

**2. Zombie**
- [x] Idle state
- [x] Run toward player state
- [x] Attack player state

## Useful Resources


## Contributors
- [Tran Minh Thien](https://github.com/Denkhotieu) - SE160413 

## License & Copyright
&copy; 2022 TranMinhThien.

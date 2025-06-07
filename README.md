<a name="top"></a>
# Space Shooter Arcade Game
* [Development Time](#development-time)
* [Development Process](#development-process)
* [Features](#features)

<br>



<a name="development-time"></a>
### Development Time
1 Month w/ 1 Month Unity Experience.



<a name="evelopment-process"></a>
### Development Process
1. First thing I did was create a new unity project as well as a new <a href="https://miro.com/">miro</a> canvas.
2. The next step was to make sure I had a solid idea of the mechanics and features etc. that I wanted the game to have. [Mind Map](#mind-map)
3. Next I watched many YouTube tutorials on how a game should be structured and its basic architecture.
4. Finally I just started working on the game. I just followed my mind map and went step by step so that it would be more manageable.



   
<a name="features"></a>
### Features (not all)
* Scenes
  * Main Menu   ----   This is the first scene that loads when the game is loaded.
  * Settings    ----   This is where the user can change some game related settings.
  * Credits     ----   This is where the game narative is told as well as boiler plate credits.
  * Game        ----   This is where the main game is played.
* Player
  * Single shot projectile
  * Gravitational pull
  * 3 state animations
  * Configurable within unity
  * Omnidirectional movement
* Enemies
  * Attraction to player due to gravitational pull
  * 2 state animations
  * Configurable within unity
* Enviroment
  * Dual layer 12 tile configuration background sprites
  * Infinte size due to transformation according to camera position
  * Random rotation per transform position change to reduce repetition
* Audio
  * Tri-Channel audio mixer: Master, Music, SFX
  * Audio taken from third parties such as <a href="https://pixabay.com/">Pixabay</a>, then modified using <a href="https://twistedwave.com/online">Twisted Wave</a>
* Camera
  * Main Camera
  * Cinemachine Camera with lookahead time offset, smoothing, and hard limits
* Managers
  * State Manager
  * Audio Manager
  * Save Manager
  * Main Menu Manager
  * Settings Manager
  * Credits Manager
  * Game Manager

[Back to Top](#top)

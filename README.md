# Team 1 GED Final Project

Group Formation:
To create our group. We focused on building our group based on the bonds we’ve formed from working on past projects together. This allows us to keep our team chemistry and allows us to always be on the same page when working out solutions for our project. We also chose our members due to each of us knowing how to program. This allows us to divide and conquer our code and communicate with each other about our progress, how to fit them together, and how we can further improve our game.

Roles:
Christopher: Game mechanics programmer (player, enemies, undo, spawner, singletons)
Yushan: UI and game mechanics programmer ( Custom Button mapping, Replay system)
Owen: Game level designer (Terrain/level and background generator + plugin integration)
Interactive Media Scenario Information:
The scenario that we will use is one of our own In-Class projects that we’ve been working on throughout the semester. The purpose of this scenario is to have the player make it from the start to the end of the level without dying. It’s also meant to display our game’s randomly generated background terrain and levels, along with the undo/redo feature (more of a rewind). The player is expected to complete the level as fast as they can while using the environment in front of them. The player can run, jump, undo/redo, kill enemies, and die from hazards(falling off the map, enemies). The scenario will also utilize the pause feature, an audio manager that will handle all sounds, custom button mapping in a UI menu, and the replay system, which will allow people to view their runs and see their performance.
Singleton:
Game Manager (used to manage level restart, enemy spawner and game states)
Pseudocode UML or Flowchart: 
// —initialization—
PlayerObject <-  player
GoalObject <- LevelGoal
EnemySpawner <- spawner
Timer <- 0
gamePaused <- false

//—GameLoop—
Function Update():
player = find player gameobject
Spawner = find spawner gameobject
Goal = find goal gameobject

Spawnloop()
deathRestart()
winSequence()
if(player presses escape key and game not paused)
PauseGame()
if(player presses escape key and game is paused)
ResumeGame()

Function deathRestart();
if(player presses R and player is dead)
	Reload game scene
	Player is not dead
Function SpawnLoop():
	Add time to timer
	if(timer is greater or equal to 3)
		Spawn enemy
		Reset timer
Function PauseGame():
	Game is paused
	Time scales by 0
Function ResumeGame():
	Game is unpaused
	Time scales by 1
Function winSequence():
	if(player has won)
		Display win screen

Explanation: Implemented grabbing the player, goal and other objects needed (like spawner) then updating triggers the function each frame to check to see if its requirements have been met. If so then that function proceeds as intended. Update functions are kept clean by having function logic inside their own space, making code more readable. Project benefits from this since enemies can spawn on the level providing a challenge for the player, restart allows the player to easily try again instead of reloading the game, pause implementation allows for players to take a breather, strategize or set away from the game for a few seconds, and the win sequence provides the player with a reward for completing the level.

Audio Manager (Manages when sounds are played)
Pseudocode UML or Flowchart: 
// ---Initialization---
PlayerObject <-  player
sound effect <- jumpSound
sound effect <- deathSound
sound effect <- winSound
sound effect <- enemyDeathSound
AudioSource <-audioSource

// ---GameLoop---
 function Update():
    player = find player gameobject
    audioSource = find object in scene with the same type as AudioSource
    if((player pressing spacebar & is not dead & is on ground & game is not paused):
      play jump sound effect
    if(player jumps on-top of an enemy):
      player jump sound and enemy death sound
    if(player is dead):
      play death sound effect
    if(player beat the level):
      play win sound effect

Explanation: Implemented by taking in all sound effects for the game and playing them when a specific requirement is met. This was implemented this way since all sound effects (or sound related components) could be stored in one area, allowing for easy debugging of sound effects and it keeps the project tidy since sound files aren’t scattered across multiple scripts with multiple listeners that could overlap and ruin the experience.

Undo Feature Manager(URCommandManager)
Pseudocode UML or Flowchart: 
//—Initialization—
Create undo Stack for positions

//—GameLoop—
AddAndExecuteCommand(undoCommand parameter):
	Push undoStack

Undo():
	if(undoStack count is greater than 0)
		Remove and return top of stack(pop)
		Activate command UndoMechanic()

Explanation: Implemented by creating a stack that stores past positions. When the player performs the Undo function their character will be teleported back to the position they were 2 seconds ago (Defined in the players core movement) This manager makes it easier for the undo command to run its stack variables in the background of the game until called upon and tidies up the code, allowing us to easily isolate any issues that may arise.
Command and Design Pattern:
Custom Button mapping
Pseudocode UML or Flowchart: 
//—Initialization—
Function Execute()
KeyBindings <- default keys for each action
Commands <- command objects for each action
Actions <- keys
//—GameLoop—
Function Update():
if(game is not paused and rebind menu is closed and player is alive):
	if(action key is held):
		commands[action].execute()
//—Button Mapping UI—
Function OpenMenu():
Display rebind menu
Pause game
Function StartRebinding(action)
selectedAction <- action
Display “Press a key”
Wait for player input
Function UpdateRebining():
if(waiting for new key):
  if(backspace is pressed):
	Canel rebining
if(new key is pressed):
  if(key is taken by another action):
	Display “key already used”
else
KeyBindings[selectedAction] <- newKey
Update button text
Finish rebinding

Function CloseMenu():
	Hide rebine menu
	Game continues
Explanation: Allows players to change the keyboard controls based on their preference. Implemented by using Command pattern to separate the player’s keyboard input from the game actions. When players press a key, the system finds the command assigned to that key and executes it. When it comes to changing key bindings, players can select the key they want to change and system waits for new key. It also checks if the selected key is being used. If the key is available, the new setting will be updated in the UI. This system makes the controls more flexible for players and keeps our input system more organized.

Undo Mechanic:
Pseudocode UML or Flowchart: 
Command Script:
	Undo Function
UndoMoveCommand Script:
	//—Initialization—
	targetTransform <- target
	Vector3 previousPosition
	Timer <- 0
	//—GameLoop—
	URMoveCommand(target, pastPlayerPos):
		target = parameterTarget
		Previous position = pastPlayerPos

	Undo(): if(previous position isn’t null)
Target position = previous position

Explanation: Implemented by first creating a basis for the function in the URCommand script. Then it's defined by the move command where the target is the player and the function gets fed a previous player position. Then when the Undo function is activated (called from the manager basically) it teleports the player back to their previous position. However if the player presses this button before a position can be recorded (within the first 2 seconds of the game start) it doesn’t proceed. This benefits the project by providing our game with its core mechanic. Giving players a 1 time second chance instead of falling to whatever demise was coming to them, allowing for them to complete their run. Finally the code was implemented this way in case we wished to build upon the undo system (better management or redo) as it’s isolated from other scripts and doesn’t pose that much of a challenge to change.

Replay system
Pseudocode UML or Flowchart: 
//—Initialization—
Playerobject <- player
Rigidbody2d <- player’s rigidbody2d
Positions <- empty list
Isreplaying <- false
Maxrames <- 3000
//—GameLoop—
Function FixedUpdate():
	if(game is not replaying and position count < maxframes ):
	  Add current player’s position to positions
//—Replay System—
Function ReplayFunction():
	if( game is not replaying and position count > 1 ):
	  Start ReplayCoroutine()
Function ReplayCoroutine():
	isReplaying <- true;
	Save current player position
	Save current player velocity
	Disable player movement script
	Disable player physics
	For each position in positions:
	  Move player to recorded position
	  Wait for next physics update
	Restore original position
	Restore original velocity
	Enable player physics
	Enable player movement script
	Isreplaying <- false
Explanation: Used FixedUpdate() to record the player’s position during each physics update, with a maximum of 3000 recorded positions. When the player clicks the replay button, the system starts a coroutine to replay the recorded movement. During the replay, the player’s movement and physics simulation are temporarily disabled. The system then moves the player through each saved position. After replay finishes, the player returns to their original position, and the movement and physics simulation are enabled. It only replays the player’s position not the whole run. This system allows the player to review their movement.

Factory Pattern:
Enemy Spawner system
Pseudocode UML or Flowchart: 
Abstract Enemy Spawner:
	Abstract Enemy spawner function
BasicESpawner:
	//—Initialization—
	PremadeEntity <-basicEnemyPrefab	
	
	//—GameLoop—
	Override EnemySpawner function():
		Spawn PremadeEntity at object position and rotation
		Return spawned enemy
Explanation: Implemented by periodically spawning an enemy from a certain position and rotation. The function starts at an abstract phase, allowing us to create copies of it for enemies (basicESpawner) then we can implement the spawner function into a game manager to either spawn enemies on a timer or when a criteria is met. This allows for the game to spawn enemies for us rather than we create them making our generated levels easier to manage.

Spawning items for scene environment
Pseudocode UML or Flowchart: 
//—Initialization—
Create ShapeList
  	Set ShapeList = [Circle, Square, Triangle]

Set NumberOfShapes = 100

Set MinX and MaxX for background width

Set MinY and MaxY for background height

//—GameLoop—
GenerateShapes():
 	Repeat NumberOfShapes times
RandomShape = Select random shape from ShapeList
RandomX = Generate random number between MinX and MaxX
RandomY = Generate random number between MinY and MaxY
Create RandomShape
Set position of RandomShape to (RandomX, RandomY)

Explanation: At game start, it spawns 100 randomly picked prefab objects (Just clouds right now) and scatters them within a minimum of (-10,  6) and (200, 12).

Terrain Creation
Pseudocode UML or Flowchart:
LevelGenerator
//—Initialization—
PlatformArray <- collection of platform prefabs
PlatformHolder <- Parent object for generated platforms
PlatformSpacing <- 18 game units
//—GameLoop—
	Generate random level length between 15 and 20
	CurrentPosition <- (0, 0, 0)
		
	For each platform in level length
		Select random platform from PlatformArray
		Spawn selected platform at CurrentPosition
		Set spawned platform parent to PlatformHolder
		Move CurrentPosition right by PlatformSpacing

Explanation: At game start, it picks a random number between 15 and 20 and makes a level of that length. It also picks random platforms from the list to build after one another using copied prefabs. You can change the platform spacing as well, which modifies the distance between where each platform spawns.
Plugin/DLL:
Description: A plugin from the unity asset store that allows you to see your selection history (Previously selected game objects) 
https://assetstore.unity.com/packages/tools/utilities/selection-history-184204 

Explanation: This plugin helps us navigate our unity workspace and speeds up our workflow.

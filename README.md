# RPG Game

This is a simple game played in the console. The game runs until the health of the player hits 0.

## Setup

To be able to use the application, change the connection string in the class Config with your MS SQL connection string. This will ensure database will be stored exactly where you want it!

### Gameplay

The gameplay includes moving across 10x10 board, with monsters spawning every turn. The player can move in all directions 1 square away. Monsters will move every turn and always move towards the player.
The player can choose whether to attack or move. If the player chooses to attack, he can attack the monsters that are within the range of attack. Monsters will always attack when they are in range (1 square away).

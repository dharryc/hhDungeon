# HHDungeon

### Concept

The goal of this project is to create a console "rougue-like" turn based game!

<!-- ### Must Have
| Dungeon | Character | Player | Enemy | Items | Rooms | Console |
|-----|-----|-----|-----|-----|-----|-----|
|Floor generation Method|Health | | |Armor |List<Room> connected rooms |Draw dungeon |
|Size of floor|Strength | | |Weapon |Room Type | |
|Default room |Attack | | | | | |
|Room quantities | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |

### Should Have
| Dungeon | Character | Player | Enemy | Items | Rooms | Console |
|-----|-----|-----|-----|-----|-----|-----|
|Retreat method |Weaknesses | |Death loot drop | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |

### Could Have
| Dungeon | Character | Player | Enemy | Items | Rooms | Console |
|-----|-----|-----|-----|-----|-----|-----|
| |Move speed? |Death effect | |Enchantements | | |
| | | | |Retreat wings? | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |

### Won't Have (yet)
| Dungeon | Character | Player | Enemy | Items | Rooms | Console |
|-----|-----|-----|-----|-----|-----|-----|
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |
| | | | | | | |

------- -->

## Dungeon

Fields:

- coordMap : dictionary<(coord : (int x, int y), room : Room)>
- difficultyLevel : int // +1 per floor
- currentPlayer : Player
- currentRoom : Room
- roomsExplored : int // set to zero at each floor

Methods:
- saveGame(Player currentPlayer) : void
- MoveRooms(enum direction) : Room
- Dungeon(Player? savedPlayer) : Dungeon

-------

## Room

Fields:

- +coord : (int x, int y) {get;}
- +Type : enum
- -N,E,S,W room pointers? : room
- -NESW : (bool hasNorthDoor, bool hasEastDoor, bool hsaSouthDoor, bool hasWestDoor)
- +Explored : bool
- -enemies : List<enemy>
- -loot : List<Item>

Methods:

- +checkDoors( ) : NESW (bool hasNorthDoor, bool hasEastDoor, bool hsaSouthDoor, bool hasWestDoor)
- +Room(coord Cord , Enum direction_in, int difficulty, int RoomsExplored) : Room  // percent to be stairs based of explored
- +Loot(enemy) : (int? Gold, item? loot)
- +grabLoot((int? Gold, item? loot)) : bool


### room types (vague ideas)
-----
### Enemy rooms

Enemy rooms will contain 1-3 enemies of varying difficulty. As far as mechanics go, the inital room generation will use the difficulty level to weigh first the number of enemies in the room, and once there's been a decision on how many enemies there are, it will also generate those enemies to have some level of difficulty and loot. The room might(?) contain some retreat option, but thats something for later to decide.
 
 ### Loot rooms

Much less common. When the player enters a loot room, there aren't any enemies, there's just some rewards. I think it would make sense again for the number of items (as well as maybe the "level" of any given item) to be influenced by the difficulty. I think there's some random number of gold in each loot room, as well as 1-2 items. Each item could be really good or really bad, just depending on the level you're on.

### Store rooms

Store rooms should be a place where you can enter and again, no enemies or hostile environment, just some items that are "locked" until you pay for the item. Very straightforward, again, the difficulty level should make shops both better and more expensive I think.

### Stair rooms

Stair rooms are limited in generation to 1 per floor (again, lots up for discussion here) and are located arbitrarily. You will be able to choose if you want to decend or not, but once you decend, you can't get back. Additionally, when you decend, the difficulty of the game is incremented up by 1.

### Empty rooms

Similar to loot rooms, but with little to no guarantee of anything in there at all. I think it could be fun to add a little chance of some boxes spawning in, and once the boxes are spawned, there's a really really small chance that they contain some gold IF you have a tool that can sufficiently break the box.

-------

## Player

Fields:
- +Health : int
- +Items : List<item>
- +Effects? : List<enum> // I'm not sure about this one, but it could be fun
- +EquippedWeapon : item
- +Gold : int {get; private set;}
- +MaxInventorySpace : int


Methods:

- attack(item EquippedWeapon)
- getInventory() : List<items>
- getStatus() : (int Health, List<enum> effects)
- checkGold() : int

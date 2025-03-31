# HHDungeon

### Concept

The goal of this project is to create a console "rougue-like" turn based game!

### Must Have
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

-------

#### Dungeon

Fields:

- coordMap : dictionary<(coord : (int x, int y), room : Room)>
- difficultyLevel : int
- currentPlayer : Player
- currentRoom : Room
- roomsExplored : int

Methods:
- saveGame(Player currentPlayer) : void
- MoveRooms(enum direction) : Room

-------

#### Room

Fields:

- +coord : (int x, int y) {get;}
- +Type : enum
- -N,E,S,W room pointers? : room
- -NESW : (bool hasNorthDoor, bool hasEastDoor, bool hsaSouthDoor, bool hasWestDoor)
- +Explored : bool
- -enemies : List<enemy>
- -loot : List<Item>

Methods:

-  +checkDoors() : NESW (bool hasNorthDoor, bool hasEastDoor, bool hsaSouthDoor, bool hasWestDoor)

-------
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

#### Dungeon

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

- +checkDoors() : NESW (bool hasNorthDoor, bool hasEastDoor, bool hsaSouthDoor, bool hasWestDoor)
- +Room(coord Cord , Enum direction_in, int difficulty, int RoomsExplored) : Room  // percent to be stairs based of explored
- +Loot(enemy) : (int? Gold, item? loot)
- +grabLoot((int? Gold, item? loot)) : bool

-------

#### Player

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

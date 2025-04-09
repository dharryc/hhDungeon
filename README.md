# HHDungeon

### Concept

The goal of this project is to create a console "rougue-like" turn based game!

Methods are assumed to be public unless otherwise specified.

## Dungeon

Fields:

- coordMap : dictionary<(coord : (int x, int y), room : Room)>
- difficultyLevel : int // +1 per floor
- currentPlayer : Player
- currentRoom : Room
- roomsExplored : int // set to zero at each floor
- MaxRooms : int // set to x at each floor

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

Enemy rooms will contain 1-3 (5 for slimes) enemies of varying difficulty. As far as mechanics go, the inital room generation will use the difficulty level to weigh first the number of enemies in the room, and once there's been a decision on how many enemies there are, it will also generate those enemies to have some level of difficulty and loot. The room might(?) contain some retreat option, but thats something for later to decide.
 
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
- +XP : int
- +MaxHealth : int
- +CurrentHealth : int
- +Items : List<item>
- +Effects? : List<enum>
- +EquippedWeapon : item
- +Gold : int {get; private set;}
- +MaxInventorySpace : int
- NewLevelXPThreshold : int
- CurrentLevel : int

- BaseATK int // how much damage the player deals to enimies.

Methods:

- attack(item EquippedWeapon) : int
- getInventory() : List<items>
- getStatus() : (int Health, List<enum> effects)
- checkGold() : int
- levelUp() : void


## Enimies

Fields:
- +Difficulty : int // used in constructor to determine other fields.
- +Health : int
- +Potential_Loot : <item> // I assume singular item per enimy
- EquippedWeapon : item
- +defense: int // reduces incoming damage? if wanted.
- +goldfromKill: int
- +Creature type : <enum>
- +XPDrop : int

Methods:
- +takeDamage(int amount) // this could just take in the amount or it could take in the weapon status and then calculate it.
- +Attak(Player)
- +ViewLoot(): <item> int gold // if dead will return possible loot
- +GrabLoot(): <item> int gold // and set those to 0.
- +GetType(): <enum> type // if types added becomes nessasary.
- +WeakenArmor(): void // small chance to weeken its defence if desired.

### Enemy types
-----
### Goblin

Goblin is the most "basic" enemy type. Very common, a small little green guy. Will be fairly easy to deal with on almost every difficulty, usually only holding a dagger or shortsword, sometimes not even that.
##### Damage
    ((1/(7-12) * maxPlayerHP) + base weapon damage) * floor/3
##### Gold range
    ((dif + 1) * (0 - 2)) + (floor * player level)capped at 10
##### Item pool:
- Dagger
- Shortsword
- +10 health potion
- leather armor
##### Damage
    ((1/(7-12) * maxPlayerHP) + base weapon damage) * floor/3
##### Gold range
    ((dif + 1) * (0 - 2)) + (floor * player level)capped at 10
##### Item pool:
- Dagger
- Shortsword
- +10 health potion
- leather armor

### Orc

Orcs are goblins big brothers. They're bigger, tougher, and harder to beat. They usually carry a club or sword, and instead of ever being unarmed, they always have at LEAST a dagger. Somewhat harder to beat, less frequent to spawn.
##### Damage
    ((1/(6-9) * maxPlayerHP) + base weapon damage) * floor/4
##### Gold range
    ((dif + 1) * (1 - 4)) + (floor * player level)capped at 20
##### Item pool:
- Club
- Dagger
- Sword
- chain armor

### Troll

BIG OL THICCUMS. They're much bigger, and much harder to beat than Goblins or Orcs. Also generally carrying a club. If in a room, are the only ones there.
##### Damage
    ((1/(3-6) * maxPlayerHP) + base weapon damage) * floor/5
##### Gold range
    ((dif + 1) * (0 - 7)) + (floor * player level)capped at 30
##### Item pool:
- Club
- Sword
- +50% max health
- +(n%)strength for (3-5) rooms
- +(n%)defense for (3-5) rooms
- permanant +(n%) strength or defense 

### Skeleton

More similar to goblins in difficulty and frequency, generally armed with their own ribs or something similar, maybe a chain.
##### Damage
    ((1/(7-12) * maxPlayerHP) + base weapon damage) * floor/3
##### Gold range
    ((dif + 1) * (0 - 2)) + (floor * player level)capped at 15
##### Item pool:
- Rib
- +15 health potion
- nicer armor (tbd)
- +(n%) strength or defence potion

### Slime

Literally a slime creature. Very tough and hard to kill, but they do very little damage. The risk of slimes is the effects that they can give you. You can also have many more per room
##### Damage
    ((1/(15-20) * maxPlayerHP) + base weapon damage) * floor/5
    EFFECTS(tbd)
##### Gold range
    ((dif + 1) * (0 - 1)) + (floor * player level)capped at 5
##### Item pool:
- potions(tbd)


## Equipment
Fields:
- Type <enum> //chest, head, weapon, feet/shoes, 
- base damage/deffense: int
- bool equiped
- durability: int // if we want to force them to get new equipment every now and then.
- size int:  //how much storage space it takes such as potions 1, boots 3, chest 10, weapons:1-3

Methods:
- +Equip 
- +Store // to store 
- +GetDurabilty : int
- +Getsize: int
- +discared // sets to null and deletes.

## weapons : Equipment
Fields:
- WeaponType <enum>

Methods:
- +ATK // atks and adds to player damage from fists

## Potions
Fields:
- Effct <enum>
- Duration: int // rooms that I last through

Methods:
- +USE(): (effect, duration) // uses potion and then removes from storage
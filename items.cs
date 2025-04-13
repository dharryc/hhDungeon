using static System.Runtime.InteropServices.JavaScript.JSType;

namespace hhDungeon;


//## Equipment
//Fields:
//- Type <enum> //chest, head, weapon, feet/shoes, 
//- base damage/deffense: int
//- bool equiped
//- durability: int // if we want to force them to get new equipment every now and then.
//- size int:  //how much storage space it takes such as potions 1, boots 3, chest 10, weapons:1-3

//Methods:
//- +Equip 
    //- +Store // to store 
    //- +GetDurabilty : int
    //- +Getsize: int
    //- +discared // sets to null and deletes.

    //## weapons : Equipment
    //Fields:
    //- WeaponType<enum>

    //Methods:
    //- +ATK // atks and adds to player damage from fists

    //## Potions
    //Fields:
    //- Effct<enum>
    //- Duration: int // rooms that I last through

    //Methods:
    //- +USE() : (effect, duration) // uses potion and then removes from storage
public class Equipment
{

}

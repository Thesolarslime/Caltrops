using System.Globalization;
using UnityEngine;

public class ObjectStats : MonoBehaviour
{
    public string Name;
    public string Type; //player, enemy, wall, trap, item

    public int Health;
    public int MaxHealth;
    public int Armour;
    public float ArmourModifier;
    public int Mana; // PLAYER ONLY
    public int MaxMana; // PLAYER ONLY
    public int Speed;
    public float SpeedModifier;
    public int RegenBase; // PLAYER ONLY, base 10?, determines the number of times the player moves before they regen.
    public int RegenModifier; // PLAYER ONLY, base 0, deducted from RegenBase to get the number of times the player moves before they regen.
    public int RegenCount; // PLAYER ONLY, this goes up when the player moves and when it reaches RegenBase-RegenModifier the player regens and it resets to 0

    public string Facing; // UP DOWN LEFT RIGHT
    public int XPos;
    public int YPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnforceMaxStats()
    {
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        if (Mana > MaxMana)
        {
            Mana = MaxMana;
        }
    }

    public void Regen()
    {
        if (Type == "Player")
        {
            RegenCount++;
            if (RegenCount >= RegenBase - RegenModifier)
            {
                RegenCount = 0;
                if (Mana < MaxMana)
                {
                    Mana++;
                }
                else if (Health < MaxHealth)
                {
                    Health++;
                }
                else
                {
                    RegenCount = RegenBase - RegenModifier - 1;
                }
            }
        }
    }
}

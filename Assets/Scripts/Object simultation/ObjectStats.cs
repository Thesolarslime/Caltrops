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
    public int Mana;
    public int MaxMana;
    public int Speed;
    public float SpeedModifier;

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
}

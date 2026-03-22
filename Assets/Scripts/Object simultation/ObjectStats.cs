using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ObjectStats : MonoBehaviour
{
    public string Name;
    public string Type; //Player, Enemy, Wall, Trap, Item
    public string Class; //Construct for traps, walls, and construct enemies, animal for player and animal enemies, spirit for... spirits

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

    public int EnemyMeleeDamage; // how much damage an enemy deals on a normal attack
    public bool EnemyAttacksInMelee; // if true, the enemy will attempt to melee attack the player if they move next to them

    public string Facing; // UP DOWN LEFT RIGHT
    public int FacingNumber; // A numerical version of which way you're facing, use 4 for up, 5 for right, 6 for down, and 7 for left. 0-3 and 8-11 also follow this pattern, thats for spinning related mechanics.
    public int XPos;
    public int YPos;

    public bool Dead;

    public ParticleSystem[] ObjectParticles; // 0 is for the surprise particle, 1 is for death particle, 2 is for hurt particle, 3 is for bump into something particle 4 and above are for particles the object uses for other things
    private SpriteRenderer ObjectSprite;
    public Light2D ObjectLight; //EVERY ENEMY NEEDS A LIGHT NOW

    private AudioPlayer Sound;

    [SerializeField] private Animator CameraHurtAnimation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectSprite = GetComponent<SpriteRenderer>();
        Sound = GetComponent<AudioPlayer>();
        Dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Facing) // set the number for which direction this is facing based on the string version
        {
            case "UP":
                FacingNumber = 4; break;
            case "RIGHT":
                FacingNumber = 5; break;
            case "DOWN":
                FacingNumber = 6; break;
            case "LEFT":
                FacingNumber = 7; break;
        }
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

    public void TakeDamage(int Damage)
    {
        Health -= Damage;

        if (Health > 0)
        {
            if (Type == "Player") { Sound.PlaySound(Random.Range(0, 2), true, 0.6f); }
            else { Sound.PlaySound(0, true, 0.5f); }
            if (Type == "Player") { CameraHurtAnimation.SetTrigger("Hurt"); }
            ObjectParticles[2].Play();
        }
        else
        {
            if (Type == "Player") { Sound.PlaySound(3, true, 0.6f); }
            else { Sound.PlaySound(1, true, 0.5f); }
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        Dead = true;
        if (Type != "Player")
        {
            ObjectParticles[1].Play();
            ObjectSprite.enabled = false;
            if (Type == "Enemy") { ObjectLight.enabled = false; }
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
        else
        {
            ObjectParticles[1].Play();
            ObjectParticles[2].Play();
            ObjectSprite.enabled = false;
            CameraHurtAnimation.SetTrigger("Hurt");
            yield return new WaitForSeconds(1); //MAKE THE PLAYER DIE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Destroy(gameObject);
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

    public int GetFacingDirection(string Direction)
    {
        switch (Direction)
        {
            case "UP":
                return 4;
            case "RIGHT":
                return 5;
            case "DOWN":
                return 6;
            case "LEFT":
                return 7;
        }
        return 4;
    }
}

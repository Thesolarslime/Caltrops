using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Linq;
using Unity.VisualScripting;

public class ObjectStats : MonoBehaviour
{
    public string Name;
    public string Type; //Player, Enemy, Wall, Trap, Item
    public string Class; //Construct for traps, walls, and construct enemies, animal for player and animal enemies, spirit for... spirits

    public int Health;
    public int MaxHealth;
    public int Armour;
    public int ArmourModifier;
    public int Mana; // PLAYER ONLY
    public int MaxMana; // PLAYER ONLY
    public int Speed;
    public int SpeedModifier;
    public int RegenBase; // PLAYER ONLY, base 10?, determines the number of times the player moves before they regen.
    public int RegenModifier; // PLAYER ONLY, base 0, deducted from RegenBase to get the number of times the player moves before they regen.
    public int RegenCount; // PLAYER ONLY, this goes up when the player moves and when it reaches RegenBase-RegenModifier the player regens and it resets to 0

    //The following are all statuses, while they are above 0 they are reduced by deltatime each frame and are in effect.
    public float StatusVulnerable;
    public float StatusSlowed;
    public float StatusPoisoned;
    public float StatusStunned;
    public float StatusDisoriented;

    public int EnemyMeleeDamage; // how much damage an enemy deals on a normal attack
    public bool EnemyAttacksInMelee; // if true, the enemy will attempt to melee attack the player if they move next to them
    public int XPValue; // how valuable a member to society this enemy is

    public bool CaltropThatMoves; // true if this object, is a caltrop that moves. it lets them move.

    public string Facing; // UP DOWN LEFT RIGHT
    public int FacingNumber; // A numerical version of which way you're facing, use 4 for up, 5 for right, 6 for down, and 7 for left. 0-3 and 8-11 also follow this pattern, thats for spinning related mechanics.
    public int XPos;
    public int YPos;

    public bool Dead;

    public ParticleSystem[] ObjectParticles; // 0 is for the surprise particle, 1 is for death particle, 2 is for hurt particle, 3 is for bump into something particle 4 and above are for particles the object uses for other things
    private SpriteRenderer ObjectSprite;
    public Light2D ObjectLight; //EVERY ENEMY NEEDS A LIGHT NOW

    private AudioPlayer Sound;

    private GameManager GameManager;

    [SerializeField] private Animator CameraHurtAnimation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager = FindAnyObjectByType<GameManager>();
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
        EnforceMaxStats();

        if (!GameManager.Paused)
        {
            StatusTimers();
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
        if (!GameManager.Paused)
        {
            Damage -= Armour + ArmourModifier;
            if (Damage < 0) { Damage = 0; }
            Health -= Damage;

            if (Health > 0)
            {
                if (Type == "Player") { Sound.PlaySound(Random.Range(0, 2), true, 0.6f); }
                else { Sound.PlaySound(0, true, 0.5f); }
                if (Type == "Player") { CameraHurtAnimation.SetTrigger("Hurt"); }
                ObjectParticles[2].Play();
                StartCoroutine(DamageColourPulse());
            }
            else
            {
                if (Type == "Player") { Sound.PlaySound(3, true, 0.6f); }
                else { Sound.PlaySound(1, true, 0.5f); }
                StartCoroutine(Die());
            }
        }
    }

    private IEnumerator DamageColourPulse()
    {
        ObjectSprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        ObjectSprite.color = Color.white;
    }

    public IEnumerator Die()
    {
        Dead = true;
        if (Type != "Player")
        {
            ObjectParticles[1].Play();
            ObjectSprite.enabled = false;
            if (Type == "Enemy") 
            { 
                ObjectLight.enabled = false;

                //things that happen if the player is near
                float DistanceToPlayer = Vector2.Distance(GameManager.PlayerStats.gameObject.transform.position, transform.position);

                if (DistanceToPlayer <= 8f)
                {
                    GameManager.GainXP(XPValue);
                    if (GameManager.PassiveItemNames.Contains("LESSER SOUL GEM"))  // ITEM
                    {
                        GameManager.PlayerStats.Mana += 1;
                        if (GameManager.PlayerStats.Mana > GameManager.PlayerStats.MaxMana)
                        { GameManager.PlayerStats.Mana = GameManager.PlayerStats.MaxMana; }
                    }
                }
            }
            yield return new WaitForSeconds(1);
            if (Name == "Key") { yield return new WaitForSeconds(3); }
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

    public void GainStatus(string Type, float Seconds)
    {
        switch (Type)
        {
            case "Vulnerable":
                if (StatusVulnerable == 0) { ArmourModifier -= 1; }
                StatusVulnerable += Seconds; break;
            case "Slowed":
                if (StatusSlowed == 0) { SpeedModifier -= 2; }
                StatusSlowed += Seconds; break;
            case "Stunned":
                StatusStunned += Seconds; break;
            case "Poisoned":
                StatusPoisoned += Seconds; break;
            case "Disoriented":
                StatusDisoriented += Seconds; break;
        }
    }

    private void StatusTimers()
    {
        if (StatusVulnerable > 0)
        {
            StatusVulnerable -= Time.deltaTime;
            if (StatusVulnerable <= 0) {  StatusVulnerable = 0; ArmourModifier += 1; }
        }
        if (StatusSlowed > 0)
        {
            StatusSlowed -= Time.deltaTime;
            if (StatusSlowed <= 0) { StatusSlowed = 0; SpeedModifier += 2; }
        }
        if (StatusStunned > 0)
        {
            StatusStunned -= Time.deltaTime;
            if (StatusStunned <= 0) { StatusStunned = 0; }
        }
        if (StatusPoisoned > 0)
        {
            StatusPoisoned -= Time.deltaTime;
            if (StatusPoisoned <= 0) { StatusPoisoned = 0; }
        }
        if (StatusDisoriented > 0)
        {
            StatusDisoriented -= Time.deltaTime;
            if (StatusDisoriented <= 0) { StatusDisoriented = 0; }
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

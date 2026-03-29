using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    private ObjectStats Stats;
    private ObjectMovement Movement;
    private AudioPlayer Sound;
    private SpriteRenderer Sprite;
    public CaltropType Caltrop;

    public Sprite[] TrapSprites;
    private Animator TrapAnimator;

    public float TrapDelay; // if a trap waits for a bit before doing something, this is how long in seconds
    public int TrapDamage; // how much damage a trap does when it hits
    public int TrapDurability; // how many times this trap can activate before it dies
    public bool OnlyPlayerCanTrigger;
    public bool PlayerCantTrigger; // if true the trap won't do anything if it was triggered by the player
    public bool TrapHasAnimation; // if true this script will trigger an animation when trap is triggered

    public ObjectStats MostRecentTriggerer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stats = GetComponent<ObjectStats>();
        Movement = GetComponent<ObjectMovement>();
        Sound = GetComponent<AudioPlayer>();
        Sprite = GetComponent<SpriteRenderer>();

        if (TrapHasAnimation) { TrapAnimator = GetComponent<Animator>(); }

        if (Stats.Name == "Caltrop")
        {
            TrapDelay = 0.1f;
            TrapDamage = Caltrop.Damage;
            TrapDurability = Caltrop.Durability;
            PlayerCantTrigger = Caltrop.PlayerImmune;
        }
        else
        {
            TrapDurability = 100000;
            PlayerCantTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerTrap(bool PlayerTriggered, ObjectStats Triggerer)
    {
        if ((PlayerTriggered || (!OnlyPlayerCanTrigger)) && !Stats.Dead)
        {
            if (!PlayerCantTrigger || !PlayerTriggered)
            {
                if (TrapHasAnimation) { TrapAnimator.SetTrigger("TrapTrigger"); Sound.PlaySound(3, true, 0.8f); }
                StartCoroutine(TrapAction(Triggerer));
            }  
        }
    }

    private IEnumerator TrapAction(ObjectStats Triggerer)
    {
        yield return new WaitForSeconds(TrapDelay);

        switch (Stats.Name)
        {
            case "Caltrop":
                switch (Caltrop.Name)
                {
                    case "MUNDANE":
                        Triggerer.TakeDamage(TrapDamage);
                        break;
                    case "SOUTHWARD":
                        Triggerer.TakeDamage(TrapDamage);
                        break;
                    case "NORTHWARD":
                        Triggerer.TakeDamage(TrapDamage);
                        break;
                    case "GHOSTLY":
                        Triggerer.TakeDamage(TrapDamage);
                        break;
                }
                TrapDurability -= 1;
                if (TrapDurability <= 0) { Stats.StartCoroutine(Stats.Die()); }
                break;
            case "Spike trap":
                Sprite.sprite = TrapSprites[1];
                Sound.PlaySound(2, true, 0.8f);
                if (MostRecentTriggerer.XPos == Stats.XPos && MostRecentTriggerer.YPos == Stats.YPos)
                {
                    MostRecentTriggerer.TakeDamage(TrapDamage);
                }
                yield return new WaitForSeconds(0.1f);
                Sprite.sprite = TrapSprites[2];
                yield return new WaitForSeconds(0.05f);
                Sprite.sprite = TrapSprites[0];
                break;
            case "Ice trap":
                Sound.PlaySound(2, true, 0.8f);
                if (MostRecentTriggerer.XPos == Stats.XPos && MostRecentTriggerer.YPos == Stats.YPos)
                {
                    MostRecentTriggerer.GetComponent<ObjectMovement>().MoveObject(MostRecentTriggerer.Facing, 1);
                }
                break;
            case "Flip trap":
                if (MostRecentTriggerer.XPos == Stats.XPos && MostRecentTriggerer.YPos == Stats.YPos)
                {
                    MostRecentTriggerer.TakeDamage(TrapDamage);
                    TrapAnimator.SetTrigger("TrapTrigger");
                }
                else
                {
                    Stats.Name = "Flipped trap";
                    TrapDelay = 0.1f;
                }
                break;
            case "Flipped trap":
                Sound.PlaySound(2, true, 0.8f);
                if (MostRecentTriggerer.XPos == Stats.XPos && MostRecentTriggerer.YPos == Stats.YPos)
                {
                    MostRecentTriggerer.TakeDamage(TrapDamage);
                }
                Stats.Name = "Flip trap";
                TrapDelay = 2f;
                break;
            case "Door":
                switch (FindAnyObjectByType<GameManager>().CurrentLevelID)
                {
                    case 1:
                        FindAnyObjectByType<GameManager>().ChangeLevel("Level2"); break;
                    case 2:
                        FindAnyObjectByType<GameManager>().ChangeLevel("Level3"); break;
                    case 3:
                        FindAnyObjectByType<GameManager>().ChangeLevel("Boss1"); break;
                    case 4:
                        FindAnyObjectByType<GameManager>().ChangeLevel("Boss2"); break;
                    case 5:
                        FindAnyObjectByType<GameManager>().ChangeLevel("Level4"); break;
                    case 6:
                        FindAnyObjectByType<GameManager>().ChangeLevel("Boss3"); break;
                    case 7:
                        FindAnyObjectByType<GameManager>().ChangeLevel("Win"); break;
                }
                break;
            case "Key":
                GetComponent<DoorKeyManager>().KeyGet();
                Sound.PlaySound(0, false, 1f);
                Stats.StartCoroutine(Stats.Die());
                break;
        }
    }
}
